using Abp.EntityFramework;
using P4.Dictionarys;
using P4.Inspectors;
using P4.Inspectors.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using P4.Companys;
using P4.Parks;
using P4.Employees;
using System.Data.SqlClient;

namespace P4.EntityFramework.Repositories
{

    /// <summary>
    /// 
    /// </summary>
    public class InspectorRepository : P4RepositoryBase<Inspector, long>, IInspectorRepository
    {
        public InspectorRepository(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public GetAllInspectorOutput GetInspectorAll(InspectorInput input)
        {
            int records = Table.Filters(input).Count();
            var query = Table.Select(c => new InspectorDto
            {
                Id = c.Id,
                Address = c.Address,
                BankCard = c.BankCard,
                UserName = c.UserName,
                TrueName = c.TrueName,
                Telephone = c.Telephone,
                Password = c.Password,
                CheckIn = c.CheckIn,
                CheckInTime = c.CheckInTime,
                CheckOut = c.CheckOut,
                CheckOutTime = c.CheckOutTime,
                CompanyId = c.CompanyId,
                CompanyName = Context.Set<OperatorsCompany>().FirstOrDefault(company => company.Id == c.CompanyId).CompanyName,
                CheckOuttype = Context.Set<DictionaryValue>().FirstOrDefault(dictype => dictype.ValueCode == c.CheckOuttype && dictype.TypeCode == P4Consts.InspectorCheckOutType).ValueData,
                AccountStatus = Context.Set<DictionaryValue>().FirstOrDefault(dictype => dictype.ValueCode == c.AccountStatus && dictype.TypeCode == P4Consts.InspectorStatus).ValueData
            }).Orders(input).Filters(input).PageBy(input);
            return new GetAllInspectorOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }


        /// <summary>
        /// 获取巡查任务列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllInspectorTasksOutput GetAllInspectorTasksList(InspectorTasksInput input)
        {
            //int records = Table.Filters(input).Count();
            var InspectorTasks = Context.Set<InspectorTask>();
            int records = InspectorTasks.Filters(input).Count();
            var query = InspectorTasks.Select(c => new InspectorTasksDto
            {
                Id = c.Id,
                CompanyName = Context.Set<OperatorsCompany>().FirstOrDefault(dictype => dictype.Id == c.CompanyId).CompanyName,
                ParkName = Context.Set<Park>().FirstOrDefault(dictype => dictype.Id.ToString()==c.ParkId).ParkName,
                InspectorName = Context.Set<Inspector>().FirstOrDefault(dictype => dictype.Id == c.InspectorId).TrueName,
                Status = Context.Set<DictionaryValue>().FirstOrDefault(dictype => dictype.ValueCode == c.Status.ToString() && dictype.TypeCode == P4Consts.InspectorTaskStatus).ValueData, 
                EffectiveTime=c.EffectiveTime,
                Remark=c.Remark
                
            }).Orders(input).Filters(input).PageBy(input);
            return new GetAllInspectorTasksOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 获取巡查反馈任务列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllInspectorTaskFeedbacksOutput GetAllInspectorTaskFeedbacksList(InspectorTaskFeedbacksInput input)
        {
            var InspectorTaskFeedbacks = Context.Set<InspectorTaskFeedback>();
            int records =InspectorTaskFeedbacks.Filters(input).Count();
            var query=InspectorTaskFeedbacks.Select(c=>new InspectorTaskFeedbacksDto
            {
                Id=c.Id,
                BerthNumber=c.BerthNumber,
                EmployeeName = Context.Set<Employee>().FirstOrDefault(entity => entity.Id == c.EmployeeId).TrueName,
                TaskContent=c.TaskContent,
                UploadTime=c.UploadTime,
                Remark=c.Remark,
                PicUrl1=c.PicUrl1
            }).Orders(input).Filters(input).PageBy(input);
            return new GetAllInspectorTaskFeedbacksOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
        /// <summary>
        /// 巡查员签到停车场
        /// </summary>
        /// <param name="parkId"></param>
        /// <param name="InspectorId"></param>
        public void InspectorCheckInPark(long InspectorId, List<string> parkId, string DeviceCode, int CompanyId, int TenantId)
        {
            foreach (var parkid in parkId)
            {
                string sqlStr = @"
insert into AbpInspectorCheck (InspectorId, ParkId, CheckIn, CheckInTime, CheckOut, DeviceCode,BerthsecId,CompanyId,TenantId)
values(@InspectorId,@ParkId,@CheckIn,@CheckInTime,@CheckOut,@DeviceCode,@BerthsecId,@CompanyId,@TenantId)";
                int Park = Convert.ToInt32(parkid);
                Context.Database.ExecuteSqlCommand(sqlStr, new SqlParameter[]
                {
                new SqlParameter("@InspectorId",InspectorId),
                new SqlParameter("@ParkId",Park),
                new SqlParameter("@CheckIn",true),
                new SqlParameter("@CheckInTime",DateTime.Now),
                new SqlParameter("@CheckOut",false),
                new SqlParameter("@DeviceCode",DeviceCode),
                new SqlParameter("@BerthsecId",12),
                new SqlParameter("@CompanyId",CompanyId),
                new SqlParameter("@TenantId",TenantId)
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InspectorId"></param>
        /// <param name="inspectorRPB"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="CompanyId"></param>
        /// <param name="TenantId"></param>
        public void InspectorCheckInPark(long InspectorId, InspectorRPB inspectorRPB, string DeviceCode, int CompanyId, int TenantId)
        {
            for (int i = 0; i < inspectorRPB.berthsecId.Count; i++)
            {
                string sqlStr = @"
insert into AbpInspectorCheck (InspectorId, ParkId, CheckIn, CheckInTime, CheckOut, DeviceCode,BerthsecId,CompanyId,TenantId)
values(@InspectorId, @ParkId, @CheckIn, @CheckInTime, @CheckOut, @DeviceCode, @BerthsecId, @CompanyId, @TenantId)";
                Context.Database.ExecuteSqlCommand(sqlStr, new SqlParameter[]
                {
                    new SqlParameter("@InspectorId",InspectorId),
                    new SqlParameter("@ParkId", Convert.ToInt32( inspectorRPB.parkId[i])),
                    new SqlParameter("@CheckIn",true),
                    new SqlParameter("@CheckInTime",DateTime.Now),
                    new SqlParameter("@CheckOut",false),
                    new SqlParameter("@DeviceCode",DeviceCode),
                    new SqlParameter("@BerthsecId",inspectorRPB.berthsecId[i]),
                    new SqlParameter("@CompanyId",CompanyId),
                    new SqlParameter("@TenantId",TenantId)
                });
            }
        }
    }
}
