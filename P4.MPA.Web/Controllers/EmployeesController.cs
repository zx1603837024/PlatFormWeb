using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.Dictionarys;
using P4.EmployeeLoggings;
using P4.Employees;
using P4.Employees.Dtos;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [AbpMvcAuthorize]
    public class EmployeesController : P4ControllerBase
    {
        #region var
        private readonly IDictionarysAppService _dictionarysApplicationService;
        private readonly IEmployeeAppService _employeeApplicationService;
        private readonly IEmployeeCheckAppService _employeeCheckAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionarysApplicationService"></param>
        /// <param name="employeeApplicationService"></param>
        /// <param name="employeeCheckAppService"></param>
        public EmployeesController(IDictionarysAppService dictionarysApplicationService, IEmployeeAppService employeeApplicationService, IEmployeeCheckAppService employeeCheckAppService)
        {
            _dictionarysApplicationService = dictionarysApplicationService;
            _employeeApplicationService = employeeApplicationService;
            _employeeCheckAppService = employeeCheckAppService;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("EmployeeManagement")]
        public ActionResult EmployeeManagement()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetAllAccountStaus()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _dictionarysApplicationService.GetAllValueData("A10003"))
            {
                strtemp.AppendFormat(option, model.ValueCode, model.ValueData);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 获取收费员管理列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult GetAllEmployeeList(EmployeeInput entity)
        {
            return this.Json(_employeeApplicationService.GetAllEmployeeList(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("EmployeeGps")]
        public ActionResult EmployeeGps()
        {
            return View();
        }

        /// <summary>
        /// 收费员地图
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllEmployeeGps()
        {
            return this.Json(_employeeApplicationService.GetEmployeeGps(0));
        }

        /// <summary>
        /// 收费员地图
        /// </summary>
        /// <returns></returns>
        public JsonResult GetEmplopyeeGps(int Id)
        {
            return this.Json(_employeeApplicationService.GetEmployeeGps(Id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult ProcessEmployee(CreatOrUpdateEmployee input)
        {
            JsonResult returnJson = new JsonResult();
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case P4Consts.JqGridInsert:
                    info.Message = EmployeeInsert(input);
                    break;
                case P4Consts.JqGridDelete:
                    returnJson = EmployeeDelete(input);
                    break;
                case P4Consts.JqGridUpdate:
                    info.Message = EmployeeEdit(input);
                    break;
                default:
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }
        /// <summary>
        /// 收费员批量签退
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("EmployeeManagement.Field1")]
        public JsonResult EmployeeBatchCheckOut(List<int> Ids)
        {
            int count = _employeeApplicationService.EmployeeBatchCheckoutBGO(Ids);
            if (count > 0)
            {
                return Json(new { a = "OK" });
            }
            else
            {
                return Json(new { a = "NO" });
            }
        }

        /// <summary>
        /// 取消对账
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("EmployeeManagement.Field2")]
        public JsonResult EmployeeNOAccountCheckOut(int Id)
        {
            int count = _employeeApplicationService.EmployeeNOAccountCheckOutBGO(Id);
            if (count > 0)
            {
                return Json(new { a = "OK" });
            }
            else
            {
                return Json(new { a = "NO" });
            }
        }







        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("EmployeeManagement.Insert")]
        public string EmployeeInsert(CreatOrUpdateEmployee input)
        {
            return _employeeApplicationService.EmployeeInsert(input);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("EmployeeManagement.Delete")]
        public JsonResult EmployeeDelete(CreatOrUpdateEmployee input)
        {
            _employeeApplicationService.EmployeeDelete(input);
            return this.Json("123123");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("EmployeeManagement.Update")]
        public string EmployeeEdit(CreatOrUpdateEmployee input)
        {
            return _employeeApplicationService.EmployeeUpdate(input);
        }

        public JsonResult GetEmployeeCheckTopList(long Id)
        {
            return this.Json(_employeeCheckAppService.GetAllEmployeeCheckToplist(Id), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetEmployeeSelectList()
        {
            StringBuilder strtemp = new StringBuilder("<select>");

            strtemp.AppendFormat(option, 0, "请选择");

            foreach (var model in _employeeApplicationService.GetEmployeeSelectList())
            {
                strtemp.AppendFormat(option, model.Id, model.TrueName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 微信获取人员下拉框
        /// </summary>
        /// <returns></returns>
        public string GetEmployeeSelect()
        {
            StringBuilder strtemp = new StringBuilder("<select>");

            strtemp.AppendFormat(option, 0, "可选择");

            foreach (var model in _employeeApplicationService.GetEmployeeSelectList())
            {
                strtemp.AppendFormat(option, model.Id, model.TrueName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }
    }
}