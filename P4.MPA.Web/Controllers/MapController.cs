using Abp.Web.Mvc.Authorization;
using P4.Employees;
using P4.Equipments;
using P4.InspectorCharges.Dtos;
using P4.Inspectors;
using P4.Web.Models;
using System.Web.Mvc;

namespace P4.Web.Controllers
{

    /// <summary>
    /// 地图控制器
    /// </summary>
    [AbpMvcAuthorize]
    public class MapController : P4ControllerBase
    {

        #region Var
        private readonly IEquipmentAppService _equipmentAppService;
        private readonly IEmployeeAppService _employeeAppService;
        private readonly IInspectorAppService _InspectorApplicationService;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="equipmentAppService"></param>
        /// <param name="employeeAppService"></param>
        /// <param name="InspectorApplicationService"></param>
        public MapController(IEquipmentAppService equipmentAppService, IEmployeeAppService employeeAppService, IInspectorAppService InspectorApplicationService)
        {
            _equipmentAppService = equipmentAppService;
            _employeeAppService = employeeAppService;
            _InspectorApplicationService = InspectorApplicationService;
        }

        /// <summary>
        /// 收费员轨迹
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("EmployeeLocus")]
        public ActionResult EmployeeLocus()
        {
            EmployeeChargesModel model = new EmployeeChargesModel();
            model.EmployeeList = _employeeAppService.GetEmployeeAll();
            //model.AllOption = alloption;
            return View(model);
        }

        /// <summary>
        /// 巡查员轨迹
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("InspectorLocus")]
        public ActionResult InspectorLocus()
        {
            InspectorChargesModel model = new InspectorChargesModel();
            model.InspectorList = _InspectorApplicationService.GetInspectorAll();
            model.AllOption = alloption;
            return View(model);
        }

        /// <summary>
        /// 人员轨迹
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetInspectorLocusList(InspectorChargeInput input)
        {
            return this.Json(_equipmentAppService.GetEquipmentGpsList(input));
        }

       

        /// <summary>
        /// 车辆停车轨迹
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("CarStopLocus")]
        public ActionResult CarStopLocus()
        {
            return View();
        }
    }
}