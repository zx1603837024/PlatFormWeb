using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace P4.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.IgnoreRoute("{resource}.woff/{*pathInfo}");

            //ASP.NET Web API Route Config
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Platform", action = "Login", id = UrlParameter.Optional }
            );

            //routes.Add("DomainRoute", new DomainRoute(
            //    "{TenantDomain}.developing.com",
            //    "{controller}/{action}/{id}",
            //     new { TenantDomain = "bouwa", controller = "Platform", action = "Login", id = UrlParameter.Optional }
            //));
        }
    }
}
