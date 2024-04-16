using Abp.Application.Services;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Helper;
using Abp.Linq.Extensions;
using Abp.Runtime.Caching;
using Abp.SignalR.SignalrService;
using MongoDB;
using P4.Berths;
using P4.BlackLists;
using P4.Businesses.Dtos;
using P4.Companys;
using P4.CouponsDetails;
using P4.CouponsDetails.Dtos;
using P4.EmployeeBatchNoDynamicReport.Dtos;
using P4.Employees;
using P4.Equipments;
using P4.MonthlyCars;
using P4.MonthlyCars.Dtos;
using P4.OtherAccounts;
using P4.OtherAccounts.Dtos;
using P4.OtherPlateNumbers;
using P4.ParkingLot;
using P4.ParkingLot.Dto;
using P4.Rates;
using P4.Sensors;
using P4.Signo;
using P4.SmsManagements;
using P4.SmsManagements.Dtos;
using P4.SubscribeManager;
using P4.Users;
using P4.Weixin;
using P4.WhiteLists;
using P4.WhiteLists.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;

namespace P4.Businesses
{
    /// <summary>
    /// 系统业务
    /// </summary>
    public class BusinessAppService : ApplicationService, IBusinessAppService
    {
        #region Var
        private readonly IUserRepository _userRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly IRepository<BusinessDetail, long> _businessDetailRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<Berth, long> _berthRepository;
        private readonly IRepository<DeductionRecord, long> _deductionRecordRepository;    //OtherAccount
        private readonly IRepository<OtherAccount, long> _otherAccountRepository;
        private readonly ISmsManagementAppService _smsManagementAppService;  //短信接口
        private readonly IRepository<OtherPlateNumber, long> _otherPlateNumberRepository;  //账户与车牌关联表
        private readonly IBerthRepository _appBerthRepository;
        private readonly IBerthAppService _appBerthAppService;
        private readonly IRepository<SensorBusinessDetail, long> _sensorBusinessDetailRepository;
        private readonly IRepository<Sensor> _sensorRepository;
        private readonly ISettingStore _settingStore;
        private readonly IP4ChatService _p4ChatService;
        private readonly IRepository<BusinessDetailPicture> _businessDetailPictureRepository;
        private readonly IRepository<Employee, long> _employeeAppService;
        private readonly ISmsModelAppService _smsModelAppService;
        private readonly IRepository<BlackList> _blackListRepository;
        private readonly IRepository<Berthsecs.Berthsec> _berthsecRepository;
        private readonly IRepository<RemoteGuid> _remoteGuidRepository;
        private readonly IRepository<WhiteList> _whiteRepository;
        private readonly IRatesAppService _ratesAppService;
        private readonly ISubscribeAppService _subscribeAppService;
        private readonly ICacheManager _abpCacheManager;//缓存
        private readonly IRepository<WhiteList> _abpWhiteListRepository;
        private readonly IRepository<MonthlyCar> _monthlyCarAppService;
        private readonly IRepository<EmployeeCheck, long> _abpEmployeeCheckRepository;
        private readonly IOtherAccountAppService _otherAccountAppService;
        private readonly IRepository<Coupon> _couponRepository;//优惠券
        private readonly ICouponDetailsAppService _couponDetailsAppService;//优惠券明细
        private readonly IRepository<MultiTenancy.Tenant> _tenantRepository;
        private readonly IRepository<OperatorsCompany> _companyRepository;
        private readonly IRepository<ParkingRecord,long> _parkingRecordRepository;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="businessRepository"></param>
        /// <param name="businessDetailRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="berthRepository"></param>
        /// <param name="deductionRecordRepository"></param>
        /// <param name="otherAccountRepository"></param>
        /// <param name="smsManagementAppService"></param>
        /// <param name="otherPlateNumberRepository"></param>
        /// <param name="appBerthRepository"></param>
        /// <param name="sensorBusinessDetailRepository"></param>
        /// <param name="sensorRepository"></param>
        /// <param name="settingStore"></param>
        /// <param name="businessDetailPictureRepository"></param>
        /// <param name="p4ChatService"></param>
        /// <param name="employeeAppService"></param>
        /// <param name="smsModelAppService"></param>
        /// <param name="blackListRepository"></param>
        /// <param name="berthsecRepository"></param>
        /// <param name="remoteGuidRepository"></param>
        /// <param name="whiteRepository"></param>
        /// <param name="ratesAppService"></param>
        /// <param name="subscribeAppService"></param>
        /// <param name="abpWhiteListRepository"></param>
        /// <param name="userRepository"></param>
        /// <param name="appBerthAppService"></param>
        /// <param name="abpCacheManager"></param>
        /// <param name="abpEmployeeCheckRepository"></param>
        /// <param name="monthlyCarAppService"></param>
        /// <param name="otherAccountAppService"></param>
        /// <param name="couponRepository"></param>
        /// <param name="couponDetailsAppService"></param>
        /// <param name="tenantRepository"></param>
        /// <param name="companyRepository"></param>
        public BusinessAppService(IBusinessRepository businessRepository, IRepository<BusinessDetail, long> businessDetailRepository, IUnitOfWorkManager unitOfWorkManager, IRepository<Berth, long> berthRepository, IRepository<DeductionRecord, long> deductionRecordRepository, IRepository<OtherAccount, long> otherAccountRepository, ISmsManagementAppService smsManagementAppService, IRepository<OtherPlateNumber, long> otherPlateNumberRepository, IBerthRepository appBerthRepository, IRepository<SensorBusinessDetail, long> sensorBusinessDetailRepository, IRepository<Sensor> sensorRepository, ISettingStore settingStore, IRepository<BusinessDetailPicture> businessDetailPictureRepository, IP4ChatService p4ChatService, IRepository<Employees.Employee, long> employeeAppService, ISmsModelAppService smsModelAppService, IRepository<BlackLists.BlackList> blackListRepository, IRepository<Berthsecs.Berthsec> berthsecRepository, IRepository<RemoteGuid> remoteGuidRepository, IRepository<WhiteLists.WhiteList> whiteRepository, IRatesAppService ratesAppService, ISubscribeAppService subscribeAppService, IRepository<WhiteList> abpWhiteListRepository, IUserRepository userRepository, IBerthAppService appBerthAppService, ICacheManager abpCacheManager, IRepository<EmployeeCheck, long> abpEmployeeCheckRepository, IRepository<MonthlyCar> monthlyCarAppService, IOtherAccountAppService otherAccountAppService, IRepository<P4.Weixin.Coupon> couponRepository, ICouponDetailsAppService couponDetailsAppService, IRepository<MultiTenancy.Tenant> tenantRepository, IRepository<OperatorsCompany> companyRepository, IRepository<ParkingRecord, long> parkingRecordRepository)
        {
            _abpEmployeeCheckRepository = abpEmployeeCheckRepository;
            _userRepository = userRepository;
            _monthlyCarAppService = monthlyCarAppService;
            _businessRepository = businessRepository;
            _businessDetailRepository = businessDetailRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _abpWhiteListRepository = abpWhiteListRepository;
            _berthRepository = berthRepository;
            _deductionRecordRepository = deductionRecordRepository;
            _otherAccountRepository = otherAccountRepository;
            _smsManagementAppService = smsManagementAppService;
            _otherPlateNumberRepository = otherPlateNumberRepository;
            _appBerthRepository = appBerthRepository;
            _sensorBusinessDetailRepository = sensorBusinessDetailRepository;
            _sensorRepository = sensorRepository;
            _settingStore = settingStore;
            _businessDetailPictureRepository = businessDetailPictureRepository;
            _p4ChatService = p4ChatService;
            _employeeAppService = employeeAppService;
            _smsModelAppService = smsModelAppService;
            _berthsecRepository = berthsecRepository;
            _blackListRepository = blackListRepository;
            _remoteGuidRepository = remoteGuidRepository;
            _whiteRepository = whiteRepository;
            _ratesAppService = ratesAppService;
            _subscribeAppService = subscribeAppService;
            _abpCacheManager = abpCacheManager;
            _appBerthAppService = appBerthAppService;
            _otherAccountAppService = otherAccountAppService;
            _couponRepository = couponRepository;
            _couponDetailsAppService = couponDetailsAppService;
            _tenantRepository = tenantRepository;
            _companyRepository = companyRepository;
            _parkingRecordRepository = parkingRecordRepository;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.GetMoneyOutput IndexMoneyTotal(Dtos.GetMoneyInput input)
        {
            return _businessRepository.GetMoneyTotal(input);
        }

        /// <summary>
        /// 获取停车场停车记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetParkRecordListOutput GetParkRecordList(GetParkRecordListInput input)
        {
            return _businessRepository.GetParkRecordList(input);
        }

        /// <summary>
        /// 获取停车记录图片列表
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public List<AbpParkingRecord_Pic> GetParkingRecordPicList(Guid recordId)
        {
            string sql = $"SELECT * FROM dbo.AbpParkingRecord_Pic WHERE RecordID='{recordId}' ORDER BY CreateTime DESC" ;
            return Abp.DataProcessHelper.GetEntityFromTable<AbpParkingRecord_Pic>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql).Tables[0]);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(long Id)
        {
            _businessDetailRepository.Delete(Id);
        }

        /// <summary>
        /// 逃逸明细删除记录
        /// </summary>
        /// <param name="Id"></param>
        public int Delete(Guid Id)
        {
            var operatorId = AbpSession.UserId.Value;
            var operatorName = _userRepository.Load(operatorId).Name;
            var datetime = DateTime.Now.ToString();
            string content = operatorName + "用户在" + datetime + "时间删除逃逸明细记录；";

            var model = _businessDetailRepository.FirstOrDefault(e => e.guid == Id && e.Status != 4);

            if (!string.IsNullOrEmpty(model.PaymentBatchNo))
            {
                var employeeCheckModel = _abpEmployeeCheckRepository.FirstOrDefault(entity => entity.BatchNo == model.PaymentBatchNo);
                if (employeeCheckModel != null)
                {
                    employeeCheckModel.IsNormal = false;
                    employeeCheckModel.IsRepeal = false;
                    employeeCheckModel.Remark = employeeCheckModel.Remark + content;
                    _abpEmployeeCheckRepository.Update(employeeCheckModel);
                }
            }

            if (!string.IsNullOrEmpty(model.OutBatchNo))
            {
                var employeeCheckModel = _abpEmployeeCheckRepository.FirstOrDefault(entity => entity.BatchNo == model.OutBatchNo);
                if (employeeCheckModel != null)
                {
                    employeeCheckModel.IsNormal = false;
                    employeeCheckModel.IsRepeal = false;
                    employeeCheckModel.Remark = employeeCheckModel.Remark + content;
                    _abpEmployeeCheckRepository.Update(employeeCheckModel);
                }            
            }

            if (!string.IsNullOrEmpty(model.InBatchNo))
            {
                var employeeCheckModel = _abpEmployeeCheckRepository.FirstOrDefault(entity => entity.BatchNo == model.InBatchNo);
                if (employeeCheckModel != null)
                {
                    employeeCheckModel.IsNormal = false;
                    employeeCheckModel.IsRepeal = false;
                    employeeCheckModel.Remark = employeeCheckModel.Remark + content;
                    _abpEmployeeCheckRepository.Update(employeeCheckModel);
                }
            }


            //string sqlAbpEmployeeCheck = " update AbpEmployeeCheck set AbpEmployeeCheck.IsRepeal = 0, AbpEmployeeCheck.IsNormal = case when AbpEmployeeCheck.CheckOutTime is null then 1 else 0 end from AbpEmployeeCheck left join AbpBusinessDetail on AbpBusinessDetail.PaymentBatchNo = AbpEmployeeCheck.BatchNo or AbpBusinessDetail.InBatchNo = AbpEmployeeCheck.BatchNo or AbpBusinessDetail.OutBatchNo =  AbpEmployeeCheck.BatchNo where Exists (select AbpBusinessDetail.PaymentBatchNo,AbpBusinessDetail.OutBatchNo,AbpBusinessDetail.InBatchNo from AbpBusinessDetail where guid in('" + string.Join("', '", Id) + "') and (AbpBusinessDetail.PaymentBatchNo = AbpEmployeeCheck.BatchNo or AbpBusinessDetail.InBatchNo = AbpEmployeeCheck.BatchNo or AbpBusinessDetail.OutBatchNo = AbpEmployeeCheck.BatchNo) and AbpBusinessDetail.Status !=4)";
            //SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, sqlAbpEmployeeCheck);
            _businessDetailRepository.Delete(entity => entity.guid == Id && entity.Status != 4);
            return _businessDetailRepository.Count(entity => entity.guid == Id && entity.Status != 4);
        }

        /// <summary>
        /// 恢复删除
        /// </summary>
        /// <param name="Id"></param>
        public void RestoreDelete(long Id)
        {
            _businessRepository.RestoreDelete(Id);
        }

        /// <summary>
        /// 逃逸明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.GetEscapeDetailsOutput GetEscapeDetailsList(Dtos.GetEscapeDetailsInput input)
        {
            var templist = new List<int>();
            templist.Add(-1);
            input.CompanyIds = input.CompanyId.HasValue == false ? AbpSession.CompanyIds ?? templist : new List<int> { input.CompanyId.Value };
            return _businessRepository.GetEscapeDetailsList(input);
        }
        /// <summary>
        /// 根据车牌号 车辆欠费接口
        /// </summary>
        /// <param name="plateNumber"></param>
        /// <returns></returns>
        [AbpAuthorize]
        [UnitOfWorkAttribute(isTransactional: false)]
        public Dtos.GetEscapeDetailsOutputTow GetEscapeDetailsListTow(string plateNumber)
        {
            //如果全国追缴默认必须支持分公司追缴
            //if (bool.Parse(_settingStore.GetSettingOrNull(AbpSession.TenantId, null, "TheRecovered").Value))
            //    _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany);

            //if (bool.Parse(_settingStore.GetSettingOrNull(AbpSession.TenantId, null, "TheRecoveredCompany").Value))
            //    _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveCompany);

            //_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark, AbpDataFilters.MustHaveBerthsec);
            //  return _businessRepository.GetEscapeDetailsListTowBySql(plateNumber, AbpSession.UserId, AbpSession.TenantId);
            return _businessRepository.GetEscapeDetailsListTowBySql(plateNumber, AbpSession.UserId, AbpSession.TenantId, AbpSession.CompanyId);
        }


        /// <summary>
        /// 对账
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public string GetReconciliationModel(string batchNo)

