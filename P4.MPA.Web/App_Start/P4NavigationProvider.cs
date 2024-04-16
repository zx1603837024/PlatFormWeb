using Abp.Application.Navigation;
using Abp.Localization;
using P4.Menus;
using System.Collections.Generic;

namespace P4.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class P4NavigationProvider : NavigationProvider
    {
        #region Var
        private readonly IMenuAppService _menuApplicationService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuApplicationService"></param>
        public P4NavigationProvider(IMenuAppService menuApplicationService)
        {
            _menuApplicationService = menuApplicationService;
        }


        /// <summary>
        /// 公共功能权限读取
        /// </summary>
        /// <param name="context"></param>
        public override void SetNavigation(INavigationProviderContext context)
        {
            Menu menu = new Menu() { 
                IsFunction = true,
                IsStatic = true,
                IsActive = true
            };
            //var menus = _menuApplicationService.GetMenus(menu);
            var menus = _menuApplicationService.GetMenus();
            context.Manager.MainMenu.Items = ProcessMenuStructure("0", menus);
        }

        /// <summary>
        /// 处理菜单结构（父子关系）
        /// </summary>
        /// <returns></returns>
        private IList<MenuItemDefinition> ProcessMenuStructure(string name, List<Menu> menus)
        {
            IList<MenuItemDefinition> menulist = new List<MenuItemDefinition>();
            foreach (var model in menus.FindAll(menu => menu.FatherCode == name))
            {
                MenuItemDefinition menu = new MenuItemDefinition(
                       model.Name,
                       new LocalizableString(model.RequiredPermissionName, P4Consts.LocalizationSourceName),
                       url: model.Url,
                       icon: model.Icon,
                       requiredPermissionName: model.RequiredPermissionName,
                       discriminator: model.Discriminator,
                       menuType: model.MenuType
                       );
                menu.Items = ProcessMenuStructure(model.RequiredPermissionName, menus);
                menulist.Add(menu);
            }
            return menulist;
        }
    }
}
