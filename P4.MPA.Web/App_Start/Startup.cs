using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using P4.Web;
using Owin;
using P4.WebApi.Api.Controllers;
using P4.WebApi.Api.WebApi;
using Abp.Owin;
using ZooKeeperNet;
using Hangfire;
using Abp.Hangfire;

[assembly: OwinStartup(typeof(Startup))]

namespace P4.Web
{
    /// <summary>
    /// 启动项
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        private static IZooKeeper zk;
        public void Configuration(IAppBuilder app)
        {

            app.UseAbp();
            app.UseOAuthBearerAuthentication(AccountController.OAuthBearerOptions);
            app.UseOAuthAuthorizationServer(OAuthOptions.CreateServerOptions());

            // 如果cookies过期，跳转登录界面
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Platform/login")
            });
            // cookies身份认证
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //singnalr
            app.MapSignalR();

            //app.UseZookeeper();


            //app.UseHangfireDashboard("/hangfire", new DashboardOptions
            //{
            //    Authorization = new[] { new AbpHangfireAuthorizationFilter() }
            //});

            //app.UseHangfireDashboard();

        }
    }
}