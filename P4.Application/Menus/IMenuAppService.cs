using Abp.Application.Services;
using Abp.Application.Services.Dto;
using P4.Menus.Dto;
using System.Collections.Generic;

namespace P4.Menus
{
    /// <summary>
    /// 菜单
    /// </summary>
    public interface IMenuAppService : IApplicationService
    {
        ListResultOutput<MenuDto> GetMenus(GetMenuInput input);

        /// <summary>
        /// 获取系统菜单
        /// </summary>
        /// <returns></returns>
        List<Menu> GetMenus();

        int Update(MenuDto entity);

        int Insert(MenuDto entity);

        int Delete(int Id);

        /// <summary>
        /// 获取静态权限
        /// </summary>
        /// <returns></returns>
        List<Menu> GetMenus(Menu entity);

        /// <summary>
        /// 获取树形菜单
        /// </summary>
        /// <returns></returns>
        TreeMenusOutput GetTreeMenus();
    }
}
