using Abp.Configuration;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace P4.Web.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [AbpMvcAuthorize]
    public class SystemParamSettingController : P4ControllerBase
    {
        #region Var
        private readonly ISettingStore _settingStore;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingStore"></param>
        public SystemParamSettingController(ISettingStore settingStore)
        {
            _settingStore = settingStore;
        }

        /// <summary>
        /// 商户系统数据
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> SystemParamSetting()
        {
            var settingAdm = await _settingStore.GetSettingOrNullAsync(null, null, "EmailAdress");
            var settings = await _settingStore.GetAllListAsync(AbpSession.TenantId, null);
            var model = new Models.SystemParamSetting
            {
                ParkApprove = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "ParkApprove")?.Value??"False"),
                OperationLog = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "OperationLog")?.Value ?? "False"),
                DataLog = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "DataLog")?.Value?? "False"),
                AuditLog = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "AuditLog")?.Value?? "False"),
                MapLocation = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "MapLocation")?.Value?? "False"),
                SMSChannel = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "SMSChannel")?.Value?? "False"),
                TheRecovered = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "TheRecovered")?.Value?? "False"),
                WhiteListOperation = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "WhiteListOperation")?.Value?? "False"),
                SystemMailbox = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "SystemMailbox")?.Value?? "False"),
                TaskScheduling = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "TaskScheduling")?.Value?? "False"),
                TheRecoveredCompany = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "TheRecoveredCompany")?.Value?? "False"),
                EscapeLock = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "EscapeLock")?.Value?? "False"),//追缴锁定
                EscapeBlack = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "EscapeBlack")?.Value?? "False"),//欠费黑名单，短信提醒
                EscapeBlackMoney = int.Parse(settings.FirstOrDefault(entity => entity.Name == "EscapeBlackMoney")?.Value?? "0"),// 欠费黑名单，拍照
                EscapePhoto = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "EscapePhoto")?.Value?? "False"),//欠费黑名单，拍照
                EscapePhotoMoney = int.Parse(settings.FirstOrDefault(entity => entity.Name == "EscapePhotoMoney")?.Value?? "0"),//欠费黑名单，拍照
                TargetAmount = decimal.Parse(settings.FirstOrDefault(entity => entity.Name == "TargetAmount")?.Value?? "0"),//达标金额
                EmailAdress = settingAdm.Value//试用邮箱地址                
            };
            return View(model);
        }

        /// <summary>
        /// 系统参数设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<JsonResult> SetSystemParamSetting(P4.Web.Models.SystemParamSetting entity)
        {
            SettingInfo settingInfo = new SettingInfo();
            //增加试用邮箱
            settingInfo.Value = entity.EmailAdress.ToString();
            settingInfo.Name = "EmailAdress";
            await _settingStore.UpdateAsync(settingInfo);

            SettingInfo model = new SettingInfo();
            model.TenantId = AbpSession.TenantId.Value;
            model.Value = entity.ParkApprove.ToString();
            model.Name = "ParkApprove";
            await _settingStore.UpdateAsync(model);

            //达标金额
            model.Value = entity.TargetAmount.ToString();
            model.Name = "TargetAmount";
            await _settingStore.UpdateAsync(model);

            model.Value = entity.OperationLog.ToString();
            model.Name = "OperationLog";
            await _settingStore.UpdateAsync(model);

            model.Value = entity.DataLog.ToString();
            model.Name = "DataLog";
            await _settingStore.UpdateAsync(model);

            model.Value = entity.AuditLog.ToString();
            model.Name = "AuditLog";
            await _settingStore.UpdateAsync(model);

            model.Value = entity.MapLocation.ToString();
            model.Name = "MapLocation";
            await _settingStore.UpdateAsync(model);

            model.Value = entity.SMSChannel.ToString();
            model.Name = "SMSChannel";
            await _settingStore.UpdateAsync(model);

            model.Value = entity.TheRecovered.ToString();
            model.Name = "TheRecovered";
            await _settingStore.UpdateAsync(model);

            model.Value = entity.WhiteListOperation.ToString();
            model.Name = "WhiteListOperation";
            await _settingStore.UpdateAsync(model);

            model.Value = entity.SystemMailbox.ToString();
            model.Name = "SystemMailbox";
            await _settingStore.UpdateAsync(model);

            model.Value = entity.TaskScheduling.ToString();
            model.Name = "TaskScheduling";
            await _settingStore.UpdateAsync(model);

            model.Value = entity.TaskScheduling.ToString();
            model.Name = "TaskScheduling";
            await _settingStore.UpdateAsync(model);

            model.Value = entity.TheRecoveredCompany.ToString();
            model.Name = "TheRecoveredCompany";
            await _settingStore.UpdateAsync(model);

            model.Value = entity.EscapeLock.ToString();
            model.Name = "EscapeLock";
            await _settingStore.UpdateAsync(model);

            //黑名单
            model.Value = entity.EscapeBlack.ToString();
            model.Name = "EscapeBlack";
            await _settingStore.UpdateAsync(model);

            //黑名单金额
            model.Value = entity.EscapeBlackMoney.ToString();
            model.Name = "EscapeBlackMoney";
            await _settingStore.UpdateAsync(model);

            //黑名单拍照
            model.Value = entity.EscapePhoto.ToString();
            model.Name = "EscapePhoto";
            await _settingStore.UpdateAsync(model);

            //黑名单拍照金额
            model.Value = entity.EscapePhotoMoney.ToString();
            model.Name = "EscapePhotoMoney";
            await _settingStore.UpdateAsync(model);

            return this.Json(new MvcAjaxResponse());
        }
    }
}