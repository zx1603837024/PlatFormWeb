using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(F2.SPA.Web.Startup))]
namespace F2.SPA.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