        {
            GetReconciliation model = new GetReconciliation();
            SqlParameter[] param = new SqlParameter[] {
                 new SqlParameter("@BatchNo", batchNo)
            };
            #region sql语句
            //List<GetReconciliation> list = Abp.DataProcessHelper.GetEntityFromTable<GetReconciliation>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select IsNormal,IsRepeal,sum(case when PaymentBatchNo=@BatchNo and Status = 4 then 1 else 0 end)as RecoverCount,sum(case when InBatchNo = @BatchNo then 1 else 0 end) as CarInCount, 0.00 as RealPaySum,sum(case when OutBatchNo = @BatchNo then 1 else 0 end) as CarOutCount, sum(case when  InBatchNo = @BatchNo then Prepaid else 0 end) as PrepaySum,sum(case when OutBatchNo = @BatchNo  then FactReceive else 0 end) as OutPaySum,sum(case when OutBatchNo = @BatchNo or InBatchNo = @BatchNo then Arrearage else 0 end) as OwePaySum,sum(case when PaymentBatchNo = @BatchNo then Repayment else 0 end) as RecPaySum, sum(case when InBatchNo = @BatchNo then Prepaid else 0 end) as CashPrePay,sum(case when OutBatchNo = @BatchNo then Money else 0 end) as ShouldPaySum,sum(case when PayStatus = '1' and OutBatchNo = @BatchNo and InBatchNo = @BatchNo then FactReceive when PayStatus = '1' and OutBatchNo <> @BatchNo and InBatchNo = @BatchNo then Prepaid when PayStatus = '0' then Prepaid when PayStatus = '1' and OutBatchNo = @BatchNo and InBatchNo <> @BatchNo then Receivable else 0 end) as CashPaySum,sum(case when PayStatus = '3' and OutBatchNo = @BatchNo then Receivable else 0 end) as OnlinePaySum,sum(case when EscapePayStatus = '1' and PaymentBatchNo = @BatchNo then Repayment else 0 end) as CashRecPaySum,sum(case when EscapePayStatus = '3'and PaymentBatchNo = @BatchNo then Repayment else 0 end) as OnlineRecPaySum from AbpBusinessDetail with(nolock)left join AbpEmployeeCheck on BatchNo = @BatchNo  where IsDeleted = 0 and AbpBusinessDetail.TenantId = " + AbpSession.TenantId.Value + " and AbpBusinessDetail.CompanyId = " + AbpSession.CompanyId.Value + " and (PaymentBatchNo = @BatchNo or OutBatchNo = @BatchNo or InBatchNo = @BatchNo) group by IsRepeal,IsNormal", param).Tables[0]);
            #endregion

            //待优化
            List<GetReconciliation> list = Abp.DataProcessHelper.GetEntityFromTable<GetReconciliation>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select (select Remark from AbpEmployeeCheck where BatchNo = @BatchNo and TenantId = " + AbpSession.TenantId.Value + " and CompanyId = " + AbpSession.CompanyId.Value + ")Remark,(select IsNormal from AbpEmployeeCheck where BatchNo = @BatchNo and TenantId = " + AbpSession.TenantId.Value + " and CompanyId = " + AbpSession.CompanyId.Value + ")IsNormal,(select IsRepeal from AbpEmployeeCheck where BatchNo = @BatchNo and TenantId = " + AbpSession.TenantId.Value + " and CompanyId = " + AbpSession.CompanyId.Value + ")IsRepeal,ISNULL(sum(case when PaymentBatchNo = @BatchNo and Status = 4 then 1 else 0 end), 0) as RecoverCount,ISNULL(sum(case when InBatchNo = @BatchNo then 1 else 0 end), 0) as CarInCount, 0.00 as RealPaySum,ISNULL(sum(case when OutBatchNo = @BatchNo then 1 else 0 end), 0.00) as CarOutCount,ISNULL(sum(case when  InBatchNo = @BatchNo then Prepaid else 0 end), 0.00) as PrepaySum,ISNULL(sum(case when OutBatchNo = @BatchNo  then FactReceive else 0 end), 0.00) as OutPaySum,ISNULL(sum(case when OutBatchNo = @BatchNo or InBatchNo = @BatchNo then Arrearage else 0 end), 0.00) as OwePaySum,ISNULL(sum(case when PaymentBatchNo = @BatchNo then Repayment else 0 end), 0.00) as RecPaySum,ISNULL(sum(case when InBatchNo = @BatchNo then Prepaid else 0 end), 0.00) as CashPrePay,ISNULL(sum(case when OutBatchNo = @BatchNo then Money else 0 end), 0.00) as ShouldPaySum,ISNULL(sum(case when PayStatus = '1' and OutBatchNo = @BatchNo and InBatchNo = @BatchNo then FactReceive when InBatchNo = @BatchNo then Prepaid when PayStatus = '1' and OutBatchNo = @BatchNo and InBatchNo <> @BatchNo and status != 3 then Receivable else 0 end), 0.00) as CashPaySum,ISNULL(sum(case when PayStatus = '3' and OutBatchNo = @BatchNo then Receivable else 0 end), 0.00) as OnlinePaySum,ISNULL(sum(case when EscapePayStatus = '1' and PaymentBatchNo = @BatchNo then Repayment else 0 end), 0.00) as CashRecPaySum,ISNULL(sum(case when EscapePayStatus = '3'and PaymentBatchNo = @BatchNo then Repayment else 0 end), 0.00) as OnlineRecPaySum from AbpBusinessDetail with(nolock) where IsDeleted = 0 and AbpBusinessDetail.TenantId = " + AbpSession.TenantId.Value + " and AbpBusinessDetail.CompanyId = " + AbpSession.CompanyId.Value + " and(PaymentBatchNo = @BatchNo or OutBatchNo = @BatchNo or InBatchNo = @BatchNo)", param).Tables[0]);

            if (list.Count > 0)
            {
                model = list[0];
                model.CashPaySum = model.CashPaySum + model.CashRecPaySum;
                model.RealPaySum = model.PrepaySum + model.OutPaySum;
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(model);
        }


        /// <summary>
        /// 获取分公司对账功能
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetEscapeDetailsOutput GetEscapeDetailsListByCompany(GetEscapeDetailsInput input)
        {
            //RegionId 商户追缴类型
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveCompany, AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark, AbpDataFilters.MustHaveBerthsec);
            int records = _businessDetailRepository.GetAll().Filters(input).Where(entity =>
            entity.Status == 4 && entity.CompanyId != entity.EscapeCompanyId.Value && entity.EscapeCompanyId.HasValue &&
            entity.CarRepaymentTime > input.begindt &&
            entity.CarRepaymentTime < input.enddt).
            WhereIf(input.RegionId == 2, entity => entity.CompanyId == AbpSession.CompanyId.Value).//本公司
            WhereIf(input.RegionId == 1, entity => entity.EscapeCompanyId == AbpSession.CompanyId.Value).ToList().Count;

            var rows = _businessDetailRepository.GetAll().Orders(input).Where(entity =>
            entity.Status == 4 && entity.CompanyId != entity.EscapeCompanyId.Value && entity.EscapeCompanyId.HasValue &&
            entity.CarRepaymentTime > input.begindt &&
            entity.CarRepaymentTime < input.enddt).
            WhereIf(input.RegionId == 2, entity => entity.CompanyId == AbpSession.CompanyId.Value).//本公司
            WhereIf(input.RegionId == 1, entity => entity.EscapeCompanyId == AbpSession.CompanyId.Value).PageBy(input).Filters(input).ToList().MapTo<List<EscapeDetailsDto>>();
            List<EscapeDetailsDto> tempval = new List<EscapeDetailsDto>();
            foreach (var v in rows)
            {
                var row = v;
                row.CompanyName = _companyRepository.FirstOrDefault(v.CompanyId).CompanyName;
                row.EscapeCompanyName = _companyRepository.FirstOrDefault(v.EscapeCompanyId).CompanyName;
                row.EscapeEmployeeName = _employeeAppService.FirstOrDefault(v.EscapeOperaId.Value).TrueName;
                tempval.Add(row);
            }
            return new GetEscapeDetailsOutput()
            {
                rows = tempval,
                records = records,
                //userdata = new EscapeDetailsUserData() { Money = tempmodel.Sum(entity=>entity.Money), Repayment = tempmodel.Sum(entity=>entity.Repayment) },
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 获取商户对账功能
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetEscapeDetailsOutput GetEscapeDetailsListByTenant(GetEscapeDetailsInput input)
        {
            //RegionId 商户追缴类型
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany, AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark, AbpDataFilters.MustHaveBerthsec);
            int records = _businessDetailRepository.GetAll().Filters(input).Where(entity =>
            entity.Status == 4 && entity.EscapeTenantId.HasValue == true &&
            entity.TenantId != entity.EscapeTenantId.Value &&
            entity.CarRepaymentTime > input.begindt &&
            entity.CarRepaymentTime < input.enddt).
            WhereIf(input.RegionId == 2, entity => entity.TenantId == AbpSession.TenantId.Value).
            WhereIf(input.RegionId == 1, entity => entity.EscapeTenantId == AbpSession.TenantId.Value).ToList().Count;

            var rows = _businessDetailRepository.GetAll().Orders(input).Where(entity =>
               entity.TenantId != entity.EscapeTenantId && entity.EscapeTenantId != null &&
               entity.CarRepaymentTime > input.begindt && entity.CarRepaymentTime < input.enddt).WhereIf(input.RepaymentYorN != 0, entity => entity.EscapeTenantId == input.RepaymentYorN || entity.TenantId == input.RepaymentYorN).WhereIf(input.RegionId == 2, entity => entity.TenantId == AbpSession.TenantId.Value).
            WhereIf(input.RegionId == 1, entity => entity.EscapeTenantId == AbpSession.TenantId.Value).PageBy(input).Filters(input).ToList().MapTo<List<EscapeDetailsDto>>();
            List<EscapeDetailsDto> tempval = new List<EscapeDetailsDto>();
            foreach(var v in rows)
            {
                var row = v;
                row.Tenant = _tenantRepository.FirstOrDefault(v.TenantId).TenancyName;
                row.EscapeTenantName = _tenantRepository.FirstOrDefault(v.EscapeTenantId.Value).TenancyName;
                row.EscapeEmployeeName = _employeeAppService.FirstOrDefault(v.EscapeOperaId.Value).TrueName;
                tempval.Add(row);
            }
            return new GetEscapeDetailsOutput()
            {
                rows = tempval,
                records = records,
                //userdata = new EscapeDetailsUserData() { Money = tempmodel.Sum(entity=>entity.Money), Repayment = tempmodel.Sum(entity=>entity.Repayment) },
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
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
        /// <param name="RegionId"></param>
        /// <param name="ParkId"></param>
        /// <param name="BerthsecId"></param>
        /// <param name="InBatchNo"></param>
        /// <returns></returns>
        [AbpAuthorize]
        [UnitOfWork(isTransactional: false)]
        public bool InsertCarIn(string BerthNumber, string PlateNumber, string CarType, string Prepaid, string CarInTime, string guid, string SensorsInCarTime, string StopType, string CardNo, int RegionId, int ParkId, int BerthsecId, string InBatchNo)
        {
            if (StopType == "F4")//白名单
            {
                StopType = "7";
            }
            Guid g = new Guid(guid);
            int count = 0;
            Berth berthmodel = null;
            List<Berth> list = Abp.DataProcessHelper.GetEntityFromTable<Berth>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpBerths where BerthsecId =  " + BerthsecId + " and BerthNumber = '" + BerthNumber + "'").Tables[0]);

            if (list != null && list.Count > 0)
            {
                berthmodel = list[0];
            }
            else
            {
                throw new AbpAuthorizationException("入场失败：泊位号不存在！", "23");
            }

            count = int.Parse(Abp.Helper.SqlHelper.ExecuteScalar(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select count(1) from AbpBusinessDetail with(nolock) where guid = '" + g + "'").ToString());
            if (count > 0)
            {
                throw new AbpAuthorizationException("入场失败：guid已经存在！", "20");
            }

            short PrepaidPayStatus;
            if (CardNo != "0")
            {
                PrepaidPayStatus = 4;// 卡号不等于0  支付类型为4属账号支付
            }
            else
            {
                PrepaidPayStatus = 1;//卡号等于0  支付类型为1属现金支付
            }

            DateTime? SensorsInCarTime1 = null;

            if (berthmodel == default(Berth))
            {
                SensorsInCarTime1 = null;
            }
            else
            {
                if (berthmodel.ParkStatus == 1 && !string.IsNullOrWhiteSpace(berthmodel.SensorNumber) && berthmodel.SensorGuid == g)
                {
                    SensorsInCarTime1 = berthmodel.SensorsInCarTime;
                }
            }

            BusinessDetail entity = new BusinessDetail()
            {
                InBatchNo = InBatchNo,
                BerthNumber = BerthNumber,//泊位号 
                PlateNumber = PlateNumber,//车牌号
                CarType = Convert.ToInt16(CarType),//车辆类型
                Prepaid = Convert.ToDecimal(Prepaid),//预付费
                CarInTime = Convert.ToDateTime(CarInTime),//车辆入场时间
                InOperaId = AbpSession.UserId.Value,//入场收费员ID
                InDeviceCode = AbpSession.DeviceCode,//入场设备
                guid = g,//
                SensorsInCarTime = SensorsInCarTime1,//车检器入场时间
                StopType = Convert.ToInt16(StopType),//停车类型（是否为包月车）
                RegionId = RegionId,//区域ID
                ParkId = ParkId,//停车场ID
                BerthsecId = BerthsecId,//泊位段ID
                Status = 1,
                PrepaidCarNo = CardNo,  //进场卡号
                PrepaidPayStatus = PrepaidPayStatus   //进场支付类型
            };

            _appBerthRepository.CarInUpdateBerhs(BerthNumber, entity.BerthsecId, PlateNumber, Convert.ToDateTime(CarInTime), g, Convert.ToInt16(CarType), CardNo, Convert.ToDecimal(Prepaid));


            var otherAccount = _otherAccountRepository.FirstOrDefault(c => c.CardNo == CardNo && c.IsActive == true);
            if (otherAccount != null && Convert.ToDecimal(Prepaid) > 0)
            {
                DeductionRecord deductionrecord = new DeductionRecord()
                {
                    OtherAccountId = otherAccount.Id,
                    OperType = 3,
                    Money = Convert.ToDecimal(Prepaid),
                    PayStatus = true,
                    CardNo = CardNo,
                    EmployeeId = AbpSession.UserId.Value,
                    PlateNumber = PlateNumber,
                    Remark = "预付",
                    InTime = Convert.ToDateTime(CarInTime),
                    BeginMoney = otherAccount.Wallet,
                    EndMoney = otherAccount.Wallet - Convert.ToDecimal(Prepaid)
                };
                _deductionRecordRepository.Insert(deductionrecord);
                //预付  短信发送
                _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto() { Destnumbers = otherAccount.TelePhone, Msg = string.Format(_smsModelAppService.GetSmsModelByType("PaymentModel").SmsContext, Convert.ToDateTime(CarInTime).ToString("yyyy年MM月dd日hh时mm分"), BerthNumber, Prepaid) });
                otherAccount.Wallet = otherAccount.Wallet - Convert.ToDecimal(Prepaid);//车辆入场 账户余额扣除预付
                _otherAccountRepository.Update(otherAccount);
            }

            Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString, CommandType.Text, " update AbpSensors set RelateNumber = '" + PlateNumber + "' where BerthsecId = " + BerthsecId + " and BerthNumber = '" + BerthNumber + "' " +
                "	update AbpSensorBusinessDetail set PlateNumber = '" + PlateNumber + "' where [guid] = '" + g + "'");
            _businessDetailRepository.Insert(entity);
            var employee = _employeeAppService.Load(AbpSession.UserId.Value);
            var Berthsec = _berthsecRepository.FirstOrDefault(entity.BerthsecId);
            string berthsecName = "";
            if (Berthsec != default(Berthsecs.Berthsec))
            {
                berthsecName = Berthsec.BerthsecName;
            }


            string message = "进场车辆" + entity.PlateNumber + "在" + entity.CarInTime + "驶入" + berthsecName + entity.BerthNumber + "泊位号, 支付类型:" + ChangePayStatusName(entity.PrepaidPayStatus) + ",预缴金额:" + entity.Prepaid + "元,操作收费员:" + employee.TrueName + "(" + employee.UserName + ")";
            _p4ChatService.CreateChatService().Clients.Group(P4Consts.EmployeeGroup + AbpSession.TenantId.Value).EmployeeMessage(employee.TrueName + "(" + employee.UserName + ")", message, 2);
            _subscribeAppService.SendMessage("CarInOutManagement", message, "");
            //黑名单检测，黑名单检测短信报警
            var blackModel = _blackListRepository.FirstOrDefault(entry => entry.RelateNumber == PlateNumber && entry.IsActive == true);
            if (blackModel != default(BlackLists.BlackList))
            {

                //黑名单{0},车主姓名{1}，在{2}驶入{3}{4}泊位号，请悉知！
                var model = new SmsAccountDto() { Msg = string.Format(_smsModelAppService.GetSmsModelByType("BlackCarInModel").SmsContext, blackModel.RelateNumber, blackModel.CarOwner, Convert.ToDateTime(CarInTime).ToString("yyyy年MM月dd日hh时mm分"), berthsecName, BerthNumber), Destnumbers = blackModel.IdNumber };
                _smsManagementAppService.SendSmsNoTenant(model);

                _subscribeAppService.SendMessage("BlackManagement", model.MsgValue, "");
            }
            return true;
        }

        /// <summary>
        /// 转换支付类型
        /// </summary>
        /// <param name="PayStatus"></param>
        /// <returns></returns>
        private string ChangePayStatusName(int PayStatus)
        {
            switch (PayStatus)
            {
                case 1:
                    return "现金";
                case 2:
                    return "刷卡支付";
                case 3:
                    return "微信支付";
                case 4:
                    return "账号支付";
                case 5:
                    return "未付";
                case 6:
                    return "支付宝支付";
                case 7:
                    return "其他";
                default:
                    return "未知";
            }
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
        /// <param name="CardNo"></param>
        /// <param name="BerthsecID"></param>
        /// <param name="OutBatchNo"></param>
        /// <returns></returns>
        [AbpAuthorize]
        [UnitOfWorkAttribute(isTransactional: false)]
        public bool InsertCarOut(decimal Receivable, string FactReceive, string CarOutTime, string CarPayTime, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money, string CardNo, int BerthsecID, string OutBatchNo)
        {

            return DiscountInsertCarOut(Receivable, FactReceive, CarOutTime, CarPayTime, guid, SensorsOutCarTime, SensorsStopTime, SensorsReceivable, PayStatus, IsPay, FeeType, StopTime, Money, CardNo, Money, 0, 10, BerthsecID, OutBatchNo);
        }

        /// <summary>
        /// 下班签退车牌批量出场
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
        /// <param name="CardNo"></param>
        /// <param name="BerthsecId"></param>
        /// <param name="OutBatchNo"></param>
        /// <returns></returns>
        [AbpAuthorize]
        [UnitOfWorkAttribute(isTransactional: false)]
        public bool InsertBatchCarOut(decimal Receivable, string FactReceive, string CarOutTime, string CarPayTime, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money, string CardNo, int BerthsecId, string OutBatchNo)
        {
            return DiscountInsertBatchCarOut(Receivable, FactReceive, CarOutTime, CarPayTime, guid, SensorsOutCarTime, SensorsStopTime, SensorsReceivable, PayStatus, IsPay, FeeType, StopTime, Money, CardNo, Money, 0, 10, BerthsecId, OutBatchNo);
        }

        /// <summary>
        /// 
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
        /// <param name="CardNo"></param>
        /// <param name="BeforeDiscount"></param>
        /// <param name="DiscountMoney"></param>
        /// <param name="DiscountRate"></param>
        /// <param name="BerthsecId"></param>
        /// <param name="OutBatchNo"></param>
        /// <returns></returns>
        private bool DiscountInsertBatchCarOut(decimal Receivable, string FactReceive, string CarOutTime, string CarPayTime, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money, string CardNo, decimal BeforeDiscount, decimal DiscountMoney, double DiscountRate, int BerthsecId, string OutBatchNo)
        {
            if (PayStatus != "4")
                CardNo = "0";
            Guid g = new Guid(guid);
            List<BusinessDetail> entitys = new List<BusinessDetail>();

            entitys = Abp.DataProcessHelper.GetEntityFromTable<BusinessDetail>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select * from AbpBusinessDetail with(nolock) where isdeleted = 0 and guid = '" + g + "'").Tables[0]);

            //using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany, AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark, AbpDataFilters.MustHaveBerthsec))
            //{
            //    entity = _businessDetailRepository.FirstOrDefault(b => b.guid == g);
            //}
            if (entitys.Count == 0)
            {
                //写入系统异常消息提醒
                throw new AbpAuthorizationException("出场失败：guid不存在！", "22");
            }

            BusinessDetail entity = new BusinessDetail();
            entity = entitys[0];
            if (entity.Status != 1 && entity.Status != 3)//已经出场处理
                throw new AbpAuthorizationException("出场失败：该数据已出场！", "201");

            Berth berthmodel = Abp.DataProcessHelper.GetEntityFromTable<Berth>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select * from AbpBerths with(nolock) where BerthsecId = " + BerthsecId + " and BerthNumber = '" + entity.BerthNumber + "'").Tables[0])[0];

            DateTime? SensorsOutCarTime1 = null;

            //处理白名单           
            var whitemodel = _whiteRepository.FirstOrDefault(whiteentity => whiteentity.RelateNumber == berthmodel.RelateNumber);
            if (whitemodel != default(WhiteLists.WhiteList))
            {
                Receivable = 0;
                FactReceive = "0";
                Money = 0;
            }
            else//包月车辆数据
            {
                var param = new SqlParameter[] { new SqlParameter("@parkid", "," + AbpSession.ParkIds[0] + ",") };
                var MonthlyCarList = Abp.DataProcessHelper.GetEntityFromTable<MonthlyCarDto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select * from AbpMonthlyCars with(nolock) where (case when CompanyId is null then AbpMonthlyCars.tenantid else " + AbpSession.TenantId.Value + "  end) = " + AbpSession.TenantId.Value + " and(case when CompanyId is not null then AbpMonthlyCars.CompanyId else " + AbpSession.CompanyId.Value + " end) = " + AbpSession.CompanyId.Value + " and BeginTime <= getdate() and EndTime >= getdate() and(charindex(@parkid, ',' + ParkIds + ',') > 0 or ParkIds = '0') and IsDeleted = 0 and PlateNumber = '" + berthmodel.RelateNumber + "'", param).Tables[0]);

                //判断是否为全天包月
                if (MonthlyCarList.Count > 0)
                {
                    if (MonthlyCarList[0].MonthyType == "MonthyAll")//如果为全天包月金额计费为0
                    {
                        Receivable = 0;
                        FactReceive = "0";
                        Money = 0;
                        entity.Status = 2;
                        entity.IsPay = true;
                    }
                    else
                    {
                        var model = _ratesAppService.RateCalculate(berthmodel.BerthsecId, entity.CarInTime.Value, Convert.ToDateTime(CarOutTime), entity.CarType, 0, berthmodel.ParkId, entity.PlateNumber, entity.CompanyId);
                        Receivable = model.CalculateMoney;
                        FactReceive = "0";
                        Money = model.CalculateMoney;
                        entity.Status = 3;
                        entity.IsPay = false;
                    }
                }
            }

            if (berthmodel == default(Berth))
            {
                SensorsOutCarTime1 = null;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(berthmodel.SensorNumber))
                {
                    SensorsOutCarTime1 = berthmodel.SensorsOutCarTime;
                }
            }

            //    return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 12, Message = "退出失败，在线用户信息已经丢失！ " }), JsonRequestBehavior.AllowGet);
            if (entity != null)
            {
                if (bool.Parse(_settingStore.GetSettingOrNull(AbpSession.TenantId, null, "EscapeLock").Value))
                    PlateNumberDebLock(entity.PlateNumber);//车辆出场的时候 对车辆进行解锁

                entity.OutDeviceCode = AbpSession.DeviceCode;               //出场设备
                //entity.Receivable = Convert.ToDecimal(Receivable);        //应收

                //entity.Arrearage = Convert.ToDecimal(Arrearage);          //欠费
                entity.ChargeOperaId = AbpSession.UserId;                   //收费操作员Id
                entity.ChargeDeviceCode = AbpSession.DeviceCode;            //收费员设备
                entity.CarOutTime = Convert.ToDateTime(CarOutTime);         //出场时间
                entity.CarPayTime = Convert.ToDateTime(CarPayTime);         //支付时间
                entity.OutBatchNo = OutBatchNo;
                entity.OutOperaId = Convert.ToInt16(AbpSession.UserId);     //出场收费员ID
                entity.guid = g;                               //唯一guid

                if (entity.guid == berthmodel.SensorGuid)                    //如果车检器与收费记录中guid相等，关联车检器入场出场信息
                {
                    entity.SensorsOutCarTime = SensorsOutCarTime1;//车检器出场时间
                    entity.SensorsStopTime = Convert.ToInt32(SensorsStopTime.Split('.')[0]);//车检器停车时长
                    entity.SensorsReceivable = Convert.ToDecimal(SensorsReceivable);//车检收器应
                }
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
                if (PayStatus == "1")                                           //现金
                {
                    //entity.Receivable = Receivable;                             //出场应收 
                    entity.FactReceive = Convert.ToDecimal(FactReceive);        //实收
                    entity.DiscountRate = DiscountRate;//折扣率
                    entity.DiscountMoney = DiscountMoney;//折扣金额
                    entity.BeforeDiscount = BeforeDiscount;//打折前应收
                }
                else if (PayStatus == "3")                                      //微信支付
                {
                    //entity.Receivable = Receivable;                             //出场应收 
                    entity.FactReceive = Convert.ToDecimal(FactReceive);        //实收
                    entity.DiscountRate = DiscountRate;//折扣率
                    entity.DiscountMoney = DiscountMoney;//折扣金额
                    entity.BeforeDiscount = BeforeDiscount;//打折前应收
                }
                else if (PayStatus == "4")                                      //账号支付
                {
                    //entity.Receivable = Receivable;                             //出场应收 
                    entity.FactReceive = Convert.ToDecimal(FactReceive);        //实收
                    entity.DiscountRate = DiscountRate;//折扣率
                    entity.DiscountMoney = DiscountMoney;//折扣金额
                    entity.BeforeDiscount = BeforeDiscount;//打折前应收
                }
                entity.FeeType = Convert.ToInt16(FeeType);//费用类型（1.正常收费，2.追缴收费）
                entity.StopTime = Convert.ToInt32(StopTime.Split('.')[0]);//停车时长
                entity.ReceivableCarNo = CardNo;   //出场卡号   如果为0的话，出场支付类型是现金

                var count = Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "update abpberths set BerthStatus = '2', RelateNumber = '" +
                    entity.PlateNumber + "', OutCarTime = '" + CarOutTime + "', SensorGuid = '00000000-0000-0000-0000-000000000000' where BerthNumber = '" + entity.BerthNumber + "' and IsActive = 1 and BerthsecId = " + BerthsecId);

                if (count == 0)
                    throw new AbpAuthorizationException("出场失败：不存在该泊位编号！", "23");
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
                //        berth.SensorGuid = new Guid("00000000-0000-0000-0000-000000000000");

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
                            EmployeeId = AbpSession.UserId.Value,
                            PlateNumber = entity.PlateNumber,
                            Remark = "消费",
                            InTime = Convert.ToDateTime(CarOutTime),
                            BeginMoney = otherAccount.Wallet,
                            EndMoney = otherAccount.Wallet - (Money - entity.Prepaid)
                        };
                        _deductionRecordRepository.Insert(deductionrecord);
                        if (otherAccount.TelePhone != null && entity.Money > 0)
                        {
                            //消费金额 以及账户余额（短信发送）
                            _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto() { Destnumbers = otherAccount.TelePhone, Msg = string.Format(_smsModelAppService.GetSmsModelByType("ConsumptionEqualModel").SmsContext, Convert.ToDateTime(CarOutTime).ToString("yyyy年MM月dd日HH时mm分"), entity.BerthNumber, entity.Prepaid, entity.PlateNumber, otherAccount.Wallet, entity.StopTime, entity.Money) });
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

                            BeginMoney = otherAccount.Wallet,
                            EndMoney = otherAccount.Wallet + entity.Prepaid - Money,

                            PayStatus = true,
                            CardNo = CardNo,
                            EmployeeId = AbpSession.UserId.Value,
                            PlateNumber = entity.PlateNumber,
                            Remark = "返还",
                            InTime = Convert.ToDateTime(CarOutTime)
                        };
                        _deductionRecordRepository.Insert(dedu);
                        if (otherAccount.TelePhone != null)
                        {
                            //返回金额 以及账户余额（短信发送）
                            _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto() { Destnumbers = otherAccount.TelePhone, Msg = string.Format(_smsModelAppService.GetSmsModelByType("ConsumptionGreaterModel").SmsContext, Convert.ToDateTime(CarOutTime).ToString("yyyy年MM月dd日HH时mm分"), entity.BerthNumber, entity.Prepaid, dedu.Money, entity.PlateNumber, otherAccount.Wallet + dedu.Money) });
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
                            EmployeeId = AbpSession.UserId.Value,
                            PlateNumber = entity.PlateNumber,
                            Remark = "消费",
                            InTime = Convert.ToDateTime(CarOutTime),
                            BeginMoney = otherAccount.Wallet,
                            EndMoney = otherAccount.Wallet - (Money - entity.Prepaid)
                        };
                        _deductionRecordRepository.Insert(deductionrecord);
                        if (!string.IsNullOrEmpty(otherAccount.TelePhone) && deductionrecord.Money > 0)
                        {
                            //消费金额 以及账户余额（短信发送）
                            _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto() { Destnumbers = otherAccount.TelePhone, Msg = string.Format(_smsModelAppService.GetSmsModelByType("ConsumptionLessModel").SmsContext, Convert.ToDateTime(CarOutTime).ToString("yyyy年MM月dd日HH时mm分"), entity.BerthNumber, entity.Prepaid, deductionrecord.Money, entity.PlateNumber, otherAccount.Wallet - deductionrecord.Money) });
                        }
                        otherAccount.Wallet = otherAccount.Wallet - deductionrecord.Money;  //账户余额减掉消费金额
                        _otherAccountRepository.Update(otherAccount);
                    }
                    //出场应收 
                    entity.FactReceive = Convert.ToDecimal(FactReceive);        //实收
                    entity.BeforeDiscount = BeforeDiscount;//折前应收
                    entity.DiscountMoney = DiscountMoney;//折扣金额    
                    entity.DiscountRate = DiscountRate;//折扣
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
                entity.Receivable = Receivable;
                _businessDetailRepository.Update(entity);
                if (AbpSession.UserSource == 1)
                {
                    return true;
                }
                var employee = _employeeAppService.Load(AbpSession.UserId.Value);

