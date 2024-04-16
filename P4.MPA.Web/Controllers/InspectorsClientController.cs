using P4.Web.Models.Weixin;
using P4.WorkGroups;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 巡查员功能管理
    /// </summary>
    [AllowAnonymous]
    public class InspectorsClientController : P4ControllerBase
    {
        private readonly IWorkGroupAppService _employeeAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeAppService"></param>
        public InspectorsClientController(IWorkGroupAppService employeeAppService)
        {
            _employeeAppService = employeeAppService;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 收费员地图
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult EmployeeMap(long InspectorId)
        {
            WeixinModel model = new WeixinModel();
            model.InspectorId = InspectorId;
            return View(model);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="InspectorId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult BerthsecMap(long InspectorId)
        {
            WeixinModel model = new WeixinModel();
            model.InspectorId = InspectorId;
            return View(model);
        }

        /// <summary>
        /// 通过巡查员查询泊位段数据
        /// </summary>
        /// <param name="InspectorId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public JsonResult GetAllBerthsecGps(long InspectorId)
        {
            return this.Json(_employeeAppService.GetBerthsecGps(InspectorId), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 通过巡查员获取收费员数据
        /// </summary>
        /// <param name="InspectorId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public JsonResult GetEmplopyeeGps(long InspectorId)
        {
            return this.Json(_employeeAppService.GetEmployeeGps(InspectorId), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BerthsecId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult BerthClient(int BerthsecId)
        {
            WeixinModel model = new WeixinModel();
            model.BerthList = _employeeAppService.GetBerthList(BerthsecId);
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BerthsecId"></param>
        /// <returns></returns>
        public JsonResult BerthListClient(int BerthsecId)
        {
            return Json(_employeeAppService.GetBerthList(BerthsecId), JsonRequestBehavior.AllowGet);
        }
    }
}