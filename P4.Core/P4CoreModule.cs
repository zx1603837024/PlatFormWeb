using System.Reflection;
using Abp.Modules;

namespace P4
{
    /// <summary>
    /// 
    /// </summary>
    public class P4CoreModule : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            //Configuration.MultiTenancy.IsEnabled = true;
        }
    }
}