                var Berthsec = _berthsecRepository.FirstOrDefault(entity.BerthsecId);
                string berthsecName = "";
                if (Berthsec != default(Berthsecs.Berthsec))
                {
                    berthsecName = Berthsec.BerthsecName;
                }


                if (entity.Status == 3)
                {
                    string message = "出场车辆" + entity.PlateNumber + ",驶入时间:" + entity.CarInTime + ",驶出时间:" + entity.CarOutTime + "停靠" + berthsecName + entity.BerthNumber + "泊位，停车时长:" + StopTimes(entity.StopTime) + ",应收金额" + entity.Money + "元，预缴金额:" + entity.Prepaid + "元,支付金额:" + entity.FactReceive + "元，欠费金额:" + entity.Arrearage + "元，操作收费员:" + employee.TrueName + "(" + employee.UserName + ")";
                    _p4ChatService.CreateChatService().Clients.Group(P4Consts.EmployeeGroup + AbpSession.TenantId.Value).EmployeeMessage(employee.TrueName + "(" + employee.UserName + ")", message, 4);
                    _p4ChatService.CreateChatService().Clients.Group("index" + AbpSession.TenantId.Value).sendIndexMessage(berthsecName, entity.BerthsecId, entity.BerthsecId, employee.Id, entity.Arrearage, 3);
                    _p4ChatService.CreateChatService().Clients.Group("index" + AbpSession.TenantId.Value).sendIndexMessage(berthsecName, entity.BerthsecId, entity.BerthsecId, employee.Id, entity.Money, 1);
                }
                else
                {
                    string message = "出场车辆" + entity.PlateNumber + ",驶入时间:" + entity.CarInTime + ",驶出时间:" + entity.CarOutTime + "停靠" + berthsecName + entity.BerthNumber + "泊位，停车时长:" + StopTimes(entity.StopTime) + ",应收金额:" + entity.Money + "元，预缴金额:" + entity.Prepaid + "元,支付金额:" + entity.FactReceive + "元,操作收费员:" + employee.TrueName + "(" + employee.UserName + ")";
                    _p4ChatService.CreateChatService().Clients.Group(P4Consts.EmployeeGroup + AbpSession.TenantId.Value).EmployeeMessage(employee.TrueName + "(" + employee.UserName + ")", message, 3);
                    _p4ChatService.CreateChatService().Clients.Group("index" + AbpSession.TenantId.Value).sendIndexMessage(berthsecName, entity.BerthsecId, employee.Id, entity.FactReceive, 2);
                    _p4ChatService.CreateChatService().Clients.Group("index" + AbpSession.TenantId.Value).sendIndexMessage(berthsecName, entity.BerthsecId, employee.Id, entity.Money, 1);
                }

