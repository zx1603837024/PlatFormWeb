using Abp.Configuration;
using Abp.Web.Mvc.Authorization;
using P4.Web.Models;
using Abp.Web.Mvc.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [AbpMvcAuthorize]
    public class EscapeNoticesController : P4ControllerBase
    {
        #region Var
        private readonly ISettingStore _settingStore;
        #endregion

        public EscapeNoticesController(ISettingStore settingStore)
        {
            _settingStore = settingStore;
        }

        // GET: EscapeNotices
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("EscapeNotice")]
        public ActionResult EscapeNotice()
        {
            EscapeNoticeModel model = new EscapeNoticeModel();
            model.SentTime = _settingStore.GetSettingOrNull(AbpSession.TenantId, null, "SentTime").Value;
            model.WeixinSent = bool.Parse(_settingStore.GetSettingOrNull(AbpSession.TenantId, null, "Weixin.Sent").Value);
            model.SmsSent = bool.Parse(_settingStore.GetSettingOrNull(AbpSession.TenantId, null, "Sms.Sent").Value);
            model.UnpaidRecovery = bool.Parse(_settingStore.GetSettingOrNull(AbpSession.TenantId, null, "UnpaidRecovery").Value);
            model.UnpaidMoney = int.Parse(_settingStore.GetSettingOrNull(AbpSession.TenantId, null, "UnpaidMoney").Value);
            model.IsolationTime = int.Parse(_settingStore.GetSettingOrNull(AbpSession.TenantId, null, "IsolationTime").Value);
            model.SendTimes = int.Parse(_settingStore.GetSettingOrNull(AbpSession.TenantId, null, "SendTimes").Value);
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        [AbpMvcAuthorize("EscapeNotice")]
        public async Task<JsonResult> SaveEscapeNotice(EscapeNoticeModel entity)
        {
            SettingInfo model = new SettingInfo();
            model.TenantId = AbpSession.TenantId.Value;
            model.Name = "SentTime";
            model.Value = entity.SentTime;
            await _settingStore.UpdateAsync(model);

            model.Name = "Weixin.Sent";
            model.Value = entity.WeixinSent.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "Sms.Sent";
            model.Value = entity.SmsSent.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "UnpaidRecovery";
            model.Value = entity.UnpaidRecovery.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "UnpaidMoney";
            model.Value = entity.UnpaidMoney.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "IsolationTime";
            model.Value = entity.IsolationTime.ToString();
            await _settingStore.UpdateAsync(model);

            model.Name = "SendTimes";
            model.Value = entity.SendTimes.ToString();
            await _settingStore.UpdateAsync(model);

            return Json(new MvcAjaxResponse());
        }
    }
}