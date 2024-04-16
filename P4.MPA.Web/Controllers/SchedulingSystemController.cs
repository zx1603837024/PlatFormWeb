using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.SchedulingSystem;
using P4.SchedulingSystem.Dto;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [AbpMvcAuthorize]
    public class SchedulingSystemController : P4ControllerBase
    {

        #region Var
        private readonly ISchedulingSystemAppService _schedulingSystemAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedulingSystemEmployeeCheckAppSrvice"></param>
        public SchedulingSystemController(ISchedulingSystemAppService schedulingSystemAppService)
        {
            _schedulingSystemAppService = schedulingSystemAppService;
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("EmployeeCheck")]
        public ActionResult EmployeeCheck()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("EmployeeCheck")]
        public JsonResult GetAllSchedulingSystemEmployeeCheckList(SearchSchedulingSystemEmployeeCheckInput input)
        {
            if (!string.IsNullOrWhiteSpace(input.filters))
                input.filters = input.filters.Replace("WorkTimeStr", "WorkTime").Replace("OffTimeStr", "OffTime");
            input.sidx = input.sidx.Replace("WorkTimeStr", "WorkTime").Replace("OffTimeStr", "OffTime");
            var model = _schedulingSystemAppService.GetAllSchedulingSystemEmployeeCheckList(input);
            return Json(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("HolidaysManager")]
        public ActionResult HolidaysManager()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("HolidaysManager")]
        public JsonResult GetAllHolidaysManagerList(SearchSchedulingSystemHolidaysManagerInput input)
        {
            var model = _schedulingSystemAppService.GetAllSchedulingSystemHolidaysManagerList(input);
            return Json(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("HolidaysManager.Insert")]
        public int InsertHolidays(CreateOrUpdateHolidaysInput input)
        {
            return _schedulingSystemAppService.InsertHoliday(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("HolidaysManager.Delete")]
        public int DeleteHolidays(CreateOrUpdateHolidaysInput input)
        {
            return _schedulingSystemAppService.DeleteHoliday(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("HolidaysManager.Update")]
        public int UpdateHolidays(CreateOrUpdateHolidaysInput input)
        {
            return _schedulingSystemAppService.UpdateHoliday(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult ProcessHolidays(CreateOrUpdateHolidaysInput input)
        {
            switch (input.oper)
            {
                case "del":
                    DeleteHolidays(input);
                    break;
                case "add":
                    InsertHolidays(input);
                    break;
                case "edit":
                    UpdateHolidays(input);
                    break;
                default:
                    break;
            }
            return Json(new MvcAjaxResponse());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("LeaveAdministration")]
        public ActionResult LeaveAdministration()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("ScheduleAdjustment")]
        public ActionResult ScheduleAdjustment()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("SchedulePreferences")]
        public ActionResult SchedulePreferences()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("WorkScheme")]
        public ActionResult WorkScheme()
        {
            return View();
        }
    }
}