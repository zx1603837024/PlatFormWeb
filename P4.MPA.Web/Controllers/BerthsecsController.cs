using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.Berthsecs;
using P4.Berthsecs.Dto;
using P4.Parks;
using P4.Parks.Dtos;
using P4.Rates;
using P4.Rates.Dtos;
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
    public class BerthsecsController : P4ControllerBase
    {
        #region Var
        private readonly IBerthsecAppService _berthsecAppService;
        private readonly IParkAppService _parkAppService;
        private readonly IRegionAppService _RegionAppService;
        private readonly IRatesAppService _rateAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="berthsecAppService"></param>
        /// <param name="parkAppService"></param>
        /// <param name="RegionAppService"></param>
        /// <param name="rateAppService"></param>
        public BerthsecsController(IBerthsecAppService berthsecAppService, IParkAppService parkAppService, IRegionAppService RegionAppService, IRatesAppService rateAppService)
        {
            _berthsecAppService = berthsecAppService;
            _parkAppService = parkAppService;
            _RegionAppService = RegionAppService;
            _rateAppService = rateAppService;
        }
       
        
        // GET: Berthsecs
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("BerthsecManagement")]
        public ActionResult BerthsecManagement()
        {
            return View();
        }

        /// <summary>
        /// 费率下拉列表
        /// </summary>
        /// <returns></returns>
        public string GetAllRateName()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            strtemp.AppendFormat(option, "0", "");
            foreach (var model in _rateAppService.GetAllRateName())
            {
                strtemp.AppendFormat(option, model.Id, model.RateName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }
      
        /// <summary>
        /// 停车场下拉列表
        /// </summary>
        /// <returns></returns>
        public string GetAllParkName()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _parkAppService.GetParkAll())
            {
                if (AbpSession.ParkIds != null && AbpSession.ParkIds.Exists(e => e == model.Id))
                    strtemp.AppendFormat(option, model.Id, model.ParkName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public string GetParkNamebyRegionID(string regionId)
        {
            int id = string.IsNullOrEmpty(regionId) ? 0 : int.Parse(regionId);
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _parkAppService.GetParkByRegionID(id))
            {
                if (AbpSession.ParkIds != null && AbpSession.ParkIds.Exists(e => e == model.Id))
                    strtemp.AppendFormat(option, model.Id, model.ParkName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetAllRegionName()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _RegionAppService.GetAllRegion())
            {
                if (AbpSession.RegionIds != null && AbpSession.RegionIds.Exists(e => e == model.Id))
                    strtemp.AppendFormat(option, model.Id, model.RegionName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetAllRegionNameAddAll()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            strtemp.AppendFormat(option, "0", "不限路段");
            foreach (var model in _parkAppService.GetParkAll())
            {
                if (AbpSession.ParkIds != null && AbpSession.ParkIds.Exists(e => e == model.Id))
                    strtemp.AppendFormat(option, model.Id, model.ParkName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public JsonResult GetAllParkName1(string regionId)
        {
            int id = string.IsNullOrEmpty(regionId) ? 0 : int.Parse(regionId);
            return this.Json(_parkAppService.GetParkByRegionID(id));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetAllParkName2()
        {
            StringBuilder strtemp = new StringBuilder();
            foreach (var model in _RegionAppService.GetAllRegion())
            {
                strtemp.AppendFormat(option, model.Id, model.RegionName);
            }
            return strtemp.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parkId"></param>
        /// <returns></returns>
        public JsonResult GetBerthsecByParkId(string parkId)
        {
            int id = string.IsNullOrEmpty(parkId) ? 0 : int.Parse(parkId);
            return this.Json(_berthsecAppService.GetBerthsecByParkID(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="searchField"></param>
        /// <param name="searchOper"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public JsonResult GetBerthsecList(SearchBerthsecInput input, string searchField, string searchOper,
           string searchString)
        {
            if (!string.IsNullOrWhiteSpace(input.filters))
                input.filters = input.filters.Replace("BerthsecName", "BerthsecName.temp");
            return this.Json(_berthsecAppService.GetBerthsecList(input));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetBerthsecListSelect()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _berthsecAppService.GetBerthsecList().rows)
            {
                strtemp.AppendFormat(option, model.Id, model.BerthsecName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        public string GetBerthsList(int id)
        {
            return _berthsecAppService.GetBerthList(id);
        }

        public JsonResult SaveBerthsToBerthsec(string savetype, int chooseberthsecid, string nodes)
        {

            bool flag = _berthsecAppService.SaveBerthsToBerthsec(savetype, chooseberthsecid, nodes);
            if (flag)
            {
                return this.Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
            }
            else if (!flag)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("请签退该泊位段后再修改！")));
            }
            else
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("!!！")));
            }
        } 

        public JsonResult ProcessBerthsec(CreateOrUpdateBerthsecInput input)
        {
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case P4Consts.JqGridInsert:
                    info.Message = InsertBerthsec(input);
                    break;
                case P4Consts.JqGridDelete:
                    DeleteBerthsec(input);
                    break;
                case P4Consts.JqGridUpdate:
                    info.Message= ModifyBerthsec(input);
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }
        /// <summary>
        /// 批量签退泊位段
        /// </summary>
        /// <param name="berthsecId"></param>
        /// <returns></returns>
        public JsonResult BatchBerthsecCheckOut(List<int> berthsecId)
        {
            int count = _berthsecAppService.BerthsecBatchCheckOutBGO(berthsecId);
            if (count > 0)
            {
                return Json(new { a = "OK" });
            }
            else
            {
                return Json(new { a = "NO" });
            }
        }
        

        public string InsertBerthsec(CreateOrUpdateBerthsecInput input)
        {
            return _berthsecAppService.InsertBerthsec(input);
        }

        public string ModifyBerthsec(CreateOrUpdateBerthsecInput input)
        {
           return _berthsecAppService.ModifyBerthsec(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void DeleteBerthsec(CreateOrUpdateBerthsecInput input)
        {
            _berthsecAppService.DeleteBerthsec(input.Id);
        }

        /// <summary>
        /// 保存泊位段坐标
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public JsonResult SaveBerthsecAddress(BerthsecMapDto model)
        {
            return this.Json(_berthsecAppService.SaveBerthsecAddress(model));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetBerthsecById(int Id)
        {
            return this.Json(_berthsecAppService.GetBerthsecById(Id));
        }

        /// <summary>
        /// 泊位段地图
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetAllBerthsecGps(int Id)
        {
            return this.Json(_berthsecAppService.GetBerthsecMapList(Id), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 停车场下拉列表
        /// </summary>
        /// <returns></returns>
        public string GetAllParkName3()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            var parkList = _parkAppService.GetBindParkingParks();
            foreach (var model in parkList)
            {
               if (AbpSession.ParkIds != null && AbpSession.ParkIds.Exists(e => e == model.Id))
                  strtemp.AppendFormat(option, model.Id, model.ParkName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }
    }
}