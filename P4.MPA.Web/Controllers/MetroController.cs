using Abp.Configuration;
using Abp.Web.Mvc.Authorization;
using P4.Users;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [AbpMvcAuthorize]
    public class MetroController : P4ControllerBase
    {
        private readonly ISettingStore _settingStore;
        private readonly UserManager _userManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingStore"></param>
        /// <param name="userManager"></param>
        public MetroController(ISettingStore settingStore, UserManager userManager)
        {
            _settingStore = settingStore;
            _userManager = userManager;
        }
        // GET: Metro
        public ActionResult MetroIndex()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuType"></param>
        public async Task<JsonResult> UpdateMenuType(string menuType)
        {
            var model = _settingStore.GetSettingOrNull(AbpSession.TenantId, AbpSession.UserId, "MenuType");
            model.Value = menuType;
            await _settingStore.UpdateAsync(model);
            _userManager.DatabaseToCache(AbpSession.TenantId, AbpSession.UserId.Value);
            return null;
        }
    }
}