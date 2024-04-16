using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 工作流控制器
    /// </summary>
    public class WorkflowController : P4ControllerBase
    {
        // GET: Workflow
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FlowDesign()
        {
            return View();
        }
        public ActionResult FlowDesign1()
        {
            return View();
        }
        public ActionResult Workflows()
        {
            return View();
        }
        private ActionResult StartAction()
        {

            //if (Url.IsLocalUrl(returnUrl))
            //{
            //    return Redirect(returnUrl);
            //}
            return RedirectToAction("FlowDesign", "Workflow");
        }
        public string cmAdd()
        {
            string returnjson = "{\"status\":1,\"msg\":\"success\",\"info\":{\"id\":145,\"flow_id\":34,\"process_name\":\"\u65b0\u5efa\u6b65\u9aa4\",\"process_to\":\"\",\"icon\":\"\",\"style\":\"left:407px;top:187px;color:#0e76a8;\"}}";
            return returnjson;
        }
        public string cmSave()
        {
            string returnjson = "{\"status\":1,\"msg\":\"^_^ \u4fdd\u5b58\u6210\u529f\",\"info\":\"\"}";
            return returnjson;
        }
        public string pmDelete()
        {
            string returnjson = "{\"status\":1,\"msg\":\"\u5220\u9664\u6210\u529f\",\"info\":\"\"}";
            return returnjson;
        }
        public string doubleclick()
        {
            string returnjson = "<P>good job!</p>";
            return returnjson;
        }

    }

}