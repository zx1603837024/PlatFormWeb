using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.PayLevelSettingsNs;
using P4.PayLevelSettingsNs.Dtos;
using System.Collections.Generic;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    [AbpMvcAuthorize]
    public class PayLevelSettingController : P4ControllerBase
    {
        #region Var
        private readonly IPayLevelSettingAppService _payLevelApp;
        #endregion

        public PayLevelSettingController(IPayLevelSettingAppService payLevelSettingAppService)
        {
            _payLevelApp = payLevelSettingAppService;
        }

        // GET: PayLevelSetting
        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize("PayLevelSettingData")]
        public ActionResult PayLevelSettings()
        {
            return View();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetPayLevels(PayLevelInput input)
        {
            return this.Json(_payLevelApp.GetPayLevels(input));
        }

        public JsonResult ProcessVideoEquipFaults(CreateOrUpdatePayLevel input)
        {
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case P4Consts.JqGridUpdate:
                    List<string> Ids = new List<string>(input.Id.ToString().Split(','));
                    if (Ids.Count > 1)
                    {
                        info.Message = "请选择一条数据";
                        return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
                    }
                    _payLevelApp.Update1(input);
                    break;
                case P4Consts.JqGridDelete:
                    VqFaultDelete(input);
                    break;
                case P4Consts.JqGridInsert:
                    _payLevelApp.Add1(input);
                    break;
                default:
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }


        [AbpMvcAuthorize("PayLevelSettingData.Delete")]
        public void VqFaultDelete(CreateOrUpdatePayLevel input)
        {
            _payLevelApp.Delete(input.Id);
        }

    }
}