                //黑名单检测，黑名单检测短信报警
                var blackModel = _blackListRepository.FirstOrDefault(entry => entry.RelateNumber == entity.PlateNumber && entry.IsActive == true);
                if (blackModel != default(BlackLists.BlackList))
                {
                    //黑名单车辆{0},车主姓名{1}，在{2}驶出{3}{4}泊位号，请悉知！
                    _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto() { Msg = string.Format(_smsModelAppService.GetSmsModelByType("BlackCarOutModel").SmsContext, blackModel.RelateNumber, blackModel.CarOwner, Convert.ToDateTime(entity.CarOutTime).ToString("yyyy年MM月dd日hh时mm分"), berthsecName, entity.BerthNumber), Destnumbers = blackModel.IdNumber });
                }
            }
            return true;
        }

        /// <summary>
        /// 车辆出场接口（存在折扣率）
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
        /// <param name="CardNo"></param>
        /// <param name="BeforeDiscount"></param>
        /// <param name="DiscountMoney"></param>
        /// <param name="DiscountRate"></param>
        /// <param name="BerthsecId"></param>
        /// <param name="OutBatchNo"></param>
        /// <returns></returns>
        [AbpAuthorize]
        [UnitOfWork(isTransactional: false)]
        public bool DiscountInsertCarOut(decimal Receivable, string FactReceive, string CarOutTime, string CarPayTime, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money, string CardNo, decimal BeforeDiscount, decimal DiscountMoney, double DiscountRate, int BerthsecId, string OutBatchNo)
        {
            if (PayStatus != "4")
                CardNo = "0";
            Guid g = new Guid(guid);
            List<BusinessDetail> entitys = new List<BusinessDetail>();

            entitys = Abp.DataProcessHelper.GetEntityFromTable<BusinessDetail>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpBusinessDetail with(nolock) where  guid = '" + g + "'").Tables[0]);

            if (entitys.Count == 0)
            {
                throw new AbpAuthorizationException("出场失败：guid不存在！", "22");
            }

            BusinessDetail entity = new BusinessDetail();
            entity = entitys[0];

            if ((entity.Status != 1 && entity.Status != 3) || entity.LastModifierUserId.HasValue)//已经出场处理
                throw new AbpAuthorizationException("出场失败：该数据已出场！", "201");

            Berth berthmodel = Abp.DataProcessHelper.GetEntityFromTable<Berth>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpBerths with(nolock) where BerthsecId = " + BerthsecId + " and BerthNumber = '" + entity.BerthNumber + "'").Tables[0])[0];

            DateTime? SensorsOutCarTime1 = null;

            //处理白名单         
            var whiteList = _abpCacheManager.GetCache(string.Format(Abp.Configuration.SettingManager.CacheWhiteCarValue, AbpSession.CompanyId.Value)).Get(string.Format(Abp.Configuration.SettingManager.CacheWhiteCarValue, AbpSession.CompanyId.Value), () => GetWhiteListCache()) as List<WhiteListsDto>;

            var whitemodel = whiteList.FirstOrDefault(e => e.RelateNumber == berthmodel.RelateNumber && e.CompanyId == AbpSession.CompanyId.Value);
            // var whitemodel = _whiteRepository.FirstOrDefault(whiteentity => whiteentity.RelateNumber == berthmodel.RelateNumber);
            if (whitemodel != default(WhiteListsDto))
            {
                Receivable = 0;
                FactReceive = "0";
                Money = 0;
            }
            else//包月车辆数据
            {
                List<MonthlyCarDto> MonthlyCarList = new List<MonthlyCarDto>();
                var monthlyCarList = _abpCacheManager.GetCache(string.Format(Abp.Configuration.SettingManager.CacheMonthyValue, AbpSession.CompanyId.Value))
                    .Get(string.Format(Abp.Configuration.SettingManager.CacheMonthyValue, AbpSession.CompanyId.Value), () => GetMonthlyCarCache()) as List<MonthlyCarDto>;
                var list = monthlyCarList.Where(e => e.PlateNumber == berthmodel.RelateNumber && e.CompanyId == AbpSession.CompanyId.Value && e.BeginTime <= DateTime.Now && e.EndTime >= DateTime.Now).ToList();
                var parkId = "," + berthmodel.ParkId + ",";
                for (int i = 0; i < list.Count; i++)
                {
                    if (("," + list[i].ParkIds + ",").Contains(parkId) || list[i].ParkIds == "0")
                    {
                        MonthlyCarList.Add(list[i]);
                        break;
                    }
                }
                //var param = new SqlParameter[] { new SqlParameter("@parkid", "," + AbpSession.ParkIds[0] + ",") };
                //var MonthlyCarList = Abp.DataProcessHelper.GetEntityFromTable<MonthlyCarDto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select * from AbpMonthlyCars with(nolock) where (case when CompanyId is null then AbpMonthlyCars.tenantid else " + AbpSession.TenantId.Value + "  end) = " + AbpSession.TenantId.Value + " and(case when CompanyId is not null then AbpMonthlyCars.CompanyId else " + AbpSession.CompanyId.Value + " end) = " + AbpSession.CompanyId.Value + " and BeginTime <= getdate() and EndTime >= getdate() and(charindex(@parkid, ',' + ParkIds + ',') > 0 or ParkIds = '0') and IsDeleted = 0 and PlateNumber = '" + berthmodel.RelateNumber + "'", param).Tables[0]);
                if (MonthlyCarList.Count > 0 && MonthlyCarList[0].MonthyType == "MonthyAll")
                {
                    Receivable = 0;
                    FactReceive = "0";
                    Money = 0;
                }
            }

            if (berthmodel == default(Berth))
            {
                SensorsOutCarTime1 = null;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(berthmodel.SensorNumber))
                {
                    SensorsOutCarTime1 = berthmodel.SensorsOutCarTime;
                }
            }

            //    return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 12, Message = "退出失败，在线用户信息已经丢失！ " }), JsonRequestBehavior.AllowGet);
            if (entity != null)
            {
                if (bool.Parse(_settingStore.GetSettingOrNull(AbpSession.TenantId, null, "EscapeLock").Value))
                    PlateNumberDebLock(entity.PlateNumber);//车辆出场的时候 对车辆进行解锁

                entity.OutDeviceCode = AbpSession.DeviceCode;               //出场设备
                //entity.Receivable = Convert.ToDecimal(Receivable);        //应收

                //entity.Arrearage = Convert.ToDecimal(Arrearage);          //欠费
                if (AbpSession.UserSource != 1)
                {
                    entity.ChargeOperaId = AbpSession.UserId;                   //收费操作员Id
                    entity.ChargeDeviceCode = AbpSession.DeviceCode;            //收费员设备
                    entity.OutOperaId = Convert.ToInt16(AbpSession.UserId);     //出场收费员ID
                }

                entity.CarOutTime = Convert.ToDateTime(CarOutTime);         //出场时间
                entity.CarPayTime = Convert.ToDateTime(CarPayTime);         //支付时间
                entity.OutBatchNo = OutBatchNo;       //出厂批次号

                entity.guid = g;                               //唯一guid

                if (entity.guid == berthmodel.SensorGuid)                    //如果车检器与收费记录中guid相等，关联车检器入场出场信息
                {
                    entity.SensorsOutCarTime = SensorsOutCarTime1;//车检器出场时间

                    if (entity.SensorsInCarTime.HasValue && entity.SensorsOutCarTime.HasValue)
                    {
                        entity.SensorsStopTime = Convert.ToInt32(entity.SensorsOutCarTime.Value.Subtract(entity.SensorsInCarTime.Value).TotalMinutes);
                    }
                    entity.SensorsReceivable = Convert.ToDecimal(SensorsReceivable);//车检收器应
                }
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
                if (PayStatus == "1")                                           //现金
                {
                    //entity.Receivable = Receivable;                             //出场应收 
                    entity.FactReceive = Convert.ToDecimal(FactReceive);        //实收
                    entity.DiscountRate = DiscountRate;//折扣率
                    entity.DiscountMoney = DiscountMoney;//折扣金额
                    entity.BeforeDiscount = BeforeDiscount;//打折前应收
                    entity.Arrearage = 0;
                }
                else if (PayStatus == "3")                                      //微信支付
                {
                    //entity.Receivable = Receivable;                             //出场应收 
                    entity.FactReceive = Convert.ToDecimal(FactReceive);        //实收
                    entity.DiscountRate = DiscountRate;//折扣率
                    entity.DiscountMoney = DiscountMoney;//折扣金额
                    entity.BeforeDiscount = BeforeDiscount;//打折前应收
                    entity.Arrearage = 0;
                }
                else if (PayStatus == "4")                                      //账号支付
                {
                    //entity.Receivable = Receivable;                             //出场应收 
                    entity.FactReceive = Convert.ToDecimal(FactReceive);        //实收
                    entity.DiscountRate = DiscountRate;//折扣率
                    entity.DiscountMoney = DiscountMoney;//折扣金额
                    entity.BeforeDiscount = BeforeDiscount;//打折前应收
                    entity.Arrearage = 0;
                }
                entity.FeeType = Convert.ToInt16(FeeType);//费用类型（1.正常收费，2.追缴收费）
                entity.StopTime = Convert.ToInt32(StopTime.Split('.')[0]);//停车时长
                entity.ReceivableCarNo = CardNo;   //出场卡号   如果为0的话，出场支付类型是现金

                var count = Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "update abpberths set BerthStatus = '2', RelateNumber = '" +
                    entity.PlateNumber + "', OutCarTime = '" + CarOutTime + "', SensorGuid = '00000000-0000-0000-0000-000000000000' where BerthNumber = '" + entity.BerthNumber + "' and IsActive = 1 and BerthsecId = " + BerthsecId);

                if (count == 0)
                    throw new AbpAuthorizationException("出场失败：不存在该泊位编号！", "23");

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
                            EmployeeId = AbpSession.UserId.Value,
                            PlateNumber = entity.PlateNumber,
                            Remark = "消费",
                            InTime = Convert.ToDateTime(CarOutTime),
                            BeginMoney = otherAccount.Wallet,
                            EndMoney = otherAccount.Wallet - (Money - entity.Prepaid)
                        };
                        _deductionRecordRepository.Insert(deductionrecord);
                        if (otherAccount.TelePhone != null && entity.Money > 0)
                        {
                            //消费金额 以及账户余额（短信发送）
                            _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto() { Destnumbers = otherAccount.TelePhone, Msg = string.Format(_smsModelAppService.GetSmsModelByType("ConsumptionEqualModel").SmsContext, Convert.ToDateTime(CarOutTime).ToString("yyyy年MM月dd日HH时mm分"), entity.BerthNumber, entity.Prepaid, entity.PlateNumber, otherAccount.Wallet, entity.StopTime, entity.Money) });
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

                            BeginMoney = otherAccount.Wallet,
                            EndMoney = otherAccount.Wallet + entity.Prepaid - Money,

                            PayStatus = true,
                            CardNo = CardNo,
                            EmployeeId = AbpSession.UserId.Value,
                            PlateNumber = entity.PlateNumber,
                            Remark = "返还",
                            InTime = Convert.ToDateTime(CarOutTime)
                        };
                        _deductionRecordRepository.Insert(dedu);
                        if (otherAccount.TelePhone != null)
                        {
                            //返回金额 以及账户余额（短信发送）
                            _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto() { Destnumbers = otherAccount.TelePhone, Msg = string.Format(_smsModelAppService.GetSmsModelByType("ConsumptionGreaterModel").SmsContext, Convert.ToDateTime(CarOutTime).ToString("yyyy年MM月dd日HH时mm分"), entity.BerthNumber, entity.Prepaid, dedu.Money, entity.PlateNumber, otherAccount.Wallet + dedu.Money) });
                        }

                        otherAccount.Wallet = otherAccount.Wallet + dedu.Money;  //账户余额加上返还金额
                        _otherAccountRepository.Update(otherAccount);
                    }
                    else if (Money > entity.Prepaid && entity.Status != 5 && entity.Status != 3)  //消费金额大于预付（消费）
                    {
                        //判断是否存在优惠券，确定优惠券金额
                        decimal tempmoney = Money;
                        var list = Abp.DataProcessHelper.GetEntityFromTable<Weixin.Coupon>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpCoupons where IsActive = 0 and money > 0 and EndTime > getdate() and tel = '" + otherAccount.TelePhone + "' order by Money desc").Tables[0]);
                        if (list.Count > 0)
                        {
                            P4.Weixin.Coupon model = new Weixin.Coupon();
                            model = list[0];
                            if (list[0].Money <= Money)//优惠券金额小于等于消费金额
                            {

                                CouponDetailDto dto = new CouponDetailDto();
                                dto.tel = list[0].tel;
                                dto.NewMoney = 0;
                                dto.CouponNo = list[0].CouponNo;
                                dto.OldMoney = list[0].Money;
                                dto.ConsumptionMoney = list[0].Money;
                                dto.Platenumber = entity.PlateNumber;
                                dto.Source = "平台消费";
                                dto.CouponType = 2;
                                _couponDetailsAppService.AddCouponDetail(dto);



                                tempmoney = tempmoney - list[0].Money;
                                model.Money = 0;
                                model.IsActive = true;
                                _couponRepository.Update(model);
                                
                            }
                            else//如果优惠券金额大于消费金额
                            {
                                CouponDetailDto dto = new CouponDetailDto();
                                dto.tel = list[0].tel;
                                dto.NewMoney = model.Money - tempmoney;
                                dto.CouponNo = list[0].CouponNo;
                                dto.OldMoney = list[0].Money;
                                dto.ConsumptionMoney = tempmoney;
                                dto.Platenumber = entity.PlateNumber;
                                dto.Source = "平台消费";
                                dto.CouponType = 2;
                                _couponDetailsAppService.AddCouponDetail(dto);

                                model.Money = model.Money - tempmoney;
                                _couponRepository.Update(model);
                                tempmoney = 0;
                            }
                        }
                        if (tempmoney > 0)
                        {
                            //添加消费记录
                            DeductionRecord deductionrecord = new DeductionRecord()
                            {
                                OtherAccountId = otherAccount.Id,
                                OperType = 2,
                                Money = tempmoney - entity.Prepaid,  //消费金额
                                PayStatus = true,
                                CardNo = CardNo,
                                EmployeeId = AbpSession.UserId.Value,
                                PlateNumber = entity.PlateNumber,
                                Remark = "消费",
                                InTime = Convert.ToDateTime(CarOutTime),
                                BeginMoney = otherAccount.Wallet,
                                EndMoney = otherAccount.Wallet - (tempmoney - entity.Prepaid)
                            };
                            _deductionRecordRepository.Insert(deductionrecord);
                            if (!string.IsNullOrEmpty(otherAccount.TelePhone) && deductionrecord.Money > 0)
                            {
                                //消费金额 以及账户余额（短信发送）
                                _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto() { Destnumbers = otherAccount.TelePhone, Msg = string.Format(_smsModelAppService.GetSmsModelByType("ConsumptionLessModel").SmsContext, Convert.ToDateTime(CarOutTime).ToString("yyyy年MM月dd日HH时mm分"), entity.BerthNumber, entity.Prepaid, deductionrecord.Money, entity.PlateNumber, otherAccount.Wallet - deductionrecord.Money) });
                            }
                            otherAccount.Wallet = otherAccount.Wallet - deductionrecord.Money;  //账户余额减掉消费金额
                            _otherAccountRepository.Update(otherAccount);
                        }
                    }
                    //出场应收 
                    entity.FactReceive = Convert.ToDecimal(FactReceive);        //实收
                    entity.BeforeDiscount = BeforeDiscount;//折前应收
                    entity.DiscountMoney = DiscountMoney;//折扣金额    
                    entity.DiscountRate = DiscountRate;//折扣
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
                entity.Receivable = Receivable;
                _businessDetailRepository.Update(entity);
                if (AbpSession.UserSource == 1)
                {
                    return true;
                }
                var employee = _employeeAppService.Load(AbpSession.UserId.Value);

                var Berthsec = _berthsecRepository.FirstOrDefault(entity.BerthsecId);
                string berthsecName = "";
                if (Berthsec != default(Berthsecs.Berthsec))
                {
                    berthsecName = Berthsec.BerthsecName;
                }


                if (entity.Status == 3)
                {
                    string message = "出场车辆" + entity.PlateNumber + ",驶入时间:" + entity.CarInTime + ",驶出时间:" + entity.CarOutTime + "停靠" + berthsecName + entity.BerthNumber + "泊位，停车时长:" + StopTimes(entity.StopTime) + ",应收金额" + entity.Money + "元，预缴金额:" + entity.Prepaid + "元,支付金额:" + entity.FactReceive + "元，欠费金额:" + entity.Arrearage + "元，操作收费员:" + employee.TrueName + "(" + employee.UserName + ")";
                    _p4ChatService.CreateChatService().Clients.Group(P4Consts.EmployeeGroup + AbpSession.TenantId.Value).EmployeeMessage(employee.TrueName + "(" + employee.UserName + ")", message, 4);
                    _p4ChatService.CreateChatService().Clients.Group("index" + AbpSession.TenantId.Value).sendIndexMessage(berthsecName, entity.BerthsecId, employee.Id, entity.Arrearage, 3);
                    _p4ChatService.CreateChatService().Clients.Group("index" + AbpSession.TenantId.Value).sendIndexMessage(berthsecName, entity.BerthsecId, employee.Id, entity.Money, 1);

                    _subscribeAppService.SendMessage("CarInOutManagement", message, "");
                }
                else
                {
                    string message = "出场车辆" + entity.PlateNumber + ",驶入时间:" + entity.CarInTime + ",驶出时间:" + entity.CarOutTime + "停靠" + berthsecName + entity.BerthNumber + "泊位，停车时长:" + StopTimes(entity.StopTime) + ",应收金额:" + entity.Money + "元，预缴金额:" + entity.Prepaid + "元,支付金额:" + entity.FactReceive + "元,操作收费员:" + employee.TrueName + "(" + employee.UserName + ")";
                    _p4ChatService.CreateChatService().Clients.Group(P4Consts.EmployeeGroup + AbpSession.TenantId.Value).EmployeeMessage(employee.TrueName + "(" + employee.UserName + ")", message, 3);
                    _p4ChatService.CreateChatService().Clients.Group("index" + AbpSession.TenantId.Value).sendIndexMessage(berthsecName, entity.BerthsecId, employee.Id, entity.FactReceive, 2);
                    _p4ChatService.CreateChatService().Clients.Group("index" + AbpSession.TenantId.Value).sendIndexMessage(berthsecName, entity.BerthsecId, employee.Id, entity.Money, 1);
                    _subscribeAppService.SendMessage("CarInOutManagement", message, "");
                }

                //黑名单检测，黑名单检测短信报警
                var blackModel = _blackListRepository.FirstOrDefault(entry => entry.RelateNumber == entity.PlateNumber && entry.IsActive == true);
                if (blackModel != default(BlackLists.BlackList))
                {
                    //黑名单车辆{0},车主姓名{1}，在{2}驶出{3}{4}泊位号，请悉知！
                    var model = new SmsAccountDto() { Msg = string.Format(_smsModelAppService.GetSmsModelByType("BlackCarOutModel").SmsContext, blackModel.RelateNumber, blackModel.CarOwner, Convert.ToDateTime(entity.CarOutTime).ToString("yyyy年MM月dd日hh时mm分"), berthsecName, entity.BerthNumber), Destnumbers = blackModel.IdNumber };
                    _smsManagementAppService.SendSmsNoTenant(model);
                    _subscribeAppService.SendMessage("BlackManagement", model.MsgValue, "");
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StopTime"></param>
        /// <returns></returns>
        private string StopTimes(int? StopTime)
        {
            if (!StopTime.HasValue)
                return "0分钟";
            int Getstoptime = Convert.ToInt32(StopTime);
            TimeSpan ts = new TimeSpan(0, 0, Getstoptime, 0);
            string dateDiff = "";
            if (Getstoptime >= 1440)//判断是否大于24小时
            {
                dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟";
            }
            else
            {
                dateDiff = ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟";
            }
            return dateDiff;
        }

        /// <summary>
        /// 私有的只读静态对象
        /// </summary>
        private static readonly object lockHelper = new object();

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
        /// <param name="BeforeDiscount"></param>
        /// <param name="DiscountMoney"></param>
        /// <param name="DiscountRate"></param>
        /// <param name="PaymentBatchNo"></param>
        /// <returns></returns>
        [AbpAuthorize]
        [UnitOfWork(isTransactional: false)]
        public bool DiscountFeeBack(int id, decimal Repayment, DateTime CarRepaymentTime, bool IsEscapePay, short EscapePayStatus, short PaymentType, string CardNo, decimal BeforeDiscount, decimal DiscountMoney, double DiscountRate, string PaymentBatchNo)
        {
            BMessage BM = new BMessage();
            BusinessDetail entity = new BusinessDetail();
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany, AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark, AbpDataFilters.MustHaveBerthsec);
            List<BusinessDetail> list = new List<BusinessDetail>();
            list = Abp.DataProcessHelper.GetEntityFromTable<BusinessDetail>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpBusinessDetail with(nolock) where isdeleted = 0 and Id = " + id).Tables[0]);
            if (list.Count > 0)
            {
                entity = list[0];
                if (entity.Status == 3 || entity.Status == 5)  //欠费金额不等于0  并且逃逸未收费状态
                {

                    entity.Repayment = Repayment;//补缴金额
                    entity.CarRepaymentTime = CarRepaymentTime;//补缴时间
                    entity.EscapePayStatus = EscapePayStatus;//逃逸追缴支付类型   1.现金支付 2.刷机支付
                    entity.IsEscapePay = IsEscapePay;//逃逸是否支付
                    entity.EscapeOperaId = AbpSession.UserId;//逃逸追缴收费员ID
                    entity.EscapeDeviceCode = AbpSession.DeviceCode;//逃逸追缴设备
                    entity.EscapeTenantId = AbpSession.TenantId;//追缴商户ID
                    entity.EscapeCompanyId = AbpSession.CompanyId.Value;
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
                        lock (lockHelper)
                        {
                            var data = Abp.DataProcessHelper.GetEntityFromTable<BusinessDetail>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpBusinessDetail with(nolock) where isdeleted = 0 and Id = " + id).Tables[0]);
                            if (data[0].Status == 3 || data[0].Status == 5)
                            {
                                otherAccount = Abp.DataProcessHelper.GetEntityFromTable<OtherAccount>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from ExtOtherAccount where IsActive = 1 and CardNo = '" + CardNo + "'").Tables[0])[0];
                                //添加消费记录   缴费
                                DeductionRecord deductionrecord = new DeductionRecord()
                                {
                                    OtherAccountId = otherAccount.Id,
                                    OperType = 2,
                                    Money = Repayment,  //消费金额
                                    PayStatus = true,
                                    CardNo = CardNo,
                                    EmployeeId = AbpSession.UserId.Value,
                                    PlateNumber = entity.PlateNumber,
                                    Remark = "消费",
                                    InTime = CarRepaymentTime,
                                    BeginMoney = otherAccount.Wallet,
                                    EndMoney = otherAccount.Wallet - Repayment
                                };
                                _deductionRecordRepository.Insert(deductionrecord);
                                if (!string.IsNullOrWhiteSpace(otherAccount.TelePhone))
                                {
                                    //欠费追缴短信推送
                                    _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto() { Destnumbers = otherAccount.TelePhone, Msg = string.Format(_smsModelAppService.GetSmsModelByType("ConsumptionPaymentModel").SmsContext, Convert.ToDateTime(CarRepaymentTime).ToString("yyyy年MM月dd日hh时mm分"), Repayment, entity.PlateNumber, otherAccount.Wallet - Repayment) });
                                }
                                //费用补缴  卡的余额扣去缴费金额
                                otherAccount.Wallet = otherAccount.Wallet - Repayment;
                                SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, "update ExtOtherAccount set Wallet =  " + otherAccount.Wallet + " where Id = " + otherAccount.Id);
                                entity.EscapeOtherAccountId = otherAccount.Id;
                                decimal Money = BeforeDiscount - DiscountMoney;
                                entity.Arrearage = Convert.ToDecimal(Money) - entity.Prepaid;   //如f果车主逃逸（未付费）  写入欠费字段
                                entity.BeforeDiscount = BeforeDiscount;//折扣前的钱
                                entity.DiscountMoney = DiscountMoney;//折扣金额
                                entity.DiscountRate = DiscountRate;//折扣率
                                entity.Money = Convert.ToDecimal(Money);
                                entity.PaymentBatchNo = PaymentBatchNo;
                                _businessDetailRepository.Update(entity);
                                var employee = _employeeAppService.Load(AbpSession.UserId.Value);
                                string message = "补缴车辆" + entity.PlateNumber + "在" + entity.CarRepaymentTime + "补缴欠费金额:" + entity.Arrearage + "元，操作收费员:" + employee.TrueName + "(" + employee.UserName + ")";
                                _p4ChatService.CreateChatService().Clients.Group(P4Consts.EmployeeGroup + AbpSession.TenantId.Value).EmployeeMessage(employee.TrueName + "(" + employee.UserName + ")", message, 5);
                                _p4ChatService.CreateChatService().Clients.Group("index" + AbpSession.TenantId.Value).sendIndexMessage(employee.TrueName, entity.BerthsecId, employee.Id, entity.Arrearage, 4);
                                _subscribeAppService.SendMessage("CarInOutManagement", message, "");
                                return true;
                            }
                            else
                            {
                                throw new AbpAuthorizationException("补缴失败：该车没有欠费", "24");
                            }
                        }
                    }
                    else
                    {
                        decimal Money = BeforeDiscount - DiscountMoney;
                        entity.Arrearage = Convert.ToDecimal(Money) - entity.Prepaid;   //如f果车主逃逸（未付费）  写入欠费字段
                        entity.BeforeDiscount = BeforeDiscount;//折扣前的钱
                        entity.DiscountMoney = DiscountMoney;//折扣金额
                        entity.DiscountRate = DiscountRate;//折扣率
                        entity.Money = Convert.ToDecimal(Money);
                        entity.PaymentBatchNo = PaymentBatchNo;
                        _businessDetailRepository.Update(entity);
                        var employee = _employeeAppService.Load(AbpSession.UserId.Value);
                        string message = "补缴车辆" + entity.PlateNumber + "在" + entity.CarRepaymentTime + "补缴欠费金额:" + entity.Arrearage + "元，操作收费员:" + employee.TrueName + "(" + employee.UserName + ")";
                        _p4ChatService.CreateChatService().Clients.Group(P4Consts.EmployeeGroup + AbpSession.TenantId.Value).EmployeeMessage(employee.TrueName + "(" + employee.UserName + ")", message, 5);
                        _p4ChatService.CreateChatService().Clients.Group("index" + AbpSession.TenantId.Value).sendIndexMessage(employee.TrueName, entity.BerthsecId, employee.Id, entity.Arrearage, 4);
                        _subscribeAppService.SendMessage("CarInOutManagement", message, "");
                        return true;
                    }
                }
                else
                {
                    throw new AbpAuthorizationException("补缴失败：该车没有欠费", "24");
                }
            }
            throw new AbpAuthorizationException("补缴失败：未找到该车记录！", "25");
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
        /// <param name="PaymentBatchNo"></param>
        /// <returns></returns>
        [AbpAuthorize]
        [UnitOfWork(isTransactional: false)]
        public bool FeeBack(int id, decimal Repayment, DateTime CarRepaymentTime, bool IsEscapePay, short EscapePayStatus, short PaymentType, string CardNo, string PaymentBatchNo)
        {
            return DiscountFeeBack(id, Repayment, CarRepaymentTime, IsEscapePay, EscapePayStatus, PaymentType, CardNo, Repayment, 0, 10, PaymentBatchNo);
            #region 新增加刷卡打折
            //BMessage BM = new BMessage();
            //BusinessDetail entity = new BusinessDetail();
            //_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany, AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark, AbpDataFilters.MustHaveBerthsec);

            //entity = _businessDetailRepository.FirstOrDefault(b => b.Id == id);

            //if (entity != null)
            //{
            //    if (entity.Arrearage != 0 && (entity.Status == 3 || entity.Status == 5))  //欠费金额不等于0  并且逃逸未收费状态
            //    {
            //        entity.Repayment = Repayment;//补缴金额
            //        //entity.Arrearage = entity.Arrearage - Repayment;  //欠费金额等于欠费金额减去补缴金额
            //        entity.CarRepaymentTime = CarRepaymentTime;//补缴时间
            //        entity.EscapePayStatus = EscapePayStatus;//逃逸追缴支付类型   1.现金支付 2.刷机支付
            //        entity.IsEscapePay = IsEscapePay;//逃逸是否支付
            //        entity.EscapeOperaId = AbpSession.UserId;//逃逸追缴收费员ID
            //        entity.EscapeDeviceCode = AbpSession.DeviceCode;//逃逸追缴设备
            //        entity.EscapeTenantId = AbpSession.TenantId;//追缴商户ID
            //        entity.PaymentType = PaymentType;// 追缴类型 1.前端追缴2.后台追缴3.微信追缴
            //        entity.Status = 4;//  2.逃逸已收费
            //        entity.EscapeCardNo = CardNo;

            //        //钱包扣费记录

            //        var otherAccount = _otherAccountRepository.FirstOrDefault(c => c.CardNo == CardNo && c.IsActive == true);
            //        if (otherAccount != null)
            //        {
            //            //添加消费记录   缴费
            //            DeductionRecord deductionrecord = new DeductionRecord()
            //            {
            //                OtherAccountId = otherAccount.Id,
            //                OperType = 2,
            //                Money = Repayment,  //消费金额
            //                PayStatus = true,
            //                CardNo = CardNo,
            //                EmployeeId = AbpSession.UserId.Value,
            //                PlateNumber = entity.PlateNumber,
            //                Remark = "消费",
            //                InTime = CarRepaymentTime
            //            };
            //            _deductionRecordRepository.Insert(deductionrecord);
            //            if (!string.IsNullOrWhiteSpace(otherAccount.TelePhone))
            //            {
            //                //欠费追缴短信推送
            //                _smsManagementAppService.SendSms(new SmsAccountDto() { Destnumbers = otherAccount.TelePhone, Msg = string.Format(P4Consts.ConsumptionPaymentModel, Convert.ToDateTime(CarRepaymentTime).ToString("yyyy年MM月dd日hh时mm分"), Repayment, entity.PlateNumber, otherAccount.Wallet - Repayment) });
            //            }
            //            //费用补缴  卡的余额扣去缴费金额
            //            otherAccount.Wallet = otherAccount.Wallet - Repayment;
            //            _otherAccountRepository.Update(otherAccount);
            //            entity.EscapeOtherAccountId = otherAccount.Id;
            //        }
            //        _businessDetailRepository.Update(entity);
            //        //BM.B_success = true;
            //        //BM.SuccessMessage = "费用补缴成功！";
            //        //return BM;
            //        return true;
            //}
            //else
            //{
            //    throw new AbpAuthorizationException("补缴失败：该车没有欠费", "24");
            //    //BM.B_success = false;
            //    //BM.Code = 100;
            //    //BM.ErrorMessage = "该车没有欠费！";
            //    //return BM;
            //}
            //}
            //throw new AbpAuthorizationException("补缴失败：未找到该车记录！", "25");
            //BM.B_success = false;
            //BM.Code = 200;
            //BM.ErrorMessage = "未找到该车记录！";
            //return BM;
            #endregion
        }
        //收费明细
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.GetBusinessDetailsOutput GetBusinessDetailsList(Dtos.GetBusinessDetailsInput input)
        {
            return _businessRepository.GetBusinessDetailsList(input);
        }
        /// <summary>
        /// 收费明细SQL
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetBusinessDetailsOutput GetBusinessDetailsListSql(GetBusinessDetailsInput input)
        {
            var list = new List<int>();
            list.Add(-1);
            input.CompanyIds = input.CompanyId.HasValue == false ? AbpSession.CompanyIds ?? list : new List<int> { input.CompanyId.Value };
            return _businessRepository.GetBusinessDetailsListSql(input);
        }

        /// <summary>
        /// 新增收费员批次号报表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetBusinessDetailsOutput GetGetEmployeeBatchNoDynamicReportSql(GetEmployeeBatchNoDynamicReportInput input)
        {
            var list = new List<int>();
            list.Add(-1);
            return _businessRepository.GetGetEmployeeBatchNoDynamicReportSql(input);
        }



        /// <summary>
        /// 返回当前时间   PDA那边对时间
        /// </summary>
        /// <returns></returns>
        public string dateTimeNow()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }





        /// <summary>
        /// 检测版本硬件是否启用
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        [DisableAuditing]
        public string CheckEquipmentStatus()
        {
            object obj = Abp.Helper.SqlHelper.ExecuteScalar(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select 1 from AbpEquipments where PDA = '" + AbpSession.DeviceCode + "' and IsActive = 1 and IsDeleted = 0");
            if (obj != null)
                return "1";
            return "0";
        }

        /// <summary>
        /// 车辆入场
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
        /// <param name="InBatchNo"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public bool InsertCarIn(string BerthNumber, string PlateNumber, string CarType, string Prepaid, string CarInTime, string guid, string SensorsInCarTime, string StopType, string CardNo, int RegionId, int ParkId, int BerthsecId, string Details, string InBatchNo)
        {
            return InsertCarIn(BerthNumber, PlateNumber, CarType, Prepaid, CarInTime, guid, SensorsInCarTime, StopType, CardNo, RegionId, ParkId, BerthsecId, InBatchNo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BerthNumber"></param>
        /// <param name="PlateNumber"></param>
        /// <param name="CarType"></param>
        /// <param name="Prepaid"></param>
        /// <param name="CarInTime"></param>
        /// <param name="InOperaId"></param>
        /// <param name="guid"></param>
        /// <param name="SensorsInCarTime"></param>
        /// <param name="StopType"></param>
        /// <param name="CardNo"></param>
        /// <param name="RegionId"></param>
        /// <param name="ParkId"></param>
        /// <param name="BerthsecId"></param>
        /// <param name="Details"></param>
        /// <param name="InBatchNo"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public bool InsertCarIn(string BerthNumber, string PlateNumber, string CarType, string Prepaid, string CarInTime, long InOperaId, string guid, string SensorsInCarTime, string StopType, string CardNo, int RegionId, int ParkId, int BerthsecId, string Details, string InBatchNo)
        {
            return InsertCarIn(BerthNumber, PlateNumber, CarType, Prepaid, CarInTime, guid, SensorsInCarTime, StopType, CardNo, RegionId, ParkId, BerthsecId, InBatchNo);
        }

        /// <summary>
        /// 车辆出厂
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
        /// <param name="CardNo"></param>
        /// <param name="BerthsecID"></param>
        /// <param name="OutBatchNo"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public bool InsertCarOut(decimal Receivable, string FactReceive, string Arrearage, string CarOutTime, string CarPayTime, string OutOperaId, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money, string CardNo, int BerthsecID, string OutBatchNo)
        {
            return DiscountInsertCarOut(Receivable, FactReceive, CarOutTime, CarPayTime, guid, SensorsOutCarTime, SensorsStopTime, SensorsReceivable, PayStatus, IsPay, FeeType, StopTime, Money, CardNo, Money, 0, 10, BerthsecID, OutBatchNo);
        }

        /// <summary>
        /// 远程车辆出场
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
        /// <param name="CardNo"></param>
        /// <param name="BerthsecId"></param>
        /// <param name="OutBatchNo"></param>
        /// <returns></returns>
        public bool InsertOtherCarOut(decimal Receivable, string FactReceive, string Arrearage, string CarOutTime, string CarPayTime, string OutOperaId, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money, string CardNo, string BerthsecId, string OutBatchNo)
        {
            bool flag = DiscountInsertCarOut(Receivable, FactReceive, CarOutTime, CarPayTime, guid, SensorsOutCarTime, SensorsStopTime, SensorsReceivable, PayStatus, IsPay, FeeType, StopTime, Money, CardNo, Money, 0, 10, int.Parse(BerthsecId), OutBatchNo);
            if (flag)   //出场成功
            {
                _remoteGuidRepository.Insert(new RemoteGuid() { BusinessDetailGuid = new Guid(guid), BerthsecId = int.Parse(BerthsecId) });
            }
            return flag;
        }

        /// <summary>
        /// 同步远程车辆出场数据
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        [DisableAuditing]
        public List<RemoteGuid> GetRemoteGuids()
        {
            var model = _remoteGuidRepository.GetAllList(entity => AbpSession.BerthsecIds.Contains(entity.BerthsecId) && entity.IsActive == false);
            return _remoteGuidRepository.GetAllList(entity => AbpSession.BerthsecIds.Contains(entity.BerthsecId) && entity.IsActive == false);
        }

        /// <summary>
        /// 远程出场功能
        /// </summary>
        /// <param name="guid"></param>
        public void UpdateRemoteGuidStatus(string guid)
        {
            var entity = _remoteGuidRepository.FirstOrDefault(entry => entry.BusinessDetailGuid == new Guid(guid) && entry.IsActive == false);
            entity.IsActive = true;
            _remoteGuidRepository.Update(entity);
            return;
            var tempmodel = Abp.DataProcessHelper.GetEntityFromTable<BusinessDetail>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpBusinessDetail with(nolock) where  guid = '" + guid + "' and status = 1 and isdeleted = 0").Tables[0]);
            if (tempmodel.Count > 0)
            {
                string BerthNumber = tempmodel[0].BerthNumber;
                int BerthsecId = tempmodel[0].BerthsecId;
                var berthDto = _berthRepository.FirstOrDefault(e => e.BerthNumber == BerthNumber && e.BerthsecId == BerthsecId);
                var CarOutTime = entity.CreationTime;
                if (berthDto != default(Berth) && berthDto.BerthStatus == "1")
                {

                    if (berthDto.guid == berthDto.SensorGuid && berthDto.SensorsOutCarTime != null)
                    {
                        CarOutTime = Convert.ToDateTime(berthDto.SensorsOutCarTime);
                    }
                }

                var rate = _ratesAppService.RateCalculate(tempmodel[0].BerthsecId, tempmodel[0].CarInTime.Value, CarOutTime, tempmodel[0].CarType, 0, tempmodel[0].ParkId, tempmodel[0].PlateNumber, tempmodel[0].CompanyId);
                string ispay = "false";
                string cardNo = "";
                string FactReceivable = "0";//实收金额
                string PayStatus = "0";
                if (rate.CalculateMoney == 0)
                {
                    ispay = "true";
                }
                else
                {
                    //判断车牌号是否存在账号余额
                    var otherAccount = _otherAccountAppService.QueryCard(tempmodel[0].PlateNumber, tempmodel[0].PlateNumber, tempmodel[0].PlateNumber);
                    if (!string.IsNullOrWhiteSpace(otherAccount))
                    {
                        var model = Deserialize<ReturnOtherAccountModel>(otherAccount);
                        if (model.AutoDeduction)//自动扣款，判断金额是否满足
                        {
                            //判断是否存在优惠券，确定优惠券金额
                            decimal tempmoney = rate.CalculateMoney;
                            var list = Abp.DataProcessHelper.GetEntityFromTable<Weixin.Coupon>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpCoupons where IsActive = 0 and money > 0 and EndTime > getdate() and tel = '" + model.TelePhone + "' order by Money desc").Tables[0]);
                            if (list.Count > 0)
                            {
                                if (list[0].Money <= rate.CalculateMoney)
                                {
                                    tempmoney = tempmoney - list[0].Money;
                                }
                                else
                                {
                                    tempmoney = 0;
                                }
                            }


                            if (model.Wallet >= tempmoney)
                            {
                                cardNo = model.CardNo;
                                FactReceivable = rate.CalculateMoney.ToString();
                                PayStatus = "4";
                                ispay = "true";
                            }
                        }
                    }
                }

                DiscountInsertCarOut(rate.CalculateMoney, FactReceivable, CarOutTime.ToString("yyyy-MM-dd HH:mm:sss"), CarOutTime.ToString("yyyy-MM-dd HH:mm:sss"), guid, null, null, null, PayStatus, ispay, "1", rate.ParkTime.ToString(), rate.CalculateMoney, cardNo, rate.CalculateMoney, 0, 10, tempmodel[0].BerthsecId, "0");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }


        /// <summary>
        /// 折扣率车辆出场
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
        /// <param name="CardNo"></param>
        /// <param name="BeforeDiscount"></param>
        /// <param name="DiscountMoney"></param>
        /// <param name="DiscountRate"></param>
        /// <param name="OutBatchNo"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public bool DiscountInsertCarOut(decimal Receivable, string FactReceive, string Arrearage, string CarOutTime, string CarPayTime, string OutOperaId, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money, string CardNo, decimal BeforeDiscount, decimal DiscountMoney, double DiscountRate, string OutBatchNo)
        {
            return DiscountInsertCarOut(Receivable, FactReceive, CarOutTime, CarPayTime, guid, SensorsOutCarTime, SensorsStopTime, SensorsReceivable, PayStatus, IsPay, FeeType, StopTime, Money, CardNo, BeforeDiscount, DiscountMoney, DiscountRate, AbpSession.BerthsecIds[0], OutBatchNo);
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="guid"></param>
       /// <param name="id"></param>
       /// <param name="Repayment"></param>
       /// <param name="CarRepaymentTime"></param>
       /// <param name="IsEscapePay"></param>
       /// <param name="EscapePayStatus"></param>
       /// <param name="PaymentType"></param>
       /// <param name="EscapeOperaId"></param>
       /// <param name="EscapeTenantId"></param>
       /// <param name="CardNo"></param>
       /// <param name="PayStatus"></param>
       /// <param name="PaymentBatchNo"></param>
       /// <returns></returns>
        [AbpAuthorize]
        [UnitOfWork(isTransactional: false)]
        public bool FeeBack(string guid, int id, decimal Repayment, DateTime CarRepaymentTime, bool IsEscapePay, short EscapePayStatus, short PaymentType, string EscapeOperaId, string EscapeTenantId, string CardNo, string PayStatus, string PaymentBatchNo)
        {
            //return FeeBack(id, Repayment, CarRepaymentTime, IsEscapePay, EscapePayStatus, PaymentType, CardNo);
            return DiscountFeeBack(id, Repayment, CarRepaymentTime, IsEscapePay, EscapePayStatus, PaymentType, CardNo, Repayment, 0, 10, PaymentBatchNo);
        }


        /// <summary>
        /// 折扣率补缴
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="id"></param>
        /// <param name="Repayment"></param>
        /// <param name="CarRepaymentTime"></param>
        /// <param name="IsEscapePay"></param>
        /// <param name="EscapePayStatus"></param>
        /// <param name="PaymentType"></param>
        /// <param name="EscapeOperaId"></param>
        /// <param name="EscapeTenantId"></param>
        /// <param name="CardNo"></param>
        /// <param name="PayStatus"></param>
        /// <param name="BeforeDiscount"></param>
        /// <param name="DiscountMoney"></param>
        /// <param name="DiscountRate"></param>
        /// <param name="PaymentBatchNo"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public bool DiscountFeeBack(string guid, int id, decimal Repayment, DateTime CarRepaymentTime, bool IsEscapePay, short EscapePayStatus, short PaymentType, string EscapeOperaId, string EscapeTenantId, string CardNo, string PayStatus, decimal BeforeDiscount, decimal DiscountMoney, double DiscountRate, string PaymentBatchNo)
        {
            return DiscountFeeBack(id, Repayment, CarRepaymentTime, IsEscapePay, EscapePayStatus, PaymentType, CardNo, BeforeDiscount, DiscountMoney, DiscountRate, PaymentBatchNo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="id"></param>
        /// <param name="Repayment"></param>
        /// <param name="CarRepaymentTime"></param>
        /// <param name="IsEscapePay"></param>
        /// <param name="EscapePayStatus"></param>
        /// <param name="PaymentType"></param>
        /// <param name="EscapeOperaId"></param>
        /// <param name="EscapeTenantId"></param>
        /// <param name="CardNo"></param>
        /// <param name="PayStatus"></param>
        /// <returns></returns>
        /// 
        //[AbpAuthorize]
        //public bool FeeBack(string guid, int id, decimal Repayment, DateTime CarRepaymentTime, bool IsEscapePay,
        //    short EscapePayStatus, short PaymentType, string EscapeOperaId, string EscapeTenantId, string PayStatus)
        //{
        //    return FeeBack(id, Repayment, CarRepaymentTime, IsEscapePay, EscapePayStatus, PaymentType, "0");
        //}
        /// <summary>
        /// 返回收费员锁定的记录
        /// </summary>
        /// <returns></returns>
        public List<BusinessDetail> GetAllLockBusiness()
        {
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany, AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark, AbpDataFilters.MustHaveBerthsec);
            var BusinessList = _businessDetailRepository.GetAll().Where(a => a.EmployeeId == AbpSession.UserId && a.IsLock == true).ToList();

            return BusinessList;
        }
        /// <summary>
        /// 取消追缴  对该辆车进行解锁  部分解锁
        /// </summary>
        [AbpAuthorize]
        public void CheckOutDeblock(string PlateNumber)
        {
            long? userid = AbpSession.UserId;
            _businessRepository.PlateNumberDebLock(userid, PlateNumber);
            //long? userid = AbpSession.UserId;
            //_businessRepository.CheckOutDeblock(userid);
        }
        /// <summary>
        /// 收费员签退   全部解锁
        /// </summary>
        [AbpAuthorize]
        public void CheckOutDeblockByEmployee()
        {
            long? userid = AbpSession.UserId;
            _businessRepository.CheckOutDeblock(userid);
        }
        /// <summary>
        /// 取消追缴  对该辆车进行解锁  部分解锁
        /// </summary>
        [AbpAuthorize]
        public void PlateNumberDebLock(string PlateNumber)
        {
            long? userid = AbpSession.UserId;
            _businessRepository.PlateNumberDebLock(userid, PlateNumber);
        }
        /// <summary>
        /// 修改明细 收费员解锁
        /// </summary>
        public void UpdateBusinessLock(BusinessDetail BD)
        {
            _businessDetailRepository.Update(BD);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<MonthBerthsUseDto> GetMonthBerthsUseList(GetBusinessDetailsInput input)
        {
            int? tenantID = AbpSession.TenantId;
            return _businessRepository.GetMonthBerthsUseList(input, tenantID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<MonthBerthsUseDto> GetMonthBerthsUseListOnlyMonth(GetBusinessDetailsInput input)
        {
            int? tenantID = AbpSession.TenantId;
            return _businessRepository.GetMonthBerthsUseListOnlyMonth(input, tenantID);
        }
        /// <summary>
        /// 进出场数据插入（不检测在线用户信息）
        /// </summary>
        /// <param name="exceutiontimeBegin"></param>
        /// <param name="exceutiontimeEnd"></param>
        public void CheckGuid(DateTime exceutiontimeBegin, DateTime exceutiontimeEnd)
        {
            _businessRepository.CheckGuid(exceutiontimeBegin, exceutiontimeEnd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceutiontimeBegin"></param>
        /// <param name="exceutiontimeEnd"></param>
        public void CheckGuidRepeat(DateTime exceutiontimeBegin, DateTime exceutiontimeEnd)
        {
            _businessRepository.CheckGuidRepeat(exceutiontimeBegin, exceutiontimeEnd);
        }
        /// <summary>
        /// 恢复4月12号补缴数据
        /// </summary>
        public void CheckFeeBack()
        {
            //_businessRepository.CheckFeeBack();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BusinessDetailId"></param>
        /// <returns></returns>
        public List<BusinessDetailPictureDto> GetPictureList(long BusinessDetailId)
        {
            return _businessDetailPictureRepository.GetAll().Where(entity => entity.BusinessDetailId == BusinessDetailId).ToList().MapTo<List<BusinessDetailPictureDto>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public List<BusinessDetailPictureDto> GetPictureList(Guid guid)
        {
            return _businessDetailPictureRepository.GetAll().Where(entity => entity.BusinessDetailGuid == guid).ToList().MapTo<List<BusinessDetailPictureDto>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<BusinessDetailPictureDto> GetSignoPictureList(List<SignoParkInformation> list)
        {
            List<BusinessDetailPictureDto> dataList = new List<BusinessDetailPictureDto>();

            foreach (var item in list)
            {
                var pictureDto = _businessDetailPictureRepository.GetAll().Where(entity => entity.BusinessDetailGuid == item.Guid).FirstOrDefault().MapTo<BusinessDetailPictureDto>();
                if(pictureDto != null)
                {
                    pictureDto.SignoDeviceNo = item.SignoDeviceNo;
                    pictureDto.ParkDoorStatus = item.ParkDoorName;
                    dataList.Add(pictureDto);
                }
            }
            return dataList;
        }






        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int CreatePicture(PicMongoDto entity)
        {
            Abp.Helper.MongoDbHelper.InsertOne<PicMongoDto>("PicMongoDto", entity);
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public PicMongoDto GetPicture(PicMongoDto entity)
        {
            return Abp.Helper.MongoDbHelper.GetOne<PicMongoDto>("PicMongoDto", new Document { { "BusinessDetailId", entity.BusinessDetailId } });
        }

        /// <summary>
        /// 获取大额订单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetBusinessDetailsOutput GetSubstantialOrderListSql(GetBusinessDetailsInput input)
        {
            var list = new List<int>
            {
                -1
            };
            input.CompanyIds = input.CompanyId.HasValue == false ? AbpSession.CompanyIds ?? list : new List<int> { input.CompanyId.Value };
            return _businessRepository.GetSubstantialOrderListSql(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public BusinessDetailUserData GetBusinessDetailById(Guid Id)
        {
            return _businessRepository.FirstOrDefault(entity => entity.guid == Id).MapTo<BusinessDetailUserData>();
        }

        /// <summary>
        /// 恢复欠费
        /// </summary>
        /// <param name="id"></param>
        /// <param name="money"></param>
        public void ResumeArrearage(Guid id, decimal money)
        {
            var model = _businessRepository.FirstOrDefault(entity => entity.guid == id);
            model.FactReceive = model.FactReceive - money;
            model.EscapePayStatus = 0;
            model.EscapeTime = model.CarOutTime;//出场时间
            model.PaymentType = 0;
            model.Arrearage = money + model.Arrearage;
            if (model.Arrearage > 0)
            {
                model.IsEscapePay = false;
                model.Status = 3;
            }
            else
            {
                model.IsEscapePay = true;
                model.Status = 2;
            }

            var operatorId = AbpSession.UserId.Value;
            var operatorName = _userRepository.Load(operatorId).Name;
            var datetime = DateTime.Now.ToString();
            string content = operatorName + "用户在" + datetime + "时间为" + model.PlateNumber + "恢复欠费" + money + "元；";
            if (string.IsNullOrEmpty(model.PaymentBatchNo))
            {
                model.PaymentBatchNo = "0";
            }
            else
            {
                var employeeCheckModel = _abpEmployeeCheckRepository.FirstOrDefault(entity => entity.BatchNo == model.PaymentBatchNo);
                if (employeeCheckModel != null)
                {
                    employeeCheckModel.IsNormal = false;
                    employeeCheckModel.IsRepeal = false;
                    employeeCheckModel.Remark = employeeCheckModel.Remark + content;
                    _abpEmployeeCheckRepository.Update(employeeCheckModel);
                }
            }

            if (string.IsNullOrEmpty(model.OutBatchNo))
            {
                model.OutBatchNo = "0";
            }
            else
            {
                var employeeCheckModel = _abpEmployeeCheckRepository.FirstOrDefault(entity => entity.BatchNo == model.OutBatchNo);
                if (employeeCheckModel != null)
                {
                    employeeCheckModel.IsNormal = false;
                    employeeCheckModel.IsRepeal = false;
                    employeeCheckModel.Remark = employeeCheckModel.Remark + content;
                    _abpEmployeeCheckRepository.Update(employeeCheckModel);
                }
            }

            _businessRepository.Update(model);
        }

        /// <summary>
        /// 判断图片是否已经上传
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="businessdetailId"></param>
        /// <returns>true:存在，false:不存在</returns>
        public bool GetBusinessDetailPicture(Guid guid, int businessdetailId)
        {
            if (_businessDetailPictureRepository.Count(entity => entity.BusinessDetailGuid == guid && entity.BusinessDetailId == businessdetailId) > 0)
                return true;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public BusinessDetailsDto GetOrderById(int Id)
        {
            return Abp.DataProcessHelper.GetEntityFromTable<BusinessDetailsDto>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpBusinessDetail with(nolock) where isdeleted = 0 and Id = " + Id).Tables[0])[0];
        }

        /// <summary>
        /// 撤销追缴
        /// </summary>
        /// <param name="escapeId"></param>
        /// <returns></returns>
        public int CancelEscapePay(List<string> escapeId)
        {
            var operatorId = AbpSession.UserId.Value;
            var operatorName = _userRepository.Load(operatorId).Name;
            var datetime = DateTime.Now.ToString();
            string content = operatorName + "用户在" + datetime + "时间撤销追缴；";
            if (escapeId.Count > 0)
            {
                foreach (var item in escapeId)
                {
                    var guids = item.MapTo<Guid>();
                    var model = _businessDetailRepository.FirstOrDefault(e => e.guid == guids && e.Status == 4);
                    if (!string.IsNullOrEmpty(model.PaymentBatchNo))
                    {
                        var employeeCheckModel = _abpEmployeeCheckRepository.FirstOrDefault(entity => entity.BatchNo == model.PaymentBatchNo);
                        if (employeeCheckModel != null)
                        {
                            employeeCheckModel.IsNormal = false;
                            employeeCheckModel.IsRepeal = false;
                            employeeCheckModel.Remark = employeeCheckModel.Remark + content;
                            _abpEmployeeCheckRepository.Update(employeeCheckModel);
                        }
                    }
                }


                //string sqlAbpEmployeeCheck = " update AbpEmployeeCheck set AbpEmployeeCheck.IsRepeal = case when AbpEmployeeCheck.CheckOutTime is null then 0 else 1 end, AbpEmployeeCheck.IsNormal = case when AbpEmployeeCheck.CheckOutTime is null then 0 else 1 end from AbpEmployeeCheck left join AbpBusinessDetail on AbpBusinessDetail.PaymentBatchNo = AbpEmployeeCheck.BatchNo where AbpEmployeeCheck.BatchNo in (select AbpBusinessDetail.PaymentBatchNo from AbpBusinessDetail where guid in('" + string.Join("', '", escapeId) + "') and Status = 4)";
                //SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, sqlAbpEmployeeCheck);

                string sqlAbpBusinessDetail = "update AbpBusinessDetail set Repayment = NULL, CarRepaymentTime = NULL, EscapePayStatus = 0, IsEscapePay = 0, EscapeOperaId = NULL, EscapeDeviceCode = NULL, PaymentType = 0, Status = 3, escapetenantid = NULL, PaymentBatchNo = NULL where guid in ('" + string.Join("','", escapeId) + "') and Status = 4";
                return SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, sqlAbpBusinessDetail);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取排名
        /// </summary>
        /// <returns></returns>
        public GetEscapeRankOutput GetEscapeRankList(GetEscapeRankInput input)
        {
            return _businessRepository.GetEscapeRankList(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operateDateBegin"></param>
        /// <param name="operateDateEnd"></param>
        /// <param name="PlateNumber"></param>
        /// <returns></returns>
        public List<EquipmentGps> GetPlatenumberLocusList(string operateDateBegin, string operateDateEnd, string PlateNumber)
        {
            string sql = "select top 30 CarInTime, BerthsecId, Lat as X, Lng as Y from AbpBusinessDetail with(Nolock) left join AbpBerthsecs on BerthsecId = AbpBerthsecs.Id where CarInTime > '" + operateDateBegin + "' and CarInTime < '" + DateTime.Parse(operateDateEnd).AddDays(1) + "' and platenumber = '" + PlateNumber + "' and Lat is not NULL order by CarInTime asc";
            return Abp.DataProcessHelper.GetEntityFromTable<EquipmentGps>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql).Tables[0]);
        }

        /// <summary>
        /// 删除业务记录
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public bool DeleteBusinessDetial(Guid guid)
        {
            var businessentry = _businessDetailRepository.FirstOrDefault(entity => entity.guid == guid);
            if (businessentry != default(BusinessDetail))
            {
                businessentry.IsDeleted = true;
                businessentry.Status = 2;
                _businessDetailRepository.Update(businessentry);
            }
            var entry = _appBerthRepository.FirstOrDefault(entity => entity.guid == guid);
            entry.BerthStatus = "2";
            _appBerthRepository.Update(entry);
            _remoteGuidRepository.Insert(new RemoteGuid() { BusinessDetailGuid = guid, BerthsecId = entry.BerthsecId });
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public BusinessDecisionDto GetBusinessDecisionDto(DateTime BeginTime, DateTime EndTime)
        {
            BusinessDecisionDto entity = new BusinessDecisionDto()
            {
                AllMoney = _businessDetailRepository.GetAllList(entry => entry.CarPayTime >= BeginTime && entry.CarPayTime <= EndTime).Sum(entry => entry.Money).ToString(),
                AllRepayment = _businessDetailRepository.GetAllList(entry => entry.CarPayTime >= BeginTime && entry.CarPayTime <= EndTime && entry.Status == 4).Sum(entry => entry.Repayment).ToString(),
                AllArrearage = _businessDetailRepository.GetAllList(entry => entry.CarPayTime >= BeginTime && entry.CarPayTime <= EndTime).Sum(entry => entry.Arrearage).ToString(),
                AllFactReceive = _businessDetailRepository.GetAllList(entry => entry.CreationTime >= BeginTime && entry.CreationTime <= EndTime).Sum(entry => entry.FactReceive).ToString(),
                AllStopTime = _businessDetailRepository.GetAllList(entry => entry.CarOutTime >= BeginTime && entry.CarOutTime <= EndTime).Sum(entry => entry.StopTime).ToString(),
                AllStopTimes = _businessDetailRepository.Count(entry => entry.CarOutTime >= BeginTime && entry.CarOutTime <= EndTime).ToString()
            };
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WhiteListsDto> GetWhiteListCache()
        {
            return _abpWhiteListRepository.GetAll().Where(entity => entity.IsActive == true).ToList().MapTo<List<WhiteListsDto>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MonthlyCarDto> GetMonthlyCarCache()
        {
            return _monthlyCarAppService.GetAll().ToList().MapTo<List<MonthlyCarDto>>();
        }

        /// <summary>
        /// 车辆入场接口SQL语句InsertCarInForSQL出场接口InsertCarOutForSQL
        /// </summary>
        /// <returns></returns>
        [UnitOfWork(isTransactional: false)]
        public bool InsertCarInForSQL(string BerthNumber, string PlateNumber, string CarType, string Prepaid, string CarInTime, string guid, string SensorsInCarTime, string StopType, string CardNo, int RegionId, int ParkId, int BerthsecId, string InBatchNo)
        {
            if (StopType == "F4")//白名单
            {
                StopType = "7";
            }

            Guid g = new Guid(guid);
            int count = 0;
            Berth berthmodel = null;
            List<Berth> list = Abp.DataProcessHelper.GetEntityFromTable<Berth>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpBerths where BerthsecId =  " + BerthsecId + " and BerthNumber = '" + BerthNumber + "'").Tables[0]);

            if (list != null && list.Count > 0)
            {
                berthmodel = list[0];
            }
            else
            {
                throw new AbpAuthorizationException("入场失败：泊位号不存在！", "23");
            }

            count = int.Parse(SqlHelper.ExecuteScalar(SqlHelper.connectionString, CommandType.Text, "select count(1) from AbpBusinessDetail with(nolock) where guid = '" + g + "'").ToString());
            if (count > 0)
            {
                throw new AbpAuthorizationException("入场失败：guid已经存在！", "20");
            }

            short PrepaidPayStatus;
            if (CardNo != "0")
            {
                PrepaidPayStatus = 4;// 卡号不等于0  支付类型为4属账号支付
            }
            else
            {
                PrepaidPayStatus = 1;//卡号等于0  支付类型为1属现金支付
            }

            DateTime? SensorsInCarTime1 = null;

            if (berthmodel == default(Berth))
            {
                SensorsInCarTime1 = null;
            }
            else
            {
                if (berthmodel.ParkStatus == 1 && !string.IsNullOrWhiteSpace(berthmodel.SensorNumber) && berthmodel.SensorGuid == g)
                {
                    SensorsInCarTime1 = berthmodel.SensorsInCarTime;
                }
            }
         
            _appBerthRepository.CarInUpdateBerhs(BerthNumber, BerthsecId, PlateNumber, Convert.ToDateTime(CarInTime), g, Convert.ToInt16(CarType), CardNo, Convert.ToDecimal(Prepaid));

            OtherAccount otherAccount = new OtherAccount();
            List<OtherAccount> otherAccountList = Abp.DataProcessHelper.GetEntityFromTable<OtherAccount>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from ExtOtherAccount where CardNo =  " + CardNo + " and IsActive = 1").Tables[0]);

            if (otherAccountList.Count > 0)
            {
                otherAccount = otherAccountList[0];
            }

            var result = SqlHelper.ExecuteReader(SqlHelper.connectionString, CommandType.Text, "select CheckInEmployeeId,CheckInDeviceCode,TenantId,CompanyId from AbpBerthsecs where Id = '" + BerthsecId);
            var EmployeeId = long.Parse(result["CheckInEmployeeId"].ToString());
            var CheckInDeviceCode = result["CheckInDeviceCode"].ToString();
            var TenantId = int.Parse(result["TenantId"].ToString());
            var CompanyId = int.Parse(result["CompanyId"].ToString());
            BusinessDetail entity = new BusinessDetail()
            {
                CompanyId = CompanyId,
                TenantId = TenantId,
                InBatchNo = InBatchNo,
                BerthNumber = BerthNumber,//泊位号 
                PlateNumber = PlateNumber,//车牌号
                CarType = Convert.ToInt16(CarType),//车辆类型
                Prepaid = Convert.ToDecimal(Prepaid),//预付费
                CarInTime = Convert.ToDateTime(CarInTime),//车辆入场时间
                InOperaId = EmployeeId,//入场收费员ID
                InDeviceCode = CheckInDeviceCode,//入场设备
                guid = g,//
                SensorsInCarTime = SensorsInCarTime1,//车检器入场时间
                StopType = Convert.ToInt16(StopType),//停车类型（是否为包月车）
                RegionId = RegionId,//区域ID
                ParkId = ParkId,//停车场ID
                BerthsecId = BerthsecId,//泊位段ID
                Status = 1,
                PrepaidCarNo = CardNo,  //进场卡号
                PrepaidPayStatus = PrepaidPayStatus   //进场支付类型
            };

            if (otherAccount != null && Convert.ToDecimal(Prepaid) > 0)
            {
                var OtherAccountId = otherAccount.Id;
                var Money = Convert.ToDecimal(Prepaid);
                var InTime = Convert.ToDateTime(CarInTime);
                var BeginMoney = otherAccount.Wallet;
                var EndMoney = otherAccount.Wallet - Convert.ToDecimal(Prepaid);
                SqlParameter[] sqlParameters = new SqlParameter[] {
                new SqlParameter("@OtherAccountId", OtherAccountId),
                new SqlParameter("@Money", Money),
                new SqlParameter("@CardNo", CardNo),
                new SqlParameter("@EmployeeId", EmployeeId),
                new SqlParameter("@PlateNumber", PlateNumber),
                new SqlParameter("@InTime", InTime),
                new SqlParameter("@BeginMoney", BeginMoney),
                new SqlParameter("@EndMoney", EndMoney)
            };
                SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, "INSERT INTO AbpDeductionRecords(OtherAccountId,OperType,Money,PayStatus,CardNo,EmployeeId,PlateNumber,Remark,InTime,BeginMoney,EndMoney) VALUES (@OtherAccountId,3,@Money,1,@CardNo,@EmployeeId,@PlateNumber,'预付',@InTime,@BeginMoney,@EndMoney)", sqlParameters);
                //预付  短信发送
                _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto() { Destnumbers = otherAccount.TelePhone, Msg = string.Format(_smsModelAppService.GetSmsModelByType("PaymentModel").SmsContext, Convert.ToDateTime(CarInTime).ToString("yyyy年MM月dd日hh时mm分"), BerthNumber, Prepaid) });
                otherAccount.Wallet = otherAccount.Wallet - Convert.ToDecimal(Prepaid);//车辆入场 账户余额扣除预付
                SqlParameter[] sqls = new SqlParameter[] {
                new SqlParameter("@Wallet", otherAccount.Wallet),
                new SqlParameter("@CardNo", CardNo)
            };
                SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, "update ExtOtherAccount set Wallet =@Wallet where CardNo =@CardNo and IsActive = 1", sqls);
            }

            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, " update AbpSensors set RelateNumber = '" + PlateNumber + "' where BerthsecId = " + BerthsecId + " and BerthNumber = '" + BerthNumber + "' " +
                "	update AbpSensorBusinessDetail set PlateNumber = '" + PlateNumber + "' where [guid] = '" + g + "'");
            #region 待观查
            //SqlParameter[] parameters = new SqlParameter[] {
            //    new SqlParameter("@InBatchNo", entity.InBatchNo),
            //    new SqlParameter("@OutBatchNo", entity.OutBatchNo),
            //    new SqlParameter("@PaymentBatchNo", entity.PaymentBatchNo),
            //    new SqlParameter("@BerthNumber", entity.BerthNumber),
            //    new SqlParameter("@PlateNumber", entity.PlateNumber),
            //    new SqlParameter("@CarType", entity.CarType),
            //    new SqlParameter("@Prepaid", entity.Prepaid),
            //    new SqlParameter("@PrepaidPayStatus", entity.PrepaidPayStatus),
            //    new SqlParameter("@PrepaidCarNo", entity.PrepaidCarNo),
            //    new SqlParameter("@Receivable", entity.Receivable),
            //    new SqlParameter("@Money", entity.Money),
            //    new SqlParameter("@FactReceive", entity.FactReceive),
            //    new SqlParameter("@Arrearage", entity.Arrearage),
            //    new SqlParameter("@BeforeDiscount", entity.BeforeDiscount),
            //    new SqlParameter("@DiscountMoney", entity.DiscountMoney),
            //    new SqlParameter("@DiscountRate", entity.DiscountRate),
            //    new SqlParameter("@CarInTime", entity.CarInTime),
            //    new SqlParameter("@CarOutTime", entity.CarOutTime),
            //    new SqlParameter("@CarPayTime", entity.CarPayTime),
            //    new SqlParameter("@InOperaId", entity.InOperaId),
            //    new SqlParameter("@InDeviceCode", entity.InDeviceCode),
            //    new SqlParameter("@OutOperaId", entity.OutOperaId),
            //    new SqlParameter("@OutDeviceCode", entity.OutDeviceCode),
            //    new SqlParameter("@ChargeOperaId", entity.ChargeOperaId),
            //    new SqlParameter("@ChargeDeviceCode", entity.ChargeDeviceCode),
            //    new SqlParameter("@guid", entity.guid),
            //    new SqlParameter("@SensorsInCarTime", entity.SensorsInCarTime),
            //    new SqlParameter("@SensorsOutCarTime", entity.SensorsOutCarTime),
            //    new SqlParameter("@SensorsStopTime", entity.SensorsStopTime),
            //    new SqlParameter("@SensorsReceivable", entity.SensorsReceivable),
            //    new SqlParameter("@Repayment", entity.Repayment),
            //    new SqlParameter("@CarRepaymentTime", entity.CarRepaymentTime),
            //    new SqlParameter("@PaymentType", entity.PaymentType),
            //    new SqlParameter("@EscapeTime", entity.EscapeTime),
            //    new SqlParameter("@EscapePayStatus", entity.EscapePayStatus),
            //    new SqlParameter("@IsEscapePay", entity.IsEscapePay),
            //    new SqlParameter("@EscapeOperaId", entity.EscapeOperaId),
            //    new SqlParameter("@EscapeUserId", entity.EscapeUserId),
            //    new SqlParameter("@EscapeDeviceCode", entity.EscapeDeviceCode),
            //    new SqlParameter("@EscapeTenantId", entity.EscapeTenantId),
            //    new SqlParameter("@EscapeCompanyId", entity.EscapeCompanyId),
            //    new SqlParameter("@PayStatus", entity.PayStatus),
            //    new SqlParameter("@OtherAccountId", entity.OtherAccountId),
            //    new SqlParameter("@IsPay", entity.IsPay),
            //    new SqlParameter("@StopType", entity.StopType),
            //    new SqlParameter("@FeeType", entity.FeeType),
            //    new SqlParameter("@StopTime", entity.StopTime),
            //    new SqlParameter("@TenantId", entity.TenantId),
            //    new SqlParameter("@CompanyId", entity.CompanyId),
            //    new SqlParameter("@RegionId", entity.RegionId),
            //    new SqlParameter("@ParkId", entity.ParkId),
            //    new SqlParameter("@BerthsecId", entity.BerthsecId),
            //    new SqlParameter("@Status", entity.Status),
            //    new SqlParameter("@AppAccountId", entity.AppAccountId),
            //    new SqlParameter("@IsLock", entity.IsLock),
            //    new SqlParameter("@EmployeeId", EmployeeId),
            //    new SqlParameter("@ReceivableOtherAccountId", entity.ReceivableOtherAccountId),
            //    new SqlParameter("@EscapeCardNo", entity.EscapeCardNo),
            //    new SqlParameter("@EscapeOtherAccountId", entity.EscapeOtherAccountId),
            //    new SqlParameter("@ElectronicOrderid", entity.ElectronicOrderid)
            //};

            //_businessDetailRepository.Insert(entity);

            #endregion

            #region 增加入场记录
            string sql = "insert into AbpBusinessDetail(BerthNumber, PlateNumber, InDeviceCode, CarInTime, InOperaId, CarType, Prepaid, PrepaidPayStatus, Receivable, FactReceive, Arrearage, PaymentType, EscapePayStatus, IsEscapePay, PayStatus, IsPay, StopType, FeeType, IsLock, guid, TenantId, CompanyId, RegionId, ParkId, BerthsecId, Status, IsDeleted, CreationTime, CreatorUserId) " +
             " values(@BerthNumber, @PlateNumber, @InDeviceCode, @CarInTime, @InOperaId, @CarType, @Prepaid, @PrepaidPayStatus, @Receivable, @FactReceive, @Arrearage, @PaymentType, @EscapePayStatus, @IsEscapePay, @PayStatus, @IsPay, @StopType, @FeeType, @IsLock, @guid, @TenantId, @CompanyId, @RegionId, @ParkId, @BerthsecId, @Status, @IsDeleted, @CreationTime, @CreatorUserId)";
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@BerthNumber", entity.BerthNumber),
                new SqlParameter("@PlateNumber", entity.PlateNumber),
                new SqlParameter("@InDeviceCode", entity.InDeviceCode),
                new SqlParameter("@CarInTime", entity.CarInTime),
                new SqlParameter("@InOperaId", entity.InOperaId),
                new SqlParameter("@CarType", entity.CarType),
                new SqlParameter("@Prepaid", entity.Prepaid),
                new SqlParameter("@PrepaidPayStatus", 1),
                new SqlParameter("@Receivable", "0"),
                new SqlParameter("@FactReceive", "0"),
                new SqlParameter("@Arrearage", "0"),
                new SqlParameter("@PaymentType", "0"),
                new SqlParameter("@EscapePayStatus", "0"),
                new SqlParameter("@IsEscapePay", false),
                new SqlParameter("@PayStatus", "0"),
                new SqlParameter("@IsPay", false),
                new SqlParameter("@StopType", 1),
                new SqlParameter("@FeeType", "0"),
                new SqlParameter("@IsLock", false),
                new SqlParameter("@guid", entity.guid),
                new SqlParameter("@TenantId", entity.TenantId),
                new SqlParameter("@CompanyId", entity.CompanyId),
                new SqlParameter("@RegionId", entity.RegionId),
                new SqlParameter("@ParkId", entity.ParkId),
                new SqlParameter("@BerthsecId", entity.BerthsecId),
                new SqlParameter("@Status", 1),
                new SqlParameter("@IsDeleted", false),
                new SqlParameter("@CreationTime", DateTime.Now),
                new SqlParameter("@CreatorUserId", "")
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, sql, param);
            #endregion

            List<Employee> employeeList = Abp.DataProcessHelper.GetEntityFromTable<Employee>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpEmployees where Id =  " + EmployeeId).Tables[0]);
            Employee employee = new Employee();
            if (employeeList.Count > 0)
            {
                employee = employeeList[0];
            }

            List<Berthsecs.Berthsec> BerthsecList = Abp.DataProcessHelper.GetEntityFromTable<Berthsecs.Berthsec>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpBerthsecs where Id =  " + entity.BerthsecId).Tables[0]);
            Berthsecs.Berthsec Berthsec = new Berthsecs.Berthsec();
            if (BerthsecList.Count > 0)
            {
                Berthsec = BerthsecList[0];
            }
            string berthsecName = "";
            if (Berthsec != default(Berthsecs.Berthsec))
            {
                berthsecName = Berthsec.BerthsecName;
            }

            string message = "进场车辆" + entity.PlateNumber + "在" + entity.CarInTime + "驶入" + berthsecName + entity.BerthNumber + "泊位号, 支付类型:" + ChangePayStatusName(entity.PrepaidPayStatus) + ",预缴金额:" + entity.Prepaid + "元,操作收费员:" + employee.TrueName + "(" + employee.UserName + ")";
            _p4ChatService.CreateChatService().Clients.Group(P4Consts.EmployeeGroup + entity.TenantId).EmployeeMessage(employee.TrueName + "(" + employee.UserName + ")", message, 2);
            _subscribeAppService.SendMessage("CarInOutManagement", message, "");

            //黑名单检测，黑名单检测短信报警
            List<BlackList> blackList = Abp.DataProcessHelper.GetEntityFromTable<BlackList>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpBlackList where IsActive =1 and RelateNumber =  " + PlateNumber).Tables[0]);
            BlackList blackModel = new BlackList();
            if (blackList.Count > 0)
            {
                blackModel = blackList[0];
            }
            if (blackModel != default(BlackList))
            {
                //黑名单{0},车主姓名{1}，在{2}驶入{3}{4}泊位号，请悉知！
                var model = new SmsAccountDto() { Msg = string.Format(_smsModelAppService.GetSmsModelByType("BlackCarInModel").SmsContext, blackModel.RelateNumber, blackModel.CarOwner, Convert.ToDateTime(CarInTime).ToString("yyyy年MM月dd日hh时mm分"), berthsecName, BerthNumber), Destnumbers = blackModel.IdNumber };
                _smsManagementAppService.SendSmsNoTenant(model);
                _subscribeAppService.SendMessage("BlackManagement", model.MsgValue, "");
            }
            return true;
        }
        /// <summary>
        /// 出场接口
        /// </summary>
        /// <returns></returns>
        [UnitOfWork(isTransactional: false)]
        public bool InsertCarOutForSQL(decimal Receivable, string FactReceive, string CarOutTime, string CarPayTime, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money, string CardNo, decimal BeforeDiscount, decimal DiscountMoney, double DiscountRate, int BerthsecId, string OutBatchNo)
        {
            if (PayStatus != "4")
                CardNo = "0";
            Guid g = new Guid(guid);
            var result = SqlHelper.ExecuteReader(SqlHelper.connectionString, CommandType.Text, "select CheckInEmployeeId,CheckInDeviceCode,TenantId,CompanyId from AbpBerthsecs where Id = '" + BerthsecId);
            var EmployeeId = long.Parse(result["CheckInEmployeeId"].ToString());
            var DeviceCode = result["CheckInDeviceCode"].ToString();
            var TenantId = int.Parse(result["TenantId"].ToString());
            var CompanyId = int.Parse(result["CompanyId"].ToString());

            List<BusinessDetail> entitys = new List<BusinessDetail>();
            entitys = Abp.DataProcessHelper.GetEntityFromTable<BusinessDetail>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpBusinessDetail with(nolock) where  guid = '" + g + "'").Tables[0]);

            if (entitys.Count == 0)
            {
                throw new AbpAuthorizationException("出场失败：guid不存在！", "22");
            }

            BusinessDetail entity = new BusinessDetail();
            entity = entitys[0];
            if ((entity.Status != 1 && entity.Status != 3) || entity.LastModifierUserId.HasValue)//已经出场处理
                throw new AbpAuthorizationException("出场失败：该数据已出场！", "201");

            Berth berthmodel = Abp.DataProcessHelper.GetEntityFromTable<Berth>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpBerths with(nolock) where BerthsecId = " + BerthsecId + " and BerthNumber = '" + entity.BerthNumber + "'").Tables[0])[0];      
            DateTime? SensorsOutCarTime1 = null;

            //处理白名单         
            var whiteList = _abpCacheManager.GetCache(string.Format(Abp.Configuration.SettingManager.CacheWhiteCarValue, CompanyId)).Get(string.Format(Abp.Configuration.SettingManager.CacheWhiteCarValue, CompanyId), () => GetWhiteListCache()) as List<WhiteListsDto>;
            var whitemodel = whiteList.FirstOrDefault(e => e.RelateNumber == berthmodel.RelateNumber && e.CompanyId == CompanyId);

            if (whitemodel != default(WhiteListsDto))
            {
                Receivable = 0;
                FactReceive = "0";
                Money = 0;
            }
            else//包月车辆数据
            {
                List<MonthlyCarDto> MonthlyCarList = new List<MonthlyCarDto>();
                var monthlyCarList = _abpCacheManager.GetCache(string.Format(Abp.Configuration.SettingManager.CacheMonthyValue, CompanyId))
                    .Get(string.Format(Abp.Configuration.SettingManager.CacheMonthyValue, CompanyId), () => GetMonthlyCarCache()) as List<MonthlyCarDto>;
                var list = monthlyCarList.Where(e => e.PlateNumber == berthmodel.RelateNumber && e.CompanyId == CompanyId && e.BeginTime <= DateTime.Now && e.EndTime >= DateTime.Now).ToList();
                var parkId = "," + berthmodel.ParkId + ",";
                for (int i = 0; i < list.Count; i++)
                {
                    if (("," + list[i].ParkIds + ",").Contains(parkId) || list[i].ParkIds == "0")
                    {
                        MonthlyCarList.Add(list[i]);
                        break;
                    }
                }
                if (MonthlyCarList.Count > 0 && MonthlyCarList[0].MonthyType == "MonthyAll")
                {
                    Receivable = 0;
                    FactReceive = "0";
                    Money = 0;
                }
            }

            if (berthmodel == default(Berth))
            {
                SensorsOutCarTime1 = null;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(berthmodel.SensorNumber))
                {
                    SensorsOutCarTime1 = berthmodel.SensorsOutCarTime;
                }
            }
      
            if (entity != null)
            {
                if (bool.Parse(_settingStore.GetSettingOrNull(TenantId, null, "EscapeLock").Value))
                    PlateNumberDebLock(entity.PlateNumber);//车辆出场的时候 对车辆进行解锁
                entity.OutDeviceCode = DeviceCode;               //出场设备
                if (AbpSession.UserSource != 1)
                {
                    entity.ChargeOperaId = EmployeeId;                   //收费操作员Id
                    entity.ChargeDeviceCode = DeviceCode;            //收费员设备
                    entity.OutOperaId = Convert.ToInt16(EmployeeId);     //出场收费员ID
                }

                entity.CarOutTime = Convert.ToDateTime(CarOutTime);         //出场时间
                entity.CarPayTime = Convert.ToDateTime(CarPayTime);         //支付时间
                entity.OutBatchNo = OutBatchNo;       //出厂批次号
                entity.guid = g;                               //唯一guid

                if (entity.guid == berthmodel.SensorGuid)   //如果车检器与收费记录中guid相等，关联车检器入场出场信息
                {
                    entity.SensorsOutCarTime = SensorsOutCarTime1;//车检器出场时间

                    if (entity.SensorsInCarTime.HasValue && entity.SensorsOutCarTime.HasValue)
                    {
                        entity.SensorsStopTime = Convert.ToInt32(entity.SensorsOutCarTime.Value.Subtract(entity.SensorsInCarTime.Value).TotalMinutes);
                    }
                    entity.SensorsReceivable = Convert.ToDecimal(SensorsReceivable);//车检收器应
                }
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
                    entity.PayStatus = Convert.ToInt16(PayStatus);  // 出场支付类型（出场应收） 1.现金 2.刷卡 3.在线支付 4.MOne卡
                }
                if (PayStatus == "1")                                           //现金
                {
                    entity.FactReceive = Convert.ToDecimal(FactReceive);        //实收
                    entity.DiscountRate = DiscountRate;//折扣率
                    entity.DiscountMoney = DiscountMoney;//折扣金额
                    entity.BeforeDiscount = BeforeDiscount;//打折前应收
                    entity.Arrearage = 0;
                }
                else if (PayStatus == "3")                                      //微信支付
                {
                    entity.FactReceive = Convert.ToDecimal(FactReceive);        //实收
                    entity.DiscountRate = DiscountRate;//折扣率
                    entity.DiscountMoney = DiscountMoney;//折扣金额
                    entity.BeforeDiscount = BeforeDiscount;//打折前应收
                    entity.Arrearage = 0;
                }
                else if (PayStatus == "4")                                      //账号支付
                {
                    entity.FactReceive = Convert.ToDecimal(FactReceive);        //实收
                    entity.DiscountRate = DiscountRate;//折扣率
                    entity.DiscountMoney = DiscountMoney;//折扣金额
                    entity.BeforeDiscount = BeforeDiscount;//打折前应收
                    entity.Arrearage = 0;
                }
                entity.FeeType = Convert.ToInt16(FeeType);//费用类型（1.正常收费，2.追缴收费）
                entity.StopTime = Convert.ToInt32(StopTime.Split('.')[0]);//停车时长
                entity.ReceivableCarNo = CardNo;   //出场卡号   如果为0的话，出场支付类型是现金

                var count = SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, "update abpberths set BerthStatus = '2', RelateNumber = '" +
                    entity.PlateNumber + "', OutCarTime = '" + CarOutTime + "', SensorGuid = '00000000-0000-0000-0000-000000000000' where BerthNumber = '" + entity.BerthNumber + "' and IsActive = 1 and BerthsecId = " + BerthsecId);

                if (count == 0)
                    throw new AbpAuthorizationException("出场失败：不存在该泊位编号！", "23");

                //钱包扣费记录
                List<OtherAccount> otherAccountList = Abp.DataProcessHelper.GetEntityFromTable<OtherAccount>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from ExtOtherAccount where IsActive = 1 and CardNo =  " + CardNo).Tables[0]);
                OtherAccount otherAccount = new OtherAccount();
                if (otherAccountList.Count > 0)
                {
                    otherAccount = otherAccountList[0];
                }
                if (otherAccount != null)
                {
                    if (Money == entity.Prepaid) //消费金额等于预缴金额  添加消费记录
                    {
                        //添加消费记录
                        var BeginMoney = otherAccount.Wallet;
                        var InTime = Convert.ToDateTime(CarOutTime);
                        var EndMoney = otherAccount.Wallet - (Money - entity.Prepaid);
                        var Moneys = Money - entity.Prepaid;
                        SqlParameter[] sqlParameters = new SqlParameter[] {
                              new SqlParameter("@OtherAccountId", otherAccount.Id),
                              new SqlParameter("@Money", Moneys),
                              new SqlParameter("@CardNo", CardNo),
                              new SqlParameter("@EmployeeId", EmployeeId),
                              new SqlParameter("@PlateNumber", entity.PlateNumber),
                              new SqlParameter("@InTime", InTime),
                              new SqlParameter("@BeginMoney", BeginMoney),
                              new SqlParameter("@EndMoney", EndMoney)
                        };
                        SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, "INSERT INTO AbpDeductionRecords(OtherAccountId,OperType,Money,PayStatus,CardNo,EmployeeId,PlateNumber,Remark,InTime,BeginMoney,EndMoney) VALUES (@OtherAccountId,2,@Money,1,@CardNo,@EmployeeId,@PlateNumber,'消费',@InTime,@BeginMoney,@EndMoney)", sqlParameters);

                        if (otherAccount.TelePhone != null && entity.Money > 0)
                        {
                            //消费金额 以及账户余额（短信发送）
                            _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto() { Destnumbers = otherAccount.TelePhone, Msg = string.Format(_smsModelAppService.GetSmsModelByType("ConsumptionEqualModel").SmsContext, Convert.ToDateTime(CarOutTime).ToString("yyyy年MM月dd日HH时mm分"), entity.BerthNumber, entity.Prepaid, entity.PlateNumber, otherAccount.Wallet, entity.StopTime, entity.Money) });
                        }

                        otherAccount.Wallet = otherAccount.Wallet - Moneys;  //账户余额减掉消费金额
                        SqlParameter[] sqls = new SqlParameter[] {
                                new SqlParameter("@Wallet", otherAccount.Wallet),
                                new SqlParameter("@CardNo", CardNo)
                        };
                        SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, "update ExtOtherAccount set Wallet =@Wallet where CardNo =@CardNo and IsActive = 1", sqls);
                    }
                    else if (Money < entity.Prepaid)  //消费金额小于预付  （返还）
                    {
                        //添加返还消费记录
                        var payMoney = entity.Prepaid - Money;  //消费金额
                        var EndMoney = otherAccount.Wallet + entity.Prepaid - Money;
                        var PlateNumber = entity.PlateNumber;
                        var InTime = Convert.ToDateTime(CarOutTime);
                        SqlParameter[] sqlParameters = new SqlParameter[] {
                              new SqlParameter("@OtherAccountId", otherAccount.Id),
                              new SqlParameter("@Money", payMoney),
                              new SqlParameter("@CardNo", CardNo),
                              new SqlParameter("@EmployeeId", EmployeeId),
                              new SqlParameter("@PlateNumber", entity.PlateNumber),
                              new SqlParameter("@InTime", InTime),
                              new SqlParameter("@BeginMoney", otherAccount.Wallet),
                              new SqlParameter("@EndMoney", EndMoney)
                        };
                        SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, "INSERT INTO AbpDeductionRecords(OtherAccountId,OperType,Money,PayStatus,CardNo,EmployeeId,PlateNumber,Remark,InTime,BeginMoney,EndMoney) VALUES (@OtherAccountId,4,@Money,1,@CardNo,@EmployeeId,@PlateNumber,'返还',@InTime,@BeginMoney,@EndMoney)", sqlParameters);
                        if (otherAccount.TelePhone != null)
                        {
                            //返回金额 以及账户余额（短信发送）
                            _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto() { Destnumbers = otherAccount.TelePhone, Msg = string.Format(_smsModelAppService.GetSmsModelByType("ConsumptionGreaterModel").SmsContext, Convert.ToDateTime(CarOutTime).ToString("yyyy年MM月dd日HH时mm分"), entity.BerthNumber, entity.Prepaid, payMoney, entity.PlateNumber, otherAccount.Wallet + payMoney) });
                        }
                        otherAccount.Wallet = otherAccount.Wallet + payMoney;  //账户余额加上返还金额
                        SqlParameter[] sqls = new SqlParameter[] {
                                new SqlParameter("@Wallet", otherAccount.Wallet),
                                new SqlParameter("@CardNo", CardNo)
                        };
                        SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, "update ExtOtherAccount set Wallet =@Wallet where CardNo =@CardNo and IsActive = 1", sqls);
                    }
                    else if (Money > entity.Prepaid && entity.Status != 5 && entity.Status != 3)  //消费金额大于预付（消费）
                    {
                        //判断是否存在优惠券，确定优惠券金额
                        decimal tempmoney = Money;
                        var list = Abp.DataProcessHelper.GetEntityFromTable<Weixin.Coupon>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpCoupons where IsActive = 0 and money > 0 and EndTime > getdate() and tel = '" + otherAccount.TelePhone + "' order by Money desc").Tables[0]);
                        if (list.Count > 0)
                        {
                            Coupon model = new Coupon();
                            model = list[0];
                            if (list[0].Money <= Money)//优惠券金额小于等于消费金额
                            {
                                CouponDetailDto dto = new CouponDetailDto();
                                dto.tel = list[0].tel;
                                dto.NewMoney = 0;
                                dto.CouponNo = list[0].CouponNo;
                                dto.OldMoney = list[0].Money;
                                dto.ConsumptionMoney = list[0].Money;
                                dto.Platenumber = entity.PlateNumber;
                                dto.Source = "平台消费";
                                dto.CouponType = 2;
                                _couponDetailsAppService.AddCouponDetail(dto);

                                tempmoney = tempmoney - list[0].Money;
                                model.Money = 0;
                                model.IsActive = true;
                                _couponRepository.Update(model);

                            }
                            else//如果优惠券金额大于消费金额
                            {
                                CouponDetailDto dto = new CouponDetailDto();
                                dto.tel = list[0].tel;
                                dto.NewMoney = model.Money - tempmoney;
                                dto.CouponNo = list[0].CouponNo;
                                dto.OldMoney = list[0].Money;
                                dto.ConsumptionMoney = tempmoney;
                                dto.Platenumber = entity.PlateNumber;
                                dto.Source = "平台消费";
                                dto.CouponType = 2;
                                _couponDetailsAppService.AddCouponDetail(dto);

                                model.Money = model.Money - tempmoney;
                                _couponRepository.Update(model);
                                tempmoney = 0;
                            }
                        }
                        if (tempmoney > 0)
                        {
                            //添加消费记录
                            var payMoney = tempmoney - entity.Prepaid;  //消费金额
                            var EndMoney = otherAccount.Wallet - (tempmoney - entity.Prepaid);
                            var PlateNumber = entity.PlateNumber;
                            var InTime = Convert.ToDateTime(CarOutTime);
                            SqlParameter[] sqlParameters = new SqlParameter[] {
                              new SqlParameter("@OtherAccountId", otherAccount.Id),
                              new SqlParameter("@Money", payMoney),
                              new SqlParameter("@CardNo", CardNo),
                              new SqlParameter("@EmployeeId", EmployeeId),
                              new SqlParameter("@PlateNumber", entity.PlateNumber),
                              new SqlParameter("@InTime", InTime),
                              new SqlParameter("@BeginMoney", otherAccount.Wallet),
                              new SqlParameter("@EndMoney", EndMoney)
                        };
                            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, "INSERT INTO AbpDeductionRecords(OtherAccountId,OperType,Money,PayStatus,CardNo,EmployeeId,PlateNumber,Remark,InTime,BeginMoney,EndMoney) VALUES (@OtherAccountId,2,@Money,1,@CardNo,@EmployeeId,@PlateNumber,'消费',@InTime,@BeginMoney,@EndMoney)", sqlParameters);

                            if (!string.IsNullOrEmpty(otherAccount.TelePhone) && payMoney > 0)
                            {
                                //消费金额 以及账户余额（短信发送）
                                _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto() { Destnumbers = otherAccount.TelePhone, Msg = string.Format(_smsModelAppService.GetSmsModelByType("ConsumptionLessModel").SmsContext, Convert.ToDateTime(CarOutTime).ToString("yyyy年MM月dd日HH时mm分"), entity.BerthNumber, entity.Prepaid, payMoney, entity.PlateNumber, otherAccount.Wallet - payMoney) });
                            }
                            otherAccount.Wallet = otherAccount.Wallet - payMoney;  //账户余额减掉消费金额
                            SqlParameter[] sqls = new SqlParameter[] {
                                new SqlParameter("@Wallet", otherAccount.Wallet),
                                new SqlParameter("@CardNo", CardNo)
                        };
                            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, "update ExtOtherAccount set Wallet =@Wallet where CardNo =@CardNo and IsActive = 1", sqls);
                        }
                    }
                    //出场应收 
                    entity.FactReceive = Convert.ToDecimal(FactReceive);        //实收
                    entity.BeforeDiscount = BeforeDiscount;//折前应收
                    entity.DiscountMoney = DiscountMoney;//折扣金额    
                    entity.DiscountRate = DiscountRate;//折扣
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
                entity.Receivable = Receivable;
                SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@Status", entity.Status),
                new SqlParameter("@Money", entity.Money),
                new SqlParameter("@FactReceive", entity.FactReceive),
                new SqlParameter("@CarOutTime", entity.CarOutTime),
                new SqlParameter("@CarPayTime", entity.CarPayTime),
                new SqlParameter("@OutOperaId", entity.OutOperaId),
                new SqlParameter("@OutDeviceCode", entity.OutDeviceCode),
                new SqlParameter("@PayStatus", entity.PayStatus),
                new SqlParameter("@IsPay", entity.IsPay),
                new SqlParameter("@FeeType", "1"),
                new SqlParameter("@StopTime", entity.StopTime),
                new SqlParameter("@OutBatchNo", entity.OutBatchNo),
                new SqlParameter("@guid", entity.guid)
            };
                SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, "update AbpBusinessDetail set Status = @Status, Money = @Money, FactReceive = @FactReceive, CarOutTime = @CarOutTime, CarPayTime = @CarPayTime, OutOperaId = @OutOperaId, OutDeviceCode = @OutDeviceCode, PayStatus = @PayStatus, IsPay = @IsPay, FeeType = @FeeType, StopTime = @StopTime, OutBatchNo = @OutBatchNo where guid = @guid", param);
                if (AbpSession.UserSource == 1)
                {
                    return true;
                }

                List<Employee> employeeList = Abp.DataProcessHelper.GetEntityFromTable<Employee>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpEmployees where Id =  " + EmployeeId).Tables[0]);
                Employee employee = new Employee();
                if (employeeList.Count > 0)
                {
                    employee = employeeList[0];
                }

                List<Berthsecs.Berthsec> BerthsecList = Abp.DataProcessHelper.GetEntityFromTable<Berthsecs.Berthsec>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpBerthsecs where Id =  " + entity.BerthsecId).Tables[0]);
                Berthsecs.Berthsec Berthsec = new Berthsecs.Berthsec();
                if (BerthsecList.Count > 0)
                {
                    Berthsec = BerthsecList[0];
                }
                string berthsecName = "";
                if (Berthsec != default(Berthsecs.Berthsec))
                {
                    berthsecName = Berthsec.BerthsecName;
                }

                if (entity.Status == 3)
                {
                    string message = "出场车辆" + entity.PlateNumber + ",驶入时间:" + entity.CarInTime + ",驶出时间:" + entity.CarOutTime + "停靠" + berthsecName + entity.BerthNumber + "泊位，停车时长:" + StopTimes(entity.StopTime) + ",应收金额" + entity.Money + "元，预缴金额:" + entity.Prepaid + "元,支付金额:" + entity.FactReceive + "元，欠费金额:" + entity.Arrearage + "元，操作收费员:" + employee.TrueName + "(" + employee.UserName + ")";
                    _p4ChatService.CreateChatService().Clients.Group(P4Consts.EmployeeGroup + TenantId).EmployeeMessage(employee.TrueName + "(" + employee.UserName + ")", message, 4);
                    _p4ChatService.CreateChatService().Clients.Group("index" + TenantId).sendIndexMessage(berthsecName, entity.BerthsecId, employee.Id, entity.Arrearage, 3);
                    _p4ChatService.CreateChatService().Clients.Group("index" + TenantId).sendIndexMessage(berthsecName, entity.BerthsecId, employee.Id, entity.Money, 1);
                    _subscribeAppService.SendMessage("CarInOutManagement", message, "");
                }
                else
                {
                    string message = "出场车辆" + entity.PlateNumber + ",驶入时间:" + entity.CarInTime + ",驶出时间:" + entity.CarOutTime + "停靠" + berthsecName + entity.BerthNumber + "泊位，停车时长:" + StopTimes(entity.StopTime) + ",应收金额:" + entity.Money + "元，预缴金额:" + entity.Prepaid + "元,支付金额:" + entity.FactReceive + "元,操作收费员:" + employee.TrueName + "(" + employee.UserName + ")";
                    _p4ChatService.CreateChatService().Clients.Group(P4Consts.EmployeeGroup + TenantId).EmployeeMessage(employee.TrueName + "(" + employee.UserName + ")", message, 3);
                    _p4ChatService.CreateChatService().Clients.Group("index" + TenantId).sendIndexMessage(berthsecName, entity.BerthsecId, employee.Id, entity.FactReceive, 2);
                    _p4ChatService.CreateChatService().Clients.Group("index" + TenantId).sendIndexMessage(berthsecName, entity.BerthsecId, employee.Id, entity.Money, 1);
                    _subscribeAppService.SendMessage("CarInOutManagement", message, "");
                }

                        //黑名单检测，黑名单检测短信报警
            List<BlackList> blackList = Abp.DataProcessHelper.GetEntityFromTable<BlackList>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpBlackList where IsActive =1 and RelateNumber =  " + entity.PlateNumber).Tables[0]);
            BlackList blackModel = new BlackList();
            if (blackList.Count > 0)
            {
                blackModel = blackList[0];
            }
                if (blackModel != default(BlackLists.BlackList))
                {
                    //黑名单车辆{0},车主姓名{1}，在{2}驶出{3}{4}泊位号，请悉知！
                    var model = new SmsAccountDto() { Msg = string.Format(_smsModelAppService.GetSmsModelByType("BlackCarOutModel").SmsContext, blackModel.RelateNumber, blackModel.CarOwner, Convert.ToDateTime(entity.CarOutTime).ToString("yyyy年MM月dd日hh时mm分"), berthsecName, entity.BerthNumber), Destnumbers = blackModel.IdNumber };
                    _smsManagementAppService.SendSmsNoTenant(model);
                    _subscribeAppService.SendMessage("BlackManagement", model.MsgValue, "");
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PlateNumber"></param>
        /// <param name="id"></param>
        /// <param name="oper"></param>
        /// <returns></returns>
        public int UpdateBusiness(string PlateNumber, string id, string oper)
        {
            var entity = _businessDetailRepository.FirstOrDefault(p => p.guid == new Guid(id));
            entity.PlateNumber = PlateNumber;
            _businessDetailRepository.Update(entity);
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public int UpdateMoney(string id, string money)
        {
            var entity = _businessDetailRepository.FirstOrDefault(p => p.guid == new Guid(id));
            entity.FactReceive = 0;
            entity.Arrearage = decimal.Parse( money);
            entity.Money = decimal.Parse(money);
            entity.Receivable = decimal.Parse(money);
            _businessDetailRepository.Update(entity);
            return 1;
        }
    }
}
