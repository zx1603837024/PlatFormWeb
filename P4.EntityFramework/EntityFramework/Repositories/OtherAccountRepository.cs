using Abp.Domain.Repositories;
using Abp.EntityFramework;
using P4.OtherAccounts;
using P4.OtherAccounts.Dtos;
using P4.OtherPlateNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using System.Data.SqlClient;
using P4.Employees;
using System.Data;


namespace P4.EntityFramework.Repositories
{
    public class OtherAccountRepository : P4RepositoryBase<OtherAccount, long>, IOtherAccountRepository
    {
        #region Var
        private readonly IRepository<OtherAccount, long> _abpOtherAccountRepository;
        private readonly IRepository<DeductionRecord, long> _abpDeductionRecordRepository;
        private readonly IRepository<OtherPlateNumber, long> _abpOtherPlateNumberRepository;

        #endregion
        public OtherAccountRepository(IDbContextProvider<P4DbContext> dbContextProvider, IRepository<OtherAccount, long> abpOtherAccountRepository, IRepository<DeductionRecord, long> abpDeductionRecordRepository,
            IRepository<OtherPlateNumber, long> abpOtherPlateNumberRepository)
            : base(dbContextProvider)
        {
            _abpOtherAccountRepository = abpOtherAccountRepository;
            _abpDeductionRecordRepository = abpDeductionRecordRepository;
            _abpOtherPlateNumberRepository = abpOtherPlateNumberRepository;

        }

        /// <summary>
        /// 卡面查询
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="phone"></param>
        /// <param name="plateNumber"></param>
        /// <returns></returns>
        public OtherAccountDto QueryCard(string cardNo, string phone, string plateNumber, int TenantId)
        {
            var list = Abp.DataProcessHelper.GetEntityFromTable<OtherAccountDto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select CardNo, TelePhone, PlateNumber, Wallet, IsEnabled, AutoDeduction from ExtOtherAccount left join ExtOtherPlateNumber on ExtOtherAccount.Id = ExtOtherPlateNumber.AssignedOtherAccountId where (ExtOtherAccount.CardNo = '" + cardNo + "' or TelePhone = '" + phone + "' or (ExtOtherPlateNumber.PlateNumber = '" + plateNumber + "' and ExtOtherPlateNumber.IsDeleted = 0)) and ExtOtherAccount.IsActive = 1 and ExtOtherAccount.IsDeleted = 0 and ExtOtherAccount.IsEnabled = 1 and TenantId = " + TenantId).Tables[0]);

            if (list.Count > 0)
                return list[0];
            else
                return default(OtherAccountDto);

            IQueryable<OtherAccount> otherAccount = Context.Set<OtherAccount>();
            IQueryable<OtherPlateNumber> otherPlateNumber = Context.Set<OtherPlateNumber>();

            var query = from oa in otherAccount
                        join opn in otherPlateNumber
                           on oa.Id equals opn.AssignedOtherAccountId
                        into oaopn
                        from r1 in oaopn.DefaultIfEmpty()
                        where (((oa.CardNo == cardNo || oa.TelePhone == phone) && oa.IsActive == true)
                        || (r1.PlateNumber == plateNumber && r1.IsActive == true))

                        select new OtherAccountDto
                        {
                            CardNo = oa.CardNo,
                            TelePhone = oa.TelePhone,
                            PlateNumber = r1.PlateNumber,
                            Wallet = oa.Wallet,
                            AutoDeduction = oa.AutoDeduction,
                            IsEnabled = oa.IsEnabled
                        };
            return query.FirstOrDefault();

        }

