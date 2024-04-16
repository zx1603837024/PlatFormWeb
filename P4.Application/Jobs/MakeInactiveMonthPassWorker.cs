using Abp.BackgroundJobs;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Helper;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using P4.MultiTenancy;
using System.Collections.Generic;

namespace P4.Jobs
{
    /// <summary>
    /// 包月车辆过期检测
    /// </summary>
    public class MakeInactiveMonthPassWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        #region  Var
        private readonly IRepository<MonthlyCars.MonthlyCar> _monthRepository;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IRepository<Tenant> _abpTenantRepository;
        private readonly ISettingStore _settingStore;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="monthRepository"></param>
        /// <param name="backgroundJobManager"></param>
        /// <param name="abpTenantRepository"></param>
        /// <param name="settingStore"></param>
        public MakeInactiveMonthPassWorker(AbpTimer timer, IRepository<MonthlyCars.MonthlyCar> monthRepository, IBackgroundJobManager backgroundJobManager, IRepository<Tenant> abpTenantRepository, ISettingStore settingStore)
        : base(timer)
        {
            _monthRepository = monthRepository;
            _backgroundJobManager = backgroundJobManager;
            _abpTenantRepository = abpTenantRepository;
            _settingStore = settingStore;
            Timer.Period = 40000;
        }

        /// <summary>
        /// 
        /// </summary>
        [UnitOfWork]
        protected override void DoWork()
        {
            var tenants = _abpTenantRepository.GetAllList();
            foreach (var tenant in tenants) 
            {
                if (_settingStore.GetSettingOrNull(tenant.Id, null, "Sms.Sent") != null) 
                {
                    if (bool.Parse(_settingStore.GetSettingOrNull(tenant.Id, null, "Sms.Sent").Value))
                    {
                        string sql = $@"select * from AbpMonthlyCars where IsDeleted = 0 and TenantId = {tenant.Id} and IsSms = 0 and datediff(DAY,GETDATE(),EndTime) =3 ";
                        var monthModel = Abp.DataProcessHelper.GetEntityFromTable<MonthlyCars.MonthlyCar>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, System.Data.CommandType.Text, sql).Tables[0]);
                        if (monthModel.Count > 0)
                        {
                            _backgroundJobManager.Enqueue<MonthyProcessJob, List<MonthlyCars.MonthlyCar>>(monthModel);
                        }
                    }
                }
            }

            
            //ProcessMonthy();
            //var oneMonthAgo = Clock.Now.Subtract(TimeSpan.FromDays(30));

            //var inactiveUsers = _monthRepository.GetAllList(u =>
            //    u.IsActive &&
            //    ((u.LastLoginTime < oneMonthAgo && u.LastLoginTime != null) || (u.CreationTime < oneMonthAgo && u.LastLoginTime == null))
            //    );
            //foreach (var inactiveUser in inactiveUsers)
            //{
            //    inactiveUser.IsActive = false;
            //}
            //CurrentUnitOfWork.SaveChanges();

        }
    }
}
