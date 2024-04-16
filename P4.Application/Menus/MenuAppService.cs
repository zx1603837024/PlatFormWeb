using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using P4.Menus.Dto;
using Abp.Domain.Uow;
using Abp.Authorization;
using Abp.Runtime.Caching;
using Abp.Configuration;
using P4.Common;

namespace P4.Menus
{
    public class MenuAppService : IMenuAppService
    {
        #region Var
        private readonly IRepository<Menu> _abpMenuRepository;
        private readonly ICacheManager _cacheManager;
        #endregion
        public MenuAppService(IRepository<Menu> abpMenuRepository, ICacheManager cacheManager)
        {
            _abpMenuRepository = abpMenuRepository;
            _cacheManager = cacheManager;
        }
        public Abp.Application.Services.Dto.ListResultOutput<Dto.MenuDto> GetMenus(Dto.GetMenuInput input)
        {
            return new Abp.Application.Services.Dto.ListResultOutput<Dto.MenuDto>
            {
                Items = _abpMenuRepository.GetAllList().Where(menu => menu.IsActive == true).MapTo<List<MenuDto>>().OrderBy(menu => menu.Order).ToList()
            };
        }

        /// <summary>
        /// 获取系统菜单
        /// 只过滤活动菜单
        /// </summary>
        /// <returns></returns>
        public List<Menu> GetMenus()
        {
            var model = _cacheManager.GetCache(SettingManager.ApplicationSettingsCacheName).Get(SettingManager.ApplicationSettingsMenu, () => GetMenuFromDatabase()) as List<Menu>;
            return _cacheManager.GetCache(SettingManager.ApplicationSettingsCacheName).Get(SettingManager.ApplicationSettingsMenu, () => GetMenuFromDatabase()) as List<Menu>;
        }

        private List<Menu> GetMenuFromDatabase()
        {
            var model = _abpMenuRepository.GetAllList().Where(menu => menu.IsActive == true).OrderBy(menu => menu.FatherCode).OrderBy(menu => menu.Order).ToList();
            return _abpMenuRepository.GetAllList().Where(menu => menu.IsActive == true).OrderBy(menu => menu.FatherCode).OrderBy(menu => menu.Order).ToList();
        }





        public int Update(MenuDto entity)
        {
            var model = _abpMenuRepository.Get(entity.Id);
            model.Level = entity.Level;
            return _abpMenuRepository.Update(model) == null ? 0 : 1;
        }

        public int Insert(MenuDto entity)
        {
            return _abpMenuRepository.Insert(entity.MapTo<Menu>()) == null ? 0 : 1;
        }

        public int Delete(int Id)
        {
             _abpMenuRepository.Delete(Id);
             return 1;
        }

        /// <summary>
        /// 获取静态权限
        /// </summary>
        /// <returns></returns>
        public  List<Menu> GetMenus(Menu entity)
        {
            return _abpMenuRepository.GetAllList().Where(menu => menu.IsActive == entity.IsActive && menu.IsStatic == entity.IsStatic && menu.IsFunction == entity.IsFunction).OrderBy(menu => menu.FatherCode).OrderBy(menu => menu.Order).ToList();
        }


        public TreeMenusOutput GetTreeMenus()
        {
            return new TreeMenusOutput() { 
                rows = GetMenus().MapTo<List<MenuTreeDto>>()
            };
        }
    }
}
