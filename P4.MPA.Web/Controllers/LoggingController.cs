using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.AuditLogs;
using P4.AuditLogs.Dto;
using P4.DataLogs;
using P4.Dictionarys;
using P4.EmployeeLoggings;
using P4.EmployeeLoggings.Dtos;
using P4.OperationLog;
using P4.OperationLog.Dtos;
using P4.Web.Models;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 日志管理
    /// </summary>
    [AbpMvcAuthorize]
    public class LoggingController : P4ControllerBase
    {

        #region Var
        private readonly IAuditLogAppService _auditLogAppService;
        private readonly IDataLogsAppService _dataLogsAppService;
        private readonly IOperationLogAppService _operationLogAppService;
        private readonly IEmployeeCheckAppService _EmployeeCheckLogAppService;
        private readonly IDictionarysAppService _DictionarysAppServic;
        #endregion

        public LoggingController(IAuditLogAppService auditLogAppService,
            IDataLogsAppService dataLogsAppService,
            IOperationLogAppService operationLogAppService, IEmployeeCheckAppService EmployeeCheckLogAppService, IDictionarysAppService DictionarysAppServic)
        {
            _auditLogAppService = auditLogAppService;
            _dataLogsAppService = dataLogsAppService;
            _operationLogAppService = operationLogAppService;
            _EmployeeCheckLogAppService = EmployeeCheckLogAppService;
            _DictionarysAppServic = DictionarysAppServic;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("DataLog")]
        public ActionResult DataLog()
        {
            SystemLogModel SLM = new SystemLogModel();
            SLM.DictionaryValue = _DictionarysAppServic.GetDictionaryValue();
            return View();
        }

        public JsonResult GetAllDataLogList(DataLogs.Dtos.DataLogInput input)
        {
            return this.Json(_dataLogsAppService.GetAllDataLogByPage(input));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult ProcessDataLog(long Id)
        {
            _dataLogsAppService.DeleteDatalogInfo(Id);
            return this.Json(new MvcAjaxResponse());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllDataLogItemsList(DataLogs.Dtos.DataLogItemInput input, long id = default(long))
        {
            input.DataLogId = id;
            return this.Json(_dataLogsAppService.GetAllDataLogItemById(input).rows, JsonRequestBehavior.AllowGet);
        }

        [AbpMvcAuthorize("OperationLog")]
        public ActionResult OperationLog()
        {
            return View();
        }

        public JsonResult GetAllOperationLogList(OperationLogInput input)
        {
            return this.Json(_operationLogAppService.GetAllOperationLogList(input));
        }



        public JsonResult ProcessOperationLog(long Id)
        {
             _operationLogAppService.DeleteOperationLog(Id);
             return this.Json(new MvcAjaxResponse());
        }

        [AbpMvcAuthorize("AuditLog")]
        public ActionResult AuditLog() 
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetAllAuditLog(SearchAuditLogInput input)
        {
            return this.Json(_auditLogAppService.GetAll(input));
        }

        [AbpMvcAuthorize("EmployeeLog")]
        public ActionResult EmployeeLog()
        {
            return View();
        }
        
        /// <summary>
        /// 收费员日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetEmployeeALlChecklist(EmployeeCheckInput input)
        {
            if (!string.IsNullOrWhiteSpace(input.filters))
                input.filters = input.filters.Replace("TrueName", "TrueNameChange");
            return this.Json(_EmployeeCheckLogAppService.GetAllEmployeeChecklist(input));
        }

        public JsonResult ProcessBerthType(CreatOrUpdateEmployeeCheckInput input)
        {
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case P4Consts.JqGridDelete:
                    EmployeeCheckDelete(input);
                    break;
                default:
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }
        
        [AbpMvcAuthorize("EmployeeLog.Delete")]
        public void EmployeeCheckDelete(CreatOrUpdateEmployeeCheckInput input)
        {
            _EmployeeCheckLogAppService.Delete(input.Id);
        }

    }
}