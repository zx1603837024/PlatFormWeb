using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.Collectors;
using P4.Employees;
using P4.Regions;
using P4.Regions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 区域
    /// </summary>
    [AbpMvcAuthorize]
    public class RegionsController : P4ControllerBase
    {
        #region Var
        private readonly IRegionAppService _regionAppService;
        private readonly ICollectorAppService _collectorAppService;
        #endregion


        public RegionsController(IRegionAppService regionAppService, ICollectorAppService collectorAppService)
        {
            _regionAppService = regionAppService;
            _collectorAppService = collectorAppService;
        }

        [AbpMvcAuthorize("RegionManagement")]
        public ActionResult Region()
        {
            return View();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("RegionManagement.Search")]
        public JsonResult GetAllRegionsList(GetAllRegionsInput input)
        {
            if (!string.IsNullOrWhiteSpace(input.filters))
                input.filters = input.filters.Replace("RegionName", "RegionName.temp");
            return this.Json(_regionAppService.GetAllRegionsList(input));
        }

        /// <summary>
        /// 处理区域数据
        /// </summary>
        /// <returns></returns>
        public JsonResult ProcessRegions(CreateOrUpdateDtoInput input)
        {
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            { 
                case "del":
                    DeleteRegion(input);
                    break;
                case "add":
                    info.Message = InsertRegion(input);
                    break;
                case "edit":
                    info.Message = ModifyRegion(input);
                    break;
                default:
                    break;

            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }

        [AbpMvcAuthorize("RegionManagement.Insert")]
        public string InsertRegion(CreateOrUpdateDtoInput input)
        {
           return _regionAppService.Insert(input);
         
        }

        [AbpMvcAuthorize("RegionManagement.Delete")]
        public JsonResult DeleteRegion(CreateOrUpdateDtoInput input)
        {
            _regionAppService.Delete(input.Id);
            return this.Json("");
        }

        [AbpMvcAuthorize("RegionManagement.Update")]
        public string ModifyRegion(CreateOrUpdateDtoInput input)
        {
          return  _regionAppService.Modify(input);
         
        }
    }
}