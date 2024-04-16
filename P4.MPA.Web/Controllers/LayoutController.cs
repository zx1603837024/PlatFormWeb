using Abp.Application.Navigation;
using Abp.Auditing;
using Abp.Configuration;
using Abp.Localization;
using Abp.Runtime.Caching;
using Abp.Threading;
using Abp.Web.Mvc.Authorization;
using P4.Menus;
using P4.Messages;
using P4.Notices;
using P4.Tasks;
using P4.Tenants;
using P4.Users;
using P4.Web.Models.Layout;
using System.Collections.Generic;
using System.Web.Mvc;
using P4.Common;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 公共页类
    /// </summary>
    [AbpMvcAuthorize]
    public class LayoutController : P4ControllerBase
    {
        #region Var
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly UserManager _userManager;
        private readonly ILocalizationManager _localizationManager;
        private readonly IMessageAppService _messageApplicationService;
        private readonly ISettingStore _settingStore;
        private readonly INoticeAppService _noticeApplicationService;
        private readonly ITaskAppService _taskApplicationService;
        private readonly IUserAppService _userApplicationService;
        private readonly IMenuAppService _menuApplicationService;
        private readonly ITenantAppService _tenantApplicationService;
        private readonly ICacheManager _cacheManager;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userNavigationManager"></param>
        /// <param name="localizationManager"></param>
        /// <param name="messageApplicationService"></param>
        /// <param name="noticeApplicationService"></param>
        /// <param name="taskApplicationService"></param>
        /// <param name="userApplicationService"></param>
        /// <param name="settingStore"></param>
        /// <param name="menuApplicationService"></param>
        /// <param name="tenantApplicationService"></param>
        /// <param name="cacheManager"></param>
        /// <param name="userManager"></param>
        public LayoutController(IUserNavigationManager userNavigationManager, ILocalizationManager localizationManager, IMessageAppService messageApplicationService, INoticeAppService noticeApplicationService, ITaskAppService taskApplicationService, IUserAppService userApplicationService, ISettingStore settingStore, IMenuAppService menuApplicationService, ITenantAppService tenantApplicationService, ICacheManager cacheManager, UserManager userManager)
        {
            _userNavigationManager = userNavigationManager;
            _localizationManager = localizationManager;
            _messageApplicationService = messageApplicationService;
            _noticeApplicationService = noticeApplicationService;
            _taskApplicationService = taskApplicationService;
            _userApplicationService = userApplicationService;
            _settingStore = settingStore;
            _menuApplicationService = menuApplicationService;
            _tenantApplicationService = tenantApplicationService;
            _cacheManager = cacheManager;
            _userManager = userManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult Header()
        {
            HeaderModel model = new HeaderModel();
            if (AbpSession.TenantId.HasValue)
            {
                model.TenantName = _tenantApplicationService.FirstOrDefault(AbpSession.TenantId.Value).HQ;
            }
            else
            {
                model.TenantName = L("CompanyName");
            }
            return PartialView("_Header", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activeMenu"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult Active(string activeMenu = "")
        {
            TopActiveModel topActiveModel = new TopActiveModel { Shortcuts = new List<Menu>() };
            SettingInfo info = new SettingInfo();
            var dic = _cacheManager.GetUserSettingsCache().GetOrDefault(AbpSession.UserId.Value);
            if (dic == null)
            {
                _userManager.DatabaseToCache(AbpSession.TenantId, AbpSession.UserId.Value);
                dic = _cacheManager.GetUserSettingsCache().GetOrDefault(AbpSession.UserId.Value);
            }
            //菜单类型
            bool val = dic.TryGetValue("MenuType", out info);
            if (info != default(SettingInfo))
                topActiveModel.MenuType = info.Value;
            else
            {
                topActiveModel.MenuType = "Road";
            }


            //初始化
            var menu = _menuApplicationService.GetMenus();
            var settingInfo = _settingStore.GetSettingOrNull(AbpSession.TenantId, AbpSession.UserId.Value, "Shortcuts");
            if (settingInfo != null)
            {
                List<Menu> list = new List<Menu>();
                foreach (var str in settingInfo.Value.Split(','))
                {
                    list.Add(menu.Find(p => p.Name == str));
                }
                topActiveModel.Shortcuts = list;
            }
            topActiveModel.ActiveMenu = L(string.IsNullOrWhiteSpace(activeMenu) == true ? "Cotrol" : activeMenu);
            topActiveModel.TenantField = AbpSession.TenantId.HasValue == true ? "true" : "false";
            topActiveModel.CompanyStatus = AbpSession.CompanyId.HasValue == true ? "true" : "false";
            topActiveModel.Language = _settingStore.GetSettingOrNull(AbpSession.TenantId, AbpSession.UserId.Value, "Language").Value;
            topActiveModel.TenantId = AbpSession.TenantId;
            topActiveModel.UserName = AbpSession.UserName;
            topActiveModel.UserId = AbpSession.UserId.Value;         
            return PartialView("_Active", topActiveModel);
        }

        /// <summary>
        /// 个性化设置
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [DisableAuditing]
        public PartialViewResult ProfileSetting()
        {
            ProfileModel model = new ProfileModel();
            SettingInfo info = new SettingInfo();

            var dic = _cacheManager.GetUserSettingsCache().GetOrDefault(AbpSession.UserId.Value);
            if (dic == null)
            {
                _userManager.DatabaseToCache(AbpSession.TenantId, AbpSession.UserId.Value);
                dic = _cacheManager.GetUserSettingsCache().GetOrDefault(AbpSession.UserId.Value);
            }
            bool val = false;
            val = dic.TryGetValue("breadcrumbs", out info);
            model.Breadcrumbs = info.Value;
            val = dic.TryGetValue("container", out info);
            model.Container = info.Value;
            val = dic.TryGetValue("navbar", out info);
            model.Navbar = info.Value;
            val = dic.TryGetValue("sidebar", out info);
            model.Sidebar = info.Value;
            val = dic.TryGetValue("rtl", out info);
            model.Rtl = info.Value;
            val = dic.TryGetValue("skin", out info);
            model.Skin = info.Value;

            val = dic.TryGetValue("sidebarcollapsed", out info);
            model.SidebarCollapsed = info.Value;
            #region MyRegion
            //var entry = _settingStore.GetAllList(AbpSession.TenantId, AbpSession.UserId);
            //model.Breadcrumbs = entry.Find(entity => entity.Name == "breadcrumbs").Value;
            //model.Container = entry.Find(entity => entity.Name == "container").Value;
            //model.Navbar = entry.Find(entity => entity.Name == "navbar").Value;
            //model.Sidebar = entry.Find(entity => entity.Name == "sidebar").Value;
            //model.Rtl = entry.Find(entity => entity.Name == "rtl").Value;
            //model.Skin = entry.Find(entity => entity.Name == "skin").Value;
            //model.SidebarCollapsed = entry.Find(entity => entity.Name == "sidebarcollapsed").Value;
            #endregion
            return PartialView("_ProfileSetting", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private UserMenu CacheUserMenus()
        {
              return AsyncHelper.RunSync(() => _userNavigationManager.GetMenuAsync("MainMenu", AbpSession.UserId));
        }

        /// <summary>
        /// 系统导航栏
        /// </summary>
        /// <param name="activeMenu">获取菜单名称</param>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult Menu(string activeMenu = "")
        {
            TopMenuViewModel topMenuViewModel = new TopMenuViewModel();
            topMenuViewModel.ActiveMenuItemName = activeMenu;
            //缓存用户数据菜单
            topMenuViewModel.MainMenu = CacheUserMenus();


            ProfileModel model = new ProfileModel();
            SettingInfo info = new SettingInfo();

            var dic = _cacheManager.GetUserSettingsCache().GetOrDefault(AbpSession.UserId.Value);
            if (dic == null)
            {
                _userManager.DatabaseToCache(AbpSession.TenantId, AbpSession.UserId.Value);
                dic = _cacheManager.GetUserSettingsCache().GetOrDefault(AbpSession.UserId.Value);
            }
            //菜单类型
            bool val = dic.TryGetValue("MenuType", out info);
            if (info != default(SettingInfo))
                topMenuViewModel.MenuType = info.Value;
            else
            {
                topMenuViewModel.MenuType = "Road";
            }

            topMenuViewModel.ActiveMenuItem = GetActiveMenu(topMenuViewModel);
            return PartialView("_Menu", topMenuViewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private UserMenuItem GetActiveMenu(TopMenuViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.ActiveMenuItemName))
                return new UserMenuItem() { Discriminator = "" };
            return GetActiveMenu(model.MainMenu.Items, model.ActiveMenuItemName);
        }

        /// <summary>
        /// 获取活动菜单
        /// </summary>
        /// <param name="items"></param>
        /// <param name="ActiveMenuItemName"></param>
        /// <returns></returns>
        private UserMenuItem GetActiveMenu(IList<UserMenuItem> items, string ActiveMenuItemName)
        {
            foreach (var menu in items)
            {
                if (menu.Name == ActiveMenuItemName)
                    return menu;
                else
                {
                    if (menu.Items.Count > 0)
                    {
                        var temp = GetActiveMenu(menu.Items, ActiveMenuItemName);
                        if (temp.Discriminator != "")
                            return temp;
                    }
                }
            }
            return new UserMenuItem() { Discriminator = "" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [DisableAuditing]
        public PartialViewResult TopBar()
        {
            var model = new TopBarViewModel();
            model.Messages = _messageApplicationService.GetAll().Items; //消息
            model.MessagesCount = _messageApplicationService.GetAllCount();
            model.Notices = _noticeApplicationService.GetAll().Items;   //通知
            model.NoticesCount = _noticeApplicationService.GetAllCount();
            model.Tasks = _taskApplicationService.GetAll().Items;       //任务
            model.TasksCount = _taskApplicationService.GetAllCount();
            model.Name = AbpSession.Name;
            model.SystemPromptBox = _settingStore.GetSettingOrNull(AbpSession.TenantId, AbpSession.UserId.Value, "SystemPromptBox").Value;
            return PartialView("_TopBar", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [DisableAuditing]
        public PartialViewResult Signalr()
        {
            var model = new SignalrModel();

            return PartialView("_Signalr", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult LanguageSelection()
        {
            var model = new LanguageSelectionViewModel
                        {
                            CurrentLanguage = _localizationManager.CurrentLanguage,
                            Languages = _localizationManager.GetAllLanguages()
                        };

            return PartialView("_LanguageSelection", model);
        }
    }
}