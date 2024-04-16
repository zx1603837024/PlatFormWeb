using Abp.Modules;
using Abp.Quartz;

namespace F2.QuartzJob
{
    /// <summary>
    /// 任务调度
    /// </summary>
    [DependsOn(typeof(AbpQuartzModule))]
    public class F2QuartzJobModule : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        public override void PreInitialize()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public override void PostInitialize()
        {
            base.PostInitialize();


        }
        }
}
