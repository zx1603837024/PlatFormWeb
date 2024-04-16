using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.Modules;
using Abp.Threading.BackgroundWorkers;
using Hangfire;
using P4.Jobs;

namespace P4.HangFireJob
{

    /// <summary>
    /// 任务调度
    /// </summary>
    [DependsOn(typeof(AbpHangfireModule))]
    public class F2JobModule : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        public override void PreInitialize()
        {
            Configuration.BackgroundJobs.UseHangfire(configuration =>
            {
                configuration.GlobalConfiguration.UseSqlServerStorage("Default");
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public override void PostInitialize()
        {
            base.PostInitialize();

            var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
            workManager.Add(IocManager.Resolve<MakeInactiveMonthPassWorker>());
            workManager.Add(IocManager.Resolve<MakeInactiveNoticePassWorker>());
        }
    }
}
