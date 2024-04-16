using System;
using Abp.Dependency;
using Abp.Web;
using Castle.Facilities.Logging;

namespace P4.Web
{

    /// <summary>
    /// 
    /// </summary>
    public class MvcApplication : AbpWebApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Application_Start(object sender, EventArgs e)
        {
            IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.UseLog4Net().WithConfig("log4net.config"));

            //MiniProfilerEF6.Initialize();
            base.Application_Start(sender, e);
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Application_Error(object sender, EventArgs e)
        {
            base.Application_Error(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Application_BeginRequest(object sender, EventArgs e)
        {
            //if (Request.IsLocal)
            //{
            //    MiniProfiler.Start();
            //}
            base.Application_BeginRequest(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Application_EndRequest(object sender, EventArgs e)
        {
            //MiniProfiler.Stop();
            base.Application_EndRequest(sender, e);
        }
    }
}
