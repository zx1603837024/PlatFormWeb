using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 网络拓扑图
    /// </summary>
    public class TopologicalController : P4ControllerBase
    {
        // GET: Topological
        public ActionResult Index()
        {
            return View();
        }
    }
}