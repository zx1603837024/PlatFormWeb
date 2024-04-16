using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;

namespace P4
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(AbpWebApiModule), typeof(P4ApplicationModule))]
    public class P4WebApiModule : AbpModule
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(P4ApplicationModule).Assembly, "F2")
                //.WithConventionalVerbs()
                .Build();

            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
            //ConfigureSwaggerUi();

        }

        //private void ConfigureSwaggerUi()
        //{
        //    Configuration.Modules.AbpWebApi().HttpConfiguration.EnableSwagger(c =>
        //     {
        //         c.SingleApiVersion("v1", "P4.WebApi");
        //         c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        //     }).EnableSwaggerUi();
        //}
    }
}
