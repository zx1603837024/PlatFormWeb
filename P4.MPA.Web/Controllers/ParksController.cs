using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.Dictionarys;
using P4.Parks;
using P4.Parks.Dtos;
using P4.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [AbpMvcAuthorize]
    public class ParksController : P4ControllerBase
    {
        #region Var
        private readonly IParkAppService _parkAppService;
        private readonly IRegionAppService _regionAppService;
        private readonly IDictionarysAppService _dictionarysApplicationService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parkAppService"></param>
        /// <param name="regionAppService"></param>
        /// <param name="dictionarysApplicationService"></param>
        public ParksController(IParkAppService parkAppService, IRegionAppService regionAppService, IDictionarysAppService dictionarysApplicationService)
        {
            _parkAppService = parkAppService;
            _regionAppService = regionAppService;
            _dictionarysApplicationService = dictionarysApplicationService;
        }


        // GET: Parks
        [AbpMvcAuthorize("ParkManagement")]
        public ActionResult ParkManagement()
        {
            return View();
        }

        /// <summary>
        /// 停车场列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetParkAll(ParkInput input)
        {
            if (!string.IsNullOrWhiteSpace(input.filters))
                input.filters = input.filters.Replace("ParkName", "ParkName.temp");
            var temp = _parkAppService.GetParkAll(input);
            return this.Json(temp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetRegionCombox()
        {
            /*StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _regionAppService.GetAllRegion())
            {
                if (AbpSession.RegionIds.Exists(e => e == model.Id))
                    strtemp.AppendFormat(option, model.Id, model.RegionName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();*/
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _regionAppService.GetAllRegion())
            {
                strtemp.AppendFormat(option, model.Id, model.RegionName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        public string GetParkTypeList()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _dictionarysApplicationService.GetAllValueData("A10013"))
            {
                strtemp.AppendFormat(option, model.ValueCode, model.ValueData);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 处理停车场事务
        /// </summary>
        /// <returns></returns>
        public JsonResult ProcessPark(CreateOrUpdateParkInput input)
        {
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case "del":
                    DetelePark(input);
                    break;
                case "edit":
                    info.Message = ModifyPark(input);
                    break;
                case "add":
                    info.Message = InsertPark(input);
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }


        public void DetelePark(CreateOrUpdateParkInput input)
        {
            _parkAppService.Delete(input.Id);
        }

        public string InsertPark(CreateOrUpdateParkInput input)
        {
            return _parkAppService.Insert(input);
        }

        public string ModifyPark(CreateOrUpdateParkInput input)
        {
            return _parkAppService.Modify(input);
        }

        /// <summary>
        /// 停车场地图
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("ParkManagementMap")]
        public ActionResult ParkManagementMap()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetParkNumbersList()
        {
            return this.Json(_parkAppService.GetParkNumber());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetParkInfoById(int Id)
        {
            return this.Json(_parkAppService.GetParkInfoById(Id));
        }

        /// <summary>
        /// 保存停车场坐标
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public JsonResult SaveParkAddress(ParkInfoMap map)
        {
            return this.Json(_parkAppService.SaveParkAddress(map));
        }

    }
}