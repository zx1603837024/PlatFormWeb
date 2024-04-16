using Abp.Web.Mvc.Authorization;
using P4.Businesses;
using P4.EmployeeBatchNoDynamicReport;
using P4.EmployeeBatchNoDynamicReport.Dtos;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    [AbpMvcAuthorize]
    public class EmployeeBatchNoReportController : P4ControllerBase
    {
        private readonly IBusinessAppService _businessAppService;
        private readonly IEmployeeBatchNoDynamicReportAppSerVice _employeeBatchNoReport;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="businessAppService"></param>
        public EmployeeBatchNoReportController(IBusinessAppService businessAppService, IEmployeeBatchNoDynamicReportAppSerVice employeeBatchNoReport)
        {
            _businessAppService = businessAppService;
            _employeeBatchNoReport = employeeBatchNoReport;
        }

        /// <summary>
        /// 获取视图
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("EmployeeBatchNoReport")]
        public ActionResult EmployeeBatchNoDynamicReport()
        {
            return View();
        }

        /// <summary>
        /// 获取对账和员工详细
        /// </summary>
        /// <param name="BatchNo"></param>
        /// <returns></returns>
        public JsonResult GetReconciliationModel(string BatchNo)
        {
            var TenantId = AbpSession.TenantId.Value;
            var CompanyId = AbpSession.CompanyId.Value;
            var employDetail = _employeeBatchNoReport.GetEmployDetail(BatchNo, TenantId, CompanyId);
            var reconciliationModel = _businessAppService.GetReconciliationModel(BatchNo);
            var model = employDetail.Replace("}",",")+ reconciliationModel.Replace("{","");      
            return this.Json(model);
        }
        /// <summary>
        /// 获取jqueryGrid
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetEmployeeBatchNoDynamicReport(GetEmployeeBatchNoDynamicReportInput input)
        {
            if (string.IsNullOrEmpty(input.BatchNo))
                input.BatchNo="111";
            input.TenantId = AbpSession.TenantId.Value;
            input.CompanyId = AbpSession.CompanyId.Value;
            return this.Json(_businessAppService.GetGetEmployeeBatchNoDynamicReportSql(input));
        }
    }
}