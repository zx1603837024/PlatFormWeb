using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Localization;
using Abp.Localization.Sources.Xml;
using Abp.Modules;
using P4.Authorization;
using Abp.Runtime.Caching.Redis;
using P4.HangFireJob;

namespace P4.Web
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(P4DataModule), typeof(P4ApplicationModule), typeof(P4WebApiModule), typeof(AbpRedisCacheModule), typeof(F2JobModule))]
    public class P4WebModule : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        public override void PreInitialize()
        {
            Configuration.Localization.Languages.Add(new LanguageInfo("zh-CN", "简体中文", "famfamfam-flag-cn", true));
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flag-england"));
            Configuration.Localization.Sources.Add(
                new XmlLocalizationSource(
                    P4Consts.LocalizationSourceName,
                    HttpContext.Current.Server.MapPath("~/Localization/P4")
                    )
                );


            //使用redis缓存
            //Configuration.Caching.UseRedis();

            //配置导航栏菜单
            Configuration.Navigation.Providers.Add<P4NavigationProvider>();

            //配置权限访问菜单
            Configuration.Authorization.Providers.Add<P4AuthorizationProvider>();

            //配置版本功能菜单控制
            Configuration.Features.Providers.Add<P4FeatureProvider>();

            //开启多租户模式
            Configuration.MultiTenancy.IsEnabled = true;

            //审计日志开关
            Configuration.Auditing.MvcControllers.IsEnabledForChildActions = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
