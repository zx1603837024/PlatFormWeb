using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using P4.EntityFramework;

namespace P4
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(P4CoreModule))]
    public class P4DataModule : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<P4DbContext>(null);
        }
    }
}
