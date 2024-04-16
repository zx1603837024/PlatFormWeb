using Abp.Domain.Repositories;
using Abp.EntityFramework;
using Abp.Linq.Extensions;
using P4.BigScreen.Dtos;
using P4.Companys;
using P4.Dictionarys;
using P4.Employees;
using P4.Employees.Dtos;
using P4.Users;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace P4.EntityFramework.Repositories
{
    public class EmployeeRepository : P4RepositoryBase<Employee, long>, IEmployeeRepository
    {
        #region Var
        private readonly IRepository<EmployeeCheck, long> _abpEmployeeCheckRepository;
        private readonly IRepository<Employee, long> _abpEmployeeRepository;
        private readonly IUserRepository _userRepository;
        #endregion
        public EmployeeRepository(IDbContextProvider<P4DbContext> dbContextProvider, IRepository<EmployeeCheck, long> abpEmployeeCheckRepository, IUserRepository userRepository, IRepository<Employee, long> abpEmployeeRepository)
            : base(dbContextProvider)
        {
            _userRepository = userRepository;
            _abpEmployeeCheckRepository = abpEmployeeCheckRepository;
            _abpEmployeeRepository = abpEmployeeRepository;
        }

        /// <summary>
        /// 收费员签到列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllEmployeeOutput GetEmployeeAll(EmployeeInput input)
        {
            if(input.filters=="1")
            {
                var query = Table.Where(a=>a.CheckInTime>=input.begindt&&a.CheckInTime<=input.enddt).Select(c => new EmployeeDto
                {
                    Id = c.Id,
                    CompanyName = Context.Set<OperatorsCompany>().FirstOrDefault(company => company.Id == c.CompanyId).CompanyName,
                    UserName = c.UserName,
                    TrueName = c.TrueName,
                    BankCard = c.BankCard,
                    Password = c.Password,
                    Telephone = c.Telephone,
                    Address = c.Address,
                    AccountStatus = Context.Set<DictionaryValue>().FirstOrDefault(dictype => dictype.ValueCode == c.AccountStatus && dictype.TypeCode == P4Consts.EmployeeStatus).ValueData,
                    CheckIn = c.CheckIn,
                    CheckInTime = c.CheckInTime,
                    CheckOut = c.CheckOut,
                    CheckOutTime = c.CheckOutTime,
                    CheckOuttype = Context.Set<DictionaryValue>().FirstOrDefault(dictype => dictype.ValueCode == c.CheckOuttype && dictype.TypeCode == P4Consts.EmployeeCheckOutType).ValueData,
                    BatchNo = c.BatchNo,
                    CompanyId = c.CompanyId
                }).Orders(input);
                int records = query.ToList().Count();
                return new GetAllEmployeeOutput()
                {
                    rows = query.PageBy(input).ToList(),
                    records = records,
                    total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
                };
            }
            else
            {
            int records = Table.Filters(input).Count();
            var query = Table.Select(c => new EmployeeDto
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
                BatchNo = c.BatchNo,
                CheckOuttype = Context.Set<DictionaryValue>().FirstOrDefault(dictype => dictype.ValueCode == c.CheckOuttype && dictype.TypeCode == P4Consts.EmployeeCheckOutType).ValueData,
                AccountStatus = Context.Set<DictionaryValue>().FirstOrDefault(dictype => dictype.ValueCode == c.AccountStatus && dictype.TypeCode == P4Consts.EmployeeStatus).ValueData
            }).Orders(input).Filters(input).PageBy(input);
            return new GetAllEmployeeOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
            }
        }


        /// <summary>
        /// 获取收费员坐标
        /// </summary>
        /// <returns></returns>
        public List<EmpolyeePoint> GetEmpolyeePoints()
        {
            var strTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
                new SqlParameter("@strTime", strTime)
            };

            string sql = @"select ISNULL(Sum(case when (CarOutTime >= @strTime and C.EmpolyeeId = OutOperaId) then FactReceive else 0 end ),0) FactReceive,ISNULL(Sum(case when (CarRepaymentTime >= @strTime and C.EmpolyeeId = EscapeOperaId) then Repayment else 0 end ),0) Repayment,X,Y,TrueName,EmpolyeeId from (select X,Y,TrueName,EmpolyeeId from AbpEquipmentGps as a, (select max(CreatorUserId) as EmpolyeeId, max(CreationTime) as create_time from AbpEquipmentGps as b group by CreatorUserId ) as b left join AbpEmployees on  EmpolyeeId =AbpEmployees.Id where a.CreatorUserId=b.EmpolyeeId and a.CreationTime = b.create_time and a.CreationTime >= @strTime and CheckOut =0) as C left join AbpBusinessDetail on (C.EmpolyeeId = OutOperaId or C.EmpolyeeId = EscapeOperaId) and (CarOutTime >= @strTime or CarRepaymentTime>= @strTime) group by C.X,C.EmpolyeeId,C.TrueName,C.Y";

          
            SqlParameter[] param = sqlParameters.ToArray();
            var list = Context.Database.SqlQuery<EmpolyeePoint>(sql, param).ToList();
            return list;
        }

        /// <summary>
        /// 获取单个收费员坐标
        /// </summary>
        /// <returns></returns>
        public EmpolyeePoint GetEmpolyeePointModel(int Employeeid)
        {
            var strTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
                new SqlParameter("@strTime", strTime),
                 new SqlParameter("@Employeeid", Employeeid)
            };
            string sql = @"select X,Y,EmpolyeeId from AbpEquipmentGps as a, (select max(CreatorUserId) as EmpolyeeId, max(CreationTime) as create_time from AbpEquipmentGps as b group by CreatorUserId ) as b where a.CreatorUserId=b.EmpolyeeId and a.CreationTime = b.create_time and a.CreationTime >= @strTime and EmpolyeeId = @Employeeid";
            SqlParameter[] param = sqlParameters.ToArray();
            return Context.Database.SqlQuery<EmpolyeePoint>(sql, param).FirstOrDefault();
        }

        /// <summary>
        /// 收费员签到abpEmployeeCheck    EmployeeCheckInOutInput input
        /// </summary>
        public void UpdateEmployeeCheckIn(int employeeID, int ParkID, bool checkIn, DateTime checkInOrOutTime, bool checkOut, string DeviceCode, int berthsecId, int companyId, int tenantId)
        {

            string sql = @"insert AbpEmployeeCheck (EmployeeId, ParkId, CheckIn, CheckInTime, CheckOut,
DeviceCode, BerthsecId, CompanyId, TenantId, IsRepeal, IsNormal) values(@EmployeeId, @ParkId, @CheckIn, @CheckInTime, @CheckOut,
@DeviceCode, @BerthsecId, @CompanyId, @TenantId, @IsRepeal, @IsNormal)";
            Context.Database.ExecuteSqlCommand(sql, new SqlParameter[]
            {
                new SqlParameter("@EmployeeId",employeeID),
                new SqlParameter("@ParkId",ParkID),
                new SqlParameter("@CheckIn",checkIn),
                new SqlParameter("@CheckInTime",checkInOrOutTime),
                new SqlParameter("@CheckOut",checkOut),
                new SqlParameter("@DeviceCode",DeviceCode),
                new SqlParameter("@BerthsecId",berthsecId),
                new SqlParameter("@CompanyId",companyId),
                new SqlParameter("@TenantId",tenantId),
                new SqlParameter("@IsRepeal",true),
                new SqlParameter("@IsNormal",true)
            });
        }
        /// <summary>
        /// PDA收费员签退
        /// </summary>
        /// <param name="employessId"></param>
        public void UpdateEmployeeCheckOut(int employessId)
        {
            string sqlStr = string.Format("update AbpEmployees set CheckOut=1,CheckIn=0,CheckOutTime='{0}',CheckOuttype=1 where id={1}", DateTime.Now, employessId);
            Context.Database.ExecuteSqlCommand(sqlStr);
        }
        /// <summary>
        /// 后台收费员批量签退
        /// 并写入签退记录
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int EmployeeBatchCheckoutBGO(List<int> Ids, long userId)
        {
            string IdsStr = string.Join(",", Ids);
            
            string SqlStr = @"update AbpEmployeeCheck set CheckIn = 0, CheckOut = 1, CheckOutUserId = " + userId + ", CheckOutTime = @CheckOutTime where EmployeeId in (" + IdsStr + ") and CheckOutTime is NULL and CheckIn = 1 and CheckOut = 0 update AbpEmployees set CheckOut= 1, CheckIn= 0, CheckOutTime = @CheckOutTime,CheckOuttype=2 where id in (" + IdsStr + ")";
            return Context.Database.ExecuteSqlCommand(SqlStr, new SqlParameter[]
            {
                new SqlParameter("@CheckOutTime",DateTime.Now)
            });
        }

        /// <summary>
        /// 后台收费员取消对账
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int EmployeeNOAccountCheckOutBGO(int Id,long operatorId)
        {
            var operatorName = _userRepository.Load(operatorId).Name;
            var batchNo = _abpEmployeeRepository.FirstOrDefault(e => e.Id == Id).BatchNo;
            var model = _abpEmployeeCheckRepository.FirstOrDefault(entity => entity.BatchNo == batchNo);

                if(model.Remark == null)
                {
                    model.Remark =  operatorName + "用户在平台中取消了对账功能；";
                }else if(!model.Remark.Contains(operatorName + "用户在平台中取消了对账功能"))
                {
                    model.Remark = model.Remark + operatorName + "用户在平台中取消了对账功能；";
                }
                model.IsRepeal = false;
                _abpEmployeeCheckRepository.Update(model);
                return 1;        
        }
    }
}