        /// <summary>
        /// 动态报表 获取账户明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllDeductionRecordOutput GetDynamicReportDeductionRecord(SearchDeductionRecordInput input)
        {
            //int records = _abpDeductionRecordRepository.GetAll().Where(dedu => dedu.InTime >= input.begindt && dedu.InTime <= input.enddt).ToList().Count;

            var list = GetDynamicRDeductionEchar(input);

            int records = list.Count;
            return new GetAllDeductionRecordOutput()
            {
                rows = list,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
        /// <summary>
        /// List<DeductionRecordDto> 分离成一个方法 填充Echar数据
        /// </summary>
        /// <returns></returns>
        public List<DeductionRecordDto> GetDynamicRDeductionEchar(SearchDeductionRecordInput input)
        {
            IQueryable<OtherAccount> otherAccount = Context.Set<OtherAccount>().Where(ota => ota.IsActive == true);
            IQueryable<DeductionRecord> deductionRecord = Context.Set<DeductionRecord>();

            var query = from de in deductionRecord
                        join oa in otherAccount
                        on de.OtherAccountId equals oa.Id
                        into deoa

                        from r1 in deoa.DefaultIfEmpty()
                        select new DeductionRecordDto
                        {
                            Id = de.Id,
                            InTime = de.InTime,//消费时间
                            UserName = r1.UserName,//用户名
                            Name = r1.Name,//姓名
                            CardNo = r1.CardNo,//卡号
                            TelePhone = r1.TelePhone,//手机号码
                            Remark = de.Remark,//消费类型 就是备注
                            Money = de.Money,//费用金额
                            Wallet = r1.Wallet == null ? (decimal)0 : r1.Wallet//账户余额
                        };
            return query.Where(dedu => dedu.InTime >= input.begindt && dedu.InTime <= input.enddt).OrderByDescending(dic => dic.Id).PageBy(input).ToList();
        }
        /// <summary>
        /// 根据商户 进行统计商户时间段内总的消费详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<DeductionRecordDto> GetDynamicRDeduStatisEchar(SearchDeductionRecordInput input, int? tenantID)
        {
            //IQueryable<OtherAccount> otherAccount = Context.Set<OtherAccount>();
            //IQueryable<DeductionRecord> deductionRecord = Context.Set<DeductionRecord>();
            //IQueryable<>
            string sqlStr = @"
select ISNULL(ChongZhiMoney,0) as SumCZ_Money,ISNULL(XiaoFeiMoney,0)as SumXF_Money,
ISNULL(YuFuMoney,0)as SumYF_Money,ISNULL(FanHuangMoney,0)as SumFH_Money,ISNULL(Wallet,0)as SumWallet_Money,c.TenantId,TenancyName from 
((select SUM(Money) as ChongZhiMoney ,ex.TenantId from AbpDeductionRecords as ad  
left join ExtOtherAccount as ex on ad.OtherAccountId=ex.Id where OperType=1 
and InTime between @beginTime and @endTime group by ex.TenantId) 
as c left join 
(select SUM(Money) as XiaoFeiMoney,ex.TenantId from AbpDeductionRecords as ad  
left join ExtOtherAccount as ex on ad.OtherAccountId=ex.Id where OperType=2 
and InTime between @beginTime and @endTime group by ex.TenantId)  
as x on c.TenantId=x.TenantId left join
(select SUM(Money) as YuFuMoney,ex.TenantId from AbpDeductionRecords as ad  
left join ExtOtherAccount as ex on ad.OtherAccountId=ex.Id where OperType=3 
and InTime between @beginTime and @endTime group by ex.TenantId) 
as y on y.TenantId=c.TenantId or y.TenantId=x.TenantId  left join
(select SUM(Money) as FanHuangMoney,ex.TenantId from AbpDeductionRecords as ad  
left join ExtOtherAccount as ex on ad.OtherAccountId=ex.Id where OperType=4 
and InTime between @beginTime and @endTime group by ex.TenantId) 
as f on f.TenantId=y.TenantId or f.TenantId=x.TenantId or f.TenantId=c.TenantId left join
(select SUM(Wallet) as Wallet,ExtOtherAccount.TenantId from ExtOtherAccount where AccountLoginDatetime 
between @beginTime and @endTime group by ExtOtherAccount.TenantId)
as w on w.TenantId=f.TenantId or w.TenantId=c.TenantId or w.TenantId=x.TenantId or w.TenantId=y.TenantId left join
(select TenancyName,Id from AbpTenants)
as t on t.Id=w.TenantId or t.Id=f.TenantId or t.Id=y.TenantId or t.Id=x.TenantId or t.Id=c.TenantId)";
            List<DeductionRecordDto> dr = new List<DeductionRecordDto>();
            if (tenantID == null)
            {
                dr = Context.Database.SqlQuery<DeductionRecordDto>(sqlStr, new SqlParameter[] { 
                    new SqlParameter("@beginTime", input.begindt), 
                    new SqlParameter("@endTime", input.enddt)
                }).ToList();
            }
            else
            {
                sqlStr += " where c.TenantId = @tenantID";
                dr = Context.Database.SqlQuery<DeductionRecordDto>(sqlStr, new SqlParameter[] { 
                    new SqlParameter("@beginTime", input.begindt), 
                    new SqlParameter("@endTime", input.enddt),
                    new SqlParameter("@tenantID", tenantID)
                }).ToList();
            }
            return dr;
        }

        /// <summary>
        /// 老的报表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public List<AccountAndDeductionRecordsDto> GetAccountAndDeductionRecords(SearchDeductionRecordInput input, int? tenantID)
        {
            DateTime FirstDayTime = input.begindt;
            DateTime LastDayTime = input.enddt;
            DateTime nowDate = DateTime.Now;

            List<DeductionRecordDto> Listdto = new List<DeductionRecordDto>();
            List<AccountAndDeductionRecordsDto> ListRTdto = new List<AccountAndDeductionRecordsDto>();

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@FirstDt", FirstDayTime));
            param.Add(new SqlParameter("@LastDt", LastDayTime));
            string sqlwhere = "";
            if (tenantID.HasValue)
            {
                sqlwhere = " and TenantId = @tenantId ";
                param.Add(new SqlParameter("@tenantId", tenantID.Value));
            }

            string sqlStr = @"select  ROW_NUMBER() OVER(Order by id DESC ) AS Id, CardNo,
            isnull(EarlyMonthBalance,0) as EarlyMonthBalance ,
            isnull(TheFirstValue,0) as TheFirstValue,
            isnull(StoredValue ,0) as StoredValue, 
            isnull(CurrentConsumptionValue, 0) as CurrentConsumptionValue ,
            (isnull(TheFirstValue, 0) + isnull(EarlyMonthBalance, 0) + isnull(StoredValue, 0)- isnull(CurrentConsumptionValue, 0)) as TheFinalBalance
            from (select  case when productno is null then CardNo when productno='0' then CardNo  else productno end as cardno, id from 
            extotheraccount where 1 = 1 " + sqlwhere + " and isdeleted=0  ) AS A left join (select AbpDeductionRecords.OtherAccountId, sum( case when opertype = 1 then Money else 0 end  ) - (sum ( case when opertype = 2 and remark='消费' then Money else 0 end  )+sum(case when opertype = 3 and remark='预付' then Money else 0 end )-sum(case when opertype = 4 and remark='返还' then Money else 0 end )) as EarlyMonthBalance from AbpDeductionRecords where InTime between @FirstDt and @LastDt " + sqlwhere + " group by AbpDeductionRecords.OtherAccountId) AS B on A.id = B.OtherAccountId left join ( select AbpDeductionRecords.OtherAccountId,sum ( case when opertype = 1 and remark = '续费' then Money else 0 end  ) as StoredValue, (sum(case when opertype = 2 and remark = '消费' then Money else 0 end  )+sum(case when opertype = 3 and remark='预付' then Money else 0 end )-sum(case when opertype = 4 and remark='返还' then Money else 0 end )) as CurrentConsumptionValue from AbpDeductionRecords  where Intime between @FirstDt  and @LastDt " + sqlwhere + " group by  OtherAccountId) AS D on A.id = D.OtherAccountId left join (select Money as TheFirstValue, OtherAccountId from AbpDeductionRecords where opertype = 1 and remark = '开卡' and Intime between @FirstDt  and @LastDt " + sqlwhere + ") as G on A.Id = G.OtherAccountId";

            ListRTdto = Context.Database.SqlQuery<AccountAndDeductionRecordsDto>(sqlStr, param.ToArray()).ToList();
            return ListRTdto;
        }



        public List<AccountAndDeductionRecordsDto> GetAccountAndDeductionRecordsOnlyMonth(SearchDeductionRecordInput input, int? tenantID)
        {

            DateTime FirstDayTime = input.begindt;
            DateTime LastDayTime = input.enddt;
            int months = (input.enddt.Year - input.begindt.Year) * 12 + (input.enddt.Month - input.begindt.Month);//月数差
            List<DeductionRecordDto> Listdto = new List<DeductionRecordDto>();
            List<AccountAndDeductionRecordsDto> ListRTdto = new List<AccountAndDeductionRecordsDto>();

            string sqlwhere = tenantID.HasValue == true ? " and TenantId = @tenantId " : "";
            DateTime nowdate = DateTime.Now.AddDays(1 - DateTime.Now.Day);  //当前时间月初
            nowdate = nowdate.AddMonths(1);//当前时间月末
            for (int i = 1; i < months + 2; i++)
            {
                List<AccountAndDeductionRecordsDto> ListRTdtoDepent = new List<AccountAndDeductionRecordsDto>();
                DateTime FristMonth = input.begindt.AddDays(1 - input.begindt.Day).AddMonths(i - 1);//月初
                DateTime LastMonth = FristMonth.AddMonths(1);//月末
                string YearMonth = FristMonth.Year + "年" + FristMonth.Month + "月";
                string sqlStr = @"
select @YearMonth as YearMonth,sum(EarlyMonthBalance) as EarlyMonthBalance,sum(TheFirstValue) as TheFirstValue,sum(StoredValue) as StoredValue,sum(CurrentConsumptionValue) as CurrentConsumptionValue,sum(TheFinalBalance) as TheFinalBalance from (select 1 as groupuser,    ROW_NUMBER() OVER(Order by id DESC ) AS Id, CardNo, isnull(EarlyMonthBalance,0) as EarlyMonthBalance ,isnull(TheFirstValue,0) as TheFirstValue,isnull(StoredValue ,0) as StoredValue
, isnull(CurrentConsumptionValue,0) as CurrentConsumptionValue ,(isnull(TheFirstValue,0)+isnull(EarlyMonthBalance,0)+isnull(StoredValue,0)- isnull(CurrentConsumptionValue,0)) as TheFinalBalance
from (
select  case when productno is null then CardNo else productno end as cardno, id from 
extotheraccount where enabledtime < @nowdate " + sqlwhere + " ) AS A left join (select AbpDeductionRecords.OtherAccountId, sum( case when opertype = 1 then Money else 0 end  ) - (sum ( case when opertype = 2 and remark='消费' then Money else 0 end  )+sum(case when opertype = 3 and remark='预付' then Money else 0 end ) - sum(case when opertype = 4 and remark='返还' then Money else 0 end )) as EarlyMonthBalance from AbpDeductionRecords where InTime < @FirstDt " + sqlwhere + " group by AbpDeductionRecords.OtherAccountId) AS B on A.id = B.OtherAccountId left join (select AbpDeductionRecords.OtherAccountId,sum ( case when opertype = 1 and remark='续费' then Money else 0 end  ) as StoredValue, (sum ( case when opertype = 2 and remark='消费' then Money else 0 end  )+sum(case when opertype = 3 and remark='预付' then Money else 0 end )-sum(case when opertype = 4 and remark='返还' then Money else 0 end )) as CurrentConsumptionValue from AbpDeductionRecords  where Intime >= @FirstDt  and Intime<= @LastDt " + sqlwhere + " group by OtherAccountId ) AS D on A.id = D.OtherAccountId left join (select Money as TheFirstValue, OtherAccountId from AbpDeductionRecords where opertype = 1 and remark = '开卡' and Intime >= @FirstDt  and Intime<= @LastDt " + sqlwhere + ") as G on A.Id = G.OtherAccountId ) as chen group by groupuser";

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@FirstDt", FristMonth));
                param.Add(new SqlParameter("@LastDt", LastMonth));
                param.Add(new SqlParameter("@YearMonth", YearMonth));
                param.Add(new SqlParameter("@nowdate", nowdate));

                if (tenantID.HasValue)
                {
                    param.Add(new SqlParameter("@tenantId", tenantID.Value));
                }

                ListRTdtoDepent = Context.Database.SqlQuery<AccountAndDeductionRecordsDto>(sqlStr, param.ToArray()).ToList();

                AccountAndDeductionRecordsDto AADRD = new AccountAndDeductionRecordsDto();
                if (ListRTdtoDepent.Count > 0)
                {
                    AADRD.YearMonth = ListRTdtoDepent[0].YearMonth;
                    AADRD.CardNo = "";
                    AADRD.EarlyMonthBalance = ListRTdtoDepent[0].EarlyMonthBalance;
                    AADRD.TheFirstValue = ListRTdtoDepent[0].TheFirstValue;
                    AADRD.StoredValue = ListRTdtoDepent[0].StoredValue;
                    AADRD.CurrentConsumptionValue = ListRTdtoDepent[0].CurrentConsumptionValue;
                    AADRD.TheFinalBalance = ListRTdtoDepent[0].TheFinalBalance;
                    ListRTdto.Add(AADRD);
                }
            }
            return ListRTdto;
        }

        /// <summary>
        /// 获取所选时间消费数据
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public List<AccountAndDeductionRecordsDto> GetThisAccountAndDeductionRecords(SearchDeductionRecordInput input, int? tenantID)
        {
            return null;
        }

        /// <summary>
        /// 获取所选时间上一月份的数据
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public List<AccountAndDeductionRecordsDto> GetLastMonthAccountAndDeductionRecords(SearchDeductionRecordInput input, int? tenantID)
        {

            return null;
            //return new List<AccountAndDeductionRecordsDto>();
        }

        /// <summary>
        /// 获取所选时间下一月的数据
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public List<AccountAndDeductionRecordsDto> GetNextMonthAccountAndDeductionRecords(SearchDeductionRecordInput input, int? tenantID)
        {


            return null;
            //return new List<AccountAndDeductionRecordsDto>();
        }


        /// <summary>
        /// 返回第三方账号记录列表
        /// </summary>
        /// <returns></returns>
        public GetAllOtherAccountOutput GetAllOtherAccountAndPlateNumber(SearchOtherAccountInput input)
        {
            //var otherAccount=  Context.Set<OtherAccount>();
            //var otherPlateNumber = Context.Set<OtherPlateNumber>();
            //int records = Context.Set<OtherAccount>().Filters(input).Count();
            var query = Context.Set<OtherAccount>().Select(c => new OtherAccountDto
          {
              PlateNumber = Context.Set<OtherPlateNumber>().FirstOrDefault(dic => dic.AssignedOtherAccountId == c.Id).PlateNumber,
              Id = c.Id,
              UserName = c.UserName,
              Name = c.Name,
              TelePhone = c.TelePhone,
              IsPhoneConfirmed = c.IsPhoneConfirmed,
              CardNo = c.CardNo,
              ProductNo = c.ProductNo,
              Wallet = c.Wallet,
              IsEnabled = c.IsEnabled,
              EnabledTime = c.EnabledTime,
              EnabledUserName = Context.Set<Employee>().FirstOrDefault(chen => chen.Id == c.EnabledUserId).TrueName,  //激活的用户
              AuthenticationSource = c.AuthenticationSource,
              Client = c.Client,
              IsActive = c.IsActive,
              AutoDeduction = c.AutoDeduction,
              CreationTime = c.CreationTime,
              LastModificationTime = c.LastModificationTime
          });
            //int records = _abpOtherAccountRepository.GetAll().Filters(input).ToList().Count;
            //List<OtherAccountDto> haoba = _abpOtherAccountRepository.GetAll().OrderByDescending(dic => dic.Id).Filters(input).PageBy(input).ToList().MapTo<List<OtherAccountDto>>();
            int records = query.Filters(input).Count();
            return new GetAllOtherAccountOutput()
            {
                rows = query.OrderByDescending(e =>e.CreationTime).Filters(input).PageBy(input).ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }


        /// <summary>
        /// 恢复激活失败的卡的数据
        /// </summary>
        public void CheckEnabled()
        {
            string sqlStrEnbled = @"select a.id,b.Par,b.userid,a.creationtime,a.cardno,a.wallet,
b.ServiceName,b.executiontime,b.tenantid from 
(select * from extotheraccount where tenantid=2 and isdeleted=0 and isenabled=0 and islock=0 and isactive=1 ) as a
left join 
(select substring(Parameters,12,20) as Par ,substring(Parameters,48,19) as Partime, * from cardauditlog where methodname like 'UpdateEnabled' 
and ExecutionTime>'2016-02-19 18:46:18.213') as b
on a.CardNo=b.Par
where parameters is not null
order by a.id asc ";
            List<EnbledCardNo> FeeBackData = Context.Database.SqlQuery<EnbledCardNo>(sqlStrEnbled).ToList();

            if (FeeBackData != null)
            {
                foreach (var s in FeeBackData)
                {
                    int a = UpdateEnabled2(s.Par, null, null, s.executiontime.ToString(), Convert.ToInt64(s.userid));
                }
            }
        }




        /// <summary>
        /// 激活卡片
        /// </summary>
        /// <param name="CardNo"></param>
        /// <param name="TelePhone"></param>
        /// <param name="PlateNumber"></param>
        /// <param name="EnableTime"></param>
        /// <returns></returns>
        public int UpdateEnabled2(string CardNo, string TelePhone, string PlateNumber, string EnableTime, long UserId)
        {

            var entry = _abpOtherAccountRepository.FirstOrDefault(entity => entity.CardNo == CardNo);
            if (entry == default(OtherAccount))
                //throw new AbpAuthorizationException("激活失败:未找到该卡号！", "501");//报文
                return 0;
            // return 4;
            if (!entry.IsActive)
                return 5;
            if (entry.IsEnabled)
                return 3;
            entry.IsEnabled = true;
            if (TelePhone != "0")//TelePhone=="0" 空值
                entry.TelePhone = TelePhone;
            entry.EnabledTime = Convert.ToDateTime(EnableTime);//卡激活时间;
            entry.EnabledUserId = UserId;
            // e.Remark == "开卡" 是为了兼容晋中1017之前的版本
            var entity1 = _abpDeductionRecordRepository.FirstOrDefault(e => e.OtherAccountId == entry.Id && e.OperType == 1 && (e.Remark == "财务开卡" || e.Remark == "开卡"));
            if (entity1 != default(DeductionRecord))
            {
                entity1.InTime = entry.EnabledTime.Value;
                entity1.Remark = "开卡";
                if (PlateNumber != "0")
                    entity1.PlateNumber = PlateNumber;
                _abpDeductionRecordRepository.Update(entity1);
            }

            if (!string.IsNullOrWhiteSpace(PlateNumber) && PlateNumber != "0")//PlateNumber=="0" 空值
            {
                OtherPlateNumber otherPlateNumber = new OtherPlateNumber();
                otherPlateNumber.AssignedOtherAccountId = entry.Id;
                otherPlateNumber.PlateNumber = PlateNumber;
                otherPlateNumber.Order = 1;
                otherPlateNumber.IsActive = true;
                otherPlateNumber.IsDeleted = false;
                _abpOtherPlateNumberRepository.Insert(otherPlateNumber);
            }

            return _abpOtherAccountRepository.Update(entry) == default(OtherAccount) ? 2 : 1;
        }

    }
}
