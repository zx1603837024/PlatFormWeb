using Abp.EntityFramework;
using P4.Berthsecs;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Linq.Extensions;
using P4.Berthsecs.Dto;
using P4.Parks;
using P4.Regions;
using P4.Rates;
using P4.Employees;
using System.Data.SqlClient;

namespace P4.EntityFramework.Repositories
{
    public class BerthsecRepository : P4RepositoryBase<Berthsec>, IBerthsecRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public BerthsecRepository(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllBerthsecsListOutput GetAllBerthsecsList(SearchBerthsecInput input)
        {
            int records = Table.Filters(input).Count();
            var query = Table.Select(c => new BerthsecDto
            {
                SignoCommunationTime = c.SignoCommunationTime,
                SignoInSize = c.SignoInSize,
                SignoOutSize = c.SignoOutSize,

                Id = c.Id,
                PushStatus = c.PushStatus,
                RegionId = c.RegionId,
                ParkId = c.ParkId,
                BeginNumeber = c.BeginNumeber,
                UseStatus = c.UseStatus,
                EndNumeber = c.EndNumeber,
                RateName = Context.Set<Rate>().FirstOrDefault(dic => dic.Id == c.RateId).RateName,
                RateName1 = Context.Set<Rate>().FirstOrDefault(dic => dic.Id == c.RateId1).RateName,
                RateName2 = Context.Set<Rate>().FirstOrDefault(dic => dic.Id == c.RateId2).RateName,
                CheckInName = Context.Set<Employee>().FirstOrDefault(entity => entity.Id == c.CheckInEmployeeId).TrueName,
                CheckOutName = Context.Set<Employee>().FirstOrDefault(entity => entity.Id == c.CheckOutEmployeeId).TrueName,
                CustomNumeber = c.CustomNumeber,
                CheckInDeviceCode = c.CheckInDeviceCode,
                CheckOutDeviceCode = c.CheckInDeviceCode,
                CheckInTime = c.CheckInTime,
                CheckOutTime = c.CheckOutTime,
                IsActive = c.IsActive,
                ParkName = Context.Set<Park>().FirstOrDefault(dictype => dictype.Id == c.ParkId).ParkName,
                RegionName = Context.Set<Region>().FirstOrDefault(dictype => dictype.Id == c.RegionId).RegionName,
                BerthsecName = c.BerthsecName,
                BerthCount = c.BerthCount
            }).Orders(input).Filters(input).PageBy(input);
            return new GetAllBerthsecsListOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 道闸
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllBerthsecsListOutput GetAllSignoParkList(SearchBerthsecInput input)
        {
            int records = Table.Filters(input).Count();
            var query = Table.Select(c => new BerthsecDto
            {
                SignoCommunationTime = c.SignoCommunationTime,
                SignoInSize = c.SignoInSize,
                SignoOutSize = c.SignoOutSize,
                Id = c.Id,             
                IsActive = c.IsActive,
                BerthsecName = c.BerthsecName,
            }).Orders(input).Filters(input).PageBy(input).ToList();
            List<BerthsecDto> list = new List<BerthsecDto>();

            foreach (var item in query)
            {
                TimeSpan timeSpan = DateTime.Now - item.SignoCommunationTime;
                if(timeSpan.TotalMinutes > P4Consts.SignoOnline)
                {
                    item.IsActive = false;
                }
                list.Add(item);
            }
            return new GetAllBerthsecsListOutput()
            {
                rows = list,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        

        /// <summary>
        /// 巡查员获取泊位段
        /// </summary>
        /// <param name="LParkIds"></param>
        /// <returns></returns>
        public List<BerthsecDto> LoadListBerhtsec(List<int> LParkIds)
        {
            string ParkIds = "";
            foreach (var p in LParkIds)
            {
                ParkIds += p + ",";
            }
            ParkIds = ParkIds.Remove(ParkIds.Length - 1, 1);
            //var parm=new SqlParameter[]{
            //new SqlParameter("@parkids",ParkIds)
            //};
            string sqlstr = string.Format("select * from AbpBerthsecs where ParkId in({0})", ParkIds);
            var temp = Context.Database.SqlQuery<BerthsecDto>(sqlstr).ToList();
            return temp;
        }


        /// <summary>
        /// 后台批量签退泊位段
        /// </summary>
        /// <param name="BerthsecId"></param>
        /// <returns></returns>
        public int BerthsecBatchCheckOutBGO(List<int> BerthsecId, int UserId)
        {
            string IdsStr = "";
            if (BerthsecId != null)
            {
                foreach (var id in BerthsecId)
                {
                    IdsStr += id + ",";
                }
                IdsStr = IdsStr.Remove(IdsStr.Length - 1, 1);
            }
            string SqlStr = @"update AbpBerthsecs set CheckOutEmployeeId = @CheckOutEmployeeId, CheckOutTime = @CheckOutTime,CheckStatus = 0, UseStatus = 0 where id in (" + IdsStr + ")";
            return Context.Database.ExecuteSqlCommand(SqlStr, new SqlParameter[]
            {
                new SqlParameter("@CheckOutEmployeeId",UserId),
                new SqlParameter("@CheckOutTime",DateTime.Now)
            });
        }
        /// <summary>
        /// PDA收费员签退泊位段
        /// </summary>
        /// <param name="berthsecID"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="employessID"></param>
        public void EmployeeCheckOutBerthsec(int berthsecID, string DeviceCode, int employessID)
        {
            string sqlStr =
                string.Format(
                    "update AbpBerthsecs set CheckOutEmployeeId={0},CheckOutDeviceCode='{1}',CheckOutTime='{2}',CheckStatus={3},UseStatus={4} where id={5}",
                    employessID, DeviceCode, DateTime.Now, 0, 0, berthsecID);
            Context.Database.ExecuteSqlCommand(sqlStr); //执行sql语句签退泊位段
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllBerthsecCheckListOutput GetAllBerthsecCheckLiist(BerthsecCheckInput input, int Status)
        {
            if (Status != 0)
            {
                Status = 1;//早班
                if (DateTime.Now.Hour >= 22 && DateTime.Now.Hour < 7)//晚班
                    Status = 3;
            }

            var workgroup = Context.Set<WorkGroups.WorkGroup>();
            var WorkGroupEmployee = Context.Set<WorkGroups.WorkGroupEmployee>();
            var WorkGroupBerthsec = Context.Set<WorkGroups.WorkGroupBerthsec>();
            var query = from m in workgroup
                        join e in WorkGroupBerthsec
                            on m.Id equals e.WorkGroupId
                        join b in WorkGroupEmployee
                            on m.Id equals b.WorkGroupId
                        where b.EmployeeId == input.EmployeeId && m.IsActive == true && e.Berthsec.IsActive == true && (e.Status == Status || Status == 0)
                        select
                            (new BerthsecCheckDto()
                            {
                                Id = e.BerthsecId,
                                BerthsecName = e.Berthsec.BerthsecName,
                                Checked = e.Berthsec.CheckStatus,
                                UseStatus = e.Berthsec.UseStatus,
                                PushStatus = e.Berthsec.PushStatus
                            });

            return new GetAllBerthsecCheckListOutput()
            {
                rows = query.ToList()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public List<BerthsecDto> LoadToSql(List<int> Ids)
        {
            return Context.Database.SqlQuery<BerthsecDto>("select AbpBerthsecs.Id, AbpBerthsecs.BerthsecName, BeginNumeber, EndNumeber, CustomNumeber, CheckInStatus, CheckStatus, CheckOutStatus, CheckInTime, CheckInEmployeeId, CheckOutTime, CheckOutEmployeeId, CheckInDeviceCode, CheckOutDeviceCode, XPoint, YPoint, RegionId, ParkId, AbpBerthsecs.TenantId, AbpBerthsecs.IsActive, UseStatus, AbpBerthsecs.CompanyId,  RateId, A.RatePDA as FeeModel,  RateId1,B.RatePDA as FeeModel1, RateId2, C.RatePDA as FeeModel2, BerthCount, CONVERT(bit, 1) as PushStatus, Lat, Lng, AbpBerthsecs.IsDeleted, AbpBerthsecs.DeleterUserId, AbpBerthsecs.DeletionTime, AbpBerthsecs.LastModificationTime, AbpBerthsecs.LastModifierUserId,  AbpBerthsecs.CreationTime, AbpBerthsecs.CreatorUserId from AbpBerthsecs with(nolock) left join AbpRates as A on A.Id = RateId left join AbpRates as B on B.Id = RateId1 left join AbpRates as C on C.Id = RateId2 where AbpBerthsecs.Id in (" + string.Join(",", Ids) + ") and AbpBerthsecs.IsDeleted = 0", new SqlParameter[] { }).ToList();
        }
    }
}