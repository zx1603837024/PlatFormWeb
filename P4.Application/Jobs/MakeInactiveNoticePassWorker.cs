using System;
using Abp.Dependency;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Abp.BackgroundJobs;
using P4.MultiTenancy;
using Abp.Domain.Repositories;
using Abp.Configuration;
using P4.SmsManagements;

namespace P4.Jobs
{
    /// <summary>
    /// 
    /// </summary>
    public class MakeInactiveNoticePassWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {

        #region  Var
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IRepository<Tenant> _abpTenantRepository;
        private readonly ISettingStore _settingStore;
        private readonly ISmsModelAppService _smsModelAppService;
        private readonly ISmsManagementAppService _smsManagementAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="backgroundJobManager"></param>
        /// <param name="abpTenantRepository"></param>
        /// <param name="settingStore"></param>
        /// <param name="smsManagementAppService"></param>
        /// <param name="smsModelAppService"></param>
        public MakeInactiveNoticePassWorker(AbpTimer timer, IBackgroundJobManager backgroundJobManager, IRepository<Tenant> abpTenantRepository, ISettingStore settingStore, ISmsManagementAppService smsManagementAppService, ISmsModelAppService smsModelAppService) : base(timer)
        {
            _backgroundJobManager = backgroundJobManager;
            _abpTenantRepository = abpTenantRepository;
            _settingStore = settingStore;
            _smsManagementAppService = smsManagementAppService;
            _smsModelAppService = smsModelAppService;
            Timer.Period = 60000 * 31;//30分钟执行一次
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void DoWork()
        {

            var tenants = _abpTenantRepository.GetAllList();

            foreach (var tenant in tenants)
            {
                if (bool.Parse(_settingStore.GetSettingOrNull(tenant.Id, null, "UnpaidRecovery").Value))
                {
                    if (DateTime.Now.Hour == int.Parse(_settingStore.GetSettingOrNull(tenant.Id, null, "SentTime").Value.Split(':')[0]))
                    {
                        if (bool.Parse(_settingStore.GetSettingOrNull(tenant.Id, null, "Weixin.Sent").Value))
                        {
                            _backgroundJobManager.Enqueue<WeixinNoticeProcessJob, int>(tenant.Id);
                        }
                        if (bool.Parse(_settingStore.GetSettingOrNull(tenant.Id, null, "Sms.Sent").Value))
                        {
                            _backgroundJobManager.Enqueue<SmsNoticeProcessJob, int>(tenant.Id);
                        }
                    }
                }
            }
        }
    }
}
