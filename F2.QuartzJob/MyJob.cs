using Abp.Dependency;
using Abp.Quartz;

namespace F2.QuartzJob
{
    /// <summary>
    /// 
    /// </summary>
    public class MyJob : IJob, ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            var test = 1;
        }
    }
}
