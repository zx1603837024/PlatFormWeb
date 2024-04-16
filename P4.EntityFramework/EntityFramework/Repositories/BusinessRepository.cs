using Abp.EntityFramework;
using P4.Businesses;
using P4.Businesses.Dtos;
using P4.EmployeeCharges.Dto;
using P4.ParkCharges.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using P4.Dictionarys;
using System.Data;
using P4.Employees;
using P4.StaticReport.Dtos;
using System.Data.Entity.Infrastructure;
using Newtonsoft.Json;
using Abp.Domain.Uow;
using P4.OtherAccounts;
using P4.OtherPlateNumbers;
using Abp.Authorization;
using P4.Berths;
using Abp.Configuration;
using P4.InspectorCharges.Dtos;
using P4.Sensors;
using P4.EmployeeBatchNoDynamicReport.Dtos;
using P4.BigScreen.Dtos;
using System.Data.Entity;
using P4.Berths.Dtos;
using System.Data.Entity.Core.Objects;
using P4.ParkingLot.Dto;
using Abp.Extensions;
using P4.ParkingLot;
using Abp.Runtime.Session;

namespace P4.EntityFramework.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessRepository : P4RepositoryBase<BusinessDetail, long>, IBusinessRepository
    {
        #region var
        private readonly IRepository<BusinessDetail, long> _BusinessBetailAppService;
        private readonly IRepository<DictionaryValue> _abpDictionaryValueRepository;
        private readonly IEmployeeAppService _EmployeeAppService;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<BusinessDetail, long> _businessDetailRepository;
        private readonly IRepository<OtherAccount, long> _otherAccountRepository;
        private readonly IRepository<DeductionRecord, long> _deductionRecordRepository;
        private readonly IRepository<OtherPlateNumber, long> _otherPlateNumberRepository;
        private readonly IRepository<Berth, long> _berthRepository;
        private readonly IRepository<ParkingRecord, long> _parkingRecordRepository;
        private readonly ISettingStore _settingStore;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextProvider"></param>
        /// <param name="BusinessBetailAppService"></param>
        /// <param name="abpDictionaryValueRepository"></param>
        /// <param name="EmployeeAppService"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="businessDetailRepository"></param>
        /// <param name="otherAccountRepository"></param>
        /// <param name="deductionRecordRepository"></param>
        /// <param name="otherPlateNumberRepository"></param>
        /// <param name="berthRepository"></param>
        /// <param name="settingStore"></param>
        public BusinessRepository(IDbContextProvider<P4DbContext> dbContextProvider, IRepository<BusinessDetail, long> BusinessBetailAppService,
            IRepository<DictionaryValue> abpDictionaryValueRepository, IEmployeeAppService EmployeeAppService, IUnitOfWorkManager unitOfWorkManager, IRepository<BusinessDetail, long> businessDetailRepository, IRepository<OtherAccount, long> otherAccountRepository, IRepository<DeductionRecord, long> deductionRecordRepository, IRepository<OtherPlateNumber, long> otherPlateNumberRepository, IRepository<Berth, long> berthRepository,
             ISettingStore settingStore, IRepository<ParkingRecord, long> parkingRecordRepository)
            : base(dbContextProvider)
        {
            _BusinessBetailAppService = BusinessBetailAppService;
            _abpDictionaryValueRepository = abpDictionaryValueRepository;
            _EmployeeAppService = EmployeeAppService;
            _unitOfWorkManager = unitOfWorkManager;
            _businessDetailRepository = businessDetailRepository;
            _otherAccountRepository = otherAccountRepository;
            _deductionRecordRepository = deductionRecordRepository;
            _otherPlateNumberRepository = otherPlateNumberRepository;
            _berthRepository = berthRepository;
            _settingStore = settingStore;
            _parkingRecordRepository=parkingRecordRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetMoneyOutput GetMoneyTotal(GetMoneyInput input)
        {
            GetMoneyOutput returnentity = new GetMoneyOutput();
            returnentity.ArrearageTotal = 0;
            returnentity.FactReceiveTotal = 0;
            returnentity.PrepaidTotal = 0;
            returnentity.ReceivableTotal = 0;
            returnentity.RepaymentTotal = 0;
            return returnentity;
        }

        /// <summary>
        /// 获取停车场停车记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetParkRecordListOutput GetParkRecordList(GetParkRecordListInput input)
        {
            var filter = Context.AbpParkingRecord.Where(u=>u.IsDeleted==false);
            if (input.TenantId.HasValue)
            {
                filter = filter.Where(u => u.TenantId == input.TenantId.Value);
            }
            if (input.CompanyId.HasValue)
            {
                filter = filter.Where(u => u.CompanyId == input.CompanyId.Value);
            }
            if (!input.PlateNumber.IsNullOrWhiteSpace())
            {
                filter = filter.Where(u => u.PlateNumber.Contains(input.PlateNumber.Trim()));
            }
            if (input.StartTime.HasValue)
            {
                filter = filter.Where(u => u.CarInTime >= input.StartTime.Value);
            }
            if (input.EndTime.HasValue)
            {
                var endDate = input.EndTime.Value;
                filter = filter.Where(u => u.CarInTime <= endDate);
            }
            if (input.InChannelId > 0)
            {
                filter = filter.Where(u => u.CarInChannelId == input.InChannelId);
            }
            if (input.OutChannelId > 0)
            {
                filter = filter.Where(u => u.CarOutChannelId == input.OutChannelId);
            }
            if (input.ParkId > 0)
            {
                filter = filter.Where(u => u.ParkId ==input.ParkId);
            }
            if (input.CarType > 0)
            {
                filter = filter.Where(u => u.CarType == input.CarType);
            }
            if (input.StopStatus > 0)
            {
                filter = filter.Where(u => u.OrderStatus == input.StopStatus);
            }
            if (!string.IsNullOrEmpty(input.RegionName))
            {
                filter = filter.Where(u => u.Region.RegionName.Contains(input.RegionName.Trim()));
            }
            var records = filter.Count();
            var dbList = filter.OrderBy(u=>u.CarInTime).PageBy(input).ToList();
            return new GetParkRecordListOutput
            {
                rows = dbList.Select(u=>new ParkingRecordEntity{
                    OrderNo=u.RecordId,
                    OrderId=u.Id,
                    TenantName=u.Tenant.TenancyName,
                    CompanyName=u.Company.CompanyName,
                    //ReceiveOperaName=u.ReceiveOperaName,
                    InChannel=u.CarInChannelName,
                    OutChannel=u.CarOutChannelName,
                    ParkName=u.ParkName,
                    PlateNumber=u.PlateNumber,
                    StopTime=u.StopTime,
                    PayableAmount=u.PayableAmount,
                    FactReceive=u.FactReceive,
                    DiscountMoney=u.DiscountMoney,
                    OrderStatus= Enum.GetName(typeof(EOrderStatus), u.OrderStatus),
                    CarInTime=u.CarInTime,
                    CarOutTime=u.CarOutTime,
                    CarType= Enum.GetName(typeof(ECarType), u.CarType),
                    RegionName=u.Region?.RegionName
                }).ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }


        /// <summary>
        /// 停车场报表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllParkChargesOutput GetMoneyTotalGroupbyPark(ParkChargeInput input)
        {
            var temp = Table.Where(p => p.CarPayTime >= input.begindt && p.CarPayTime <= input.enddt);
            if (!string.IsNullOrWhiteSpace(input.parkIdInput) && input.parkIdInput != "0")
            {
                int parkid = int.Parse(input.parkIdInput);
                temp = temp.Where(p => p.ParkId == parkid);
            }

            var expr = from p in temp
                       group p by p.ParkId
                           into g
                       select new ParkChargesDto
                       {
                           ParkName = Context.Set<Parks.Park>().FirstOrDefault(a => a.Id == g.Key) == default(Parks.Park) ? "" : Context.Set<Parks.Park>().FirstOrDefault(a => a.Id == g.Key).ParkName,
                           SumFactReceive = g.Sum(p => p.FactReceive),
                           XJSumFactReceive = g.Where(p => p.PayStatus == 1 && p.Status == 2).Sum(p => p.FactReceive),
                           SKSumFactReceive = g.Where(p => p.PayStatus == 2 && p.Status == 2).Sum(p => p.FactReceive),
                           OnlineSumFactReceive = g.Where(p => p.PayStatus == 3 && p.Status == 2).Sum(p => p.FactReceive),
                           SumRepayment = g.Where(p => p.Status == 4).Sum(p => p.Repayment.HasValue == true ? p.Repayment.Value : 0),
                           SumArrearage = g.Sum(p => p.Arrearage),
                           PosTimes = g.Count(),
                           SensorSumReceivable = Context.Set<SensorBusinessDetail>().Where(entity => entity.ParkId == g.Key && entity.CarOutTime >= input.begindt && entity.CarOutTime <= input.enddt).Sum(p => p.Receivable ?? 0),
                           SensorTimes = Context.Set<SensorBusinessDetail>().Where(entity => entity.ParkId == g.Key && entity.CarOutTime >= input.begindt && entity.CarOutTime <= input.enddt).Count(),
                           SumMoney = g.Sum(p => p.Money.HasValue == true ? p.Money.Value : 0)//应收
                       };

            ParkChargesData data = new ParkChargesData
            {
                ParkName = "汇总",
                SumFactReceive = 0,
                XJSumFactReceive = 0,
                SKSumFactReceive = 0,
                OnlineSumFactReceive = 0,
                SumRepayment = 0,
                SumArrearage = 0,
                SumMoney = 0,
                SensorSumReceivable = 0,
                PosTimes = 0,
                SensorTimes = 0
            };

            int records = expr.ToList().Count;
            if (records > 0)
            {
                foreach (var item in expr)
                {
                    data.SumFactReceive += item.SumFactReceive;
                    data.XJSumFactReceive += item.XJSumFactReceive;
                    data.SKSumFactReceive += item.SKSumFactReceive;
                    data.OnlineSumFactReceive += item.OnlineSumFactReceive;
                    data.SumRepayment += item.SumRepayment;
                    data.SumArrearage += item.SumArrearage;
                    data.SumMoney += item.SumMoney;
                    data.SensorSumReceivable += item.SensorSumReceivable;
                    data.PosTimes += item.PosTimes;
                    data.SensorTimes += item.SensorTimes;
                }
            }




            return new GetAllParkChargesOutput()
            {
                rows = expr.OrderBy(p => p.SumFactReceive).PageBy(input).ToList(),
                userdata = data,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ParkChargesDto> GetMoneyTotalGroupbyParkEchar(ParkChargeInput input)
        {
            var temp = Table.Where(p => p.CarInTime >= input.begindt && p.CarInTime <= input.enddt && p.Park.ParkName != null);
            if (!string.IsNullOrWhiteSpace(input.parkIdInput) && input.parkIdInput != "0")
            {
                int parkid = int.Parse(input.parkIdInput);
                temp = temp.Where(p => p.ParkId == parkid);
            }

            var expr = from p in temp
                       group p by p.ParkId
                           into g
                       select new ParkChargesDto
                       {
                           ParkName = Context.Set<Parks.Park>().FirstOrDefault(a => a.Id == g.Key) == default(Parks.Park) ? "" : Context.Set<Parks.Park>().FirstOrDefault(a => a.Id == g.Key).ParkName,
                           SumFactReceive = g.Sum(p => p.FactReceive),
                           XJSumFactReceive = g.Where(p => p.PayStatus == 1).Sum(p => p.FactReceive),
                           SKSumFactReceive = g.Where(p => p.PayStatus == 2).Sum(p => p.FactReceive),
                           SumArrearage = g.Sum(p => p.Arrearage),
                           SumMoney = g.Sum(p => p.Money)
                       };
            return expr.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ParkChargesDto> GetMoneyTotalGroupbyBerthsecEchar(ParkChargeInput input)
        {
            var temp = Table.Where(p => p.CarInTime >= input.begindt && p.CarInTime <= input.enddt && p.Berthsec.BerthsecName != null);
            if (!string.IsNullOrWhiteSpace(input.berthsecIdInput) && input.berthsecIdInput != "0")
            {
                int berthsecId = int.Parse(input.berthsecIdInput);
                temp = temp.Where(p => p.BerthsecId == berthsecId);
            }

            var expr = from p in temp
                       group p by p.BerthsecId
                           into g
                       select new ParkChargesDto
                       {
                           ParkName = Context.Set<Berthsecs.Berthsec>().FirstOrDefault(a => a.Id == g.Key) == default(Berthsecs.Berthsec) ? "" : Context.Set<Berthsecs.Berthsec>().FirstOrDefault(a => a.Id == g.Key).BerthsecName,
                           SumFactReceive = g.Sum(p => p.FactReceive),
                           XJSumFactReceive = g.Where(p => p.PayStatus == 1).Sum(p => p.FactReceive),
                           SKSumFactReceive = g.Where(p => p.PayStatus == 2).Sum(p => p.FactReceive),
                           SumArrearage = g.Sum(p => p.Arrearage),
                           SumMoney = g.Sum(p => p.Money)
                       };
            return expr.ToList();
        }

        /// <summary>
        /// 收费员所收的钱  汇总
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public EmployeeUserData GetEmployeeMoneyCount(EmployeeChargeInput input)
        {
            List<SqlParameter> SqlParam = new List<SqlParameter>();
            DbRawSqlQuery<EmployeeChargesDto> employee = null;

            string sqlwhere = string.IsNullOrWhiteSpace(input.BerthsecIds)?" and 1 = 1 ":" and BerthsecId in (" + input.BerthsecIds + ")";
            string noberthsecsqlwhere = "";
            if (input.TenantId.HasValue)
            {
                sqlwhere += " and TenantId = @TenantId ";
                noberthsecsqlwhere = " and TenantId = @TenantId ";
                SqlParam.Add(new SqlParameter("@TenantId", input.TenantId.Value));
            }

            string sqlstr = @"select isnull(b.CarInCount,0) as CarInCount,isnull(b.CarOutCount,0) as CarOutCount, InOperaId,isnull(XJSumFactReceive,0) as XJSumFactReceive,isnull(SKSumFactReceive,0) as SKSumFactReceive,isnull(SumMoney,0) as SumMoney,isnull(SumArrearage,0) as SumArrearage,isnull(XJSumRepayment,0) as XJSumRepayment,isnull(SKSumRepayment,0) as SKSumRepayment,isnull(SumFactReceive,0) as SumFactReceive,RowNumber,isnull(ChargeOperaName,bujiaoName) as ChargeOperaName,chuchangName,bujiaoName, isnull(WxSumFactReceive, 0) as WxSumFactReceive, ISNULL(WXSumRepayment, 0) WXSumRepayment, CardMoney from

(select A.CarInCount,B.CarOutCount, isnull(A.xj,0) + isnull(B.xj,0) as XJSumFactReceive, isnull(A.sk,0) + isnull(B.sk,0) as SKSumFactReceive, isnull(A.xj,0)    + isnull(B.xj,0)+ isnull(A.sk,0) + isnull(B.sk,0)+isnull(Arrearage,0) as SumMoney,isnull(B.Arrearage, 0) AS SumArrearage,  isnull(InOperaId, ChargeOperaId) as InOperaId,isnull(de.SJ_Repayment,0) AS XJSumRepayment,isnull(de.SK_Repayment,0) AS SKSumRepayment , isnull(A.wx, 0) + isnull(B.wx, 0) as WxSumFactReceive,  isnull(de.WX_Repayment, 0) AS WXSumRepayment, (isnull(A.xj,0) + isnull(B.xj,0)+isnull(A.sk,0) + isnull(B.sk,0)) as SumFactReceive ,B.ChargeOperaId,ROW_NUMBER() OVER(Order by A.InOperaId DESC ) AS RowNumber,isnull(case when AE.isdeleted = 1 then AE.truename + '(该收费员已删除)' else AE.truename end,AR.truename) as ChargeOperaName,AR.truename as chuchangName,AT.truename as bujiaoName, isnull(dr.CardMoney, 0) as CardMoney from (

select sum( case when PrepaidPayStatus = 1 then Prepaid else 0 end) xj, 
sum( case when PrepaidPayStatus = 2 then Prepaid else 0 end) sk, 
 sum( case when PrepaidPayStatus = 3 then Prepaid else 0 end) wx,
COUNT(*) CarInCount,
 InOperaId from AbpBusinessDetail with(nolock) where 
 CarInTime > @beginTime and CarInTime < @endTime" + sqlwhere + "  and isdeleted = 0 ";
            //sqlstr += @" and BerthsecId in (" + input.BerthsecIds + ")";
            if (input.CompanyId != null)
            {
                sqlstr += @" and CompanyId = @CompanyId ";
            }
            if (!string.IsNullOrWhiteSpace(input.employeeIdInput) && input.employeeIdInput != "0")
            {
                long employeeId = long.Parse(input.employeeIdInput);
                sqlstr += @"and InOperaId=@InOperaId  group by InOperaId) AS A

 full join (

 select COUNT(*) CarOutCount,sum( case when PayStatus = 1 then FactReceive - Prepaid else 0 end) xj,
 sum( case when PayStatus = 2 then FactReceive - Prepaid else 0 end) sk, sum( case when PayStatus = 3 then FactReceive - Prepaid else 0 end) wx, sum(Money) as money, sum(Arrearage) as Arrearage,
    ChargeOperaId
  from AbpBusinessDetail with(nolock) where
  CarpayTime >  @beginTimeA and CarpayTime < @endTimeB" + sqlwhere;

                if (input.CompanyId != null)
                {
                    sqlstr += @" and CompanyId = @CompanyId ";
                }
                //sqlstr += @" and BerthsecId in (" + input.BerthsecIds + ")";
                string escapeBegin = string.Empty;
                if (!string.IsNullOrWhiteSpace(input.EscapeTimeDateBegin))
                {
                    //逃逸开始时间
                    escapeBegin = " and EscapeTime >= '" + input.EscapeTimeDateBegin + "'";
                }
                string escapeEnd = string.Empty;
                if (!string.IsNullOrWhiteSpace(input.EscapeTimeDateEnd))
                {
                    //逃逸结束时间
                    escapeEnd = " and EscapeTime <= '" + input.EscapeTimeDateEnd + "'";
                }
                sqlstr += @" and ChargeOperaId=@ChargeOperaId and isdeleted=0  group by ChargeOperaId
  ) AS B on A.InOperaId = B.ChargeOperaId 
full join(select sum(case when EscapePayStatus=1 then Repayment else 0 end ) as SJ_Repayment,
 sum(case when EscapePayStatus=2 then Repayment else 0 end ) as SK_Repayment, sum(case when EscapePayStatus = 3 then Repayment else 0 end) as WX_Repayment, escapeoperaid from AbpBusinessDetail with(nolock) where
  CarRepaymentTime > @beginCarRepaymentTime and CarRepaymentTime < @endCarRepaymentTime  "+ escapeBegin + escapeEnd + sqlwhere;
                if (input.CompanyId != null)
                {
                    sqlstr += @" and CompanyId=@CompanyId ";
                    SqlParam.Add(new SqlParameter("@CompanyId", input.CompanyId));
                }

                //sqlstr += @" and BerthsecId in (" + input.BerthsecIds + ")";

                sqlstr += @"and escapeoperaid = @escapeoperaid and isdeleted = 0 group by escapeoperaid)as de on A.InOperaId=de.escapeoperaid full join ( select sum(Money) as CardMoney, EmployeeId from AbpDeductionRecords with(nolock) where OperType = 1 and EmployeeId <> 0 " + noberthsecsqlwhere + " and InTime > @beginTime and InTime < @endTime group by EmployeeId) AS dr on A.InOperaId = dr.EmployeeId  left join AbpEmployees as AE on A.InOperaId=AE.id left join AbpEmployees as AR on B.ChargeOperaId=AR.id  left join AbpEmployees as AT on de.escapeoperaid=AT.id ) as b where  InOperaId is not null and RowNumber BETWEEN @beginSize and @endSize ";
                SqlParam.Add(new SqlParameter("@beginTime", input.begindt));
                SqlParam.Add(new SqlParameter("@endTime", input.enddt));
                SqlParam.Add(new SqlParameter("@beginTimeA", input.begindt));
                SqlParam.Add(new SqlParameter("@endTimeB", input.enddt));
                SqlParam.Add(new SqlParameter("@InOperaId", employeeId));
                SqlParam.Add(new SqlParameter("@beginCarRepaymentTime", input.begindt));
                SqlParam.Add(new SqlParameter("@endCarRepaymentTime", input.enddt));
                SqlParam.Add(new SqlParameter("@escapeoperaid", employeeId));
                SqlParam.Add(new SqlParameter("@ChargeOperaId", employeeId));
                SqlParam.Add(new SqlParameter("@beginSize", 1));
                SqlParam.Add(new SqlParameter("@endSize", 1000));
                SqlParameter[] param = SqlParam.ToArray();
                employee = Context.Database.SqlQuery<EmployeeChargesDto>(sqlstr, param);
            }
            else
            {
                sqlstr += @"group by InOperaId) AS A
                 full join (
                 select COUNT(*) CarOutCount,sum( case when PayStatus = 1 then FactReceive - Prepaid else 0 end) xj,
                 sum( case when PayStatus = 2 then FactReceive - Prepaid else 0 end) sk, sum( case when PayStatus = 3 then FactReceive - Prepaid else 0 end) wx,  sum(Money) as money, sum(Arrearage) as Arrearage,
                    ChargeOperaId
                  from AbpBusinessDetail with(nolock) where
                  CarpayTime >  @beginTimeC and CarpayTime < @endTimeD" + sqlwhere;

                if (input.CompanyId != null)
                {
                    sqlstr += @" and CompanyId = @CompanyId ";
                    SqlParam.Add(new SqlParameter("@CompanyId", input.CompanyId));
                }
                string escapeBegin = string.Empty;
                if (!string.IsNullOrWhiteSpace(input.EscapeTimeDateBegin))
                {
                    //逃逸开始时间
                    escapeBegin = " and EscapeTime >= '" + input.EscapeTimeDateBegin + "'";
                }
                string escapeEnd = string.Empty;
                if (!string.IsNullOrWhiteSpace(input.EscapeTimeDateEnd))
                {
                    //逃逸结束时间
                    escapeEnd = " and EscapeTime <= '" + input.EscapeTimeDateEnd + "'";
                }
                sqlstr += @"and isdeleted=0  group by ChargeOperaId
  ) AS B on A.InOperaId = B.ChargeOperaId full join(select sum(case when EscapePayStatus = 1 then Repayment else 0 end ) as SJ_Repayment,
 sum(case when EscapePayStatus = 2 then Repayment else 0 end ) as SK_Repayment, sum(case when EscapePayStatus = 3 then Repayment else 0 end) as WX_Repayment, escapeoperaid from AbpBusinessDetail with(nolock) where
  CarRepaymentTime > @beginTime and CarRepaymentTime < @endTime " + escapeBegin + escapeEnd + sqlwhere;
                if (input.CompanyId != null)
                {
                    sqlstr += @" and CompanyId=@CompanyId ";
                }

                //sqlstr += @" and BerthsecId in (" + input.BerthsecIds + ")";

                sqlstr += @" and isdeleted = 0 group by escapeoperaid)as de on A.InOperaId = de.escapeoperaid full join ( select sum(Money) as CardMoney, EmployeeId from AbpDeductionRecords with(nolock) where OperType = 1 and EmployeeId <> 0 " + noberthsecsqlwhere + " and " +
                    "InTime > @beginTime and InTime < @endTime group by EmployeeId) AS dr on A.InOperaId = dr.EmployeeId left join AbpEmployees as AE on A.InOperaId=AE.id    " +
                    "left join AbpEmployees as AR on B.ChargeOperaId=AR.id  " +
                    "left join AbpEmployees as AT on de.escapeoperaid = AT.id ) as b where  InOperaId is not null and RowNumber BETWEEN @beginSize and @endSize";

                SqlParam.Add(new SqlParameter("@beginTime", input.begindt));
                SqlParam.Add(new SqlParameter("@endTime", input.enddt));
                SqlParam.Add(new SqlParameter("@beginTimeC", input.begindt));
                SqlParam.Add(new SqlParameter("@endTimeD", input.enddt));
                SqlParam.Add(new SqlParameter("@beginSize", 1));
                SqlParam.Add(new SqlParameter("@endSize", 1000));
                SqlParameter[] param = SqlParam.ToArray();
                employee = Context.Database.SqlQuery<EmployeeChargesDto>(sqlstr, param);
            }
            var entry = employee.ToList();
            EmployeeUserData eud = new EmployeeUserData();
            string ChargeOperaName = "汇总";
            decimal SumFactReceive = 0;
            decimal? XJSumFactReceive = 0;
            decimal? SKSumFactReceive = 0;
            decimal SumArrearage = 0;
            decimal? SumMoney = 0;
            decimal? XJSumRepayment = 0;
            decimal? SKSumRepayment = 0;
            decimal WXSumRepayment = 0;
            decimal WxSumFactReceive = 0;
            int CarInCount = 0;
            int CarOutCount = 0;

            if (entry.Count() > 0)
            {
                foreach (var e in entry)
                {
                    CarInCount += Convert.ToInt32( e.CarInCount);
                    CarOutCount += Convert.ToInt32(e.CarOutCount);
                    SumFactReceive += e.SumFactReceive;
                    XJSumFactReceive += e.XJSumFactReceive;
                    SKSumFactReceive += e.SKSumFactReceive;
                    SumArrearage += e.SumArrearage;
                    SumMoney += e.SumMoney;
                    XJSumRepayment += e.XJSumRepayment;
                    SKSumRepayment += e.SKSumRepayment;
                    WXSumRepayment += e.WXSumRepayment;
                    WxSumFactReceive += e.WxSumFactReceive;
                    var temp = e.XJSumRepayment + e.XJSumFactReceive;
                }
            }
            eud.CarOutCount = CarOutCount;
            eud.CarInCount = CarInCount;
            eud.ChargeOperaName = ChargeOperaName;
            eud.SumFactReceive = SumFactReceive;
            eud.XJSumFactReceive = XJSumFactReceive;
            eud.SKSumFactReceive = SKSumFactReceive;
            eud.SumArrearage = SumArrearage;
            eud.SumMoney = SumMoney;
            eud.XJSumRepayment = XJSumRepayment;
            eud.SKSumRepayment = SKSumRepayment;
            eud.WxSumFactReceive = WxSumFactReceive;
            eud.WXSumRepayment = WXSumRepayment;
            return eud;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllEmployeeChargesOutput GetMoneyTotalGroupbyEmployeeDetail(EmployeeChargeInput input)
        {
            List<SqlParameter> SqlParam = new List<SqlParameter>();
            DbRawSqlQuery<EmployeeChargesDto> employee = null;

            string sqlwhere = string.IsNullOrWhiteSpace(input.BerthsecIds)?" and 1 = 1 ":" and BerthsecId in (" + input.BerthsecIds + ")";
            string noberthsecsqhwhere = "";
            if (input.TenantId.HasValue)
            {
                sqlwhere += " and TenantId = @TenantId ";
                noberthsecsqhwhere = " and TenantId = @TenantId ";
                SqlParam.Add(new SqlParameter("@TenantId", input.TenantId.Value));
            }
            //+ isnull(SKSumFactReceive, 0) 账号汇总
            string sqlstr = @"select isnull(b.CarInCount,0) as CarInCount,isnull(b.CarOutCount,0) as CarOutCount,isnull(InOperaId, ChargeOperaId) as InOperaId, isnull(XJSumFactReceive, 0) as XJSumFactReceive, Date, isnull(SKSumFactReceive,0) as SKSumFactReceive,isnull(SumMoney, 0) as SumMoney, isnull(SumArrearage, 0) as SumArrearage, isnull(XJSumRepayment,0) as XJSumRepayment, isnull(SKSumRepayment,0) as SKSumRepayment, isnull(SumFactReceive, 0) as SumFactReceive, isnull(ChargeOperaName,bujiaoName) as ChargeOperaName, chuchangName, bujiaoName, isnull(UserName, bujiaoUserName) as UserName, (isnull(XJSumFactReceive, 0)  + isnull(XJSumRepayment, 0)) as SumIncomePlusBack, (isnull(XJSumRepayment, 0) + isnull(SKSumRepayment, 0) + isnull(WXSumRepayment, 0)) as SumRepayment, ISNULL(WXSumRepayment, 0) WXSumRepayment, isnull(WxSumFactReceive, 0) as WxSumFactReceive, CardMoney from (select A.CarInCount,B.CarOutCount,isnull(A.xj, 0) + isnull(B.xj, 0) as XJSumFactReceive, isnull(A.sk, 0) + isnull(B.sk,0) as SKSumFactReceive, isnull(A.wx, 0) + isnull(B.wx, 0) as WxSumFactReceive, isnull(A.xj, 0) + isnull(B.xj,0)+ isnull(A.sk,0) + isnull(B.sk, 0)+isnull(Arrearage, 0) as SumMoney, isnull(B.Arrearage, 0) AS SumArrearage, InOperaId,isnull(de.SJ_Repayment, 0) AS XJSumRepayment, isnull(de.WX_Repayment, 0) AS WXSumRepayment, isnull(de.SK_Repayment, 0) AS SKSumRepayment, A.Date, (isnull(A.xj, 0) + isnull(B.xj, 0)+isnull(A.sk, 0) + isnull(B.sk, 0)) as SumFactReceive, B.ChargeOperaId, isnull(case when AE.isdeleted = 1 then AE.truename+'(该收费员已删除)' else AE.truename end, AR.truename) as ChargeOperaName, AR.truename as chuchangName, AT.truename as bujiaoName, isnull(AE.UserName, AR.UserName) as UserName,AT.UserName as bujiaoUserName, isnull(dr.CardMoney, 0) as CardMoney  from (
              select  COUNT(*) CarInCount,sum( case when PrepaidPayStatus = 1 then Prepaid else 0 end) xj, 
              sum( case when PrepaidPayStatus = 2 then Prepaid else 0 end) sk, 
              sum( case when PrepaidPayStatus = 3 then Prepaid else 0 end) wx,
              convert(varchar(11), CarInTime, 120) as [Date],
              InOperaId from AbpBusinessDetail with(nolock) where 
              CarInTime > @beginTime and CarInTime < @endTime" + sqlwhere + " and isdeleted = 0 ";
            if (input.CompanyId.HasValue)
            {
                sqlstr += @" and CompanyId = @CompanyId ";
            }

            //sqlstr += @" and BerthsecId in (" + input.BerthsecIds + ")";

            if (!string.IsNullOrWhiteSpace(input.employeeIdInput) && input.employeeIdInput != "0")
            {
                _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete);
                long employeeId = _EmployeeAppService.GetEmployeeByUserName(input.employeeIdInput).Id;
                sqlstr += @"and InOperaId = @InOperaId  group by InOperaId, convert(varchar(11), CarInTime, 120)) AS A full join (
                 select COUNT(*) CarOutCount,sum( case when PayStatus = 1 then FactReceive - Prepaid else 0 end) xj,
                 sum( case when PayStatus = 2 then FactReceive - Prepaid else 0 end) sk, 
                 sum( case when PayStatus = 3 then FactReceive - Prepaid else 0 end) wx,
                 sum(Money) as money, convert(varchar(11), CarpayTime, 120) as [Date],
                 sum(Arrearage) as Arrearage, ChargeOperaId from AbpBusinessDetail with(nolock) where
                  CarpayTime > @beginTimeA and CarpayTime < @endTimeB" + sqlwhere + " and isdeleted = 0 ";
                if (input.CompanyId.HasValue)
                {
                    sqlstr += @" and CompanyId = @CompanyId ";
                }

                string escapeBegin = string.Empty;
                if (!string.IsNullOrWhiteSpace(input.EscapeTimeDateBegin))
                {
                    //逃逸开始时间
                    escapeBegin = " and EscapeTime >= '" + input.EscapeTimeDateBegin + "'";
                }
                string escapeEnd = string.Empty;
                if (!string.IsNullOrWhiteSpace(input.EscapeTimeDateEnd))
                {
                    //逃逸结束时间
                    escapeEnd = " and EscapeTime <= '" + input.EscapeTimeDateEnd + "'";
                }
                sqlstr += @" and ChargeOperaId = @ChargeOperaId and isdeleted = 0  group by ChargeOperaId, convert(varchar(11), CarpayTime, 120)) AS B on A.InOperaId = B.ChargeOperaId  and A.Date = B.Date
                full join(select sum(case when EscapePayStatus = 1 then Repayment else 0 end ) as SJ_Repayment,
                sum(case when EscapePayStatus = 2 then Repayment else 0 end ) as SK_Repayment, 
                convert(varchar(11), CarRepaymentTime, 120) as [Date],
                sum(case when EscapePayStatus = 3 then Repayment else 0 end) as WX_Repayment, escapeoperaid from AbpBusinessDetail with(nolock) where
                CarRepaymentTime > @beginCarRepaymentTime and CarRepaymentTime < @endCarRepaymentTime  " + escapeBegin + escapeEnd + sqlwhere + " and isdeleted = 0 ";
                if (input.CompanyId.HasValue)
                {
                    sqlstr += @" and CompanyId = @CompanyId ";
                    SqlParam.Add(new SqlParameter("@CompanyId", input.CompanyId));
                }
                

                sqlstr += @"and escapeoperaid = @escapeoperaid group by escapeoperaid, convert(varchar(11), CarRepaymentTime, 120)) as de on A.InOperaId = de.escapeoperaid and A.Date = de.Date
                full join ( select sum(Money) as CardMoney, EmployeeId, convert(varchar(11), InTime, 120) as [Date]  from AbpDeductionRecords with(nolock) where OperType = 1 and EmployeeId <> 0 " + noberthsecsqhwhere + " and InTime > @beginTime and InTime < @endTime group by EmployeeId, convert(varchar(11), InTime, 120)) AS dr on A.InOperaId = dr.EmployeeId and A.Date = dr.Date left join AbpEmployees as AE on A.InOperaId = AE.id left join AbpEmployees as AR on B.ChargeOperaId = AR.id left join AbpEmployees as AT on de.escapeoperaid = AT.id ) as b where UserName is not NULL order by Date";
                SqlParam.Add(new SqlParameter("@beginTime", input.begindt));
                SqlParam.Add(new SqlParameter("@endTime", input.enddt));
                SqlParam.Add(new SqlParameter("@beginTimeA", input.begindt));
                SqlParam.Add(new SqlParameter("@endTimeB", input.enddt));
                SqlParam.Add(new SqlParameter("@InOperaId", employeeId));
                SqlParam.Add(new SqlParameter("@beginCarRepaymentTime", input.begindt));
                SqlParam.Add(new SqlParameter("@endCarRepaymentTime", input.enddt));
                SqlParam.Add(new SqlParameter("@escapeoperaid", employeeId));
                SqlParam.Add(new SqlParameter("@ChargeOperaId", employeeId));
                SqlParam.Add(new SqlParameter("@beginSize", input.page * input.rows - input.rows + 1));
                SqlParam.Add(new SqlParameter("@endSize", input.page * input.rows));
                SqlParameter[] param = SqlParam.ToArray();
                employee = Context.Database.SqlQuery<EmployeeChargesDto>(sqlstr, param);
            }
            var entry = employee.ToList();
            return new GetAllEmployeeChargesOutput()
            {
                rows = entry
            };
        }


        /// <summary>
        /// 收费员收费报表(只查询正常收费)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllEmployeeChargesOutput GetMoneyTotalGroupbyEmployee(EmployeeChargeInput input)
        {
            List<SqlParameter> SqlParam = new List<SqlParameter>();
            DbRawSqlQuery<EmployeeChargesDto> employee = null;

            string sqlwhere = (input.BerthsecIds??string.Empty).Length==0?" and 1=1 ":" and BerthsecId in (" + input.BerthsecIds + ")";
            string noberthsecsqlwhere = "";
            if (input.TenantId.HasValue)
            {
                sqlwhere += " and TenantId = @TenantId ";
                noberthsecsqlwhere = " and TenantId = @TenantId ";
                SqlParam.Add(new SqlParameter("@TenantId", input.TenantId.Value));
            }
            //+ isnull(SKSumFactReceive, 0) 账号汇总
            string sqlstr = @"select  isnull(b.CarInCount,0) as CarInCount,isnull(b.CarOutCount,0) as CarOutCount,isnull(InOperaId, ChargeOperaId) as InOperaId, isnull(XJSumFactReceive, 0) as XJSumFactReceive, isnull(SKSumFactReceive,0) as SKSumFactReceive,isnull(SumMoney, 0) as SumMoney, isnull(SumArrearage, 0) as SumArrearage, isnull(XJSumRepayment,0) as XJSumRepayment, isnull(SKSumRepayment,0) as SKSumRepayment, isnull(SumFactReceive, 0) as SumFactReceive, RowNumber, isnull(ChargeOperaName,bujiaoName) as ChargeOperaName, chuchangName, bujiaoName, isnull(UserName, bujiaoUserName) as UserName, (isnull(XJSumFactReceive, 0)  + isnull(XJSumRepayment, 0)) as SumIncomePlusBack, (isnull(XJSumRepayment, 0) + isnull(SKSumRepayment, 0) + isnull(WXSumRepayment, 0)) as SumRepayment, ISNULL(WXSumRepayment, 0) WXSumRepayment, isnull(WxSumFactReceive, 0) as WxSumFactReceive, CardMoney from (select A.CarInCount,B.CarOutCount,isnull(A.xj, 0) + isnull(B.xj, 0) as XJSumFactReceive, isnull(A.sk, 0) + isnull(B.sk,0) as SKSumFactReceive, isnull(A.wx, 0) + isnull(B.wx, 0) as WxSumFactReceive, isnull(A.xj, 0) + isnull(B.xj,0)+ isnull(A.sk,0) + isnull(B.sk, 0)+isnull(Arrearage, 0) as SumMoney, isnull(B.Arrearage, 0) AS SumArrearage, InOperaId,isnull(de.SJ_Repayment, 0) AS XJSumRepayment, isnull(de.WX_Repayment, 0) AS WXSumRepayment, isnull(de.SK_Repayment, 0) AS SKSumRepayment, (isnull(A.xj, 0) + isnull(B.xj, 0)+isnull(A.sk, 0) + isnull(B.sk, 0)) as SumFactReceive, B.ChargeOperaId, ROW_NUMBER() OVER(Order by A.InOperaId DESC) AS RowNumber, isnull(case when AE.isdeleted = 1 then AE.truename+'(该收费员已删除)' else AE.truename end, AR.truename) as ChargeOperaName, AR.truename as chuchangName, AT.truename as bujiaoName, isnull(AE.UserName, AR.UserName) as UserName,AT.UserName as bujiaoUserName, isnull(dr.CardMoney, 0) as CardMoney  from (
              select COUNT(*) CarInCount,sum( case when PrepaidPayStatus = 1 then Prepaid else 0 end) xj, 
              sum( case when PrepaidPayStatus = 4 then Prepaid else 0 end) sk, 
              sum( case when PrepaidPayStatus = 3 then Prepaid else 0 end) wx,
              InOperaId from AbpBusinessDetail with(nolock) where 
              CarInTime > @beginTime and CarInTime < @endTime" + sqlwhere + " and isdeleted = 0 ";
            //sqlstr += @" and BerthsecId in (" + input.BerthsecIds + ")";
            if (input.CompanyId.HasValue)
            {
                sqlstr += @" and CompanyId = @CompanyId ";
            }
            if (!string.IsNullOrWhiteSpace(input.employeeIdInput) && input.employeeIdInput != "0")
            {
                long employeeId = long.Parse(input.employeeIdInput);
                sqlstr += @"and InOperaId = @InOperaId  group by InOperaId) AS A full join (
                 select COUNT(*) CarOutCount,sum( case when PayStatus = 1 then FactReceive - Prepaid else 0 end) xj,
                 sum( case when PayStatus = 4 then FactReceive - Prepaid else 0 end) sk, 
                 sum( case when PayStatus = 3 then FactReceive - Prepaid else 0 end) wx,
                 sum(Money) as money,
                 sum(Arrearage) as Arrearage, ChargeOperaId from AbpBusinessDetail with(nolock) where
                  CarpayTime > @beginTimeA and CarpayTime < @endTimeB" + sqlwhere + " and isdeleted = 0 ";
                if (input.CompanyId.HasValue)
                {
                    sqlstr += @" and CompanyId = @CompanyId ";
                }

                //sqlstr += @" and BerthsecId in (" + input.BerthsecIds + ")";
                string escapeBegin = string.Empty;
                if (!string.IsNullOrWhiteSpace(input.EscapeTimeDateBegin))
                {
                    //逃逸开始时间
                    escapeBegin = " and EscapeTime >= '" + input.EscapeTimeDateBegin + "'";
                }
                string escapeEnd = string.Empty;
                if (!string.IsNullOrWhiteSpace(input.EscapeTimeDateEnd))
                {
                    //逃逸结束时间
                    escapeEnd = " and EscapeTime <= '" + input.EscapeTimeDateEnd + "'";
                }
                sqlstr += @" and ChargeOperaId = @ChargeOperaId and isdeleted = 0  group by ChargeOperaId ) AS B on A.InOperaId = B.ChargeOperaId 
                full join(select sum(case when EscapePayStatus=1 then Repayment else 0 end ) as SJ_Repayment,
                sum(case when EscapePayStatus = 4 then Repayment else 0 end ) as SK_Repayment, 
                sum(case when EscapePayStatus = 3 then Repayment else 0 end) as WX_Repayment, escapeoperaid from AbpBusinessDetail with(nolock) where
                CarRepaymentTime > @beginCarRepaymentTime and CarRepaymentTime < @endCarRepaymentTime  " + escapeBegin + escapeEnd + sqlwhere + " and isdeleted = 0 ";
                if (input.CompanyId.HasValue)
                {
                    sqlstr += @" and CompanyId = @CompanyId ";
                    SqlParam.Add(new SqlParameter("@CompanyId", input.CompanyId));
                }
                //sqlstr += @" and BerthsecId in (" + input.BerthsecIds + ")";

                sqlstr += @"and escapeoperaid = @escapeoperaid group by escapeoperaid) as de on A.InOperaId = de.escapeoperaid
                full join ( select sum(Money) as CardMoney, EmployeeId from AbpDeductionRecords with(nolock) where OperType = 1 and EmployeeId <> 0 " + noberthsecsqlwhere + " and InTime > @beginTime and InTime < @endTime group by EmployeeId) AS dr on A.InOperaId = dr.EmployeeId left join AbpEmployees as AE on A.InOperaId = AE.id left join AbpEmployees as AR on B.ChargeOperaId = AR.id left join AbpEmployees as AT on de.escapeoperaid = AT.id ) as b where RowNumber BETWEEN @beginSize and @endSize ";
                SqlParam.Add(new SqlParameter("@beginTime", input.begindt));
                SqlParam.Add(new SqlParameter("@endTime", input.enddt));
                SqlParam.Add(new SqlParameter("@beginTimeA", input.begindt));
                SqlParam.Add(new SqlParameter("@endTimeB", input.enddt));
                SqlParam.Add(new SqlParameter("@InOperaId", employeeId));
                SqlParam.Add(new SqlParameter("@beginCarRepaymentTime", input.begindt));
                SqlParam.Add(new SqlParameter("@endCarRepaymentTime", input.enddt));
                SqlParam.Add(new SqlParameter("@escapeoperaid", employeeId));
                SqlParam.Add(new SqlParameter("@ChargeOperaId", employeeId));
                SqlParam.Add(new SqlParameter("@beginSize", input.page * input.rows - input.rows + 1));
                SqlParam.Add(new SqlParameter("@endSize", input.page * input.rows));
                SqlParameter[] param = SqlParam.ToArray();
                employee = Context.Database.SqlQuery<EmployeeChargesDto>(sqlstr, param);
            }
            else
            {
                sqlstr += @"group by InOperaId) AS A full join ( select COUNT(*) CarOutCount,sum( case when PayStatus = 1 then FactReceive - Prepaid else 0 end) xj,
 sum( case when PayStatus = 4 then FactReceive - Prepaid else 0 end) sk,  sum( case when PayStatus = 3 then FactReceive - Prepaid else 0 end) wx, sum(Money) as money, sum(Arrearage) as Arrearage, ChargeOperaId
  from AbpBusinessDetail with(nolock) where  CarpayTime >  @beginTimeC and CarpayTime < @endTimeD" + sqlwhere;
                if (input.CompanyId.HasValue)
                {
                    sqlstr += @" and CompanyId=@CompanyId ";
                    SqlParam.Add(new SqlParameter("@CompanyId", input.CompanyId));
                }
                //sqlstr += @" and BerthsecId in (" + input.BerthsecIds + ")";
                string escapeBegin = string.Empty;
                if (!string.IsNullOrWhiteSpace(input.EscapeTimeDateBegin))
                {
                    //逃逸开始时间
                    escapeBegin = " and EscapeTime >= '"+ input.EscapeTimeDateBegin + "'";
                }
                string escapeEnd = string.Empty;
                if (!string.IsNullOrWhiteSpace(input.EscapeTimeDateEnd))
                {
                    //逃逸结束时间
                    escapeEnd = " and EscapeTime <= '" + input.EscapeTimeDateEnd + "'";
                }
                sqlstr += @"and isdeleted = 0  group by ChargeOperaId
  ) AS B on A.InOperaId = B.ChargeOperaId full join(select sum(case when EscapePayStatus=1 then Repayment else 0 end ) as SJ_Repayment,
 sum(case when EscapePayStatus = 4 then Repayment else 0 end) as SK_Repayment, sum(case when EscapePayStatus = 3 then Repayment else 0 end) as WX_Repayment, escapeoperaid from AbpBusinessDetail with(nolock) where
  CarRepaymentTime > @beginTime and isdeleted = 0 and CarRepaymentTime < @endTime " + escapeBegin + escapeEnd + sqlwhere;
                if (input.CompanyId.HasValue)
                {
                    sqlstr += @" and CompanyId = @CompanyId ";
                }

                //sqlstr += @" and BerthsecId in (" + input.BerthsecIds + ")";

                sqlstr += @"  group by escapeoperaid)as de on A.InOperaId = de.escapeoperaid full join (select sum(Money) as CardMoney, EmployeeId from AbpDeductionRecords with(nolock) where OperType = 1 and EmployeeId <> 0 " + noberthsecsqlwhere + " and InTime > @beginTime and InTime < @endTime group by EmployeeId) AS dr on A.InOperaId = dr.EmployeeId left join AbpEmployees as AE on A.InOperaId = AE.id left join AbpEmployees as AR on B.ChargeOperaId = AR.id  left join AbpEmployees as AT on de.escapeoperaid = AT.id ) as b where RowNumber BETWEEN @beginSize and @endSize and UserName is not null";

                SqlParam.Add(new SqlParameter("@beginTime", input.begindt));
                SqlParam.Add(new SqlParameter("@endTime", input.enddt));
                SqlParam.Add(new SqlParameter("@beginTimeC", input.begindt));
                SqlParam.Add(new SqlParameter("@endTimeD", input.enddt));
                SqlParam.Add(new SqlParameter("@beginSize", input.page * input.rows - input.rows + 1));
                SqlParam.Add(new SqlParameter("@endSize", input.page * input.rows));
                SqlParameter[] param = SqlParam.ToArray();

                employee = Context.Database.SqlQuery<EmployeeChargesDto>(sqlstr, param);
            }

            //IQueryable<EmployeeChargesDto> employeeChen = (DbRawSqlQuery<EmployeeChargesDto>) employee;

            var entry = employee.ToList();
            //var chendewang = _AbpEmployeeRepository.GetAll();
            int records = GetEmployeeCount(input);
            EmployeeUserData eud = GetEmployeeMoneyCount(input);
            return new GetAllEmployeeChargesOutput()
            {
                rows = entry,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0),
                userdata = eud
            };
        }



        public GetAllEmployeeChargesOutput GetMoneyTotalGroupbyDate(EmployeeChargeInput input)
        {
            List<SqlParameter> SqlParam = new List<SqlParameter>();
            DbRawSqlQuery<EmployeeChargesDto> employee = null;

            string sqlwhere = " and BerthsecId in (" + input.BerthsecIds + ")";
            string noberthsecsqlwhere = "";
            if (input.TenantId.HasValue)
            {
                sqlwhere += " and TenantId = @TenantId ";
                noberthsecsqlwhere = " and TenantId = @TenantId ";
                SqlParam.Add(new SqlParameter("@TenantId", input.TenantId.Value));
            }

            string sqlstr = @"select CreationTime Date,FactReceive SumMoney,Arrearage SumArrearage,
                                 businessCount CarInCount,
                                sensorCount CarOutCount
	                            from (
			                            select A.CreationTime ,FactReceive,Arrearage,
case when  businessCount is null then 0 else businessCount end businessCount,
case when  sensorCount is null then 0 else sensorCount end sensorCount,
SJ_Repayment XJSumRepayment,SK_Repayment SKSumRepayment,WX_Repayment WXSumRepayment,ROW_NUMBER() OVER(Order by A.CreationTime DESC) AS RowNumber
			                            from(
					                            select sum(FactReceive) FactReceive,sum(Arrearage) Arrearage,COUNT(1) businessCount ,
					                            convert(varchar(10),bus.CreationTime,120)CreationTime
					                            from AbpBusinessDetail  bus
					                            where convert(datetime,CreationTime) between @beginTime and @endTime and TenantId = 1 and CompanyId = 1 and isdeleted = 0  and CreationTime is not null 
					                            group by convert(varchar(10),CreationTime,120)
			                            ) A
			                            full join (
					                            select COUNT(1) sensorCount ,convert(varchar(10),CreationTime,120) CreationTime 
					                            from abpsensorbusinessdetail   

					                            where convert(datetime,CreationTime) between @beginTime and @endTime and TenantId = 1 and CompanyId = 1  and CreationTime is not null 
					                            group by convert(varchar(10),CreationTime,120)
				                            )as B on B.CreationTime = A.CreationTime
			                             full join(
					                            select 
					                            sum(case when EscapePayStatus=1 then Repayment else 0 end ) as SJ_Repayment,
					                            sum(case when EscapePayStatus = 4 then Repayment else 0 end) as SK_Repayment,
					                            sum(case when EscapePayStatus = 3 then Repayment else 0 end) as WX_Repayment,convert(varchar(10),CreationTime,120) CreationTime
					                            from AbpBusinessDetail with(nolock) 
					                            where convert(datetime,CreationTime) between @beginTime and @endTime and isdeleted = 0  
					                            and TenantId = 1  and CompanyId = 1   and CreationTime is not null 
					                            group by convert(varchar(10),CreationTime,120)
					                            )as de on A.CreationTime = de.CreationTime 
	                            ) temp where CreationTime is not null and RowNumber BETWEEN @beginSize and @endSize  ";

            SqlParam.Add(new SqlParameter("@beginTime", input.begindt));
            SqlParam.Add(new SqlParameter("@endTime", input.enddt));
            SqlParam.Add(new SqlParameter("@beginSize", input.page * input.rows - input.rows + 1));
            SqlParam.Add(new SqlParameter("@endSize", input.page * input.rows));
            SqlParameter[] param = SqlParam.ToArray();

            employee = Context.Database.SqlQuery<EmployeeChargesDto>(sqlstr, param);
            //sqlstr += @" and BerthsecId in (" + input.BerthsecIds + ")";
            //           if (input.CompanyId.HasValue)
            //           {
            //               sqlstr += @" and CompanyId = @CompanyId ";
            //           }
            //           if (!string.IsNullOrWhiteSpace(input.employeeIdInput) && input.employeeIdInput != "0")
            //           {
            //               long employeeId = long.Parse(input.employeeIdInput);
            //               sqlstr += @"and InOperaId = @InOperaId  group by InOperaId) AS A full join (
            //                select COUNT(*) CarOutCount,sum( case when PayStatus = 1 then FactReceive - Prepaid else 0 end) xj,
            //                sum( case when PayStatus = 4 then FactReceive - Prepaid else 0 end) sk, 
            //                sum( case when PayStatus = 3 then FactReceive - Prepaid else 0 end) wx,
            //                sum(Money) as money,
            //                sum(Arrearage) as Arrearage, ChargeOperaId from AbpBusinessDetail with(nolock) where
            //                 CarpayTime > @beginTimeA and CarpayTime < @endTimeB" + sqlwhere + " and isdeleted = 0 ";
            //               if (input.CompanyId.HasValue)
            //               {
            //                   sqlstr += @" and CompanyId = @CompanyId ";
            //               }

            //               //sqlstr += @" and BerthsecId in (" + input.BerthsecIds + ")";

            //               sqlstr += @" and ChargeOperaId = @ChargeOperaId and isdeleted = 0  group by ChargeOperaId ) AS B on A.InOperaId = B.ChargeOperaId 
            //               full join(select sum(case when EscapePayStatus=1 then Repayment else 0 end ) as SJ_Repayment,
            //               sum(case when EscapePayStatus = 4 then Repayment else 0 end ) as SK_Repayment, 
            //               sum(case when EscapePayStatus = 3 then Repayment else 0 end) as WX_Repayment, escapeoperaid from AbpBusinessDetail with(nolock) where
            //               CarRepaymentTime > @beginCarRepaymentTime and CarRepaymentTime < @endCarRepaymentTime  and EscapeTime >= @EscapeTimebeginTime and EscapeTime <= @EscapeTimeendTime" + sqlwhere + " and isdeleted = 0 ";
            //               if (input.CompanyId.HasValue)
            //               {
            //                   sqlstr += @" and CompanyId = @CompanyId ";
            //                   SqlParam.Add(new SqlParameter("@CompanyId", input.CompanyId));
            //               }
            //               //sqlstr += @" and BerthsecId in (" + input.BerthsecIds + ")";

            //               sqlstr += @"and escapeoperaid = @escapeoperaid group by escapeoperaid) as de on A.InOperaId = de.escapeoperaid
            //               full join ( select sum(Money) as CardMoney, EmployeeId from AbpDeductionRecords with(nolock) where OperType = 1 and EmployeeId <> 0 " + noberthsecsqlwhere + " and InTime > @beginTime and InTime < @endTime group by EmployeeId) AS dr on A.InOperaId = dr.EmployeeId left join AbpEmployees as AE on A.InOperaId = AE.id left join AbpEmployees as AR on B.ChargeOperaId = AR.id left join AbpEmployees as AT on de.escapeoperaid = AT.id ) as b where RowNumber BETWEEN @beginSize and @endSize ";
            //               SqlParam.Add(new SqlParameter("@beginTime", input.begindt));
            //               SqlParam.Add(new SqlParameter("@endTime", input.enddt));
            //               SqlParam.Add(new SqlParameter("@beginTimeA", input.begindt));
            //               SqlParam.Add(new SqlParameter("@endTimeB", input.enddt));
            //               SqlParam.Add(new SqlParameter("@InOperaId", employeeId));
            //               SqlParam.Add(new SqlParameter("@beginCarRepaymentTime", input.begindt));
            //               SqlParam.Add(new SqlParameter("@endCarRepaymentTime", input.enddt));
            //               SqlParam.Add(new SqlParameter("@EscapeTimebeginTime", input.EscapeTimebegindt));
            //               SqlParam.Add(new SqlParameter("@EscapeTimeendTime", input.EscapeTimeenddt));
            //               SqlParam.Add(new SqlParameter("@escapeoperaid", employeeId));
            //               SqlParam.Add(new SqlParameter("@ChargeOperaId", employeeId));
            //               SqlParam.Add(new SqlParameter("@beginSize", input.page * input.rows - input.rows + 1));
            //               SqlParam.Add(new SqlParameter("@endSize", input.page * input.rows));
            //               SqlParameter[] param = SqlParam.ToArray();
            //               employee = Context.Database.SqlQuery<EmployeeChargesDto>(sqlstr, param);
            //           }
            //           else
            //           {
            //               sqlstr += @"group by InOperaId) AS A full join ( select COUNT(*) CarOutCount,sum( case when PayStatus = 1 then FactReceive - Prepaid else 0 end) xj,
            //sum( case when PayStatus = 4 then FactReceive - Prepaid else 0 end) sk,  sum( case when PayStatus = 3 then FactReceive - Prepaid else 0 end) wx, sum(Money) as money, sum(Arrearage) as Arrearage, ChargeOperaId
            // from AbpBusinessDetail with(nolock) where  CarpayTime >  @beginTimeC and CarpayTime < @endTimeD" + sqlwhere;
            //               if (input.CompanyId.HasValue)
            //               {
            //                   sqlstr += @" and CompanyId=@CompanyId ";
            //                   SqlParam.Add(new SqlParameter("@CompanyId", input.CompanyId));
            //               }
            //               //sqlstr += @" and BerthsecId in (" + input.BerthsecIds + ")";
            //               sqlstr += @"and isdeleted = 0  group by ChargeOperaId
            // ) AS B on A.InOperaId = B.ChargeOperaId full join(select sum(case when EscapePayStatus=1 then Repayment else 0 end ) as SJ_Repayment,
            //sum(case when EscapePayStatus = 4 then Repayment else 0 end) as SK_Repayment, sum(case when EscapePayStatus = 3 then Repayment else 0 end) as WX_Repayment, escapeoperaid from AbpBusinessDetail with(nolock) where
            // CarRepaymentTime > @beginTime and isdeleted = 0 and CarRepaymentTime < @endTime and EscapeTime >= @EscapeTimebeginTime and EscapeTime <= @EscapeTimeendTime" + sqlwhere;
            //               if (input.CompanyId.HasValue)
            //               {
            //                   sqlstr += @" and CompanyId = @CompanyId ";
            //               }

            //               //sqlstr += @" and BerthsecId in (" + input.BerthsecIds + ")";

            //               sqlstr += @"  group by escapeoperaid)as de on A.InOperaId = de.escapeoperaid full join (select sum(Money) as CardMoney, EmployeeId from AbpDeductionRecords with(nolock) where OperType = 1 and EmployeeId <> 0 " + noberthsecsqlwhere + " and InTime > @beginTime and InTime < @endTime group by EmployeeId) AS dr on A.InOperaId = dr.EmployeeId left join AbpEmployees as AE on A.InOperaId = AE.id left join AbpEmployees as AR on B.ChargeOperaId = AR.id  left join AbpEmployees as AT on de.escapeoperaid = AT.id ) as b where RowNumber BETWEEN @beginSize and @endSize and UserName is not null";

            //               SqlParam.Add(new SqlParameter("@beginTime", input.begindt));
            //               SqlParam.Add(new SqlParameter("@endTime", input.enddt));
            //               SqlParam.Add(new SqlParameter("@beginTimeC", input.begindt));
            //               SqlParam.Add(new SqlParameter("@endTimeD", input.enddt));
            //               SqlParam.Add(new SqlParameter("@EscapeTimebeginTime", input.EscapeTimebegindt));
            //               SqlParam.Add(new SqlParameter("@EscapeTimeendTime", input.EscapeTimeenddt));
            //               SqlParam.Add(new SqlParameter("@beginSize", input.page * input.rows - input.rows + 1));
            //               SqlParam.Add(new SqlParameter("@endSize", input.page * input.rows));
            //               SqlParameter[] param = SqlParam.ToArray();

            //               employee = Context.Database.SqlQuery<EmployeeChargesDto>(sqlstr, param);
            //           }

            //IQueryable<EmployeeChargesDto> employeeChen = (DbRawSqlQuery<EmployeeChargesDto>) employee;

            var entry = employee.ToList();
            //var chendewang = _AbpEmployeeRepository.GetAll();
            int records = GetRateCount(input);
            EmployeeUserData eud = GetEmployeeMoneyCount(input);
            return new GetAllEmployeeChargesOutput()
            {
                rows = entry,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0),
                userdata = eud
            };
        }

        public int GetRateCount(EmployeeChargeInput input)
        {

            List<SqlParameter> SqlParam = new List<SqlParameter>();
            DbRawSqlQuery<EmployeeChargesDto> employee = null;

            string sqlwhere = " and BerthsecId in (" + input.BerthsecIds + ")";
            if (input.TenantId.HasValue)
            {
                sqlwhere = " and TenantId = @TenantId ";
                SqlParam.Add(new SqlParameter("@TenantId", input.TenantId.Value));
            }
            string sqlstr = @"select CreationTime Date,FactReceive SumMoney,Arrearage SumArrearage,
                                case when  businessCount is null then 0 else businessCount end  CarInCount,
                                case when  sensorCount is null then 0 else sensorCount end  CarOutCount
	                            from (
			                            select A.CreationTime ,FactReceive,Arrearage,businessCount,sensorCount,SJ_Repayment XJSumRepayment,SK_Repayment SKSumRepayment,WX_Repayment WXSumRepayment,ROW_NUMBER() OVER(Order by A.CreationTime DESC) AS RowNumber
			                            from(
					                            select sum(FactReceive) FactReceive,sum(Arrearage) Arrearage,COUNT(1) businessCount ,
					                            convert(varchar(10),bus.CreationTime,120)CreationTime
					                            from AbpBusinessDetail  bus
					                            where convert(datetime,CreationTime) between @beginTime and @endTime and TenantId = 1 and CompanyId = 1 and isdeleted = 0  and CreationTime is not null 
					                            group by convert(varchar(10),CreationTime,120)
			                            ) A
			                            full join (
					                            select COUNT(1) sensorCount ,convert(varchar(10),CreationTime,120) CreationTime 
					                            from abpsensorbusinessdetail   

					                            where convert(datetime,CreationTime) between @beginTime and @endTime and TenantId = 1 and CompanyId = 1  and CreationTime is not null 
					                            group by convert(varchar(10),CreationTime,120)
				                            )as B on B.CreationTime = A.CreationTime
			                             full join(
					                            select 
					                            sum(case when EscapePayStatus=1 then Repayment else 0 end ) as SJ_Repayment,
					                            sum(case when EscapePayStatus = 4 then Repayment else 0 end) as SK_Repayment,
					                            sum(case when EscapePayStatus = 3 then Repayment else 0 end) as WX_Repayment,convert(varchar(10),CreationTime,120) CreationTime
					                            from AbpBusinessDetail with(nolock) 
					                            where convert(datetime,CreationTime) between @beginTime and @endTime and isdeleted = 0  
					                            and TenantId = 1  and CompanyId = 1   and CreationTime is not null 
					                            group by convert(varchar(10),CreationTime,120)
					                            )as de on A.CreationTime = de.CreationTime 
	                            ) temp where CreationTime is not null and RowNumber BETWEEN @beginSize and @endSize  ";

            SqlParam.Add(new SqlParameter("@beginTime", input.begindt));
            SqlParam.Add(new SqlParameter("@endTime", input.enddt));
            SqlParam.Add(new SqlParameter("@beginSize", 1));
            SqlParam.Add(new SqlParameter("@endSize", 1000));
            SqlParameter[] param = SqlParam.ToArray();
            //            string sqlstr = @"select RowNumber,ChargeOperaName from
            //(select ROW_NUMBER() OVER(Order by A.InOperaId DESC ) AS RowNumber,case when isdeleted = 1 then AE.truename + '(该收费员已删除)' else AE.truename end as ChargeOperaName from (
            //select sum( case when PrepaidPayStatus = 1 then Prepaid else 0 end) xj, 
            //sum(case when PrepaidPayStatus = 2 then Prepaid else 0 end) sk, InOperaId from AbpBusinessDetail with(nolock) where 
            // CarInTime > @beginTime and CarInTime < @endTime" + sqlwhere + "  and isdeleted = 0 ";
            //            if (input.CompanyId != null)
            //            {
            //                sqlstr += @" and CompanyId=@CompanyId ";
            //            }
            //            if (!string.IsNullOrWhiteSpace(input.employeeIdInput) && input.employeeIdInput != "0")
            //            {
            //                long employeeId = long.Parse(input.employeeIdInput);
            //                sqlstr += @"and InOperaId=@InOperaId  group by InOperaId) AS A
            //                 full join (
            //                 select sum( case when PayStatus = 1 then FactReceive - Prepaid else 0 end) xj,
            //                 sum( case when PayStatus = 2 then FactReceive - Prepaid else 0 end) sk, sum(Money) as money, sum(Arrearage) as Arrearage,
            //                    ChargeOperaId
            //                  from AbpBusinessDetail with(nolock) where
            //                  CarpayTime >  @beginTimeA and CarpayTime < @endTimeB" + sqlwhere;
            //                if (input.CompanyId.HasValue)
            //                {
            //                    sqlstr += @" and CompanyId=@CompanyId ";
            //                }
            //                sqlstr += @" and ChargeOperaId = @ChargeOperaId and isdeleted=0  group by ChargeOperaId
            //  ) AS B on A.InOperaId = B.ChargeOperaId 
            //full join(select sum(case when EscapePayStatus=1 then Repayment else 0 end ) as SJ_Repayment,
            // sum(case when EscapePayStatus=2 then Repayment else 0 end ) as SK_Repayment, escapeoperaid from AbpBusinessDetail where
            //  CarRepaymentTime > @beginCarRepaymentTime and CarRepaymentTime < @endCarRepaymentTime and EscapeTime >= @EscapeTimebeginTime and EscapeTime <= @EscapeTimeendTime" + sqlwhere;
            //                if (input.CompanyId.HasValue)
            //                {
            //                    sqlstr += @" and CompanyId=@CompanyId ";
            //                    Sqlparam.Add(new SqlParameter("@CompanyId", input.CompanyId));
            //                }
            //                sqlstr += @"and escapeoperaid = @escapeoperaid and isdeleted = 0 group by escapeoperaid) as de on A.InOperaId = de.escapeoperaid left join AbpEmployees as AE on A.InOperaId=AE.id ) as b where  RowNumber BETWEEN @beginSize and @endSize";
            //                Sqlparam.Add(new SqlParameter("@beginTime", input.begindt));
            //                Sqlparam.Add(new SqlParameter("@endTime", input.enddt));
            //                Sqlparam.Add(new SqlParameter("@beginTimeA", input.begindt));
            //                Sqlparam.Add(new SqlParameter("@endTimeB", input.enddt));
            //                Sqlparam.Add(new SqlParameter("@InOperaId", employeeId));
            //                Sqlparam.Add(new SqlParameter("@beginCarRepaymentTime", input.begindt));
            //                Sqlparam.Add(new SqlParameter("@endCarRepaymentTime", input.enddt));
            //                Sqlparam.Add(new SqlParameter("@EscapeTimebeginTime", input.EscapeTimebegindt));
            //                Sqlparam.Add(new SqlParameter("@EscapeTimeendTime", input.EscapeTimeenddt));
            //                Sqlparam.Add(new SqlParameter("@escapeoperaid", employeeId));
            //                Sqlparam.Add(new SqlParameter("@ChargeOperaId", employeeId));
            //                Sqlparam.Add(new SqlParameter("@beginSize", 1));
            //                Sqlparam.Add(new SqlParameter("@endSize", 1000));
            //                SqlParameter[] param = Sqlparam.ToArray();
            //                employee = Context.Database.SqlQuery<EmployeeChargesDto>(sqlstr, param);
            //            }
            //            else
            //            {
            //                sqlstr += @"group by InOperaId) AS A
            //                 full join (
            //                 select sum( case when PayStatus = 1 then FactReceive - Prepaid else 0 end) xj,
            //                 sum( case when PayStatus = 2 then FactReceive - Prepaid else 0 end) sk, sum(Money) as money, sum(Arrearage) as Arrearage, ChargeOperaId
            //                  from AbpBusinessDetail with(nolock) where CarpayTime >  @beginTimeC and CarpayTime < @endTimeD" + sqlwhere;
            //                if (input.CompanyId != null)
            //                {
            //                    sqlstr += @" and CompanyId = @CompanyId ";
            //                }
            //                sqlstr += @" and isdeleted = 0  group by ChargeOperaId
            //  ) AS B on A.InOperaId = B.ChargeOperaId full join(select sum(case when EscapePayStatus = 1 then Repayment else 0 end ) as SJ_Repayment,
            // sum(case when EscapePayStatus = 2 then Repayment else 0 end) as SK_Repayment, escapeoperaid from AbpBusinessDetail with(nolock) where
            //  CarRepaymentTime > @beginTime and isdeleted = 0 and CarRepaymentTime < @endTime and EscapeTime >= @EscapeTimebeginTime and EscapeTime <= @EscapeTimeendTime" + sqlwhere;
            //                if (input.CompanyId != null)
            //                {
            //                    sqlstr += @" and CompanyId = @CompanyId ";
            //                    Sqlparam.Add(new SqlParameter("@CompanyId", input.CompanyId));
            //                }
            //                sqlstr += @"  group by escapeoperaid) as de on A.InOperaId = de.escapeoperaid left join AbpEmployees as AE on A.InOperaId=AE.id  ) as b where  RowNumber BETWEEN @beginSize and @endSize";
            //                Sqlparam.Add(new SqlParameter("@beginTime", input.begindt));
            //                Sqlparam.Add(new SqlParameter("@endTime", input.enddt));
            //                Sqlparam.Add(new SqlParameter("@beginTimeC", input.begindt));
            //                Sqlparam.Add(new SqlParameter("@endTimeD", input.enddt));
            //                Sqlparam.Add(new SqlParameter("@EscapeTimebeginTime", input.EscapeTimebegindt));
            //                Sqlparam.Add(new SqlParameter("@EscapeTimeendTime", input.EscapeTimeenddt));
            //                Sqlparam.Add(new SqlParameter("@beginSize", 1));
            //                Sqlparam.Add(new SqlParameter("@endSize", 1000));
            //                SqlParameter[] param = Sqlparam.ToArray();
            //                employee = Context.Database.SqlQuery<EmployeeChargesDto>(sqlstr, param);
            //            }
            employee = Context.Database.SqlQuery<EmployeeChargesDto>(sqlstr, param);
            int count = employee.ToList().Count();
            return count;
        }

        /// <summary>
        /// 返回所查的收费员总数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int GetEmployeeCount(EmployeeChargeInput input)
        {

            List<SqlParameter> Sqlparam = new List<SqlParameter>();
            DbRawSqlQuery<EmployeeChargesDto> employee = null;

            string sqlwhere =string.IsNullOrWhiteSpace(input.BerthsecIds)?" and 1 =1 ": " and BerthsecId in (" + input.BerthsecIds + ")";
            if (input.TenantId.HasValue)
            {
                sqlwhere = " and TenantId = @TenantId ";
                Sqlparam.Add(new SqlParameter("@TenantId", input.TenantId.Value));
            }

            string sqlstr = @"select RowNumber,ChargeOperaName from
(
select ROW_NUMBER() OVER(Order by A.InOperaId DESC ) AS RowNumber,
case when isdeleted = 1 then AE.truename + '(该收费员已删除)' else AE.truename end as ChargeOperaName 
from (
    select sum( case when PrepaidPayStatus = 1 then Prepaid else 0 end) xj, 
    sum(case when PrepaidPayStatus = 2 then Prepaid else 0 end) sk, InOperaId from AbpBusinessDetail with(nolock) where 
     CarInTime > @beginTime and CarInTime < @endTime" + sqlwhere + "  and isdeleted = 0 ";
            if (input.CompanyId != null)
            {
                sqlstr += @" and CompanyId=@CompanyId ";
            }
            if (!string.IsNullOrWhiteSpace(input.employeeIdInput) && input.employeeIdInput != "0")
            {
                long employeeId = long.Parse(input.employeeIdInput);
                sqlstr += @"and InOperaId=@InOperaId  group by InOperaId) AS A
                 full join (
                 select sum( case when PayStatus = 1 then FactReceive - Prepaid else 0 end) xj,
                 sum( case when PayStatus = 2 then FactReceive - Prepaid else 0 end) sk, sum(Money) as money, sum(Arrearage) as Arrearage,
                    ChargeOperaId
                  from AbpBusinessDetail with(nolock) where
                  CarpayTime >  @beginTimeA and CarpayTime < @endTimeB" + sqlwhere;
                if (input.CompanyId.HasValue)
                {
                    sqlstr += @" and CompanyId=@CompanyId ";
                }
                sqlstr += @" and ChargeOperaId = @ChargeOperaId and isdeleted=0  group by ChargeOperaId
  ) AS B on A.InOperaId = B.ChargeOperaId 
full join(
select sum(case when EscapePayStatus=1 then Repayment else 0 end ) as SJ_Repayment,
 sum(case when EscapePayStatus=2 then Repayment else 0 end ) as SK_Repayment, 
escapeoperaid 
from AbpBusinessDetail 
where CarRepaymentTime > @beginCarRepaymentTime 
  and CarRepaymentTime < @endCarRepaymentTime 
  and EscapeTime >= @EscapeTimebeginTime 
  and EscapeTime <= @EscapeTimeendTime" + sqlwhere;
                if (input.CompanyId.HasValue)
                {
                    sqlstr += @" and CompanyId=@CompanyId ";
                    Sqlparam.Add(new SqlParameter("@CompanyId", input.CompanyId));
                }
                sqlstr += @"and escapeoperaid = @escapeoperaid 
                            and isdeleted = 0 
                            group by escapeoperaid) as de 
                        on A.InOperaId = de.escapeoperaid 
                        left join AbpEmployees as AE on A.InOperaId=AE.id ) as b where  RowNumber BETWEEN @beginSize and @endSize";
                Sqlparam.Add(new SqlParameter("@beginTime", input.begindt));
                Sqlparam.Add(new SqlParameter("@endTime", input.enddt));
                Sqlparam.Add(new SqlParameter("@beginTimeA", input.begindt));
                Sqlparam.Add(new SqlParameter("@endTimeB", input.enddt));
                Sqlparam.Add(new SqlParameter("@InOperaId", employeeId));
                Sqlparam.Add(new SqlParameter("@beginCarRepaymentTime", input.begindt));
                Sqlparam.Add(new SqlParameter("@endCarRepaymentTime", input.enddt));
                Sqlparam.Add(new SqlParameter("@EscapeTimebeginTime", input.EscapeTimebegindt));
                Sqlparam.Add(new SqlParameter("@EscapeTimeendTime", input.EscapeTimeenddt));
                Sqlparam.Add(new SqlParameter("@escapeoperaid", employeeId));
                Sqlparam.Add(new SqlParameter("@ChargeOperaId", employeeId));
                Sqlparam.Add(new SqlParameter("@beginSize", 1));
                Sqlparam.Add(new SqlParameter("@endSize", 1000));
                SqlParameter[] param = Sqlparam.ToArray();
                employee = Context.Database.SqlQuery<EmployeeChargesDto>(sqlstr, param);
            }
            else
            {
                sqlstr += @"group by InOperaId) AS A
                 full join (
                 select sum( case when PayStatus = 1 then FactReceive - Prepaid else 0 end) xj,
                 sum( case when PayStatus = 2 then FactReceive - Prepaid else 0 end) sk, sum(Money) as money, sum(Arrearage) as Arrearage, ChargeOperaId
                  from AbpBusinessDetail with(nolock) where CarpayTime >  @beginTimeC and CarpayTime < @endTimeD" + sqlwhere;
                if (input.CompanyId != null)
                {
                    sqlstr += @" and CompanyId = @CompanyId ";
                }
                sqlstr += @" and isdeleted = 0  group by ChargeOperaId
  ) AS B on A.InOperaId = B.ChargeOperaId full join(select sum(case when EscapePayStatus = 1 then Repayment else 0 end ) as SJ_Repayment,
 sum(case when EscapePayStatus = 2 then Repayment else 0 end) as SK_Repayment, escapeoperaid from AbpBusinessDetail with(nolock) where
  CarRepaymentTime > @beginTime and isdeleted = 0 and CarRepaymentTime < @endTime and EscapeTime >= @EscapeTimebeginTime and EscapeTime <= @EscapeTimeendTime" + sqlwhere;
                if (input.CompanyId != null)
                {
                    sqlstr += @" and CompanyId = @CompanyId ";
                    Sqlparam.Add(new SqlParameter("@CompanyId", input.CompanyId));
                }
                sqlstr += @"  group by escapeoperaid) as de on A.InOperaId = de.escapeoperaid left join AbpEmployees as AE on A.InOperaId=AE.id  ) as b where  RowNumber BETWEEN @beginSize and @endSize";
                Sqlparam.Add(new SqlParameter("@beginTime", input.begindt));
                Sqlparam.Add(new SqlParameter("@endTime", input.enddt));
                Sqlparam.Add(new SqlParameter("@beginTimeC", input.begindt));
                Sqlparam.Add(new SqlParameter("@endTimeD", input.enddt));
                Sqlparam.Add(new SqlParameter("@EscapeTimebeginTime", input.EscapeTimebegindt));
                Sqlparam.Add(new SqlParameter("@EscapeTimeendTime", input.EscapeTimeenddt));
                Sqlparam.Add(new SqlParameter("@beginSize", 1));
                Sqlparam.Add(new SqlParameter("@endSize", 1000));
                SqlParameter[] param = Sqlparam.ToArray();
                employee = Context.Database.SqlQuery<EmployeeChargesDto>(sqlstr, param);
            }
            int count = employee.ToList().Count();
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<EmployeeChargesDto> GetMoneyTotalGroupbyEmployeeEchars(EmployeeChargeInput input)
        {
            var temp = Table.Where(p => p.CarInTime >= input.begindt && p.CarInTime <= input.enddt && p.ChargeEmployee.TrueName != null);
            if (!string.IsNullOrWhiteSpace(input.employeeIdInput) && input.employeeIdInput != "0")
            {
                long employeeId = long.Parse(input.employeeIdInput);
                temp = temp.Where(p => p.ChargeOperaId == employeeId);
            }

            var expr = from p in temp
                       group p by p.ChargeOperaId
                           into g
                       select new EmployeeChargesDto
                       {
                           ChargeOperaName = Context.Set<Employees.Employee>().FirstOrDefault(e => e.Id == g.Key) == default(Employees.Employee) ? "" : Context.Set<Employees.Employee>().FirstOrDefault(e => e.Id == g.Key).TrueName,
                           SumFactReceive = g.Sum(p => p.FactReceive),
                           XJSumFactReceive = g.Where(p => p.PayStatus == 1).Sum(p => p.FactReceive),
                           SKSumFactReceive = g.Where(p => p.PayStatus == 2).Sum(p => p.FactReceive),
                           //XJSumFactReceive = 40,
                           //SKSumFactReceive = 60,
                           SumArrearage = g.Sum(p => p.Arrearage),
                           SumMoney = g.Sum(p => p.Money)
                       };
            return expr.ToList();
        }


        public List<EmployeeChargesDto> GetMoneyTotalGroupbyRateEchars(EmployeeChargeInput input)
        {
            List<SqlParameter> SqlParam = new List<SqlParameter>();
            DbRawSqlQuery<EmployeeChargesDto> employee = null;

            string sqlwhere = " and BerthsecId in (" + input.BerthsecIds + ")";
            if (input.TenantId.HasValue)
            {
                sqlwhere = " and TenantId = @TenantId ";
                SqlParam.Add(new SqlParameter("@TenantId", input.TenantId.Value));
            }
            string sqlstr = @"select CreationTime Date,FactReceive SumMoney,Arrearage SumArrearage,
                                businessCount CarInCount,
                                sensorCount CarOutCount
	                            from (
			                            select A.CreationTime ,FactReceive,Arrearage,case when  businessCount is null then 0 else businessCount end businessCount,
                                        case when  sensorCount is null then 0 else sensorCount end sensorCount,SJ_Repayment XJSumRepayment,SK_Repayment SKSumRepayment,
                                        WX_Repayment WXSumRepayment ,ROW_NUMBER() OVER(Order by A.CreationTime DESC) AS RowNumber
			                            from(
					                            select sum(FactReceive) FactReceive,sum(Arrearage) Arrearage,COUNT(1) businessCount ,
					                            convert(varchar(10),bus.CreationTime,120)CreationTime
					                            from AbpBusinessDetail  bus
					                            where convert(datetime,CreationTime) between @beginTime and @endTime and TenantId = 1 and CompanyId = 1 and isdeleted = 0  and CreationTime is not null 
					                            group by convert(varchar(10),CreationTime,120)
			                            ) A
			                            full join (
					                            select COUNT(1) sensorCount ,convert(varchar(10),CreationTime,120) CreationTime 
					                            from abpsensorbusinessdetail   

					                            where convert(datetime,CreationTime) between @beginTime and @endTime and TenantId = 1 and CompanyId = 1  and CreationTime is not null 
					                            group by convert(varchar(10),CreationTime,120)
				                            )as B on B.CreationTime = A.CreationTime
			                             full join(
					                            select 
					                            sum(case when EscapePayStatus=1 then Repayment else 0 end ) as SJ_Repayment,
					                            sum(case when EscapePayStatus = 4 then Repayment else 0 end) as SK_Repayment,
					                            sum(case when EscapePayStatus = 3 then Repayment else 0 end) as WX_Repayment,convert(varchar(10),CreationTime,120) CreationTime
					                            from AbpBusinessDetail with(nolock) 
					                            where convert(datetime,CreationTime) between @beginTime and @endTime and isdeleted = 0  
					                            and TenantId = 1  and CompanyId = 1  and CreationTime is not null 
					                            group by convert(varchar(10),CreationTime,120)
					                            )as de on A.CreationTime = de.CreationTime 
	                            ) temp  where CreationTime is not null and RowNumber BETWEEN @beginSize and @endSize  ";

            SqlParam.Add(new SqlParameter("@beginTime", input.begindt));
            SqlParam.Add(new SqlParameter("@endTime", input.enddt));
            SqlParam.Add(new SqlParameter("@beginSize", input.page * input.rows - input.rows + 1));
            SqlParam.Add(new SqlParameter("@endSize", input.page * input.rows));
            SqlParameter[] param = SqlParam.ToArray();
            employee = Context.Database.SqlQuery<EmployeeChargesDto>(sqlstr, param);
            return employee.ToList();
        }


        /// <summary>
        /// 泊位段今日金额
        /// </summary>
        /// <param name="BerthsecId"></param>
        /// <returns></returns>
        public BerthsecStatisticsDto GetBerthsecStatistics(int CompanyId, int TenantId, int BerthsecId)
        {
            BerthsecStatisticsDto model = new BerthsecStatisticsDto();
            var time = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@CompanyId", CompanyId));
            sqlParameters.Add(new SqlParameter("@TenantId", TenantId));
            sqlParameters.Add(new SqlParameter("@BerthsecId", BerthsecId));
            sqlParameters.Add(new SqlParameter("@time", time));
            var sqlStr = @"select top 6 AbpBerthsecs.ID as BerthsecId,BerthsecName,SUM(FactReceive)+SUM(Repayment) ToatalMoney,sum(Money)Money,SUM(FactReceive)FaceMoney,SUM(Repayment)Repayment from(select (case when CarOutTime >= @time then OutOperaId when CarRepaymentTime >= @time then EscapeOperaId else 0 end) EmployeeId,(case when CarOutTime >= @time then FactReceive else 0 end)FactReceive,(case when CarRepaymentTime >= @time then Repayment else 0 end)Repayment,(case when CarOutTime >= @time then Money else 0 end)Money from AbpBusinessDetail where (CarOutTime >= @time or CarRepaymentTime >= @time) and CompanyId = @CompanyId and TenantId = @TenantId and IsDeleted = 0) as V left join AbpEmployees on V.EmployeeId = AbpEmployees.Id left join AbpBerthsecs on AbpBerthsecs.Id = BerthsecId where BerthsecId =@BerthsecId group by AbpBerthsecs.ID,BerthsecName";

            model = Context.Database.SqlQuery<BerthsecStatisticsDto>(sqlStr, sqlParameters.ToArray()).FirstOrDefault();
            return model;
        }

        /// <summary>
        /// 泊位段收入List
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public List<BerthsecStatisticsDto> GetBerthsecStatisticsList(int[] berthsecsid, int[] CompanyId, int? TenantId)
        {
            List<BerthsecStatisticsDto> list = new List<BerthsecStatisticsDto>();
            var time = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            string sqlwhere = "  and CompanyId in (" + string.Join(",", CompanyId) + ") and Berthsecid in (" + string.Join(",", berthsecsid) + ")";
            if (TenantId.HasValue)
            {
                sqlParameters.Add(new SqlParameter("@TenantId", TenantId));
                sqlwhere += " and TenantId = @TenantId";
            }

            sqlParameters.Add(new SqlParameter("@time", time));
            var sqlStr = @"select top 6 AbpBerthsecs.ID as BerthsecId,BerthsecName,SUM(FactReceive)+SUM(Repayment) ToatalMoney, sum(Money) Money,SUM(FactReceive) FaceMoney,SUM(Repayment)Repayment from(select BerthsecId, (case when CarOutTime >= @time then OutOperaId when CarRepaymentTime >= @time then EscapeOperaId else 0 end) EmployeeId,(case when CarOutTime >= @time then FactReceive else 0 end)FactReceive, (case when CarRepaymentTime >= @time then Repayment else 0 end)Repayment, (case when CarOutTime >= @time then Money else 0 end) Money from AbpBusinessDetail with(nolock) where (CarOutTime >= @time or CarRepaymentTime >= @time) " + sqlwhere + " and IsDeleted = 0) as V left join AbpEmployees on V.EmployeeId = AbpEmployees.Id left join AbpBerthsecs on AbpBerthsecs.Id = V.BerthsecId where BerthsecName is not null group by AbpBerthsecs.ID, BerthsecName";

            list = Context.Database.SqlQuery<BerthsecStatisticsDto>(sqlStr, sqlParameters.ToArray()).ToList();
            return list;

        }

        /// <summary>
        /// 收费员今日收入
        /// </summary>
        /// <returns></returns>
        public List<EmployeeFactReceiveDto> GetEmployeeFactReceiveList(int[] berthsecid ,int[] CompanyId, int? TenantId)
        {
            List<EmployeeFactReceiveDto> list = new List<EmployeeFactReceiveDto>();
            var time = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            
            string sqlwhere = " and CompanyId in (" + string.Join(",", CompanyId) + ") and BerthsecId in (" + string.Join(",", berthsecid) + ")";
            if (TenantId.HasValue)
            {
                sqlParameters.Add(new SqlParameter("@TenantId", TenantId));
                sqlwhere = " and TenantId = @TenantId ";
            }
            sqlParameters.Add(new SqlParameter("@time", time));

            var sqlStr = @"select top 6 EmployeeId,TrueName as EmployName,SUM(FactReceive)FactReceive,SUM(Repayment)Repayment from(select (case when CarOutTime >= @time then OutOperaId when CarRepaymentTime >= @time then EscapeOperaId else 0 end) EmployeeId,(case when CarOutTime >= @time then FactReceive else 0 end)FactReceive,(case when CarRepaymentTime >= @time then Repayment else 0 end)Repayment from AbpBusinessDetail where (CarOutTime >= @time or CarRepaymentTime >= @time) " + sqlwhere + " and IsDeleted = 0) as V left join AbpEmployees on V.EmployeeId = AbpEmployees.Id where EmployeeId is not null group by EmployeeId,TrueName order by EmployeeId";
            list = Context.Database.SqlQuery<EmployeeFactReceiveDto>(sqlStr, sqlParameters.ToArray()).ToList();
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public void RestoreDelete(long Id)
        {
            Context.Database.ExecuteSqlCommand("Update AbpBusinessDetail set IsDeleted = 1 where Id = @Id ", new SqlParameter[] { new SqlParameter("@Id", Id) });
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public BusinessDetailUserData GetBusinessDetailUserD(GetBusinessDetailsInput input)
        {
            BusinessDetailUserData bdud = new BusinessDetailUserData();
            IQueryable<BusinessDetail> temp = Table;
            if (input.filters == null)//自定义查询
            {
                temp = Table.Where(p => p.CarPayTime >= input.begindt && p.CarPayTime <= input.enddt)
                .WhereIf(!string.IsNullOrWhiteSpace(input.RegionId.ToString()) && input.RegionId.ToString() != "0", p => p.RegionId == input.RegionId)
                .WhereIf(!string.IsNullOrWhiteSpace(input.ParkId.ToString()) && input.ParkId.ToString() != "0", p => p.ParkId == input.ParkId)
                .WhereIf(!string.IsNullOrWhiteSpace(input.BerthsecId.ToString()) && input.BerthsecId.ToString() != "0", p => p.BerthsecId == input.BerthsecId)
                .WhereIf(!string.IsNullOrWhiteSpace(input.BerthNumber), p => p.BerthNumber == input.BerthNumber)
                .WhereIf(!string.IsNullOrWhiteSpace(input.PlateNumber), p => p.PlateNumber == input.PlateNumber);
            }
            var query = temp.Select(entity => new BusinessDetailsDto()
            {
                GroupUser = 1,
                Id = entity.Id,
                Arrearage = entity.Arrearage,
                BerthNumber = entity.BerthNumber,
                PlateNumber = entity.PlateNumber,
                RegionId = entity.RegionId,
                ParkId = entity.ParkId,
                BerthsecId = entity.BerthsecId,
                CarType = entity.CarType,
                Prepaid = entity.Prepaid,
                Receivable = entity.Receivable,
                CarInTime = entity.CarInTime,
                CarOutTime = entity.CarOutTime,
                CarPayTime = entity.CarPayTime,
                guid = entity.guid,
                InEmployeeName = entity.InEmployee.TrueName,
                InDeviceCode = entity.InDeviceCode,
                OutEmployeeName = entity.OutEmployee.TrueName,
                OutDeviceCode = entity.OutDeviceCode,
                ChargeEmployeeName = entity.ChargeEmployee.TrueName,
                ChargeDeviceCode = entity.ChargeDeviceCode,
                FactReceive = entity.FactReceive,
                ParkName = entity.Park.ParkName,
                Status = entity.Status,
                BerthsecName = entity.Berthsec.BerthsecName,
                RegionName = Context.Set<Regions.Region>().FirstOrDefault(e => e.Id == entity.RegionId).RegionName,
                StopTime = entity.StopTime,
                Tenant = entity.Tenant.Name,
                StopType = entity.StopType,
                PayStatus = entity.PayStatus,
                SensorsInCarTime = entity.SensorsInCarTime,
                SensorsOutCarTime = entity.SensorsOutCarTime,
                SensorsStopTime = entity.SensorsStopTime,
                SensorsReceivable = entity.SensorsReceivable
            }).Filters(input);

            var UserData = from a in query
                           group a by a.GroupUser into g
                           select new
                           {
                               Prepaid = g.Sum(entity => entity.Prepaid),
                               Receivable = g.Sum(entity => entity.Receivable),
                               FactReceive = g.Sum(entity => entity.FactReceive),
                               Arrearage = g.Sum(entity => entity.Arrearage)
                           };
            var userDataList = UserData.ToList();
            bdud.PlateNumber = "金额汇总";
            if (userDataList.Count() > 0)
            {
                bdud.Prepaid = userDataList[0].Prepaid;
                bdud.Receivable = userDataList[0].Receivable;
                bdud.FactReceive = userDataList[0].FactReceive;
                bdud.Arrearage = userDataList[0].Arrearage;
            }
            return bdud;
        }

        /// <summary>
        /// 收费明细  linq
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetBusinessDetailsOutput GetBusinessDetailsList(GetBusinessDetailsInput input)
        {

            //页面数据空掉的问题  可以试试使用sql语句
            IQueryable<BusinessDetail> temp = Table;
            if (input.filters == null)//自定义查询
            {
                temp = Table.Where(p => p.CarPayTime >= input.begindt && p.CarPayTime <= input.enddt)
                .WhereIf(!string.IsNullOrWhiteSpace(input.RegionId.ToString()) && input.RegionId.ToString() != "0", p => p.RegionId == input.RegionId)
                .WhereIf(!string.IsNullOrWhiteSpace(input.ParkId.ToString()) && input.ParkId.ToString() != "0", p => p.ParkId == input.ParkId)
                .WhereIf(!string.IsNullOrWhiteSpace(input.BerthsecId.ToString()) && input.BerthsecId.ToString() != "0", p => p.BerthsecId == input.BerthsecId)
                .WhereIf(!string.IsNullOrWhiteSpace(input.BerthNumber), p => p.BerthNumber == input.BerthNumber)
                .WhereIf(!string.IsNullOrWhiteSpace(input.StopType.ToString()) && input.StopType.ToString() != "0", p => p.StopType == input.StopType)
                .WhereIf(!string.IsNullOrWhiteSpace(input.PayStatus.ToString()) && input.PayStatus.ToString() != "0", p => p.PayStatus == input.PayStatus)
                .WhereIf(!string.IsNullOrWhiteSpace(input.PlateNumber), p => p.PlateNumber == input.PlateNumber);

            }
            var query = temp.Select(entity => new BusinessDetailsDto()
            {
                Id = entity.Id,
                Arrearage = entity.Arrearage,
                BerthNumber = entity.BerthNumber,
                PlateNumber = entity.PlateNumber,
                RegionId = entity.RegionId,
                ParkId = entity.ParkId,
                BerthsecId = entity.BerthsecId,
                CarType = entity.CarType,
                Prepaid = entity.Prepaid,
                Receivable = entity.Receivable,
                CarInTime = entity.CarInTime,
                CarOutTime = entity.CarOutTime,
                CarPayTime = entity.CarPayTime,
                guid = entity.guid,
                InEmployeeName = entity.InEmployee.TrueName,
                InDeviceCode = entity.InDeviceCode,
                OutEmployeeName = entity.OutEmployee.TrueName,
                OutDeviceCode = entity.OutDeviceCode,
                ChargeEmployeeName = entity.ChargeEmployee.TrueName,
                ChargeDeviceCode = entity.ChargeDeviceCode,
                FactReceive = entity.FactReceive,
                ParkName = entity.Park.ParkName,
                Status = entity.Status,
                BerthsecName = entity.Berthsec.BerthsecName,
                RegionName = Context.Set<Regions.Region>().FirstOrDefault(e => e.Id == entity.RegionId).RegionName,
                StopTime = entity.StopTime,
                Tenant = entity.Tenant.Name,
                StopType = entity.StopType,
                PayStatus = entity.PayStatus,
                SensorsInCarTime = entity.SensorsInCarTime,
                SensorsOutCarTime = entity.SensorsOutCarTime,
                SensorsStopTime = entity.SensorsStopTime,
                SensorsReceivable = entity.SensorsReceivable
            }).Filters(input);
            int records = query.Count();
            BusinessDetailUserData bdud = new BusinessDetailUserData();
            bdud = GetBusinessDetailUserD(input);
            return new GetBusinessDetailsOutput()
            {
                rows = query.Orders(input).PageBy(input).ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0),
                userdata = bdud
            };
        }



        public GetBusinessDetailsOutput GetGetEmployeeBatchNoDynamicReportSql(GetEmployeeBatchNoDynamicReportInput input)
        {
            List<SqlParameter> SqlParam = new List<SqlParameter>();
            DbRawSqlQuery<BusinessDetailsDto> BusinessTable = null;
            SqlParam.Add(new SqlParameter("@BatchNo", input.BatchNo));
            SqlParam.Add(new SqlParameter("@TenantId", input.TenantId));
            SqlParam.Add(new SqlParameter("@CompanyId", input.CompanyId));
            SqlParam.Add(new SqlParameter("@BeginSize", input.page * input.rows - input.rows + 1));
            SqlParam.Add(new SqlParameter("@EndSize", input.page * input.rows));
            #region Sql语句
            string StrSql = @"select B.InBatchNo,B.OutBatchNo,B.PaymentBatchNo,
B.Id,B.Arrearage,B.BerthNumber,B.PlateNumber, B.RegionId, B.ParkId, B.BerthsecId, AbpOperatorsCompany.CompanyName,B.CarType, B.Prepaid, B.Receivable, B.CarInTime, B.Money,B.CarOutTime, B.CarPayTime,B.guid, EA.TrueName as InEmployeeName,B.InDeviceCode,EB.TrueName as OutEmployeeName, B.OutDeviceCode, EC.TrueName as ChargeEmployeeName, B.ChargeDeviceCode,B.FactReceive,P.ParkName as ParkName,B.Status, Berthsecs.berthsecname as BerthsecName, R.regionname as RegionName,B.StopTime,T.Name as Tenant,B.StopType,B.PayStatus,B.SensorsInCarTime,B.SensorsOutCarTime,B.SensorsStopTime,B.SensorsReceivable, B.RowNumber from( select *,(ROW_NUMBER() OVER(Order by Id DESC )) AS RowNumber from AbpBusinessDetail where IsDeleted = 0 and CompanyId = @CompanyId and TenantId = @TenantId  and (InBatchNo = @BatchNo or OutBatchNo=@BatchNo or (PaymentBatchNo=@BatchNo and status =4)))as B left join AbpOperatorsCompany on B.companyid = AbpOperatorsCompany.id left join AbpRegions as R on B.regionid = R.id left join AbpEmployees as EA on B.InOperaId = EA.id left join AbpEmployees as EB on B.OutOperaId=EB.id left join AbpEmployees as EC on B.ChargeOperaId = EC.id left join AbpParks as P on B.parkid = P.id left join AbpBerthsecs as Berthsecs on B.berthsecid = Berthsecs.id left join AbpTenants as T on B.tenantid = T.id where RowNumber between @BeginSize and @EndSize";
            #endregion

            SqlParameter[] param = SqlParam.ToArray();
            BusinessTable = Context.Database.SqlQuery<BusinessDetailsDto>(StrSql, param);
            var rows = BusinessTable.ToList();
            int records = GetCount(input);
            return new GetBusinessDetailsOutput()
            {
                rows = rows,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0),
            };
        }


        public int GetCount(GetEmployeeBatchNoDynamicReportInput input)
        {
            List<SqlParameter> SqlParam = new List<SqlParameter>();
            SqlParam.Add(new SqlParameter("@BatchNo", input.BatchNo));
            SqlParam.Add(new SqlParameter("@TenantId", input.TenantId));
            SqlParam.Add(new SqlParameter("@CompanyId", input.CompanyId));
            string StrSql = @"select top 1 (ROW_NUMBER() OVER(Order by Id DESC )) AS RowNumber from AbpBusinessDetail where IsDeleted = 0 and CompanyId = @CompanyId and TenantId = @TenantId  and (InBatchNo = @BatchNo or OutBatchNo=@BatchNo or (PaymentBatchNo=@BatchNo and status =4)) order by RowNumber desc";
            SqlParameter[] param = SqlParam.ToArray();
            var BusinessTable = Context.Database.SqlQuery<Int64>(StrSql, param).ToList();
            var count = 0;
            if (BusinessTable.Count > 0)
                count = Convert.ToInt32(BusinessTable[0]);
            return count;
        }

        /// <summary>
        /// 查询 收费员明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetBusinessDetailsOutput GetBusinessDetailsListSql(GetBusinessDetailsInput input)
        {
            List<SqlParameter> SqlParam = new List<SqlParameter>();
            DbRawSqlQuery<BusinessDetailsDto> BusinessTable = null;
            IQueryable<BusinessDetail> temp = Table;
            if (input.filters == null)//自定义查询
            {
                string StrSql = @"
                select B.Id,B.Arrearage,B.BerthNumber,B.PlateNumber, B.OcrPlateNumber, B.RegionId, B.ParkId, B.BerthsecId, AbpOperatorsCompany.CompanyName,
                B.CarType, B.Prepaid, B.Receivable, B.CarInTime, B.Money,
                B.CarOutTime, B.CarPayTime,B.guid, EA.TrueName as InEmployeeName,
                B.InDeviceCode,
                EB.TrueName as OutEmployeeName,
                B.OutDeviceCode,
                EC.TrueName as ChargeEmployeeName,
                B.ChargeDeviceCode,
                B.FactReceive,
                P.ParkName as ParkName,
                B.Status,
                Berthsecs.berthsecname as BerthsecName,
                R.regionname as RegionName,
                B.StopTime,
                AbpDictionaryValue.ValueData as ParkType,
                T.Name as Tenant,
                B.StopType,case when B.Status =4 then B.EscapePayStatus else B.PayStatus end PayStatus,
                sens.CarInTime SensorsInCarTime,sens.CarOutTime SensorsOutCarTime,
                sens.StopTime SensorsStopTime,sens.Receivable SensorsReceivable, B.RowNumber from (
select *,ROW_NUMBER() OVER(Order by Id DESC ) AS RowNumber from AbpBusinessDetail with(nolock) where ";
                StrSql += @" companyid in (" + string.Join(",", input.CompanyIds) + ")";
                //SqlParam.Add(new SqlParameter("@companyid", string.Join(",", input.CompanyIds)));

                if (input.TenantId.HasValue)
                {
                    StrSql += " and tenantid = @tenantid ";
                    SqlParam.Add(new SqlParameter("@tenantid", input.TenantId));
                }
                StrSql += @" and (carpaytime between @BeginTime and @EndTime) and isdeleted = 0 ";
                SqlParam.Add(new SqlParameter("@BeginTime", input.begindt));
                SqlParam.Add(new SqlParameter("@EndTime", input.enddt));
                if (!string.IsNullOrWhiteSpace(input.RegionId.ToString()) && input.RegionId.ToString() != "0")
                {
                    StrSql += @" and RegionId = @RegionId ";
                    SqlParam.Add(new SqlParameter("@RegionId", input.RegionId));
                }
                if (!string.IsNullOrWhiteSpace(input.ParkId.ToString()) && input.ParkId.ToString() != "0")
                {
                    StrSql += @" and ParkId = @ParkId ";
                    SqlParam.Add(new SqlParameter("@ParkId", input.ParkId));
                }
                if (!string.IsNullOrWhiteSpace(input.BerthsecId.ToString()) && input.BerthsecId.ToString() != "0")
                {
                    StrSql += @" and BerthsecId = @BerthsecId ";
                    SqlParam.Add(new SqlParameter("@BerthsecId", input.BerthsecId));
                }
                if (!string.IsNullOrWhiteSpace(input.EmployeeId.ToString()) && input.EmployeeId.ToString() != "0")
                {
                    StrSql += @" and InOperaId = @InOperaId ";
                    SqlParam.Add(new SqlParameter("@InOperaId", input.EmployeeId));
                }
                if (!string.IsNullOrWhiteSpace(input.BerthNumber))
                {
                    StrSql += @" and berthnumber = @berthnumber ";
                    SqlParam.Add(new SqlParameter("@berthnumber", input.BerthNumber));
                }
                if (!string.IsNullOrWhiteSpace(input.PlateNumber))
                {
                    StrSql += @"  and platenumber like '%" + input.PlateNumber + "%' ";
                }

                if (!string.IsNullOrWhiteSpace(input.Guid))
                {
                    StrSql += @" and guid = @guid ";
                    SqlParam.Add(new SqlParameter("@guid", input.Guid));
                }
                if (!string.IsNullOrWhiteSpace(input.StopType.ToString()) && input.StopType.ToString() != "0")
                {
                    StrSql += @" and StopType = @StopType ";
                    SqlParam.Add(new SqlParameter("@StopType", input.StopType));
                }
                if (!string.IsNullOrWhiteSpace(input.PayStatus.ToString()) && input.PayStatus.ToString() != "0")
                {
                    StrSql += @" and PayStatus = @PayStatus ";
                    SqlParam.Add(new SqlParameter("@PayStatus", input.PayStatus));
                }


                StrSql += @" ) as B 
               left join abpsensorbusinessdetail sens on B.guid = sens.guid
               left join AbpOperatorsCompany on B.companyid = AbpOperatorsCompany.id 
               left join AbpRegions as R on B.regionid = R.id 
               left join AbpEmployees as EA on B.InOperaId = EA.id 
               left join AbpEmployees as EB on B.OutOperaId=EB.id 
               left join AbpEmployees as EC on B.ChargeOperaId = EC.id 
               left join AbpParks as P on B.parkid = P.id 
               left join AbpBerthsecs as Berthsecs on B.berthsecid = Berthsecs.id 
               left join AbpTenants as T on B.tenantid = T.id 
               left join AbpDictionaryValue on TypeCode = 'A10013' and P.ParkType = AbpDictionaryValue.ValueCode and AbpDictionaryValue.TenantId = B.tenantid
               where RowNumber between @BeginSize and @EndSize 
                order by B.CarInTime desc";
                SqlParam.Add(new SqlParameter("@BeginSize", input.page * input.rows - input.rows + 1));
                SqlParam.Add(new SqlParameter("@EndSize", input.page * input.rows));
                SqlParameter[] param = SqlParam.ToArray();
                BusinessTable = Context.Database.SqlQuery<BusinessDetailsDto>(StrSql, param);
                var rows = BusinessTable.ToList();
                List<BusinessDetailsDto> BBBB = GetBusinessDCount(input);
                int records = 0;
                BusinessDetailUserData bdud = new BusinessDetailUserData();
                bdud.PlateNumber = "金额汇总";
                if (BBBB.Count > 0)
                {
                    records = BBBB[0].BusinessCount;
                    bdud.Prepaid = BBBB[0].Prepaid;
                    bdud.Receivable = BBBB[0].Receivable;
                    bdud.FactReceive = BBBB[0].FactReceive;
                    bdud.Arrearage = BBBB[0].Arrearage;
                    bdud.Money = BBBB[0].Money;
                }
                return new GetBusinessDetailsOutput()
                {
                    rows = rows,
                    records = records,
                    total = records / input.rows + (records % input.rows != 0 ? 1 : 0),
                    userdata = bdud
                };
            }
            else
            {
                var query = temp.Select(entity => new BusinessDetailsDto()
                {
                    Id = entity.Id,
                    Arrearage = entity.Arrearage,
                    BerthNumber = entity.BerthNumber,
                    PlateNumber = entity.PlateNumber,
                    RegionId = entity.RegionId,
                    ParkId = entity.ParkId,
                    BerthsecId = entity.BerthsecId,
                    CarType = entity.CarType,
                    Prepaid = entity.Prepaid,
                    Receivable = entity.Receivable,
                    CarInTime = entity.CarInTime,
                    CarOutTime = entity.CarOutTime,
                    CarPayTime = entity.CarPayTime,
                    guid = entity.guid,
                    InEmployeeName = entity.InEmployee.TrueName,
                    InDeviceCode = entity.InDeviceCode,
                    OutEmployeeName = entity.OutEmployee.TrueName,
                    OutDeviceCode = entity.OutDeviceCode,
                    ChargeEmployeeName = entity.ChargeEmployee.TrueName,
                    ChargeDeviceCode = entity.ChargeDeviceCode,
                    FactReceive = entity.FactReceive,
                    ParkName = entity.Park.ParkName,
                    Status = entity.Status,
                    Money = entity.Money,
                    BerthsecName = entity.Berthsec.BerthsecName,
                    RegionName = Context.Set<Regions.Region>().FirstOrDefault(e => e.Id == entity.RegionId).RegionName,
                    StopTime = entity.StopTime,
                    Tenant = entity.Tenant.Name,
                    StopType = entity.StopType,
                    PayStatus = entity.PayStatus,
                    SensorsInCarTime = Context.Set<SensorBusinessDetail>().FirstOrDefault(e => e.guid == entity.guid).CarInTime,
                    SensorsOutCarTime = Context.Set<SensorBusinessDetail>().FirstOrDefault(e => e.guid == entity.guid).CarOutTime,
                    SensorsStopTime = Context.Set<SensorBusinessDetail>().FirstOrDefault(e => e.guid == entity.guid).StopTime,
                    SensorsReceivable = Context.Set<SensorBusinessDetail>().FirstOrDefault(e => e.guid == entity.guid).Receivable,
                }).Filters(input);
                int records = query.Count();
                BusinessDetailUserData bdud = new BusinessDetailUserData();
                bdud = GetBusinessDetailUserD(input);
                return new GetBusinessDetailsOutput()
                {
                    rows = query.Orders(input).PageBy(input).ToList(),
                    records = records,
                    total = records / input.rows + (records % input.rows != 0 ? 1 : 0),
                    userdata = bdud
                };
            }
        }

        public List<BusinessDetailsDto> GetSubstantialOrderCount(GetBusinessDetailsInput input)
        {
            List<SqlParameter> SqlParam = new List<SqlParameter>();
            string StrSql = @"select sum(B.Arrearage) as Arrearage,
                sum(B.Prepaid) as Prepaid,
                sum(B.Receivable) as Receivable,
                sum(B.FactReceive) as FactReceive,
                sum(B.Money) as Money,
                count(B.useGroup) as BusinessCount from (
select *,1 AS useGroup from AbpBusinessDetail where ";

            StrSql += @" companyid in (" + string.Join(",", input.CompanyIds) + ") and money > " + P4.P4Consts.SubstantialOrder;

            if (input.TenantId.HasValue)
            {
                StrSql += " and tenantid=@tenantid ";
                SqlParam.Add(new SqlParameter("@tenantid", input.TenantId));
            }

            StrSql += @" and carpaytime>@BeginTime and carpaytime<@EndTime
and isdeleted=0 ";
            SqlParam.Add(new SqlParameter("@BeginTime", input.begindt));
            SqlParam.Add(new SqlParameter("@EndTime", input.enddt));
            if (!string.IsNullOrWhiteSpace(input.RegionId.ToString()) && input.RegionId.ToString() != "0")
            {
                StrSql += @" and RegionId=@RegionId ";
                SqlParam.Add(new SqlParameter("@RegionId", input.RegionId));
            }
            if (!string.IsNullOrWhiteSpace(input.ParkId.ToString()) && input.ParkId.ToString() != "0")
            {
                StrSql += @" and ParkId=@ParkId ";
                SqlParam.Add(new SqlParameter("@ParkId", input.ParkId));
            }
            if (!string.IsNullOrWhiteSpace(input.BerthsecId.ToString()) && input.BerthsecId.ToString() != "0")
            {
                StrSql += @" and BerthsecId=@BerthsecId ";
                SqlParam.Add(new SqlParameter("@BerthsecId", input.BerthsecId));
            }
            if (!string.IsNullOrWhiteSpace(input.EmployeeId.ToString()) && input.EmployeeId.ToString() != "0")
            {
                StrSql += @" and InOperaId=@InOperaId ";
                SqlParam.Add(new SqlParameter("@InOperaId", input.EmployeeId));
            }
            if (!string.IsNullOrWhiteSpace(input.BerthNumber))
            {
                StrSql += @" and berthnumber=@berthnumber ";
                SqlParam.Add(new SqlParameter("@berthnumber", input.BerthNumber));
            }
            if (!string.IsNullOrWhiteSpace(input.PlateNumber))
            {
                StrSql += @" and platenumber=@platenumber ";
                SqlParam.Add(new SqlParameter("@platenumber", input.PlateNumber));
            }
            StrSql += @" ) as B group by useGroup";
            SqlParameter[] param = SqlParam.ToArray();
            var BusinessTable = Context.Database.SqlQuery<BusinessDetailsDto>(StrSql, param).ToList();
            return BusinessTable;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<BusinessDetailsDto> GetBusinessDCount(GetBusinessDetailsInput input)
        {
            List<SqlParameter> SqlParam = new List<SqlParameter>();
            string StrSql = @"select sum(B.Arrearage) as Arrearage,
                sum(B.Prepaid) as Prepaid,
                sum(B.Receivable) as Receivable,
                sum(B.FactReceive) as FactReceive,
                sum(B.Money) as Money,
                count(B.useGroup) as BusinessCount from (
select *,1 AS useGroup from AbpBusinessDetail with(nolock) where ";

            StrSql += @" companyid in (" + string.Join(",", input.CompanyIds) + ")"; // and money > " + P4.P4Consts.SubstantialOrder;

            if (input.TenantId.HasValue)
            {
                StrSql += " and tenantid = @tenantid ";
                SqlParam.Add(new SqlParameter("@tenantid", input.TenantId));
            }

            StrSql += @" and carpaytime between @BeginTime and @EndTime and isdeleted = 0 ";
            SqlParam.Add(new SqlParameter("@BeginTime", input.begindt));
            SqlParam.Add(new SqlParameter("@EndTime", input.enddt));
            if (!string.IsNullOrWhiteSpace(input.RegionId.ToString()) && input.RegionId.ToString() != "0")
            {
                StrSql += @" and RegionId = @RegionId ";
                SqlParam.Add(new SqlParameter("@RegionId", input.RegionId));
            }
            if (!string.IsNullOrWhiteSpace(input.ParkId.ToString()) && input.ParkId.ToString() != "0")
            {
                StrSql += @" and ParkId = @ParkId ";
                SqlParam.Add(new SqlParameter("@ParkId", input.ParkId));
            }
            if (!string.IsNullOrWhiteSpace(input.BerthsecId.ToString()) && input.BerthsecId.ToString() != "0")
            {
                StrSql += @" and BerthsecId = @BerthsecId ";
                SqlParam.Add(new SqlParameter("@BerthsecId", input.BerthsecId));
            }
            if (!string.IsNullOrWhiteSpace(input.EmployeeId.ToString()) && input.EmployeeId.ToString() != "0")
            {
                StrSql += @" and InOperaId = @InOperaId ";
                SqlParam.Add(new SqlParameter("@InOperaId", input.EmployeeId));
            }
            if (!string.IsNullOrWhiteSpace(input.BerthNumber))
            {
                StrSql += @" and berthnumber = @berthnumber ";
                SqlParam.Add(new SqlParameter("@berthnumber", input.BerthNumber));
            }
            if (!string.IsNullOrWhiteSpace(input.PlateNumber))
            {
                StrSql += @"  and platenumber like '%" + input.PlateNumber + "%' ";

                //StrSql += @" and platenumber = @platenumber ";
                //SqlParam.Add(new SqlParameter("@platenumber", input.PlateNumber));
            }

            if (!string.IsNullOrWhiteSpace(input.Guid))
            {
                StrSql += @" and guid = @guid ";
                SqlParam.Add(new SqlParameter("@guid", input.Guid));
            }

            if (!string.IsNullOrWhiteSpace(input.StopType.ToString()) && input.StopType.ToString() != "0")
            {
                StrSql += @" and StopType = @StopType ";
                SqlParam.Add(new SqlParameter("@StopType", input.StopType));
            }
            if (!string.IsNullOrWhiteSpace(input.PayStatus.ToString()) && input.PayStatus.ToString() != "0")
            {
                StrSql += @" and PayStatus = @PayStatus ";
                SqlParam.Add(new SqlParameter("@PayStatus", input.PayStatus));
            }

            StrSql += @" ) as B group by useGroup";
            SqlParameter[] param = SqlParam.ToArray();
            var BusinessTable = Context.Database.SqlQuery<BusinessDetailsDto>(StrSql, param).ToList();
            return BusinessTable;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<EscapeDetailsDto> GetEscapedetailsCountSql(GetEscapeDetailsInput input)
        {
            List<SqlParameter> SqlParam = new List<SqlParameter>();
            string StrSql = @"select count(groupUser) as RegionId,  sum(Money) as Money, sum(Prepaid) as Prepaid, sum(Receivable) as Receivable,sum(Arrearage) as Arrearage,sum(isnull(Repayment,0)) as Repayment from(
select 1 as groupUser ,* from AbpBusinessDetail where status in (3, 4, 5) and isdeleted = 0 ";

            if (input.TenantId.HasValue)
            {
                StrSql += " and tenantid = @tenantid";
                SqlParam.Add(new SqlParameter("@tenantid", input.TenantId));
            }

            StrSql += @" and AbpBusinessDetail.companyid in (" + string.Join(",", input.CompanyIds) + ") ";
            if (input.RepaymentYorN == 1)//已补缴
            {
                //时间  status in (3,4,5) and
                if (!string.IsNullOrWhiteSpace(input.operateDateBegin) && !string.IsNullOrWhiteSpace(input.operateDateEnd))
                {
                    StrSql += @" and CarRepaymentTime between @BeginTime and @EndTime and status = 4 ";
                    SqlParam.Add(new SqlParameter("@BeginTime", input.begindt));
                    SqlParam.Add(new SqlParameter("@EndTime", input.enddt));
                }
                //逃逸时间  status in (3,4,5) and//gcj  add 逃逸时间检查text
                if (!string.IsNullOrWhiteSpace(input.EscapeTimeDateBegin) && !string.IsNullOrWhiteSpace(input.EscapeTimeDateEnd))
                {
                    StrSql += @" and EscapeTime between @BeginTime1 and @EndTime1 and status = 4 ";
                    SqlParam.Add(new SqlParameter("@BeginTime1", input.EscapeTimebegindt));
                    SqlParam.Add(new SqlParameter("@EndTime1", input.EscapeTimeenddt));
                }
                //补缴收费员
                if (!string.IsNullOrWhiteSpace(input.EmployeeId.ToString()) && input.EmployeeId.ToString() != "0")
                {
                    StrSql += @" and EscapeOperaId = @EscapeOperaId ";
                    SqlParam.Add(new SqlParameter("@EscapeOperaId", input.EmployeeId));
                }
            }
            else if (input.RepaymentYorN == 2)//未补缴
            {
                //逃逸时间  status in (3,4,5) and
                if (!string.IsNullOrWhiteSpace(input.operateDateBegin) && !string.IsNullOrWhiteSpace(input.operateDateEnd))
                {
                    StrSql += @" and EscapeTime between @BeginTime and @EndTime and status in(3, 5) ";
                    SqlParam.Add(new SqlParameter("@BeginTime", input.begindt));
                    SqlParam.Add(new SqlParameter("@EndTime", input.enddt));
                }
                //逃逸操作收费员
                if (!string.IsNullOrWhiteSpace(input.EmployeeId.ToString()) && input.EmployeeId.ToString() != "0")
                {
                    StrSql += @" and ChargeOperaId = @EscapeOperaId ";
                    SqlParam.Add(new SqlParameter("@EscapeOperaId", input.EmployeeId));
                }
            }
            else
            {
                //逃逸时间  status in (3,4,5) and
                if (!string.IsNullOrWhiteSpace(input.operateDateBegin) && !string.IsNullOrWhiteSpace(input.operateDateEnd))
                {
                    StrSql += @" and EscapeTime between @BeginTime and @EndTime ";
                    SqlParam.Add(new SqlParameter("@BeginTime", input.begindt));
                    SqlParam.Add(new SqlParameter("@EndTime", input.enddt));
                }
                //逃逸操作收费员
                if (!string.IsNullOrWhiteSpace(input.EmployeeId.ToString()) && input.EmployeeId.ToString() != "0")
                {
                    StrSql += @" and ChargeOperaId = @EscapeOperaId ";
                    SqlParam.Add(new SqlParameter("@EscapeOperaId", input.EmployeeId));
                }
            }

            if (!string.IsNullOrWhiteSpace(input.RepaymentPayStatus.ToString()) && input.RepaymentPayStatus.ToString() != "0")
            {
                StrSql += @" and EscapePayStatus = @EscapePayStatus ";
                SqlParam.Add(new SqlParameter("@EscapePayStatus", input.RepaymentPayStatus));
            }

            if (!string.IsNullOrWhiteSpace(input.RegionId.ToString()) && input.RegionId.ToString() != "0")
            {
                StrSql += @" and RegionId = @RegionId ";
                SqlParam.Add(new SqlParameter("@RegionId", input.RegionId));
            }
            if (!string.IsNullOrWhiteSpace(input.ParkId.ToString()) && input.ParkId.ToString() != "0")
            {
                StrSql += @" and ParkId = @ParkId ";
                SqlParam.Add(new SqlParameter("@ParkId", input.ParkId));
            }
            if (!string.IsNullOrWhiteSpace(input.BerthsecId.ToString()) && input.BerthsecId.ToString() != "0")
            {
                StrSql += @" and BerthsecId = @BerthsecId ";
                SqlParam.Add(new SqlParameter("@BerthsecId", input.BerthsecId));
            }

            if (!string.IsNullOrWhiteSpace(input.BerthNumber))
            {
                StrSql += @" and berthnumber = @berthnumber ";
                SqlParam.Add(new SqlParameter("@berthnumber", input.BerthNumber));
            }

            if (!string.IsNullOrWhiteSpace(input.PlateNumber))
            {
                StrSql += @"  and platenumber like '%" + input.PlateNumber + "%' ";
            }
            StrSql += @" ) as touyoudianyun group by groupUser";
            SqlParameter[] param = SqlParam.ToArray();
            var EscapeDetailTable = Context.Database.SqlQuery<EscapeDetailsDto>(StrSql, param).ToList();
            return EscapeDetailTable;

        }
        /// <summary>
        /// 逃逸明细
        ///      /// 后台追缴，用户保存用户Id
        /// 当PaymentType为2时
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <summary>
        /// 逃逸明细
        ///      /// 后台追缴，用户保存用户Id
        /// 当PaymentType为2时
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetEscapeDetailsOutput GetEscapeDetailsList(GetEscapeDetailsInput input)
        {
            List<SqlParameter> SqlParam = new List<SqlParameter>();
            DbRawSqlQuery<EscapeDetailsDto> EscapeDetailsTable = null;
            if (input.filters == null)//自定义查询
            {
                string StrSql = @"
select 1 as GroupUser,Business.id,Business.IsLock,Arrearage,Business.RegionId,Business.ParkId,Business.BerthsecId,AbpRegions.regionname as RegionName,AbpParks.ParkName as ParkName,AbpBerthsecs.BerthsecName as BerthsecName,PlateNumber,guid,CarType,BerthNumber,CarInTime,CarOutTime,IsEscapePay,CarRepaymentTime, Money, Repayment, InDeviceCode,EA.truename as InEmployeeName,OutDeviceCode,EB.truename as OutEmployeeName,case PaymentType when 2 then EscapeUserId else EscapeOperaId end as EscapeOperaId,case PaymentType when 2 then AbpUsers.name else EC.truename end as EscapeEmployeeName,case PaymentType when 2 then '后台追缴' else EscapeDeviceCode end as EscapeDeviceCode, EscapePayStatus, PaymentType, Status, AbpTenants.name as Tenant,StopTime,Prepaid,Receivable,ED.truename as ChargeOperaName,Business.RowNumber  from (
select *,ROW_NUMBER() OVER(Order by Id DESC ) AS RowNumber from AbpBusinessDetail where  isdeleted = 0  ";

                if (input.TenantId.HasValue)
                {
                    StrSql += " and(tenantid = @tenantid or EscapeTenantId = @tenantid)";
                    SqlParam.Add(new SqlParameter("@tenantid", input.TenantId));
                    StrSql += @" and (AbpBusinessDetail.companyid in (" + string.Join(",", input.CompanyIds) + ") or " +
                        "AbpBusinessDetail.EscapeCompanyId in (" + string.Join(",", input.CompanyIds) + ")) ";
                }

                if (input.RepaymentYorN == 1)//已补缴
                {
                    //时间  status in (3,4,5) and
                    if (!string.IsNullOrWhiteSpace(input.operateDateBegin) && !string.IsNullOrWhiteSpace(input.operateDateEnd))
                    {
                        StrSql += @" and CarRepaymentTime between @BeginTime and @EndTime and status = 4 ";
                        SqlParam.Add(new SqlParameter("@BeginTime", input.begindt));
                        SqlParam.Add(new SqlParameter("@EndTime", input.enddt));
                    }
                    //逃逸时间  status in (3,4,5) and//gcj  add 逃逸时间检查text
                    if (!string.IsNullOrWhiteSpace(input.EscapeTimeDateBegin) && !string.IsNullOrWhiteSpace(input.EscapeTimeDateEnd))
                    {
                        StrSql += @" and EscapeTime between @BeginTime1 and @EndTime1 and status = 4 ";
                        SqlParam.Add(new SqlParameter("@BeginTime1", input.EscapeTimebegindt));
                        SqlParam.Add(new SqlParameter("@EndTime1", input.EscapeTimeenddt));
                    }
                    //补缴收费员
                    if (!string.IsNullOrWhiteSpace(input.EmployeeId.ToString()) && input.EmployeeId.ToString() != "0")
                    {
                        StrSql += @" and EscapeOperaId = @EscapeOperaId ";
                        SqlParam.Add(new SqlParameter("@EscapeOperaId", input.EmployeeId));
                    }
                }
                else if (input.RepaymentYorN == 2)//未补缴
                {
                    //逃逸时间  status in (3,4,5) and
                    if (!string.IsNullOrWhiteSpace(input.operateDateBegin) && !string.IsNullOrWhiteSpace(input.operateDateEnd))
                    {
                        StrSql += @" and EscapeTime between @BeginTime and @EndTime and status in(3, 5) ";
                        SqlParam.Add(new SqlParameter("@BeginTime", input.begindt));
                        SqlParam.Add(new SqlParameter("@EndTime", input.enddt));
                    }
                    //逃逸操作收费员
                    if (!string.IsNullOrWhiteSpace(input.EmployeeId.ToString()) && input.EmployeeId.ToString() != "0")
                    {
                        StrSql += @" and ChargeOperaId = @EscapeOperaId ";
                        SqlParam.Add(new SqlParameter("@EscapeOperaId", input.EmployeeId));
                    }
                }
                else
                {
                    //逃逸时间  status in (3,4,5) and
                    if (!string.IsNullOrWhiteSpace(input.operateDateBegin) && !string.IsNullOrWhiteSpace(input.operateDateEnd))
                    {
                        StrSql += @" and EscapeTime between @BeginTime and @EndTime ";
                        SqlParam.Add(new SqlParameter("@BeginTime", input.begindt));
                        SqlParam.Add(new SqlParameter("@EndTime", input.enddt));
                    }
                    //逃逸操作收费员
                    if (!string.IsNullOrWhiteSpace(input.EmployeeId.ToString()) && input.EmployeeId.ToString() != "0")
                    {
                        StrSql += @" and ChargeOperaId = @EscapeOperaId ";
                        SqlParam.Add(new SqlParameter("@EscapeOperaId", input.EmployeeId));
                    }
                }

                if (!string.IsNullOrWhiteSpace(input.RepaymentPayStatus.ToString()) && input.RepaymentPayStatus.ToString() != "0")
                {
                    StrSql += @" and EscapePayStatus = @EscapePayStatus ";
                    SqlParam.Add(new SqlParameter("@EscapePayStatus", input.RepaymentPayStatus));
                }

                if (!string.IsNullOrWhiteSpace(input.RegionId.ToString()) && input.RegionId.ToString() != "0")
                {
                    StrSql += @" and RegionId = @RegionId ";
                    SqlParam.Add(new SqlParameter("@RegionId", input.RegionId));
                }
                if (!string.IsNullOrWhiteSpace(input.ParkId.ToString()) && input.ParkId.ToString() != "0")
                {
                    StrSql += @" and ParkId = @ParkId ";
                    SqlParam.Add(new SqlParameter("@ParkId", input.ParkId));
                }
                if (!string.IsNullOrWhiteSpace(input.BerthsecId.ToString()) && input.BerthsecId.ToString() != "0")
                {
                    StrSql += @" and BerthsecId = @BerthsecId ";
                    SqlParam.Add(new SqlParameter("@BerthsecId", input.BerthsecId));
                }

                if (!string.IsNullOrWhiteSpace(input.BerthNumber))
                {
                    StrSql += @" and berthnumber = @berthnumber ";
                    SqlParam.Add(new SqlParameter("@berthnumber", input.BerthNumber));
                }

                if (!string.IsNullOrWhiteSpace(input.PlateNumber))
                {
                    StrSql += @"  and platenumber like '%" + input.PlateNumber + "%' ";
                    //SqlParam.Add(new SqlParameter("@platenumber", input.PlateNumber));
                }
                StrSql += @" and status in(3, 5, 4) ) as Business left join AbpRegions on Business.regionid = AbpRegions.id left join AbpParks on Business.ParkId = AbpParks.id left join AbpBerthsecs on Business.BerthsecId=AbpBerthsecs.id left join AbpEmployees as EA on Business.InOperaId = EA.id left join AbpEmployees as EB on Business.OutOperaId=EB.id left join AbpUsers on Business.EscapeUserId = AbpUsers.id left join AbpEmployees as EC on Business.EscapeOperaId = EC.id left join AbpTenants on Business.TenantId = AbpTenants.id left join AbpEmployees as ED on Business.ChargeOperaId = ED.id where  RowNumber between @BeginSize and @EndSize order by id";
                SqlParam.Add(new SqlParameter("@BeginSize", input.page * input.rows - input.rows + 1));
                SqlParam.Add(new SqlParameter("@EndSize", input.page * input.rows));
                SqlParameter[] param = SqlParam.ToArray();
                EscapeDetailsTable = Context.Database.SqlQuery<EscapeDetailsDto>(StrSql, param);
                var rows = EscapeDetailsTable.ToList();
                List<EscapeDetailsDto> BBBB = GetEscapedetailsCountSql(input);
                int records = 0;
                EscapeDetailsUserData bdud = new EscapeDetailsUserData();
                bdud.StopTimes = "汇总";
                bdud.BerthNumber = "车辆数";
                if (BBBB.Count > 0)
                {
                    records = BBBB[0].RegionId;//记录数
                    bdud.Prepaid = BBBB[0].Prepaid;
                    bdud.Receivable = BBBB[0].Receivable;
                    bdud.Arrearage = BBBB[0].Arrearage;
                    bdud.Repayment = BBBB[0].Repayment;
                    bdud.Money = BBBB[0].Money;
                    bdud.PlateNumber = BBBB[0].RegionId.ToString();
                }
                return new GetEscapeDetailsOutput()
                {
                    rows = rows,
                    records = records,
                    total = records / input.rows + (records % input.rows != 0 ? 1 : 0),
                    userdata = bdud
                };
            }
            else
            {
                var query = Table.Where(entity => entity.Status == 3 || entity.Status == 4 || entity.Status == 5)
                    .Select(entity => new EscapeDetailsDto()
                    {
                        GroupUser = 1,
                        Id = entity.Id,
                        Arrearage = entity.Arrearage,
                        RegionId = entity.RegionId,
                        ParkId = entity.ParkId,
                        BerthsecId = entity.BerthsecId,
                        RegionName = Context.Set<Regions.Region>().FirstOrDefault(e => e.Id == entity.RegionId).RegionName,
                        ParkName = entity.Park.ParkName,
                        BerthsecName = entity.Berthsec.BerthsecName,
                        PlateNumber = entity.PlateNumber,
                        guid = entity.guid,
                        CarType = entity.CarType,
                        BerthNumber = entity.BerthNumber,
                        CarInTime = entity.CarInTime,
                        CarOutTime = entity.CarOutTime,
                        IsEscapePay = entity.IsEscapePay,
                        CarRepaymentTime = entity.CarRepaymentTime,
                        Repayment = entity.Repayment,
                        EscapePayStatus = entity.EscapePayStatus,
                        InDeviceCode = entity.InDeviceCode,
                        InEmployeeName = entity.InEmployee.TrueName,
                        OutDeviceCode = entity.OutDeviceCode,
                        OutEmployeeName = entity.OutEmployee.TrueName,
                        EscapeOperaId = entity.PaymentType == 2 ? entity.EscapeUserId : entity.EscapeOperaId,//chulixia
                        EscapeEmployeeName =
                            entity.PaymentType == 2 ? entity.EscapeUser.Name : entity.EscapeEmployee.TrueName,
                        EscapeDeviceCode = entity.PaymentType == 2 ? "后台追缴" : entity.EscapeDeviceCode,//chulixia
                        PaymentType = entity.PaymentType,
                        Status = entity.Status,
                        Tenant = entity.Tenant.Name,
                        StopTime = entity.StopTime,
                        Prepaid = entity.Prepaid,
                        Receivable = entity.Receivable,
                        Money = entity.Money,
                        ChargeOperaName = Context.Set<Employee>().FirstOrDefault(e => e.Id == entity.ChargeOperaId).TrueName
                    }).Filters(input);
                EscapeDetailsUserData edud = new EscapeDetailsUserData();
                string StopTimes = "汇总";
                string BerthNumber = "车辆数";
                decimal Prepaid = 0;
                decimal Receivable = 0;
                decimal Arrearage = 0;
                decimal? Repayment = 0;
                decimal? Money = 0;
                string PlateNumber = "";
                var UserD = from a in query      //计算汇总
                            group a by a.GroupUser into g
                            select new
                            {
                                Prepaid = g.Sum(entity => entity.Prepaid),
                                Receivable = g.Sum(entity => entity.Receivable),
                                Arrearage = g.Sum(entity => entity.Arrearage),
                                Repayment = g.Sum(entity => (entity.Repayment.HasValue == false ? 0 : entity.Repayment.Value)),
                                Money = g.Sum(entity => entity.Money.HasValue == false ? 0 : entity.Money.Value),
                                PlateNumber = g.Count()
                            };
                var UserDList = UserD.ToList();
                if (UserDList.Count() > 0)
                {
                    Prepaid = UserDList[0].Prepaid;
                    Receivable = UserDList[0].Receivable;
                    Arrearage = UserDList[0].Arrearage;
                    Repayment = UserDList[0].Repayment;
                    Money = UserDList[0].Money;
                    PlateNumber = UserDList[0].PlateNumber.ToString();
                }
                //var queryList = query.ToList();
                //if(queryList.Count()>0)
                //{
                //    foreach (var q in queryList)
                //    {
                //        Prepaid += q.Prepaid;//预付      汇总
                //        Receivable += q.Receivable;//出场应收
                //        Arrearage += q.Arrearage;//欠费
                //        Repayment += (q.Repayment==null?0:q.Repayment);//补缴
                //    }
                //}
                edud.BerthNumber = BerthNumber;
                edud.PlateNumber = PlateNumber;
                edud.StopTimes = StopTimes;
                edud.Prepaid = Prepaid;
                edud.Receivable = Receivable;
                edud.Arrearage = Arrearage;
                edud.Repayment = Repayment;
                edud.Money = Money;
                int records = query.Count();
                return new GetEscapeDetailsOutput()
                {
                    rows = query.Orders(input).PageBy(input).ToList(),
                    records = records,
                    total = records / input.rows + (records % input.rows != 0 ? 1 : 0),
                    userdata = edud
                };
            }
        }

        /// <summary>
        /// 根据车牌号
        /// 车辆欠费接口
        /// </summary>
        /// <param name="plateNumber"></param>
        /// <param name="userId"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public GetEscapeDetailsOutputTow GetEscapeDetailsListTow(string plateNumber, long? userId, int? TenantId)
        {
            IQueryable<BusinessDetail> model1 = Table;
            if (bool.Parse(_settingStore.GetSettingOrNull(TenantId, null, "EscapeLock").Value))//启用追缴锁定
            {
                model1 = Table.Where(entity => entity.PlateNumber == plateNumber && (entity.Status == 3 || entity.Status == 5) && entity.IsLock == true && entity.EmployeeId == userId);
            }
            else
            {
                model1 = Table.Where(entity => entity.PlateNumber == plateNumber && (entity.Status == 3 || entity.Status == 5));
            }
            var model = model1.Select(entity => new EscapeDetailsDto()
            {
                Id = entity.Id,
                EscapeEmployeeName = entity.EscapeEmployee.UserName == null ? "" : entity.EscapeEmployee.UserName,//逃逸追缴收费员用户名******
                EscapeDeviceCode = entity.EscapeDeviceCode == null ? "" : entity.EscapeDeviceCode,//逃逸追缴设备 *****
                ChargeOperaName = entity.ChargeEmployee.UserName,//收费操作员用户名 ******
                EscapeOperaId = entity.EscapeOperaId == null ? 0 : entity.EscapeOperaId,//逃逸收费员ID *****
                RegionName = Context.Set<Regions.Region>().FirstOrDefault(e => e.Id == entity.RegionId).RegionName,
                ParkName = entity.Park.ParkName,
                BerthsecName = entity.Berthsec.BerthsecName,
                PlateNumber = entity.PlateNumber,
                CarType = entity.CarType,
                BerthNumber = entity.BerthNumber,
                CarInTime = entity.CarInTime,
                CarOutTime = entity.CarOutTime,
                IsEscapePay = entity.IsEscapePay,
                CarRepaymentTime = entity.CarRepaymentTime == null ? DateTime.Now : entity.CarRepaymentTime,  //补缴时间
                Repayment = entity.Repayment == null ? 0 : entity.Repayment,//补缴金额**********,
                InDeviceCode = entity.InDeviceCode,
                InEmployeeName = entity.InEmployee.TrueName,
                OutDeviceCode = entity.OutDeviceCode,
                OutEmployeeName = entity.OutEmployee.TrueName,
                PaymentType = entity.PaymentType,
                Tenant = entity.Tenant.Name,
                StopTime = entity.StopTime,
                Prepaid = entity.Prepaid,
                Receivable = entity.Receivable,
                Arrearage = entity.Arrearage  //欠费数据
            });
            if (model.Count() != 0)
            {
                int records = model.Count();
                return new GetEscapeDetailsOutputTow()
                {
                    Items = model.ToList()
                };
            }
            else
            {
                IQueryable<BusinessDetail> query1 = Table;
                if (bool.Parse(_settingStore.GetSettingOrNull(TenantId, null, "EscapeLock").Value))//启用追缴锁定
                {
                    query1 = Table.Where(entity => entity.PlateNumber == plateNumber && (entity.Status == 3 || entity.Status == 5) && entity.IsLock == false);
                }
                else
                {
                    query1 = Table.Where(entity => entity.PlateNumber == plateNumber && (entity.Status == 3 || entity.Status == 5));
                }
                //var query = Table.Where(entity => entity.PlateNumber == plateNumber && (entity.Status == 3 || entity.Status == 5) && entity.IsLock == false).Select(entity => new EscapeDetailsDto()
                var query = query1.Select(entity => new EscapeDetailsDto()
                {
                    Id = entity.Id,
                    EscapeEmployeeName = entity.EscapeEmployee.UserName == null ? "" : entity.EscapeEmployee.UserName,//逃逸追缴收费员用户名******
                    EscapeDeviceCode = entity.EscapeDeviceCode == null ? "" : entity.EscapeDeviceCode,//逃逸追缴设备 *****
                    ChargeOperaName = entity.ChargeEmployee.UserName,//收费操作员用户名 ******
                    EscapeOperaId = entity.EscapeOperaId == null ? 0 : entity.EscapeOperaId,//逃逸收费员ID *****
                    RegionName = Context.Set<Regions.Region>().FirstOrDefault(e => e.Id == entity.RegionId).RegionName,
                    ParkName = entity.Park.ParkName,
                    BerthsecName = entity.Berthsec.BerthsecName,
                    PlateNumber = entity.PlateNumber,
                    CarType = entity.CarType,
                    BerthNumber = entity.BerthNumber,
                    CarInTime = entity.CarInTime,
                    CarOutTime = entity.CarOutTime,
                    IsEscapePay = entity.IsEscapePay,
                    CarRepaymentTime = entity.CarRepaymentTime == null ? DateTime.Now : entity.CarRepaymentTime,//补缴时间
                    Repayment = entity.Repayment == null ? 0 : entity.Repayment,//补缴金额**********
                    InDeviceCode = entity.InDeviceCode,
                    InEmployeeName = entity.InEmployee.TrueName,
                    OutDeviceCode = entity.OutDeviceCode,
                    OutEmployeeName = entity.OutEmployee.TrueName,
                    PaymentType = entity.PaymentType,
                    Tenant = entity.Tenant.Name,
                    StopTime = entity.StopTime,
                    Prepaid = entity.Prepaid,
                    Receivable = entity.Receivable,
                    Arrearage = entity.Arrearage  //欠费数据
                });
                List<EscapeDetailsDto> escapeD = query.ToList();
                if (query.Count() != 0) //如果存在欠费记录
                {
                    //var EscapeDList = _BusinessBetailAppService.GetAll().Where(a => a.PlateNumber == plateNumber && (a.Status == 3 || a.Status == 5) && a.IsLock == false).ToList();
                    //if (EscapeDList != null)
                    //{
                    //    BusinessDetail BD = null;
                    //    for (int i = 0; i < EscapeDList.Count(); i++) //调用欠费的接口的时候  锁定该车牌号可能多条欠费记录
                    //    {
                    //        BD = EscapeDList[i];
                    //        BD.IsLock = true;
                    //        BD.EmployeeId = userId;
                    //        _BusinessBetailAppService.Update(BD);
                    //    }
                    //}

                    //下载车辆欠费信息为null  两个原因  1：视图有为null的字段  2：query.tolist()执行前又访问了一次数据库
                    string sqlstr = @"update AbpBusinessDetail set IsLock=1,EmployeeId=@EmployeeId where PlateNumber=@PlateNumber and  (Status=3 or Status=5) and IsLock=0";
                    Context.Database.ExecuteSqlCommand(sqlstr, new SqlParameter[]   {
                     new SqlParameter("@EmployeeId",userId),
                new SqlParameter("@PlateNumber",plateNumber)
                    });
                }

                int records = query.Count();
                return new GetEscapeDetailsOutputTow()
                {
                    Items = escapeD
                };
            }
        }

        /// <summary>
        /// 查询逃逸 改用sql语句
        /// </summary>
        /// <param name="plateNumber"></param>
        /// <param name="userId"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public GetEscapeDetailsOutputTow GetEscapeDetailsListTowBySql(string plateNumber, long? userId, int? TenantId, int? CompanyId)
        {
            List<EscapeDetailsDto> escapeList = new List<EscapeDetailsDto>();
            List<SqlParameter> parameterList = new List<SqlParameter>();

            if (plateNumber.Contains("无"))
            {
                return new GetEscapeDetailsOutputTow()
                {
                    Items = escapeList
                };
            }

            //默认全国追缴
            string sqlStr = "select chargeEmployee.UserName as ChargeOperaName,escapeEmployee.UserName as EscapeEmployeeName," +
                       " berthsec.BerthsecName ,park.ParkName,region.RegionName, business.* " +
                       " from AbpBusinessDetail as business with(nolock)" +
                       " left join AbpBerthsecs as berthsec with(nolock) on business.BerthsecId=berthsec.Id" +
                       " left join AbpParks as park with(nolock) on business.ParkId = park.Id" +
                       " left join AbpRegions as region with(nolock) on business.RegionId = region.Id" +
                       " left join AbpEmployees as chargeEmployee with(nolock) on business.ChargeOperaId = chargeEmployee.Id" +
                       " left join AbpEmployees as escapeEmployee with(nolock) on business.EscapeOperaId = escapeEmployee.Id" +
                       " where  PlateNumber = @PlateNumber and (Status = 3 or Status = 5)  and business.IsDeleted = 0 ";
            parameterList.Add(new SqlParameter("@PlateNumber", plateNumber));
            //同一分公司才能互相追缴 默认     
            //如果全国追缴默认必须支持分公司追缴  全国追缴
            if (bool.Parse(_settingStore.GetSettingOrNull(TenantId, null, "TheRecovered").Value))
            {
                sqlStr += " and business.TenantId in (select TenantId from AbpSettings with(nolock) where Name = 'TheRecovered' and Value = 'True') ";
            }
            //同一商户互相追缴
            else if (bool.Parse(_settingStore.GetSettingOrNull(TenantId, null, "TheRecoveredCompany").Value))
            {
                sqlStr += " and business.TenantId = @TenantId";
                parameterList.Add(new SqlParameter("@TenantId", TenantId.Value));
            }
            else//同一个分公司追缴
            {
                sqlStr += " and business.TenantId = @TenantId  and business.CompanyId = @CompanyId ";
                parameterList.Add(new SqlParameter("@TenantId", TenantId.Value));
                parameterList.Add(new SqlParameter("@CompanyId", CompanyId.Value));
            }

            if (bool.Parse(_settingStore.GetSettingOrNull(TenantId, null, "EscapeLock").Value))//启用追缴锁定
            {
                sqlStr += " and (IsLock = 0 or (IsLock = 1 and EmployeeId = @EmployeeId)) ";
                parameterList.Add(new SqlParameter("@EmployeeId", userId));
            }

            escapeList = Context.Database.SqlQuery<EscapeDetailsDto>(sqlStr + " order by business.CarInTime desc", parameterList.ToArray()).ToList();
            if (bool.Parse(_settingStore.GetSettingOrNull(TenantId, null, "EscapeLock").Value))//启用追缴锁定
            {
                int records = escapeList.Count();
                if (records != 0)
                {
                    string sqlstr = @"update AbpBusinessDetail set IsLock = 1, EmployeeId = @EmployeeId where PlateNumber = @PlateNumber and  (Status = 3 or Status = 5) and IsLock = 0";
                    Context.Database.ExecuteSqlCommand(sqlstr, new SqlParameter[] { new SqlParameter("@EmployeeId", userId), new SqlParameter("@PlateNumber", plateNumber) });
                }
            }
            return new GetEscapeDetailsOutputTow()
            {
                Items = escapeList.OrderBy(entity => entity.CarInTime).ToList()
            };
        }


        /// <summary>
        /// 签退的时候解锁，sql语句优化
        /// </summary>
        /// <param name="userId"></param>
        public void CheckOutDeblock(long? userId)
        {
            string sqlStr = @"update AbpBusinessDetail set IsLock=0,EmployeeId = @EmployeeId where EmployeeId = @EmployeeId and IsLock = 1";
            Context.Database.ExecuteSqlCommand(sqlStr, new SqlParameter[]{
            new SqlParameter("@EmployeeId",userId)
            });
        }
        /// <summary>
        /// 对某辆车解锁，部分解锁
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="PlateNumber"></param>
        public void PlateNumberDebLock(long? userId, string PlateNumber)
        {
            string sqlStr = @"update AbpBusinessDetail set IsLock=0,EmployeeId = @EmployeeId where EmployeeId=@EmployeeId and IsLock=1 and PlateNumber=@PlateNumber";
            Context.Database.ExecuteSqlCommand(sqlStr, new SqlParameter[]{
                new SqlParameter("@EmployeeId", userId),
                new SqlParameter("@PlateNumber", PlateNumber)
            });
        }
        //车辆入场接口
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool InsertCarIn(BusinessDetail entity)
        {
            var business = _BusinessBetailAppService.Insert(entity);
            if (business != null)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="dbType"></param>
        /// <param name="parameterSize"></param>
        /// <param name="Direction"></param>
        /// <returns></returns>
        public static SqlParameter AddParameter(string parameterName, object parameterValue, SqlDbType dbType, int parameterSize, ParameterDirection Direction)
        {
            SqlParameter par = new SqlParameter();
            par.ParameterName = parameterName;
            par.Value = parameterValue;
            par.Direction = Direction;
            par.SqlDbType = dbType;
            if (parameterSize > 0) { par.Size = parameterSize; }
            return par;
        }


        /// <summary>
        /// 车辆入场接口（存储过程）
        /// </summary>
        /// <param name="BerthNumber"></param>
        /// <param name="PlateNumber"></param>
        /// <param name="CarType"></param>
        /// <param name="Prepaid"></param>
        /// <param name="CarInTime"></param>
        /// <param name="guid"></param>
        /// <param name="SensorsInCarTime"></param>
        /// <param name="StopType"></param>
        /// <param name="CardNo"></param>
        /// <param name="RegionId"></param>
        /// <param name="ParkId"></param>
        /// <param name="BerthsecId"></param>
        /// <param name="Details"></param>
        /// <param name="TenantId"></param>
        /// <param name="InOperaId"></param>
        /// <param name="InDeviceCode"></param>
        /// <returns></returns>
        public bool InsertCarInPro(string BerthNumber, string PlateNumber, string CarType, string Prepaid, string CarInTime, string guid, string SensorsInCarTime, string StopType, string CardNo, int RegionId, int ParkId, int BerthsecId, string Details, int TenantId, long InOperaId, string InDeviceCode)
        {
            SqlParameter[] param = new SqlParameter[]{
               AddParameter("@BerthNumber",  BerthNumber,System.Data.SqlDbType.Decimal, 18, ParameterDirection.Input),
               AddParameter("@CarType",  CarType,System.Data.SqlDbType.SmallInt, 5, ParameterDirection.Input),
               AddParameter("@Prepaid",  Prepaid,System.Data.SqlDbType.Decimal, 18, ParameterDirection.Input),
               AddParameter("@CarInTime",  CarInTime,System.Data.SqlDbType.DateTime, 18, ParameterDirection.Input),
               AddParameter("@guid",  guid,System.Data.SqlDbType.UniqueIdentifier, 40, ParameterDirection.Input),
               AddParameter("@StopType",  StopType,System.Data.SqlDbType.SmallInt, 5, ParameterDirection.Input),
               AddParameter("@CardNo",  CardNo,System.Data.SqlDbType.NVarChar, 20, ParameterDirection.Input),
               AddParameter("@RegionId",  RegionId,System.Data.SqlDbType.Int, 5, ParameterDirection.Input),
               AddParameter("@ParkId",  ParkId,System.Data.SqlDbType.Int, 5, ParameterDirection.Input),
               AddParameter("@BerthsecId",  BerthsecId,System.Data.SqlDbType.Int, 5, ParameterDirection.Input),
               AddParameter("@Details",  Details,System.Data.SqlDbType.NVarChar, 10, ParameterDirection.Input),
               AddParameter("@InOperaId",  InOperaId,System.Data.SqlDbType.BigInt, 5, ParameterDirection.Input),
               AddParameter("@InDeviceCode",  InDeviceCode,System.Data.SqlDbType.SmallInt, 5, ParameterDirection.Input),
               AddParameter("@Status",  1,System.Data.SqlDbType.SmallInt, 5, ParameterDirection.Input),
               AddParameter("@TenantId",  TenantId,System.Data.SqlDbType.Int, 8, ParameterDirection.Input)
            };
            Context.Database.ExecuteSqlCommand("exec Pro_ProcessCarInPark", param);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Receivable"></param>
        /// <param name="FactReceive"></param>
        /// <param name="Arrearage"></param>
        /// <param name="CarOutTime"></param>
        /// <param name="CarPayTime"></param>
        /// <param name="OutOperaId"></param>
        /// <param name="guid"></param>
        /// <param name="SensorsOutCarTime"></param>
        /// <param name="SensorsStopTime"></param>
        /// <param name="SensorsReceivable"></param>
        /// <param name="PayStatus"></param>
        /// <param name="IsPay"></param>
        /// <param name="FeeType"></param>
        /// <param name="StopTime"></param>
        /// <param name="Money"></param>
        /// <returns></returns>
        public bool InsertCarOutPro(decimal Receivable, string FactReceive, string Arrearage, string CarOutTime, string CarPayTime, string OutOperaId, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money)
        {

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public List<MonthBerthsUseDto> GetMonthBerthsUseList(GetBusinessDetailsInput input, int? tenantID)
        {
            DateTime FirstDayTime = input.begindt;
            DateTime LastDayTime = input.enddt;
            List<DetailsParkingDto> bdlist = new List<DetailsParkingDto>();

            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@FirstDt", FirstDayTime),
                new SqlParameter("@LastDt", LastDayTime)
            };

            string sqlwhere = "";
            if (tenantID.HasValue)
            {
                sqlwhere = " and TenantId = @tenantId";
                param = new SqlParameter[]{
                    new SqlParameter("@FirstDt", FirstDayTime),
                    new SqlParameter("@LastDt", LastDayTime),
                    new SqlParameter("@tenantId", tenantID.Value)
                };
            }

            string sqlStr = @"select row_number() over(order by A.BerthNumber asc) as Id, isnull( a.BerthNumber, isnull(b.BerthNumber, C.BerthNumber)) as BerthNumber, 
            ISNULL(AllMoney, 0) as AllMoney, isnull((A.xj + B.xj),0) as CashFactReceive ,
            isnull((A.sk + B.sk), 0) as CardFactReceive ,ISNULL(AllArrearage, 0) as AllArrearage,
            ISNULL(RepayByCard, 0) as RepayByCard ,ISNULL(RepayByCash, 0) as RepayByCash  
            from(
            select sum( case when PrepaidPayStatus in(0, 1) then Prepaid else 0 end) xj, 
            sum( case when PrepaidPayStatus = 2 then Prepaid else 0 end) sk
            , sum(isnull( Money,0)) as AllMoney, sum(isnull(Arrearage,0)) as  AllArrearage , 
             BerthNumber from AbpBusinessDetail with(nolock) where 
             carpaytime  between   @FirstDt  and    @LastDt " + sqlwhere + " and isdeleted=0  group by  BerthNumber) AS A full join ( select sum( case when PayStatus in(0, 1)  then isnull((FactReceive - Prepaid),0) else 0 end) xj, sum( case when  PayStatus = 2 then isnull(( FactReceive - Prepaid),0) else 0 end) sk, BerthNumber from AbpBusinessDetail with(nolock) where carpaytime between   @FirstDt and  @LastDt " + sqlwhere + "  and isdeleted=0 group by  BerthNumber) AS B on A.BerthNumber = B.BerthNumber full  join  ( select sum( case when EscapePayStatus = 1 then repayment else 0 end) as RepayByCash, sum( case when EscapePayStatus =2 then repayment else 0 end) as RepayByCard, BerthNumber from dbo.AbpBusinessDetail with(nolock) where CarRepaymentTime between @FirstDt and @LastDt " + sqlwhere + " and isdeleted = 0  group by berthnumber) as C on B.BerthNumber=C.BerthNumber ";

            bdlist = Context.Database.SqlQuery<DetailsParkingDto>(sqlStr, param).ToList();
            List<MonthBerthsUseDto> ListRTdto = new List<MonthBerthsUseDto>();
            for (int i = 0; i < bdlist.Count; i++)
            {
                MonthBerthsUseDto Mbudto = new MonthBerthsUseDto();
                Mbudto.Id = (int)bdlist[i].Id;
                Mbudto.BerthNumber = bdlist[i].BerthNumber;
                Mbudto.Receivable = bdlist[i].AllMoney.Value;//总应收
                Mbudto.TotalConsumption = bdlist[i].TotalConsumption.Value;//实收
                Mbudto.CashCollection = bdlist[i].CashFactReceive.Value;//现金金额
                Mbudto.ByCashRepay = bdlist[i].RepayByCash.Value;//现金补缴金额
                Mbudto.PaybyCardValue = bdlist[i].CardFactReceive.Value;//刷卡金额
                Mbudto.ByCardRepay = bdlist[i].RepayByCard.Value;//刷卡补缴金额
                Mbudto.Arrearage = bdlist[i].AllArrearage.Value;//欠费总和
                ListRTdto.Add(Mbudto);
            }
            return ListRTdto;
        }

        /// <summary>
        /// 月份车位消费报表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public List<MonthBerthsUseDto> GetMonthBerthsUseListOnlyMonth(GetBusinessDetailsInput input, int? tenantID)
        {
            DateTime FirstDayTime = input.begindt;
            DateTime LastDayTime = input.enddt;
            int months = (input.enddt.Year - input.begindt.Year) * 12 + (input.enddt.Month - input.begindt.Month);//月数差
            List<MonthBerthsUseDto> ListRTdto = new List<MonthBerthsUseDto>();
            //DateTime LastMonth = input.begindt.AddMonths(1).AddDays(-1);//本月月末
            for (int i = 1; i < months + 2; i++)
            {



                DateTime FristMonth = input.begindt.AddDays(1 - input.begindt.Day).AddMonths(i - 1);//月初
                DateTime LastMonth = FristMonth.AddMonths(1);//月末
                string YearMonth = FristMonth.Year + "年" + FristMonth.Month + "月";
                List<DetailsParkingDto> bdlist = new List<DetailsParkingDto>();

                SqlParameter[] param = new SqlParameter[] {
                    new SqlParameter("@FristMonth", FristMonth),
                    new SqlParameter("@LastMonth", LastMonth),
                    new SqlParameter("@YearMonth", YearMonth)
                };

                string sqlwhere = "";
                if (tenantID.HasValue)
                {
                    sqlwhere = " and TenantId= @tenantId";
                    param = new SqlParameter[] {
                        new SqlParameter("@FristMonth", FristMonth),
                        new SqlParameter("@LastMonth", LastMonth),
                        new SqlParameter("@tenantId", tenantID.Value),
                        new SqlParameter("@YearMonth", YearMonth)
                    };
                }

                string sqlStr = @"select @YearMonth as YM, sum(AllMoney) as AllMoney,sum(CashFactReceive) as CashFactReceive,sum(CardFactReceive) as CardFactReceive,sum(AllArrearage) as AllArrearage,sum(RepayByCard) as RepayByCard,sum(RepayByCash) as RepayByCash from (select  1 as groupuse , row_number() over(order by A.BerthNumber asc) as Id, isnull( a.BerthNumber, isnull(b.BerthNumber, C.BerthNumber)) as BerthNumber, 
ISNULL(AllMoney,0) as AllMoney  , isnull((A.xj+B.xj),0) as CashFactReceive ,
isnull((A.sk + B.sk), 0) as CardFactReceive ,ISNULL(AllArrearage, 0) as AllArrearage,
ISNULL(RepayByCard, 0) as RepayByCard ,ISNULL(RepayByCash, 0) as RepayByCash  
from(
select sum( case when PrepaidPayStatus in(0, 1) then Prepaid else 0 end) xj, 
sum( case when PrepaidPayStatus = 2 then Prepaid else 0 end) sk
, sum(isnull( Money, 0)) as AllMoney, sum(isnull(Arrearage, 0)) as  AllArrearage , 
 BerthNumber from AbpBusinessDetail with(nolock) where 
 carpaytime  >   @FristMonth  and  carpaytime<  @LastMonth " + sqlwhere + "  group by  BerthNumber) AS A full join ( select sum( case when PayStatus in(0, 1)  then isnull((FactReceive - Prepaid),0) else 0 end) xj,  sum( case when  PayStatus = 2 then isnull(( FactReceive - Prepaid),0) else 0 end) sk,    BerthNumber  from AbpBusinessDetail with(nolock) where carpaytime  >   @FristMonth and  carpaytime< @LastMonth " + sqlwhere + " group by  BerthNumber)   AS B on A.BerthNumber = B.BerthNumber   full  join  (  select sum( case when EscapePayStatus = 1 then repayment else 0 end) as RepayByCash, sum( case when EscapePayStatus =2 then repayment else 0 end) as RepayByCard, BerthNumber from dbo.AbpBusinessDetail with(nolock) where CarRepaymentTime > @FristMonth and CarRepaymentTime< @LastMonth " + sqlwhere + " group by   berthnumber ) as C on B.BerthNumber = C.BerthNumber) as a group by groupuse";

                bdlist = Context.Database.SqlQuery<DetailsParkingDto>(sqlStr, param).ToList();


                MonthBerthsUseDto Mbudto = new MonthBerthsUseDto();
                if (bdlist.Count > 0)
                {
                    //Mbudto.Id = (int)bdlist[0].Id;
                    Mbudto.YearMonth = bdlist[0].YM;
                    Mbudto.BerthNumber = "";
                    Mbudto.Receivable = bdlist[0].AllMoney.Value;//总应收
                    Mbudto.TotalConsumption = bdlist[0].TotalConsumption.Value;//实收
                    Mbudto.CashCollection = bdlist[0].CashFactReceive.Value;//现金金额
                    Mbudto.PaybyCardValue = bdlist[0].CardFactReceive.Value;//刷卡金额
                    Mbudto.Arrearage = bdlist[0].AllArrearage.Value;//欠费总和
                    Mbudto.ByCardRepay = bdlist[0].RepayByCard.Value;//刷卡补缴金额
                    Mbudto.ByCashRepay = bdlist[0].RepayByCash.Value;//现金补缴金额
                    ListRTdto.Add(Mbudto);
                }
            }
            return ListRTdto;

        }
        /// <summary>
        /// guid 为0   整理数据
        /// </summary>
        /// <param name="exceutiontime"></param>
        public void CheckGuid(DateTime exceutiontimeBegin, DateTime exceutiontimeEnd)
        {
            //string sqlinsertIn = "select distinct  methodname, userid, parameters from abpauditlogs where  executiontime > @exceutiontime and methodname = 'InsertCarIn'and parameters like '%00000000-0000-0000-0000-000000000000%' order by userid desc";
            //string sqlinsertOut = "select distinct  methodname, userid, parameters from abpauditlogs where  executiontime > @exceutiontime and methodname = 'InsertCarOut'and parameters like '%00000000-0000-0000-0000-000000000000%' order by userid desc";
            //DbRawSqlQuery<BusinessDetailsDto> inData = Context.Database.SqlQuery<BusinessDetailsDto>(sqlinsertIn, new SqlParameter[] { new SqlParameter("@exceutiontime", exceutiontime) });
            //DbRawSqlQuery<BusinessDetailsDto> outData = Context.Database.SqlQuery<BusinessDetailsDto>(sqlinsertOut, new SqlParameter[] { new SqlParameter("@exceutiontime", exceutiontime) });

            string sqlStrCarInAndOut = @"
select a.methodname as methodnameIn,b.methodname as methodnameOut,a.userid,a.parameters as parametersIn,b.parameters as parametersOut from (
select distinct  methodname, userid, parameters from abpauditlogs where  executiontime > @exceutiontimeBegin
 and executiontime < @exceutiontimeEnd and methodname = 'InsertCarIn'and parameters like '%00000000-0000-0000-0000-000000000000%' and tenantid=2)as a full join (select distinct  methodname, userid, parameters from abpauditlogs where  executiontime > @exceutiontimeBegin and executiontime < @exceutiontimeEnd and methodname = 'InsertCarOut'and parameters like '%00000000-0000-0000-0000-000000000000%' and tenantid=2) as b on a.userid=b.userid order by userid asc";
            List<CarInAndCarOut> CarInOutData = Context.Database.SqlQuery<CarInAndCarOut>(sqlStrCarInAndOut, new SqlParameter[] { new SqlParameter("@exceutiontimeBegin", exceutiontimeBegin), new SqlParameter("@exceutiontimeEnd", exceutiontimeEnd) }).ToList();


            List<InsertCarInModel> ICIMInList = new List<InsertCarInModel>();
            List<InsertCarOutModel> ICIMOutList = new List<InsertCarOutModel>();
            if (CarInOutData != null)
            {
                foreach (var s in CarInOutData)
                {
                    //DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                    //string sIn = s.parametersIn;
                    //JsonReader reader = new JsonReader(new StringReader(sIn));

                    if (s.parametersIn != null && s.parametersOut != null)
                    {
                        InsertCarInModel ICIMIn = JsonConvert.DeserializeObject<InsertCarInModel>(s.parametersIn);//进场数据
                        Guid guid = Guid.NewGuid();
                        ICIMIn.guid = guid.ToString();
                        ICIMIn.userid = s.userid.ToString();
                        Boolean b = InsertCarIn(ICIMIn.berthNumber, ICIMIn.plateNumber, ICIMIn.carType, ICIMIn.prepaid, ICIMIn.carInTime, ICIMIn.guid, ICIMIn.sensorsInCarTime, ICIMIn.stopType, ICIMIn.cardNo, ICIMIn.userid, ICIMIn.regionId, ICIMIn.parkId, ICIMIn.berthsecId);
                        //ICIMInList.Add(ICIMIn);

                        InsertCarOutModel ICIMOut = JsonConvert.DeserializeObject<InsertCarOutModel>(s.parametersOut);//出场数据
                        ICIMOut.guid = guid.ToString();
                        ICIMOut.userid = s.userid.ToString();
                        Boolean a = InsertCarOut(Convert.ToDecimal(ICIMOut.receivable), ICIMOut.factReceive, ICIMOut.carOutTime, ICIMOut.carPayTime, ICIMOut.guid, ICIMOut.sensorsOutCarTime, ICIMOut.sensorsStopTime, ICIMOut.sensorsReceivable, ICIMOut.payStatus, ICIMOut.isPay, ICIMOut.feeType, ICIMOut.stopTime, Convert.ToDecimal(ICIMOut.money), ICIMOut.cardNo, ICIMOut.userid);

                        //ICIMOutList.Add(ICIMOut);

                    }


                }
            }
            //if (ICIMInList!=null)
            //{

            //}
            //if (ICIMOutList!=null)
            //{

            //}

        }


        /// <summary>
        /// 回复2016年4月12号补缴失败的数据恢复
        /// </summary>
        public void CheckFeeBack()
        {
            string sqlStrFeeBack = @"select a.id,a.parameters,a.userid,B.tenantid from(
select distinct parameters,userid, "; sqlStrFeeBack += " substring(SUBSTRING(parameters,1,charindex('\",\"id',parameters)-1),10,100) as id from feeBackAuditlog) as a left join (select * from abpbusinessdetail where id in(select distinct id from(select distinct parameters,userid,";
            sqlStrFeeBack += " substring(SUBSTRING(parameters,1,charindex('\",\"id',parameters)-1),10,100) as id from feeBackAuditlog) as a ) and repayment is  null) as B on a.id=B.id where a.id in(select id from abpbusinessdetail where id in(select distinct id from(select distinct parameters,userid,";
            sqlStrFeeBack += " substring(SUBSTRING(parameters,1,charindex('\",\"id',parameters)-1),10,100) as id from feeBackAuditlog) as a ) and repayment is  null ) order by a.id ";

            List<FeeBackBusiness> FeeBackData = Context.Database.SqlQuery<FeeBackBusiness>(sqlStrFeeBack).ToList();


            //List<InsertCarInModel> ICIMInList = new List<InsertCarInModel>();
            //List<InsertCarOutModel> ICIMOutList = new List<InsertCarOutModel>();
            if (FeeBackData != null)
            {
                foreach (var s in FeeBackData)
                {
                    //DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                    //string sIn = s.parametersIn;
                    //JsonReader reader = new JsonReader(new StringReader(sIn));

                    if (s.parameters != null)
                    {
                        FeeBackData FBD = JsonConvert.DeserializeObject<FeeBackData>(s.parameters);//进场数据
                                                                                                   //decimal BeforeDiscount, decimal DiscountMoney, double DiscountRate, long UserId, string DeviceCode, int TenantId
                        FBD.BeforeDiscount = FBD.Repayment;
                        FBD.DiscountMoney = 0;
                        FBD.DiscountRate = 10;
                        FBD.UserId = Convert.ToInt64(s.userid);
                        FBD.DeviceCode = "AAAAAA";
                        FBD.TenantId = s.tenantid;
                        Boolean b = DiscountFeeBack(FBD.id, FBD.Repayment, FBD.CarRepaymentTime, FBD.IsEscapePay, FBD.EscapePayStatus, FBD.PaymentType, FBD.CardNo, FBD.BeforeDiscount, FBD.DiscountMoney, FBD.DiscountRate, FBD.UserId, FBD.DeviceCode, FBD.TenantId);
                    }


                }
            }
        }

        /// <summary>
        /// 费用补缴接口
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Repayment"></param>
        /// <param name="CarRepaymentTime"></param>
        /// <param name="IsEscapePay"></param>
        /// <param name="EscapePayStatus"></param>
        /// <param name="PaymentType"></param>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public bool DiscountFeeBack(int id, decimal Repayment, DateTime CarRepaymentTime, bool IsEscapePay, short EscapePayStatus, short PaymentType, string CardNo, decimal BeforeDiscount, decimal DiscountMoney, double DiscountRate, long UserId, string DeviceCode, int TenantId)
        {
            BMessage BM = new BMessage();
            BusinessDetail entity = new BusinessDetail();
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany, AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark, AbpDataFilters.MustHaveBerthsec);

            entity = _businessDetailRepository.FirstOrDefault(b => b.Id == id);

            if (entity != null)
            {
                if (entity.Arrearage != 0 && (entity.Status == 3 || entity.Status == 5))  //欠费金额不等于0  并且逃逸未收费状态
                {
                    entity.Repayment = Repayment;//补缴金额
                    //entity.Arrearage = entity.Arrearage - Repayment;  //欠费金额等于欠费金额减去补缴金额
                    entity.CarRepaymentTime = CarRepaymentTime;//补缴时间
                    entity.EscapePayStatus = EscapePayStatus;//逃逸追缴支付类型   1.现金支付 2.刷机支付
                    entity.IsEscapePay = IsEscapePay;//逃逸是否支付
                    entity.EscapeOperaId = UserId;//逃逸追缴收费员ID
                    entity.EscapeDeviceCode = DeviceCode;//逃逸追缴设备
                    entity.EscapeTenantId = TenantId;//追缴商户ID
                    entity.PaymentType = PaymentType;// 追缴类型    1.前端追缴   2.后台追缴   3.微信追缴
                    entity.Status = 4;//  2.逃逸已收费
                    entity.EscapeCardNo = CardNo;

                    //钱包扣费记录
                    if (EscapePayStatus == 1)
                    {
                        entity.BeforeDiscount = BeforeDiscount;//折扣前的钱
                        entity.DiscountMoney = DiscountMoney;//折扣金额
                        entity.DiscountRate = DiscountRate;//折扣率
                    }
                    var otherAccount = _otherAccountRepository.FirstOrDefault(c => c.CardNo == CardNo && c.IsActive == true);
                    if (otherAccount != null)
                    {
                        //添加消费记录   缴费
                        DeductionRecord deductionrecord = new DeductionRecord()
                        {
                            OtherAccountId = otherAccount.Id,
                            OperType = 2,
                            Money = Repayment,  //消费金额
                            PayStatus = true,
                            CardNo = CardNo,
                            EmployeeId = UserId,
                            PlateNumber = entity.PlateNumber,
                            Remark = "消费",
                            InTime = CarRepaymentTime
                        };
                        _deductionRecordRepository.Insert(deductionrecord);
                        //if (!string.IsNullOrWhiteSpace(otherAccount.TelePhone))
                        //{
                        //    //欠费追缴短信推送
                        //    _smsManagementAppService.SendSms(new SmsAccountDto() { Destnumbers = otherAccount.TelePhone, Msg = string.Format(P4Consts.ConsumptionPaymentModel, Convert.ToDateTime(CarRepaymentTime).ToString("yyyy年MM月dd日hh时mm分"), Repayment, entity.PlateNumber, otherAccount.Wallet - Repayment) });
                        //}
                        //费用补缴  卡的余额扣去缴费金额
                        otherAccount.Wallet = otherAccount.Wallet - Repayment;
                        _otherAccountRepository.Update(otherAccount);
                        entity.EscapeOtherAccountId = otherAccount.Id;

                        decimal Money = BeforeDiscount - DiscountMoney;
                        entity.Arrearage = Convert.ToDecimal(Money) - entity.Prepaid;   //如f果车主逃逸（未付费）  写入欠费字段
                        entity.BeforeDiscount = BeforeDiscount;//折扣前的钱
                        entity.DiscountMoney = DiscountMoney;//折扣金额
                        entity.DiscountRate = DiscountRate;//折扣率
                        entity.Money = Convert.ToDecimal(Money);
                    }
                    _businessDetailRepository.Update(entity);
                    //BM.B_success = true;
                    //BM.SuccessMessage = "费用补缴成功！";
                    //return BM;
                    return true;
                }
                else
                {
                    return false;
                    //throw new AbpAuthorizationException("补缴失败：该车没有欠费", "24");
                    //BM.B_success = false;
                    //BM.Code = 100;
                    //BM.ErrorMessage = "该车没有欠费！";
                    //return BM;
                }
            }

            return false;
            //throw new AbpAuthorizationException("补缴失败：未找到该车记录！", "25");
            //BM.B_success = false;
            //BM.Code = 200;
            //BM.ErrorMessage = "未找到该车记录！";
            //return BM;
        }


        /// <summary>
        /// 处理guid重复的数据
        /// </summary>
        /// <param name="exceutiontimeBegin"></param>
        /// <param name="exceutiontimeEnd"></param>
        public void CheckGuidRepeat(DateTime exceutiontimeBegin, DateTime exceutiontimeEnd)
        {
            string sqlStrCarInAndOut = @"
select a.methodname as methodnameIn,b.methodname as methodnameOut,a.userid,a.parameters as parametersIn,b.parameters as parametersOut from (
select distinct methodname,userid,parameters from day06$ where executiontime>@exceutiontimeBegin and executiontime<@exceutiontimeEnd and exception is not null and methodname='InsertCarIn'  and tenantid=2 and  exception like'%Abp.Authorization.AbpAuthorizationException: 入场失败：guid已经存在%') as a left join(select distinct methodname,userid,parameters from day06$ where executiontime>@exceutiontimeBegin and executiontime<@exceutiontimeEnd and exception is not null and methodname='InsertCarOut'  and tenantid=2 and  exception like'%Abp.Authorization.AbpAuthorizationException: 出场失败：该数据已出场%') as b on a.userid=b.userid";
            List<CarInAndCarOut> CarInOutData = Context.Database.SqlQuery<CarInAndCarOut>(sqlStrCarInAndOut, new SqlParameter[] { new SqlParameter("@exceutiontimeBegin", exceutiontimeBegin), new SqlParameter("@exceutiontimeEnd", exceutiontimeEnd) }).ToList();


            List<InsertCarInModel> ICIMInList = new List<InsertCarInModel>();
            List<InsertCarOutModel> ICIMOutList = new List<InsertCarOutModel>();
            if (CarInOutData != null)
            {
                foreach (var s in CarInOutData)
                {
                    //DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                    //string sIn = s.parametersIn;
                    //JsonReader reader = new JsonReader(new StringReader(sIn));

                    if (s.parametersIn != null && s.parametersOut != null)
                    {
                        InsertCarInModel incar = JsonConvert.DeserializeObject<InsertCarInModel>(s.parametersIn); //同样是进场数据
                        InsertCarOutModel inout = JsonConvert.DeserializeObject<InsertCarOutModel>(s.parametersOut);//同样出场数据
                        if (incar.guid == inout.guid)
                        {
                            InsertCarInModel ICIMIn = JsonConvert.DeserializeObject<InsertCarInModel>(s.parametersIn);//进场数据
                            Guid guid = Guid.NewGuid();
                            ICIMIn.guid = guid.ToString();
                            ICIMIn.userid = s.userid.ToString();
                            Boolean b = InsertCarIn(ICIMIn.berthNumber, ICIMIn.plateNumber, ICIMIn.carType, ICIMIn.prepaid, ICIMIn.carInTime, ICIMIn.guid, ICIMIn.sensorsInCarTime, ICIMIn.stopType, ICIMIn.cardNo, ICIMIn.userid, ICIMIn.regionId, ICIMIn.parkId, ICIMIn.berthsecId);
                            //ICIMInList.Add(ICIMIn);

                            InsertCarOutModel ICIMOut = JsonConvert.DeserializeObject<InsertCarOutModel>(s.parametersOut);//出场数据
                            ICIMOut.guid = guid.ToString();
                            ICIMOut.userid = s.userid.ToString();
                            Boolean a = InsertCarOut(Convert.ToDecimal(ICIMOut.receivable), ICIMOut.factReceive, ICIMOut.carOutTime, ICIMOut.carPayTime, ICIMOut.guid, ICIMOut.sensorsOutCarTime, ICIMOut.sensorsStopTime, ICIMOut.sensorsReceivable, ICIMOut.payStatus, ICIMOut.isPay, ICIMOut.feeType, ICIMOut.stopTime, Convert.ToDecimal(ICIMOut.money), ICIMOut.cardNo, ICIMOut.userid);
                        }


                        //ICIMOutList.Add(ICIMOut);

                    }


                }
            }
            //if (ICIMInList!=null)
            //{

            //}
            //if (ICIMOutList!=null)
            //{

            //}

        }
        /// <summary>
        /// 车辆入场接口
        /// </summary>
        /// <param name="BerthNumber"></param>
        /// <param name="PlateNumber"></param>
        /// <param name="CarType"></param>
        /// <param name="Prepaid"></param>
        /// <param name="CarInTime"></param>
        /// <param name="guid"></param>
        /// <param name="SensorsInCarTime"></param>
        /// <param name="StopType"></param>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        public bool InsertCarIn(string BerthNumber, string PlateNumber, string CarType, string Prepaid, string CarInTime, string guid, string SensorsInCarTime, string StopType, string CardNo, string inOperaId, string RegionId, string ParkId, string berthsecId)
        {


            //缺少分公司id  和  商户ID
            Guid g = new Guid(guid);
            int count = 0;

            var berthmodel = _berthRepository.FirstOrDefault(entry => entry.BerthNumber == BerthNumber && entry.TenantId == 2 && entry.CompanyId == 9);

            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany, AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark, AbpDataFilters.MustHaveBerthsec))
            {
                count = _businessDetailRepository.GetAll().Where(a => a.guid == g).Count();
            }
            if (count > 0)
            {
                throw new AbpAuthorizationException("入场失败：guid已经存在！", "20");
            }

            short PrepaidPayStatus;
            if (CardNo != "0")
            {
                PrepaidPayStatus = 2;// 卡号不等于0  支付类型为2属刷卡支付
            }
            else
            {
                PrepaidPayStatus = 1;//卡号等于0  支付类型为1属现金支付
            }

            DateTime? SensorsInCarTime1 = null;

            //if (berthmodel == default(Berth))
            //{
            //    SensorsInCarTime1 = null;
            //}
            //else
            //{
            //    if (berthmodel.ParkStatus == 1 && !string.IsNullOrWhiteSpace(berthmodel.SensorNumber))
            //    {
            //        SensorsInCarTime1 = berthmodel.SensorsInCarTime;
            //    }
            //}
            BusinessDetail entity = new BusinessDetail()
            {
                BerthNumber = BerthNumber,//泊位号 
                PlateNumber = PlateNumber,//车牌号
                CarType = Convert.ToInt16(CarType),//车辆类型
                Prepaid = Convert.ToDecimal(Prepaid),//预付费
                CarInTime = Convert.ToDateTime(CarInTime),//车辆入场时间
                InOperaId = Convert.ToInt64(inOperaId),//入场收费员ID
                InDeviceCode = "BWB1401899",//入场设备  (没有设备编号参数，随便写一个了)
                guid = new Guid(guid),//
                SensorsInCarTime = SensorsInCarTime1,//车检器入场时间
                StopType = Convert.ToInt16(StopType),//停车类型（是否为包月车）
                RegionId = berthmodel.RegionId,//区域ID
                ParkId = berthmodel.ParkId,//停车场ID
                BerthsecId = berthmodel.BerthsecId,//泊位段ID
                Status = 1,
                TenantId = 2,
                CompanyId = 9,
                PrepaidCarNo = CardNo,  //进场卡号
                PrepaidPayStatus = PrepaidPayStatus   //进场支付类型
            };

            //_appBerthRepository.CarInUpdateBerhs(BerthNumber, entity.BerthsecId, PlateNumber, Convert.ToDateTime(CarInTime), g, Convert.ToInt16(CarType), CardNo, Convert.ToDecimal(Prepaid));

            //钱包扣费记录
            var otherAccount = _otherAccountRepository.FirstOrDefault(c => c.CardNo == CardNo && c.IsActive == true);//  && c.IsEnabled == true
            if (otherAccount != null)
            {
                //if (otherAccount.Wallet > P4Consts.AccountLowMoney) //余额大于最低限额   可以预付
                //{
                DeductionRecord deductionrecord = new DeductionRecord()
                {
                    OtherAccountId = otherAccount.Id,
                    OperType = 3,
                    Money = Convert.ToDecimal(Prepaid),
                    PayStatus = true,
                    CardNo = CardNo,
                    EmployeeId = Convert.ToInt64(inOperaId),
                    PlateNumber = PlateNumber,
                    Remark = "预付",
                    TenantId = 2,

                    InTime = Convert.ToDateTime(CarInTime)
                };
                _deductionRecordRepository.Insert(deductionrecord);
                //预付  短信发送
                //_smsManagementAppService.SendSms(new SmsAccountDto() { Destnumbers =otherAccount.TelePhone, Msg = string.Format(P4Consts.PaymentModel,Convert.ToDateTime(CarInTime).ToString("yyyy年MM月dd日hh时mm分"),BerthNumber,Prepaid) });
                //}
                otherAccount.Wallet = otherAccount.Wallet - Convert.ToDecimal(Prepaid);//车辆入场 账户余额扣除预付
                _otherAccountRepository.Update(otherAccount);

                //车辆入场处理ExtOtherPlateNumber  车牌与账户的关联表  如果不存在记录的话，添加一条卡号车牌相关联记录
                var otherPlate = _otherPlateNumberRepository.FirstOrDefault(a => a.AssignedOtherAccountId == otherAccount.Id && a.IsActive == true);
                if (otherPlate == null)
                {
                    OtherPlateNumber otherplate = new OtherPlateNumber();
                    otherplate.AssignedOtherAccountId = otherAccount.Id;
                    otherplate.PlateNumber = PlateNumber;
                    otherplate.CarColor = 1;  //车的颜色
                    otherplate.CarType = 1;  //车类型
                    otherplate.Order = 1;
                    otherplate.IsActive = true;
                    otherplate.CreatorUserId = Convert.ToInt64(inOperaId);
                    otherplate.CreationTime = Convert.ToDateTime(CarInTime);
                    otherplate.IsDeleted = false;
                    _otherPlateNumberRepository.Insert(otherplate);
                }
            }
            else if (CardNo != "0" && otherAccount == null) //刷卡传了卡号，但是数据库中没有找到数据 
            {
                entity.Prepaid = 0;  //预付费为0
            }

            //车检器明细表同步车牌号
            //var SBD = _sensorBusinessDetailRepository.FirstOrDefault(entity1 => entity1.guid == g);
            //if (SBD != default(SensorBusinessDetail))
            //{
            //    SBD.PlateNumber = PlateNumber;
            //    _sensorBusinessDetailRepository.Update(SBD);
            //}

            ////车检器表同步车牌号
            //var S = _sensorRepository.FirstOrDefault(sersor => sersor.BerthsecId == entity.BerthsecId && sersor.BerthNumber == entity.BerthNumber);
            //if (S != default(Sensor))
            //{
            //    S.RelateNumber = PlateNumber;
            //    _sensorRepository.Update(S);
            //}

            _businessDetailRepository.Insert(entity);

            _unitOfWorkManager.Current.SaveChanges();
            //BMessage bMess = new BMessage()
            //{
            //    B_success = true,
            //    SuccessMessage = "进场成功！"
            //};
            //return bMess; 
            return true;
        }



        /// <summary>
        /// 车辆出场接口
        /// </summary>
        /// <param name="Receivable"></param>
        /// <param name="FactReceive"></param>
        /// <param name="CarOutTime"></param>
        /// <param name="CarPayTime"></param>
        /// <param name="guid"></param>
        /// <param name="SensorsOutCarTime"></param>
        /// <param name="SensorsStopTime"></param>
        /// <param name="SensorsReceivable"></param>
        /// <param name="PayStatus"></param>
        /// <param name="IsPay"></param>
        /// <param name="FeeType"></param>
        /// <param name="StopTime"></param>
        /// <param name="Money"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public bool InsertCarOut(decimal Receivable, string FactReceive, string CarOutTime, string CarPayTime, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money, string CardNo, string outOperaId)
        {
            Guid g = new Guid(guid);
            BusinessDetail entity = new BusinessDetail();
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany, AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark, AbpDataFilters.MustHaveBerthsec))
            {
                entity = _businessDetailRepository.FirstOrDefault(b => b.guid == g);
            }
            if (entity == null)
            {
                //throw new AbpAuthorizationException("出场失败：guid不存在！", "22");
                return false;

            }

            if (entity.Status != 1)//已经出场处理
                return false;
            //throw new AbpAuthorizationException("出场失败：该数据已出场！", "201");

            //var berthmodel = _berthRepository.FirstOrDefault(entry => entry.BerthNumber == entity.BerthNumber);

            //DateTime? SensorsOutCarTime1 = null;

            //if (berthmodel == default(Berth))
            //{
            //    SensorsOutCarTime1 = null;
            //}
            //else
            //{
            //    if (!string.IsNullOrWhiteSpace(berthmodel.SensorNumber))
            //    {
            //        SensorsOutCarTime1 = berthmodel.SensorsOutCarTime;
            //    }
            //}

            //    return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 12, Message = "退出失败，在线用户信息已经丢失！ " }), JsonRequestBehavior.AllowGet);
            if (entity != null)
            {
                //PlateNumberDebLock(entity.PlateNumber);//车辆出场的时候 对车辆进行解锁

                entity.OutDeviceCode = "";               //出场设备
                //entity.Receivable = Convert.ToDecimal(Receivable);        //应收

                //entity.Arrearage = Convert.ToDecimal(Arrearage);          //欠费
                entity.ChargeOperaId = Convert.ToInt64(outOperaId);                   //收费操作员Id
                entity.ChargeDeviceCode = "BWB1401899";            //收费员设备
                entity.CarOutTime = Convert.ToDateTime(CarOutTime);         //出场时间
                entity.CarPayTime = Convert.ToDateTime(CarPayTime);         //支付时间
                entity.OutOperaId = Convert.ToInt64(outOperaId);     //出场收费员ID
                entity.guid = new Guid(guid);                               //唯一guid

                entity.SensorsOutCarTime = Convert.ToDateTime(SensorsOutCarTime);//车检器出场时间
                entity.SensorsStopTime = Convert.ToInt32(SensorsStopTime);//车检器停车时长
                entity.SensorsReceivable = Convert.ToDecimal(SensorsReceivable);//车检器应收
                entity.Money = Money;//实际应收金额
                entity.IsPay = Convert.ToBoolean(IsPay);//是否支付
                if (!entity.IsPay)  //未支付
                {
                    entity.Status = 3;
                    entity.EscapeTime = Convert.ToDateTime(CarOutTime);//逃逸时间
                    if (Convert.ToDecimal(Money) > entity.Prepaid)
                    {
                        entity.Arrearage = Convert.ToDecimal(Money) - entity.Prepaid;   //如果车主逃逸（未付费）  写入欠费字段
                    }
                }
                else  //支付状态为true   只是交了部分的费用  还有一定金额的欠费
                {

                    if (Convert.ToDecimal(Money) > Convert.ToDecimal(FactReceive))//总应收大于总实收（属于余额不足）
                    {
                        entity.Status = 5; // 记录状态  1.不完整数据 2.正常收费已缴费 3.逃逸未收费 4.逃逸已收费 5.正常收费，但是余额不足欠费
                        entity.Arrearage = Convert.ToDecimal(Money) - Convert.ToDecimal(FactReceive);
                    }
                    else  //总应收小于等于总实收
                    {
                        entity.Status = 2;
                    }
                }
                if (!string.IsNullOrEmpty(PayStatus))
                {
                    entity.PayStatus = Convert.ToInt16(PayStatus);  /// 出场支付类型（出场应收） 1.现金 2.刷卡 3.在线支付 4.MOne卡
                }
                if (PayStatus == "1")
                {
                    entity.Receivable = Receivable;                             //出场应收 
                    entity.FactReceive = Convert.ToDecimal(FactReceive);        //实收
                }
                entity.FeeType = Convert.ToInt16(FeeType);//费用类型（1.正常收费，2.追缴收费）
                entity.StopTime = Convert.ToInt32(StopTime);//停车时长
                entity.ReceivableCarNo = CardNo;   //出场卡号   如果为0的话，出场支付类型是现金
                //using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany, AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark, AbpDataFilters.MustHaveBerthsec))
                //{
                //    //车辆出场处理泊位表
                //    var berth = _berthRepository.FirstOrDefault(a => a.BerthNumber == entity.BerthNumber && a.IsActive == true && a.BerthsecId == entity.BerthsecId);
                //    if (berth != null)
                //    {
                //        berth.BerthStatus = "2";  //泊位状态
                //        // berth.LastModificationTime = DateTime.Now; //最后修改时间
                //        //berth.LastModifierUserId = AbpSession.UserId;//最后修改用户
                //        berth.RelateNumber = entity.PlateNumber;//车牌号
                //        berth.OutCarTime = Convert.ToDateTime(CarOutTime);//车辆出场时间
                //        //berth.SensorsInCarTime = Convert.ToDateTime(CarOutTime);//车检器出场时间
                //        //berth.ParkStatus = 2;//车检器停车状态
                //        // berth.CardNo = entity.c;//卡号
                //        // berth.Prepaid = Convert.ToDecimal(Prepaid);//预付费
                //        //  berth.SensorGuid = null;

                //        _berthRepository.Update(berth);
                //    }
                //    else
                //    {
                //        throw new AbpAuthorizationException("出场失败：不存在该泊位编号！", "23");
                //        //BMessage bMess = new BMessage()
                //        //{
                //        //    B_success = false,
                //        //    Code=14,
                //        //    ErrorMessage = "未找到改泊位！"
                //        //};
                //        //return bMess; 
                //    }
                //}
                //钱包扣费记录
                var otherAccount = _otherAccountRepository.FirstOrDefault(c => c.CardNo == CardNo && c.IsActive == true); //&& c.IsEnabled == true

                if (otherAccount != null)
                {
                    if (Money == entity.Prepaid) //消费金额等于预缴金额  添加消费记录
                    {
                        //添加消费记录
                        DeductionRecord deductionrecord = new DeductionRecord()
                        {
                            OtherAccountId = otherAccount.Id,
                            OperType = 2,
                            Money = Money - entity.Prepaid,  //消费金额
                            PayStatus = true,
                            CardNo = CardNo,
                            EmployeeId = Convert.ToInt64(outOperaId),
                            PlateNumber = entity.PlateNumber,
                            Remark = "消费",
                            TenantId = 2,

                            InTime = Convert.ToDateTime(CarOutTime)
                        };
                        _deductionRecordRepository.Insert(deductionrecord);
                        if (otherAccount.TelePhone != null)
                        {
                            ////消费金额 以及账户余额（短信发送）
                            //_smsManagementAppService.SendSms(new SmsAccountDto() { Destnumbers = otherAccount.TelePhone, Msg = string.Format(P4Consts.ConsumptionEqualModel, Convert.ToDateTime(CarOutTime).ToString("yyyy年MM月dd日HH时mm分"), entity.BerthNumber, entity.Prepaid, entity.PlateNumber, otherAccount.Wallet, entity.StopTime, entity.Money) });
                        }

                        otherAccount.Wallet = otherAccount.Wallet - deductionrecord.Money;  //账户余额减掉消费金额
                        _otherAccountRepository.Update(otherAccount);
                    }
                    else if (Money < entity.Prepaid)  //消费金额小于预付  （返还）
                    {
                        //添加返还消费记录
                        DeductionRecord dedu = new DeductionRecord()
                        {
                            OtherAccountId = otherAccount.Id,
                            OperType = 4,
                            Money = entity.Prepaid - Money,  //消费金额
                            PayStatus = true,
                            CardNo = CardNo,
                            EmployeeId = Convert.ToInt64(outOperaId),
                            PlateNumber = entity.PlateNumber,
                            Remark = "返还",
                            TenantId = 2,
                            InTime = Convert.ToDateTime(CarOutTime)
                        };
                        _deductionRecordRepository.Insert(dedu);
                        if (otherAccount.TelePhone != null)
                        {
                            ////返回金额 以及账户余额（短信发送）
                            //_smsManagementAppService.SendSms(new SmsAccountDto() { Destnumbers = otherAccount.TelePhone, Msg = string.Format(P4Consts.ConsumptionGreaterModel, Convert.ToDateTime(CarOutTime).ToString("yyyy年MM月dd日HH时mm分"), entity.BerthNumber, entity.Prepaid, dedu.Money, entity.PlateNumber, otherAccount.Wallet + dedu.Money) });
                        }

                        otherAccount.Wallet = otherAccount.Wallet + dedu.Money;  //账户余额加上返还金额
                        _otherAccountRepository.Update(otherAccount);
                    }
                    else if (Money > entity.Prepaid && entity.Status != 5 && entity.Status != 3)  //消费金额大于预付（消费）
                    {
                        //添加消费记录
                        DeductionRecord deductionrecord = new DeductionRecord()
                        {
                            OtherAccountId = otherAccount.Id,
                            OperType = 2,
                            Money = Money - entity.Prepaid,  //消费金额
                            PayStatus = true,
                            CardNo = CardNo,
                            EmployeeId = Convert.ToInt64(outOperaId),
                            PlateNumber = entity.PlateNumber,
                            Remark = "消费",
                            TenantId = 2,
                            InTime = Convert.ToDateTime(CarOutTime)
                        };
                        _deductionRecordRepository.Insert(deductionrecord);
                        if (!string.IsNullOrEmpty(otherAccount.TelePhone))
                        {
                            ////消费金额 以及账户余额（短信发送）
                            //_smsManagementAppService.SendSms(new SmsAccountDto() { Destnumbers = otherAccount.TelePhone, Msg = string.Format(P4Consts.ConsumptionLessModel, Convert.ToDateTime(CarOutTime).ToString("yyyy年MM月dd日HH时mm分"), entity.BerthNumber, entity.Prepaid, deductionrecord.Money, entity.PlateNumber, otherAccount.Wallet - deductionrecord.Money) });
                        }
                        otherAccount.Wallet = otherAccount.Wallet - deductionrecord.Money;  //账户余额减掉消费金额
                        _otherAccountRepository.Update(otherAccount);
                    }
                    entity.Receivable = Receivable;                             //出场应收 
                    entity.FactReceive = Convert.ToDecimal(FactReceive);        //实收
                }
                else if (CardNo != "0" && otherAccount == null)  //刷卡出场，但是数据库当中没有卡记录 当做欠费处理
                {
                    entity.Status = 3;
                    entity.EscapeTime = Convert.ToDateTime(CarOutTime);//逃逸时间
                    if (Convert.ToDecimal(Money) > entity.Prepaid)
                    {
                        entity.Arrearage = Convert.ToDecimal(Money) - entity.Prepaid;   //如果车主逃逸（未付费）  写入欠费字段
                    }
                }
                _businessDetailRepository.Update(entity);

                //BMessage bM = new BMessage()
                //{
                //    B_success = true,
                //    SuccessMessage = "出场成功"
                //};
                //return bM;
            }
            //throw new AbpAuthorizationException("出场失败！", "20");
            return true;
        }


        /// <summary>
        /// 获取大额订单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetBusinessDetailsOutput GetSubstantialOrderListSql(GetBusinessDetailsInput input)
        {
            List<SqlParameter> SqlParam = new List<SqlParameter>();
            DbRawSqlQuery<BusinessDetailsDto> BusinessTable = null;
            IQueryable<BusinessDetail> temp = Table;
            if (input.filters == null)//自定义查询
            {
                string StrSql = @"
select B.Id,B.Arrearage,B.BerthNumber,B.PlateNumber,B.RegionId,B.ParkId,B.BerthsecId,
                B.CarType,B.Prepaid, B.Receivable,B.CarInTime, B.Money,
                B.CarOutTime,B.CarPayTime,B.guid, EA.TrueName as InEmployeeName,
                B.InDeviceCode,
                EB.TrueName as OutEmployeeName,
                B.OutDeviceCode,
                EC.TrueName as ChargeEmployeeName,
                B.ChargeDeviceCode,
                B.FactReceive,
                P.ParkName as ParkName,
                B.Status,
                Berthsecs.berthsecname as BerthsecName,
                R.regionname as RegionName,
                B.StopTime,
                T.Name as Tenant,
                B.StopType,B.PayStatus,
                B.SensorsInCarTime,B.SensorsOutCarTime,
                B.SensorsStopTime,B.SensorsReceivable,B.RowNumber from (
select *,ROW_NUMBER() OVER(Order by Id DESC ) AS RowNumber from AbpBusinessDetail where ";

                StrSql += @" companyid in (" + string.Join(",", input.CompanyIds) + ") and money > " + P4.P4Consts.SubstantialOrder;
                //SqlParam.Add(new SqlParameter("@companyid", string.Join(",", input.CompanyIds)));

                if (input.TenantId.HasValue)
                {
                    StrSql += " and tenantid = @tenantid ";
                    SqlParam.Add(new SqlParameter("@tenantid", input.TenantId));
                }
                StrSql += @" and  carpaytime > @BeginTime and carpaytime < @EndTime
and isdeleted=0 ";
                SqlParam.Add(new SqlParameter("@BeginTime", input.begindt));
                SqlParam.Add(new SqlParameter("@EndTime", input.enddt));
                if (!string.IsNullOrWhiteSpace(input.RegionId.ToString()) && input.RegionId.ToString() != "0")
                {
                    StrSql += @" and RegionId = @RegionId ";
                    SqlParam.Add(new SqlParameter("@RegionId", input.RegionId));
                }
                if (!string.IsNullOrWhiteSpace(input.ParkId.ToString()) && input.ParkId.ToString() != "0")
                {
                    StrSql += @" and ParkId=@ParkId ";
                    SqlParam.Add(new SqlParameter("@ParkId", input.ParkId));
                }
                if (!string.IsNullOrWhiteSpace(input.BerthsecId.ToString()) && input.BerthsecId.ToString() != "0")
                {
                    StrSql += @" and BerthsecId=@BerthsecId ";
                    SqlParam.Add(new SqlParameter("@BerthsecId", input.BerthsecId));
                }
                if (!string.IsNullOrWhiteSpace(input.EmployeeId.ToString()) && input.EmployeeId.ToString() != "0")
                {
                    StrSql += @" and InOperaId=@InOperaId ";
                    SqlParam.Add(new SqlParameter("@InOperaId", input.EmployeeId));
                }
                if (!string.IsNullOrWhiteSpace(input.BerthNumber))
                {
                    StrSql += @" and berthnumber=@berthnumber ";
                    SqlParam.Add(new SqlParameter("@berthnumber", input.BerthNumber));
                }
                if (!string.IsNullOrWhiteSpace(input.PlateNumber))
                {
                    StrSql += @" and platenumber=@platenumber ";
                    SqlParam.Add(new SqlParameter("@platenumber", input.PlateNumber));
                }
                if (!string.IsNullOrWhiteSpace(input.StopType.ToString()) && input.StopType.ToString() != "0")
                {
                    StrSql += @" and StopType = @StopType ";
                    SqlParam.Add(new SqlParameter("@StopType", input.StopType));
                }
                if (!string.IsNullOrWhiteSpace(input.PayStatus.ToString()) && input.PayStatus.ToString() != "0")
                {
                    StrSql += @" and PayStatus = @PayStatus ";
                    SqlParam.Add(new SqlParameter("@PayStatus", input.PayStatus));
                }



                StrSql += @" ) as B left join AbpRegions as R on B.regionid=R.id left join AbpEmployees as EA on B.InOperaId=EA.id left join AbpEmployees as EB on B.OutOperaId=EB.id left join AbpEmployees as EC on B.ChargeOperaId=EC.id left join AbpParks as P on B.parkid=P.id left join AbpBerthsecs as Berthsecs on B.berthsecid=Berthsecs.id left join AbpTenants as T on B.tenantid=T.id where RowNumber between @BeginSize and @EndSize";
                SqlParam.Add(new SqlParameter("@BeginSize", input.page * input.rows - input.rows + 1));
                SqlParam.Add(new SqlParameter("@EndSize", input.page * input.rows));
                SqlParameter[] param = SqlParam.ToArray();
                BusinessTable = Context.Database.SqlQuery<BusinessDetailsDto>(StrSql, param);
                var rows = BusinessTable.ToList();
                List<BusinessDetailsDto> BBBB = GetSubstantialOrderCount(input);
                int records = 0;
                BusinessDetailUserData bdud = new BusinessDetailUserData();
                bdud.PlateNumber = "金额汇总";
                if (BBBB.Count > 0)
                {
                    records = BBBB[0].BusinessCount;
                    bdud.Prepaid = BBBB[0].Prepaid;
                    bdud.Receivable = BBBB[0].Receivable;
                    bdud.FactReceive = BBBB[0].FactReceive;
                    bdud.Arrearage = BBBB[0].Arrearage;
                    bdud.Money = BBBB[0].Money;
                }
                return new GetBusinessDetailsOutput()
                {
                    rows = rows,
                    records = records,
                    total = records / input.rows + (records % input.rows != 0 ? 1 : 0),
                    userdata = bdud
                };
            }
            else
            {
                var query = temp.Select(entity => new BusinessDetailsDto()
                {
                    Id = entity.Id,
                    Arrearage = entity.Arrearage,
                    BerthNumber = entity.BerthNumber,
                    PlateNumber = entity.PlateNumber,
                    RegionId = entity.RegionId,
                    ParkId = entity.ParkId,
                    BerthsecId = entity.BerthsecId,
                    CarType = entity.CarType,
                    Prepaid = entity.Prepaid,
                    Receivable = entity.Receivable,
                    CarInTime = entity.CarInTime,
                    CarOutTime = entity.CarOutTime,
                    CarPayTime = entity.CarPayTime,
                    guid = entity.guid,
                    InEmployeeName = entity.InEmployee.TrueName,
                    InDeviceCode = entity.InDeviceCode,
                    OutEmployeeName = entity.OutEmployee.TrueName,
                    OutDeviceCode = entity.OutDeviceCode,
                    ChargeEmployeeName = entity.ChargeEmployee.TrueName,
                    ChargeDeviceCode = entity.ChargeDeviceCode,
                    FactReceive = entity.FactReceive,
                    ParkName = entity.Park.ParkName,
                    Status = entity.Status,
                    Money = entity.Money,
                    BerthsecName = entity.Berthsec.BerthsecName,
                    RegionName = Context.Set<Regions.Region>().FirstOrDefault(e => e.Id == entity.RegionId).RegionName,
                    StopTime = entity.StopTime,
                    Tenant = entity.Tenant.Name,
                    StopType = entity.StopType,
                    PayStatus = entity.PayStatus,
                    SensorsInCarTime = entity.SensorsInCarTime,
                    SensorsOutCarTime = entity.SensorsOutCarTime,
                    SensorsStopTime = entity.SensorsStopTime,
                    SensorsReceivable = entity.SensorsReceivable
                }).Filters(input);
                int records = query.Count();
                BusinessDetailUserData bdud = new BusinessDetailUserData();
                bdud = GetBusinessDetailUserD(input);
                return new GetBusinessDetailsOutput()
                {
                    rows = query.Orders(input).PageBy(input).ToList(),
                    records = records,
                    total = records / input.rows + (records % input.rows != 0 ? 1 : 0),
                    userdata = bdud
                };
            }
        }

        /// <summary>
        /// 欠费排行
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetEscapeRankOutput GetEscapeRankList(GetEscapeRankInput input)
        {
            var temp = Table.Filters(input).Where(entity => entity.Status == 3);
            var exp = from p in temp
                      group p by p.PlateNumber
                           into g
                      select new EscapeRankDto
                      {
                          Id = 1,
                          PlateNumber = g.Key,
                          Money = g.Sum(a => a.Arrearage),
                          Times = g.Count(),
                          Average = g.Sum(a => a.Arrearage) / g.Count()
                      };
            int records = exp.Count();
            var money = exp.Sum(e => e.Money);
            var times = exp.Sum(e => e.Times);
            return new GetEscapeRankOutput()
            {
                rows = exp.Orders(input).PageBy(input).ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0),
                userdata = new EscapeRankDto() { PlateNumber = "合计", Money = money, Times = times }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllInspectorChargesOutput GetMoneyTotalGroupbyInspector(InspectorChargeInput input)
        {
            List<SqlParameter> SqlParam = new List<SqlParameter>();
            DbRawSqlQuery<InspectorChargesDto> inspector = null;

            string sqlwhere = " and InTime > @beginTime and InTime < @endTime";
            if (input.TenantId.HasValue)
            {
                sqlwhere += " and TenantId = @TenantId ";
                SqlParam.Add(new SqlParameter("@TenantId", input.TenantId.Value));
            }

            if (input.CompanyId.HasValue)
            {
                sqlwhere += @" and CompanyId = @CompanyId ";
                SqlParam.Add(new SqlParameter("@CompanyId", input.CompanyId));
            }

            SqlParam.Add(new SqlParameter("@beginTime", input.begindt));

            SqlParam.Add(new SqlParameter("@endTime", input.enddt));

            string sqlstr = "select UserName, id as InOperaId, 0 as XJSumFactReceive, 0 as SKSumFactReceive, 0 as SumMoney, 0 as SumArrearage,0 as XJSumRepayment, 0 as SKSumRepayment, 0 as SumFactReceive, Truename as ChargeOperaName, '' as chuchangName, '' as bujiaoName, isnull( CardMoney, 0) as CardMoney from AbpInspectors left join(select sum(Money) as CardMoney, EmployeeId from AbpDeductionRecords with(nolock) where OperType = 1 and EmployeeId <> 0 " + sqlwhere + " group by EmployeeId) as DR on DR.EmployeeId = id ";

            if (!string.IsNullOrWhiteSpace(input.employeeIdInput) && input.employeeIdInput != "0")
            {
                sqlstr += @" where id =  " + input.employeeIdInput;
            }
            SqlParameter[] param = SqlParam.ToArray();
            inspector = Context.Database.SqlQuery<InspectorChargesDto>(sqlstr, param);
            var entry = inspector.ToList();
            int records = GetInspectorCount(input);
            InspectorUserData eud = GetInspectorMoneyCount(input);
            return new GetAllInspectorChargesOutput()
            {
                rows = entry,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0),
                userdata = eud
            };
        }

        /// <summary>
        /// 返回所查的收费员总数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int GetInspectorCount(InspectorChargeInput input)
        {

            List<SqlParameter> Sqlparam = new List<SqlParameter>();
            DbRawSqlQuery<InspectorChargesDto> inspector = null;

            string sqlwhere = "";
            if (input.TenantId.HasValue)
            {
                sqlwhere = " and TenantId = @TenantId ";
                Sqlparam.Add(new SqlParameter("@TenantId", input.TenantId.Value));
            }

            if (input.CompanyId != null)
            {
                sqlwhere += @" and CompanyId = @CompanyId ";
                Sqlparam.Add(new SqlParameter("@CompanyId", input.CompanyId));
            }

            string sqlstr = "select Truename as UserName, id as InOperaId, 0 as XJSumFactReceive, 0 as SKSumFactReceive, 0 as SumMoney, 0 as SumArrearage,0 as XJSumRepayment, 0 as SKSumRepayment, 0 as SumFactReceive, '' as ChargeOperaName, '' as chuchangName, '' as bujiaoName, isnull( CardMoney, 0) as CardMoney from AbpInspectors left join(select sum(Money) as CardMoney, EmployeeId from AbpDeductionRecords with(nolock) where OperType = 1 and EmployeeId <> 0 " + sqlwhere + " group by EmployeeId) as DR on DR.EmployeeId = id";
            SqlParameter[] param = Sqlparam.ToArray();
            inspector = Context.Database.SqlQuery<InspectorChargesDto>(sqlstr, param);
            int count = inspector.ToList().Count();
            return count;
        }


        /// <summary>
        /// 巡查员所收的钱  汇总
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public InspectorUserData GetInspectorMoneyCount(InspectorChargeInput input)
        {
            List<SqlParameter> SqlParam = new List<SqlParameter>();
            DbRawSqlQuery<InspectorChargesDto> inspector = null;

            string sqlwhere = "";
            if (input.TenantId.HasValue)
            {
                sqlwhere = " and TenantId = @TenantId ";
                SqlParam.Add(new SqlParameter("@TenantId", input.TenantId.Value));
            }

            string sqlstr = "select Truename as UserName, id as InOperaId, 0 as XJSumFactReceive, 0 as SKSumFactReceive, 0 as SumMoney, 0 as SumArrearage,0 as XJSumRepayment, 0 as SKSumRepayment, 0 as SumFactReceive, '' as ChargeOperaName, '' as chuchangName, '' as bujiaoName, isnull( CardMoney, 0) as CardMoney from AbpInspectors left join(select sum(Money) as CardMoney, EmployeeId from AbpDeductionRecords with(nolock) where OperType = 1 and EmployeeId <> 0 " + sqlwhere + " group by EmployeeId) as DR on DR.EmployeeId = id";
            SqlParameter[] param = SqlParam.ToArray();
            inspector = Context.Database.SqlQuery<InspectorChargesDto>(sqlstr, SqlParam.ToArray());

            var entry = inspector.ToList();
            InspectorUserData eud = new InspectorUserData();
            string ChargeOperaName = "汇总";
            decimal SumFactReceive = 0;
            decimal? XJSumFactReceive = 0;
            decimal? SKSumFactReceive = 0;
            decimal SumArrearage = 0;
            decimal? SumMoney = 0;
            decimal? XJSumRepayment = 0;
            decimal? SKSumRepayment = 0;
            if (entry.Count() > 0)
            {
                foreach (var e in entry)
                {
                    SumFactReceive += e.SumFactReceive;
                    XJSumFactReceive += e.XJSumFactReceive;
                    SKSumFactReceive += e.SKSumFactReceive;
                    SumArrearage += e.SumArrearage;
                    SumMoney += e.SumMoney;
                    XJSumRepayment += e.XJSumRepayment;
                    SKSumRepayment += e.SKSumRepayment;
                }
            }

            eud.ChargeOperaName = ChargeOperaName;
            eud.SumFactReceive = SumFactReceive;
            eud.XJSumFactReceive = XJSumFactReceive;
            eud.SKSumFactReceive = SKSumFactReceive;
            eud.SumArrearage = SumArrearage;
            eud.SumMoney = SumMoney;
            eud.XJSumRepayment = XJSumRepayment;
            eud.SKSumRepayment = SKSumRepayment;
            return eud;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<InspectorChargesDto> GetMoneyTotalGroupbyInspectorEchars(InspectorChargeInput input)
        {
            var temp = Table.Where(p => p.CarInTime >= input.begindt && p.CarInTime <= input.enddt && p.ChargeEmployee.TrueName != null);
            if (!string.IsNullOrWhiteSpace(input.employeeIdInput) && input.employeeIdInput != "0")
            {
                long employeeId = long.Parse(input.employeeIdInput);
                temp = temp.Where(p => p.ChargeOperaId == employeeId);
            }

            var expr = from p in temp
                       group p by p.ChargeOperaId
                           into g
                       select new InspectorChargesDto
                       {
                           ChargeOperaName = Context.Set<Inspectors.Inspector>().FirstOrDefault(e => e.Id == g.Key) == default(Inspectors.Inspector) ? "" : Context.Set<Inspectors.Inspector>().FirstOrDefault(e => e.Id == g.Key).TrueName,
                           SumFactReceive = 0,
                           XJSumFactReceive = 0,
                           SKSumFactReceive = 0,
                           //XJSumFactReceive = 40,
                           //SKSumFactReceive = 60,
                           SumArrearage = 0,
                           SumMoney = 0
                       };
            return expr.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plateNumber"></param>
        /// <param name="userId"></param>
        /// <param name="TenantId"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public GetEscapeDetailsOutputTow GetEscapeDetailsListByTenant(GetEscapeDetailsInput input)
        {
            string sqlstr = "select * from AbpBusinessDetail with(nolock) where Status = 3 and TenantId <> EscapeTenantId and EscapeTenantId is not null and ";

            List<SqlParameter> SqlParam = new List<SqlParameter>();
            DbRawSqlQuery<InspectorChargesDto> entitylist = Context.Database.SqlQuery<InspectorChargesDto>(sqlstr, SqlParam.ToArray());
            throw new NotImplementedException();
        }

        /// <summary>
        /// 泊位段报表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllParkChargesOutput GetMoneyTotalGroupbyBerthsec(ParkChargeInput input)
        {
            var temp = Table.Where(p => (p.CarPayTime >= input.begindt && p.CarPayTime <= input.enddt)
            || (p.CarRepaymentTime >= input.begindt && p.CarRepaymentTime <= input.enddt));
            if (!string.IsNullOrWhiteSpace(input.berthsecIdInput) && input.berthsecIdInput != "0")
            {
                int berthsecId = int.Parse(input.berthsecIdInput);
                temp = temp.Where(p => p.BerthsecId == berthsecId);
            }

            var expr = from p in temp
                       group p by p.BerthsecId
                           into g
                       select new ParkChargesDto
                       {
                           ParkName = Context.Set<Berthsecs.Berthsec>().FirstOrDefault(a => a.Id == g.Key) == default(Berthsecs.Berthsec) ? "" : Context.Set<Berthsecs.Berthsec>().FirstOrDefault(a => a.Id == g.Key).BerthsecName,
                           SumFactReceive = g.Where(p => p.CarPayTime >= input.begindt && p.CarPayTime <= input.enddt).Sum(p => p.FactReceive),
                           XJSumFactReceive = g.Where(p => p.PayStatus == 1 && p.Status == 2 && p.CarPayTime >= input.begindt && p.CarPayTime <= input.enddt).Sum(p => p.FactReceive),
                           SKSumFactReceive = g.Where(p => p.PayStatus == 2 && p.Status == 2 && p.CarPayTime >= input.begindt && p.CarPayTime <= input.enddt).Sum(p => p.FactReceive),
                           OnlineSumFactReceive = g.Where(p => p.PayStatus == 3 && p.Status == 2 && p.CarPayTime >= input.begindt && p.CarPayTime <= input.enddt).Sum(p => p.FactReceive),
                           SumRepayment = g.Where(p => p.EscapePayStatus == 3 && p.CarRepaymentTime >= input.begindt && p.CarRepaymentTime <= input.enddt).Sum(p => p.Repayment.HasValue == true ? p.Repayment.Value : 0),
                           SumArrearage = g.Where(p => p.CarPayTime >= input.begindt && p.CarPayTime <= input.enddt).Sum(p => p.Arrearage),
                           PosTimes = g.Where(p => p.CarPayTime >= input.begindt && p.CarPayTime <= input.enddt).Count(),
                           PosInTimes = g.Where(p => p.CarPayTime >= input.begindt && p.CarPayTime <= input.enddt).Count(p => p.CarInTime >= input.begindt && p.CarInTime <= input.enddt),
                           PosOutTimes = g.Where(p => p.CarPayTime >= input.begindt && p.CarPayTime <= input.enddt).Count(p => p.CarOutTime >= input.begindt && p.CarOutTime <= input.enddt),
                           SensorSumReceivable = Context.Set<SensorBusinessDetail>().Where(entity => entity.BerthsecId == g.Key && entity.CarOutTime >= input.begindt && entity.CarOutTime <= input.enddt).Sum(p => p.Receivable.HasValue ? p.Receivable.Value : 0),
                           SensorTimes = Context.Set<SensorBusinessDetail>().Where(entity => entity.BerthsecId == g.Key && entity.CarOutTime >= input.begindt && entity.CarOutTime <= input.enddt).Count(),
                           AccSumOtherMoney = g.Where(p => p.CarPayTime >= input.begindt && p.CarPayTime <= input.enddt && (p.PayStatus == 0 || p.PayStatus == 1)).Sum(p => p.FactReceive),
                           AccSumFactReceive = g.Where(p => p.CarPayTime >= input.begindt && p.CarPayTime <= input.enddt && p.PayStatus == 4).Sum(p => p.FactReceive),
                           AccSumRepayment = g.Where(p => p.CarRepaymentTime >= input.begindt && p.CarRepaymentTime <= input.enddt && p.EscapePayStatus == 4).Sum(p => p.Repayment.HasValue == true ? p.Repayment.Value : 0),
                           SumMoney = g.Where(p => p.CarPayTime >= input.begindt && p.CarPayTime <= input.enddt).Sum(p => p.Money.HasValue == true ? p.Money.Value : 0)//应收
                       };
            int records = expr.ToList().Count;
            ParkChargesData data = new ParkChargesData
            {
                ParkName = "汇总",
                SumFactReceive = 0,
                XJSumFactReceive = 0,
                SKSumFactReceive = 0,
                OnlineSumFactReceive = 0,
                SumRepayment = 0,
                SumArrearage = 0,
                SumMoney = 0,
                SensorSumReceivable = 0,
                AccSumFactReceive = 0,
                AccSumRepayment = 0,
                AccSumOtherMoney = 0,
                PosInTimes = 0,
                PosOutTimes = 0,
                SensorTimes = 0
            };
            if (records > 0)
            {
                foreach (var item in expr)
                {
                    data.SumFactReceive += item.SumFactReceive.HasValue ? item.SumFactReceive.Value : 0;
                    data.XJSumFactReceive += item.XJSumFactReceive.HasValue ? item.XJSumFactReceive.Value : 0;
                    data.SKSumFactReceive += item.SKSumFactReceive.HasValue ? item.SKSumFactReceive.Value : 0;
                    data.OnlineSumFactReceive += item.OnlineSumFactReceive.HasValue ? item.OnlineSumFactReceive.Value : 0;
                    data.SumRepayment += item.SumRepayment.HasValue ? item.SumRepayment.Value : 0;
                    data.SumArrearage += item.SumArrearage.HasValue ? item.SumArrearage.Value : 0;
                    data.SumMoney += item.SumMoney.HasValue ? item.SumMoney.Value : 0;
                    data.SensorSumReceivable += item.SensorSumReceivable.HasValue ? item.SensorSumReceivable.Value : 0;
                    data.PosInTimes += item.PosInTimes;
                    data.PosOutTimes += item.PosOutTimes;
                    data.SensorTimes += item.SensorTimes;
                    data.AccSumFactReceive += item.AccSumFactReceive.HasValue ? item.AccSumFactReceive.Value : 0;
                    data.AccSumRepayment += item.AccSumRepayment.HasValue ? item.AccSumRepayment.Value : 0;
                    data.AccSumOtherMoney += item.AccSumOtherMoney.HasValue ? item.AccSumOtherMoney.Value : 0;
                }
            }
            return new GetAllParkChargesOutput()
            {
                rows = expr.OrderBy(p => p.SumFactReceive).PageBy(input).ToList(),
                userdata = data,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 欠费报表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllParkChargesOutput GetMonthOwnBerthsec(ParkChargeInput input)
        {
            IQueryable<BusinessDetail> temp = Table;
            if (!string.IsNullOrWhiteSpace(input.berthsecIdInput) && input.berthsecIdInput != "0")
            {
                int berthsecId = int.Parse(input.berthsecIdInput);
                temp = temp.Where(p => p.BerthsecId == berthsecId);
            }
            var expr = from p in temp
                       group p by p.BerthsecId
                into g
                       select new ParkChargesDto
                       {
                           ParkName = Context.Set<Berthsecs.Berthsec>().FirstOrDefault(a => a.Id == g.Key) == default(Berthsecs.Berthsec) ? "" : Context.Set<Berthsecs.Berthsec>().FirstOrDefault(a => a.Id == g.Key).BerthsecName,

                           SumOwn = g.Where(p => ((p.Status == 3 && p.IsDeleted==false) || (p.Status==3 && p.DeletionTime>=input.finishdt && p.DeletionTime<input.startdt)) && p.CreationTime >= input.startdt && p.CreationTime < input.finishdt).Sum(p => p.Arrearage),              
                           SumOwnRec = g.Where(p => p.Status == 4  && p.CreationTime >= input.startdt && p.CreationTime < input.finishdt && p.CarRepaymentTime>= input.finishdt && p.CarRepaymentTime< input.startdt).Sum(p => p.Arrearage),

                           SumRecovered = g.Where(p => p.Status == 4 && p.IsDeleted == false && p.CreationTime >= input.startdt && p.CreationTime < input.finishdt && p.CarRepaymentTime >= input.startdt && p.CarRepaymentTime < input.finishdt).Sum(p =>p.Repayment),
                           SpanSumRecovered = g.Where(p => p.Status == 4 && p.IsDeleted == false && p.CreationTime < input.startdt && p.CarRepaymentTime >= input.startdt && p.CarRepaymentTime < input.finishdt).Sum(p => p.Repayment),
                           ParkId = g.Key,
                           DelSumRecovered = g.Where(p => p.Status == 3 && p.IsDeleted == true && p.DeletionTime >= input.startdt && p.DeletionTime < input.finishdt && DbFunctions.DiffMonths(p.CreationTime,p.DeletionTime) != 0).Sum(p => p.Arrearage)
                       };

            ParkChargesData data = new ParkChargesData
            {
                ParkName = "汇总",
                SumOwn = 0,
                SumRecovered = 0,
                SpanSumRecovered = 0,
                DelSumRecovered = 0
            };
            var rows = expr.OrderByDescending(p => p.SumOwn);//.PageBy(input).ToList();

            if (rows.ToList().Count() > 0)
            {
                foreach (var item in rows)
                {
                    if (item.SumOwn == null)
                    {
                        item.SumOwn = 0;
                    }
                    if(item.SumOwnRec == null)
                    {
                        item.SumOwnRec = 0;
                    }
                    if (item.SumRecovered == null)
                    {
                        item.SumRecovered = 0;
                    }
                    if (item.SpanSumRecovered == null)
                    {
                        item.SpanSumRecovered = 0;
                    }
                    if (item.DelSumRecovered == null)
                    {
                        item.DelSumRecovered = 0;
                    }
                    data.SumOwn += item.SumOwn + item.SumOwnRec;
                    data.SumRecovered += item.SumRecovered;
                    data.SpanSumRecovered += item.SpanSumRecovered;
                    data.DelSumRecovered += item.DelSumRecovered;
                }
            }
            int records = expr.ToList().Count;
            return new GetAllParkChargesOutput()
            {
                rows = rows.PageBy(input).ToList(),
                userdata = data,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 欠费报表详细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ParkChargesDto> GetOwnReportDetail(ParkChargeInput input)
        {
            int berthsecId = int.Parse(input.parkIdInput);
            List<SqlParameter> Sqlparam = new List<SqlParameter>();
            if (input.TenantId.HasValue)
            {
                Sqlparam.Add(new SqlParameter("@TenantId", input.TenantId.Value));
            }
            if (input.CompanyId.HasValue)
            {
                Sqlparam.Add(new SqlParameter("@CompanyId", input.CompanyId.Value));
            }
            Sqlparam.Add(new SqlParameter("@BenginTime", input.begindt));
            Sqlparam.Add(new SqlParameter("@EndTime", input.enddt));
            Sqlparam.Add(new SqlParameter("@BerthsecId", input.parkIdInput));

            string sqlstr = @"select CAST(A.year AS VARCHAR(10))+'-'+CAST(A.month AS VARCHAR(10)) as Month,IsNull(D.DelSumRecovered,0)DelSumRecovered,IsNull(C.SpanSumRecovered,0)SpanSumRecovered,IsNull(B.SumRecovered,0)SumRecovered,IsNull(A.SumOwn,0)SumOwn from (select Sum(Arrearage)SumOwn,year(CreationTime)year,month(CreationTime)month from AbpBusinessDetail with (nolock) where TenantId =@TenantId and CompanyId =@CompanyId and BerthsecId =@BerthsecId and Status =3 and IsDeleted =0 and CreationTime between @BenginTime and @EndTime group by year(CreationTime),month(CreationTime))as A left join (select Sum(Repayment)SumRecovered,year(CreationTime)year,month(CreationTime)month from AbpBusinessDetail with (nolock) where TenantId =@TenantId and CompanyId =@CompanyId and BerthsecId =@BerthsecId and Status =4 and IsDeleted =0 and CreationTime between @BenginTime and @EndTime and CarRepaymentTime between @BenginTime and @EndTime group by year(CreationTime),month(CreationTime)) as B on A.month = B.month left join (select Sum(Repayment)SpanSumRecovered,year(CarRepaymentTime)year,month(CarRepaymentTime)month from AbpBusinessDetail with (nolock) where TenantId =@TenantId and CompanyId =@CompanyId and BerthsecId =@BerthsecId and Status =4 and IsDeleted =0 and CreationTime < @BenginTime and CarRepaymentTime between @BenginTime and @EndTime group by year(CarRepaymentTime),month(CarRepaymentTime)) as C on A.month = C.month left join (select Sum(Arrearage)DelSumRecovered,year(DeletionTime)year,month(DeletionTime)month from AbpBusinessDetail with (nolock) where TenantId =@TenantId and CompanyId =@CompanyId and BerthsecId =@BerthsecId and Status =3 and IsDeleted =1 and DeletionTime between @BenginTime and @EndTime and datediff(month,CreationTime,DeletionTime)<>0  group by year(DeletionTime),month(DeletionTime)) as D on A.month = D.month ";

            var Model = Context.Database.SqlQuery<ParkChargesDto>(sqlstr, Sqlparam.ToArray()).ToList();

            return Model;
        }

        /// <summary>
        /// 获取泊位使用情况
        /// </summary>
        /// <returns></returns>
        public List<BerthsecsUtilization> GetBerthsecsUtilization(int TenantId, int ComplayId)
        {
            var time = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            List<BerthsecsUtilization> list = new List<BerthsecsUtilization>();

            var parkModel = Context.Set<Parks.Park>().Where(entity => entity.IsDeleted == false && entity.TenantId == TenantId && entity.CompanyId == ComplayId).ToList();

            foreach (var item in parkModel)
            {
                BerthsecsUtilization model = new BerthsecsUtilization();
                model.ParkId = item.Id;
                model.ParkName = item.ParkName;
                model.BerthsTotal = item.BerthCount;
                // model.BerthsTotal =Convert.ToInt32(item.BerthCount.Substring(0, item.BerthCount.IndexOf("/")));
                model.BerthsUse = Context.Set<Berths.Berth>().Where(entity => entity.ParkId == item.Id && entity.InCarTime > time && entity.BerthStatus == "1" && entity.TenantId == TenantId && entity.CompanyId == ComplayId && entity.IsActive == true).Count();
                model.BerthsUseness = model.BerthsTotal - model.BerthsUse;
                list.Add(model);
            }
            return list;
        }

        /// <summary>
        /// 获取环比金额
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public List<MoneyChain> GetMoneyChain(int TenantId, int CompanyId)
        {
            var strTime = DateTime.Parse(DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd"));

            List<SqlParameter> SqlParam = new List<SqlParameter>
            {
                new SqlParameter("@strTime", strTime),
                new SqlParameter("@TenantId", TenantId),
                new SqlParameter("@CompanyId", CompanyId)
            };
            string StrSql = @"select Daytime,ISNULL(Sum(FactReceive),0) MoneyTotal from(select convert(varchar(100),t.CarOutTime,23) [Daytime] ,ISNULL(Sum(FactReceive),0) FactReceive from  AbpBusinessDetail t with (nolock) where CompanyId = @CompanyId and TenantId = @TenantId and IsDeleted =0 and CarOutTime >= @strTime group by convert(varchar(100),t.CarOutTime,23) Union select convert(varchar(100),t.CarRepaymentTime,23) [Daytime] ,ISNULL(Sum(Repayment),0) Repayment from  AbpBusinessDetail t with (nolock) where CompanyId = @CompanyId and TenantId = @TenantId and IsDeleted =0 and CarRepaymentTime >= @strTime group by convert(varchar(100),t.CarRepaymentTime,23)) as c group by Daytime order by Daytime desc";
            SqlParameter[] param = SqlParam.ToArray();
            var moneyChain = Context.Database.SqlQuery<MoneyChain>(StrSql, param).ToList();
            return moneyChain;
        }

        /// <summary>
        /// 收费员今日实收
        /// </summary>
        /// <param name="OutOperaId"></param>
        /// <returns></returns>
        public EmployeeFactReceiveDto GetTodayFactReceiveList(int EmployeeId)
        {
            var time = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            EmployeeFactReceiveDto model = new EmployeeFactReceiveDto();
            var EmployeeModel = Context.Set<Employee>().FirstOrDefault(entity => entity.Id == EmployeeId);
            model.EmployeeId = EmployeeId;
            model.EmployName = EmployeeModel.TrueName;
            var FactReceiveModel = Table.Where(entry => entry.OutOperaId == EmployeeId && entry.CreationTime >= time);
            if (FactReceiveModel.ToList().Count() == 0)
                model.FactReceive = 0;
            else
                model.FactReceive = FactReceiveModel.Sum(e => e.FactReceive);

            var RepaymentModel = Table.Where(entry => entry.EscapeOperaId == EmployeeId && entry.CarRepaymentTime >= time);
            if (RepaymentModel.ToList().Count() == 0)
                model.Repayment = 0;
            else
                model.Repayment = RepaymentModel.Sum(e => e.Repayment);
            return model;
        }

        /// <summary>
        /// 停车车次信息
        /// </summary>
        /// <returns></returns>
        public List<StopTimesRankDto> StopTimesRank()
        {
            List<StopTimesRankDto> list = new List<StopTimesRankDto>();
            for (int i = 5; i >= 0; i--)
            {
                var strTime = DateTime.Parse(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(-i).Date.ToString("yyyy-MM-dd"));
                var endime = DateTime.Parse(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(-i + 1).Date.ToString("yyyy-MM-dd"));
                StopTimesRankDto model = new StopTimesRankDto()
                {
                    Month = strTime.ToShortDateString().ToString(),
                    Totals = _businessDetailRepository.GetAll().Where(entity => entity.CreationTime >= strTime && entity.CreationTime < endime).Count()
                };
                list.Add(model);
            }
            return list;
            //var time = DateTime.Parse(DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd 00:00:00"));
            //var expr = from t in Table.Where(entity => entity.CreationTime > time)
            //           group t by  new { OperateDate = DbFunctions.TruncateTime(t.CreationTime) }
            //           into g
            //           select new StopTimesRankDto
            //           {
            //               Time = g.Key.OperateDate,
            //                StopTimes = g.Count()
            //           };
            //return expr.ToList();
        }

        /// <summary>
        /// 大屏统计金额
        /// </summary>
        /// <returns></returns>
        public StatisticsMoneyDto GetStatisticsInfo(int Status)
        {
            var time = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            var query = Table.WhereIf(Status == 0, entity => entity.CreationTime > time);
            if (query.Count() == 0)
                return new StatisticsMoneyDto();
            return new StatisticsMoneyDto
            {
                Money = query.Sum(e => e.Money) ?? 0,
                CashPay = query.Where(e => e.PayStatus == 1).Sum(e => e.FactReceive),
                EscapeMoney = query.Sum(e => e.Arrearage),
                OnlinePay = query.Where(e => e.PayStatus == 3).Count() > 0 ? query.Where(e => e.PayStatus == 3).Sum(e => e.FactReceive) : 0,
                PaymentCashPay = query.Where(e => e.EscapePayStatus == 1).Sum(e => e.Repayment) ?? 0,
                PaymentOnlinePay = query.Where(e => e.EscapePayStatus == 3).Sum(e => e.Repayment) ?? 0
            };
        }
    }
}
