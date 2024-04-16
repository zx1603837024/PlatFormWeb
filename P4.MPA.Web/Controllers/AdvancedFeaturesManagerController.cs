using Abp.Web.Mvc.Authorization;
using P4.AdvancedFeaturesManager;
using P4.AdvancedFeaturesManager.Model;
using P4.Businesses;
using P4.Employees;
using P4.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 高级功能（根据客户业务需求单独定制开发此功能 ，只开放给admin管理员）
    /// </summary>
    [AbpMvcAuthorize]
    public class AdvancedFeaturesManagerController : P4ControllerBase
    {
        #region 定义service业务数据操作类
        private readonly IAdvancedFeaturesManagerAppService _advancedFeaturesManagerAppService;
        private readonly IEmployeeAppService _employeeAppService;
        private readonly IBusinessAppService _businessAppService;
        #endregion

        public AdvancedFeaturesManagerController(IBusinessAppService businessAppService, IAdvancedFeaturesManagerAppService advancedFeaturesManagerAppService, IEmployeeAppService employeeAppService)
        {
            //初始化service类
            _businessAppService = businessAppService;
            _advancedFeaturesManagerAppService = advancedFeaturesManagerAppService;
            _employeeAppService = employeeAppService;
        }

        /// <summary>
        /// 收费员日报平账页面
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("EmployeeDailyReportBalance")]
        public ActionResult EmployeeDailyReportBalance()
        {
            EmployeeChargesModel model = new EmployeeChargesModel();
            model.EmployeeList = _employeeAppService.GetEmployeeAll();//收费员列表
            model.AllOption = alloption;

            return View(model);
        }

        /// <summary>
        /// 获取收费员分配的所有泊位段
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="TenantId"></param>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public JsonResult getBerthsecAllListByEmpId(getBerthsecAllListByEmpIdInput view)
        {
            return Json(_advancedFeaturesManagerAppService.getBerthsecAllListByEmpId(view));
        }

        /// <summary>
        /// 平账
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public JsonResult FlatAccount(FlatAccountView view)
        {
            _advancedFeaturesManagerAppService.FlatAccount(view);
            return Json(new { a = "OK" });

        }
        [AbpMvcAuthorize("ArrearageDetails")]
        public ActionResult ArrearageDetails()
        {
            return View();
        }
    }
}