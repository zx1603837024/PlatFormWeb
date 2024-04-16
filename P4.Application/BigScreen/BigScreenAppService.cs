using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.Berths.Dtos;
using P4.BigScreen.Dtos;
using P4.Businesses;
using P4.Businesses.Dtos;
using P4.Companys;
using P4.Employees;
using P4.MonthlyCars;
using P4.MultiTenancy;
using P4.Weixin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace P4.BigScreen
{
    /// <summary>
    /// 
    /// </summary>
    public class BigScreenAppService : ApplicationService, IBigScreenAppService
    {
        #region Var
        private readonly IRepository<TUser> _abpWeixinTUserRepository;
        private readonly IMonthlyCarRespository _MonthlyCarRespository;
        private readonly IRepository<BusinessDetail, long> _abpBusinessDetailRepository;
        private readonly IRepository<Berths.Berth, long> _abpSensorsRepository;
        private readonly IRepository<OperatorsCompany> _abpCompanyRepository;
        private readonly IRepository<Tenant> _abpTenantRepository;
        private readonly IRepository<Berthsecs.Berthsec> _abpBerthsecRepository;
        private readonly IEmployeeRepository _abpEmployeeRepository;
        private readonly IBusinessRepository _abpBusinessRepository;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="abpBusinessDetailRepository"></param>
        /// <param name="abpCompanyRepository"></param>
        /// <param name="abpWeixinTUserRepository"></param>
        /// <param name="MonthlyCarRespository"></param>
        /// <param name="abpTenantRepository"></param>
        /// <param name="abpSensorsRepository"></param>
        /// <param name="abpBerthsecRepository"></param>
        /// <param name="abpBusinessRepository"></param>
        /// <param name="abpEmployeeRepository"></param>
        public BigScreenAppService(IRepository<BusinessDetail, long> abpBusinessDetailRepository, IRepository<OperatorsCompany> abpCompanyRepository, IRepository<TUser> abpWeixinTUserRepository, IMonthlyCarRespository MonthlyCarRespository, IRepository<Tenant> abpTenantRepository, IRepository<Berths.Berth, long> abpSensorsRepository, IRepository<Berthsecs.Berthsec> abpBerthsecRepository, IBusinessRepository abpBusinessRepository, IEmployeeRepository abpEmployeeRepository)
        {
            _abpWeixinTUserRepository = abpWeixinTUserRepository;
            _MonthlyCarRespository = MonthlyCarRespository;
            _abpBusinessDetailRepository = abpBusinessDetailRepository;
            _abpCompanyRepository = abpCompanyRepository;
            _abpTenantRepository = abpTenantRepository;
            _abpBerthsecRepository = abpBerthsecRepository;
            _abpBusinessRepository = abpBusinessRepository;
            _abpSensorsRepository = abpSensorsRepository;
            _abpEmployeeRepository = abpEmployeeRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public StatisticsDto GetStatisticsInfo()
        {
            List<int> CompanyIds = AbpSession.CompanyIds.ToList();

            DateTime time = DateTime.Now.AddDays(-1);
            if (!AbpSession.TenantId.HasValue)
                time = DateTime.Now.AddDays(-1);
            if (AbpSession.CompanyId.HasValue && !AbpSession.TenantId.HasValue)
            {
                time = _abpTenantRepository.Load(AbpSession.TenantId.Value).CreationTime;
            }
            else if (AbpSession.CompanyId.HasValue)
            {
                time = _abpCompanyRepository.Load(AbpSession.CompanyId.Value).CreationTime;
            }
            var days = DateTime.Now.Subtract(time).Days;
            var strtime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            StatisticsDto model = new StatisticsDto();
            model.AllStopTimes = _abpBusinessDetailRepository.GetAll().Count();
            model.Today = _abpBusinessDetailRepository.GetAll().Count(entity => entity.CreationTime > strtime);
            model.AverageStopTimes = model.AllStopTimes / days;


            var companylist = AbpSession.CompanyIds;
            if (AbpSession.CompanyId.HasValue)
            {
                companylist.Add(AbpSession.CompanyId.Value);
            }

            model.berthsecList = _abpBusinessRepository.GetBerthsecStatisticsList(AbpSession.BerthsecIds.ToArray(), companylist.ToArray(), AbpSession.TenantId);    
            
            model.TodayFactReceiveList = _abpBusinessRepository.GetEmployeeFactReceiveList(AbpSession.BerthsecIds.ToArray(), companylist.ToArray(), AbpSession.TenantId);

           
            var businessDetails = _abpBusinessDetailRepository.GetAll().Where(entity => entity.CreationTime >= strtime).ToList();

            foreach (var item in businessDetails)
            {
                model.SumReceivable += item.Receivable;
            }
            var businesses = _abpBusinessDetailRepository.GetAll().Where(entity => entity.CarOutTime >= strtime).ToList();
            foreach (var item in businesses)
            {
                model.SumArrearage += item.Arrearage;
                model.SumFactReceive += item.FactReceive;
            }
            var businessesmodel = _abpBusinessDetailRepository.GetAll().Where(entity => entity.CarRepaymentTime >= strtime).ToList();
            foreach (var item in businessesmodel)
            {
                model.SumRepayment += Convert.ToDecimal(item.Repayment == null ? 0: item.Repayment);
            }         
            return model;
        }

        /// <summary>
        /// 获取地磁信息
        /// </summary>
        /// <returns></returns>
        public SensorsDetail GetSensorsDetail()
        {
            SensorsDetail model = new SensorsDetail()
            {
                SensorsTotal = _abpSensorsRepository.Count(entity => !string.IsNullOrEmpty(entity.SensorNumber)),
                OnLine = _abpSensorsRepository.GetAll().Count(entity => !string.IsNullOrEmpty(entity.SensorNumber)&& System.Data.Entity.DbFunctions.DiffMinutes(entity.SensorBeatTime, DateTime.Now) <
               P4Consts.SensorDayOnline),
                OffLine = _abpSensorsRepository.GetAll().Count(entity => !string.IsNullOrEmpty(entity.SensorNumber)&& System.Data.Entity.DbFunctions.DiffMinutes(entity.SensorBeatTime, DateTime.Now) >
               P4Consts.SensorDayOnline),
            };
            return model;
        }

        /// <summary>
        /// 获取泊位使用情况
        /// </summary>
        /// <returns></returns>
        [Abp.Auditing.DisableAuditing]
        public List<BerthsecsUtilization> GetBerthsecsUtilization()
        {
            int TenantId = AbpSession.TenantId.Value;
            int ComplayId = AbpSession.CompanyId.Value;
            return _abpBusinessRepository.GetBerthsecsUtilization(TenantId, ComplayId);
        }

        /// <summary>
        /// 获取环比金额
        /// </summary>
        /// <returns></returns>
        public List<MoneyChain> GetMoneyChain()
        {
            int TenantId = AbpSession.TenantId.Value;
            int CompanyId = AbpSession.CompanyId.Value;
            return _abpBusinessRepository.GetMoneyChain(TenantId, CompanyId);
        }

        /// <summary>
        /// 获取泊位段坐标
        /// </summary>
        /// <returns></returns>
        public List<Berthsecs.Berthsec> GetBerthsecsPoint()
        {                
            return _abpBerthsecRepository.GetAll().Where(entity => !string.IsNullOrEmpty(entity.Lat) && !string.IsNullOrEmpty(entity.Lng)).ToList();
        }

        /// <summary>
        /// 获取收费员坐标
        /// </summary>
        /// <returns></returns>
        public List<EmpolyeePoint> GetEmpolyeePoint()
        {
            return _abpEmployeeRepository.GetEmpolyeePoints();
        }
        /// <summary>
        /// 获取收费员坐标
        /// </summary>
        /// <returns></returns>
        public EmpolyeePoint GetEmpolyeePointModel(int Employeeid)
        {
            return _abpEmployeeRepository.GetEmpolyeePointModel(Employeeid);
        }

        /// <summary>
        /// 泊位段今日金额
        /// </summary>
        /// <param name="BerthsecId"></param>
        /// <returns></returns>
        public BerthsecStatisticsDto GetBerthsecStatisticsInfo(int BerthsecId)
        {
            int CompanyId = AbpSession.CompanyId.Value;
            int TenantId = AbpSession.TenantId.Value;
            return _abpBusinessRepository.GetBerthsecStatistics(CompanyId, TenantId, BerthsecId);
        }

        /// <summary>
        /// 今日出场实收
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public EmployeeFactReceiveDto GetTodayFactReceiveList(int EmployeeId)
        {
            return _abpBusinessRepository.GetTodayFactReceiveList(EmployeeId);
        }

        /// <summary>
        /// 大屏金额展示
        /// </summary>
        /// <returns></returns>
        public StatisticsMoneyDto GetStatisticsMoneyInfo(int Status)
        {
            return _abpBusinessRepository.GetStatisticsInfo(Status);
        }

        /// <summary>
        /// 停车车次信息
        /// </summary>
        /// <returns></returns>
        public List<StopTimesRankDto> StopTimesRank()
        {
            return _abpBusinessRepository.StopTimesRank();
        }

        /// <summary>
        /// 微信注册用户概况
        /// </summary>
        /// <returns></returns>
        public List<int> WeixinTuser()
        {
            List<int> list = new List<int>();
            int TotalCount = _abpWeixinTUserRepository.Count();
            list.Add(TotalCount);
            int AddCount = _abpWeixinTUserRepository.GetAll().Where(entity => entity.registerDate >= DateTime.Now).Count();
            list.Add(AddCount);
            DateTime dateTime = DateTime.Now.AddDays(-15);
            int ActiveCount= _abpWeixinTUserRepository.GetAll().Where(entity => entity.lastLoginDate >= dateTime).Count();
            list.Add(ActiveCount);
            return list;
        }

        /// <summary>
        /// 包月车辆
        /// </summary>
        /// <returns></returns>
        public  List<int> MonthlyCar()
        {
            List<int> list = new List<int>();
            var time = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            var data = DateTime.Parse(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00"));
            int TotalCount = _MonthlyCarRespository.Count();
            list.Add(TotalCount);
            int AddCount = _MonthlyCarRespository.GetAll().Where(entity => entity.CreationTime >= time).Count();
            list.Add(AddCount);
            int InvalidCount = _MonthlyCarRespository.GetAll().Where(entity => entity.EndTime <= time && entity.EndTime > data).Count();
            list.Add(InvalidCount);
            return list;
        }
    }
}
