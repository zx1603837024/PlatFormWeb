using System.Reflection;
using Abp.Modules;
using P4.Configuration;
using Abp.AutoMapper;

namespace P4
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(P4CoreModule), typeof(AbpAutoMapperModule))]
    public class P4ApplicationModule : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        public override void PreInitialize()
        {
            base.PreInitialize();
            //设置缓存方式
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;
            //IocManager.Register<ICacheManager, AbpRedisCacheManager>();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Configuration.Settings.Providers.Add<MySettingProvider>();//个人参数设置
            DtoMappings.Map();//Mapp映射关系
            RegisterEventBus.RegisterEvent();
        }
    }
}
