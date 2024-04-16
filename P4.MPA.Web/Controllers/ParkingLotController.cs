using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.Businesses;
using P4.Dictionarys;
using P4.MonthlyCars.Dtos;
using P4.ParkingLot;
using P4.ParkingLot.Dto;
using P4.Parks;
using P4.Parks.Dtos;
using P4.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    public class ParkingLotController : P4ControllerBase
    {
        #region Var
        private readonly IParkAppService _parkAppService;
        private readonly IRegionAppService _regionAppService;
        private readonly IDictionarysAppService _dictionarysApplicationService;
        private readonly IBusinessAppService _businessAppService;
        private readonly IParkingAppService _parkingAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parkAppService"></param>
        /// <param name="regionAppService"></param>
        /// <param name="dictionarysApplicationService"></param>
        public ParkingLotController(IParkAppService parkAppService, 
                                    IRegionAppService regionAppService, 
                                    IDictionarysAppService dictionarysApplicationService,
                                    IBusinessAppService businessAppService,
                                    IParkingAppService parkingAppService)
        {
            _parkAppService = parkAppService;
            _regionAppService = regionAppService;
            _dictionarysApplicationService = dictionarysApplicationService;
            _businessAppService = businessAppService;
            _parkingAppService = parkingAppService;
        }

        /// <summary>
        /// 骨架
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 路内停车场管理页
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("ParkManagement")]
        public ActionResult CurbParkingManagement()
        {
            return View();
        }


        /// <summary>
        /// 停车场列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetCurbPark(ParkInput input)
        {
            if (!string.IsNullOrWhiteSpace(input.filters))
                input.filters = input.filters.Replace("ParkName", "ParkName.temp");
            var temp = _parkAppService.GetCurbPark(input);
            return this.Json(temp);
        }

        /// <summary>
        /// 包月管理
        /// </summary>
        /// <returns></returns>
        public ActionResult MonthlyCarManage()
        {
            return View();
        }

        /// <summary>
        /// 停车场下拉菜单
        /// </summary>
        /// <returns></returns>
        public string GetAllParkNameNoAuthority()
        {
            StringBuilder strtemp = new StringBuilder("<select>");

            //strtemp.AppendFormat(option, "0", "不限停车场");

            foreach (var model in _parkAppService.GetBindParkingParks())
            {
                strtemp.AppendFormat(option, model.Id, model.ParkName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 获取包月列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMonthlyList(GetParkingMonthlyRentListInput input)
        {
            var temp = _parkingAppService.GetParkingMonthlyRentList(input);
            return Json(temp);
        }


        /// <summary>
        /// 处理包月车辆
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResult> ProcessMonthlyCar(ModifyMonthlyRentInput input)
        {
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case P4Consts.JqGridInsert:
                    info.Message = await Task.Run(() => _parkingAppService.CreateMonthlyRent(input));
                    break;
                case P4Consts.JqGridDelete:
                    info.Message = await Task.Run(() => _parkingAppService.DleteMonthlyRent(input));
                    break;
                case P4Consts.JqGridUpdate:
                    info.Message = await Task.Run(() => _parkingAppService.ModifyMonthlyRent(input));
                    break;
                default:
                    break;
            }
            return this.Json( string.IsNullOrEmpty(info.Message ) ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }


        /// <summary>
        /// 获取停车场通道列表
        /// </summary>
        /// <param name="parkId"></param>
        /// <returns></returns>
        public ActionResult GetParkChannel(int parkId)
        {
            return Json(_parkingAppService.GetParkChannel(parkId));
        }

        public ActionResult ModifyParkChannel(ModifyParkChannelInput input)
        {
            ErrorInfo info = new ErrorInfo();
            info.Message= _parkingAppService.ModifyParkChannel(input);
            return this.Json(string.IsNullOrEmpty(info.Message) ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }


        /// <summary>
        /// 通道管理页
        /// </summary>
        /// <returns></returns>
        public ActionResult ChannelManage()
        {
            return View();
        }


        /// <summary>
        /// 获取通道列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetChannelList(GetParkChannelInput input)
        {
            input.TenantId = AbpSession.TenantId;
            input.CompanyId = AbpSession.CompanyId;
            var temp = _parkAppService.GetAllChannel(input);
            return this.Json(temp);
        }

        /// <summary>
        /// 处理停车场事务
        /// </summary>
        /// <returns></returns>
        public JsonResult ProcessChannel(CreateOrUpdateParkChannelInput input)
        {
            ErrorInfo info = new ErrorInfo();
            input.CompanyId = AbpSession.CompanyId??0;
            input.TenantId = AbpSession.TenantId??0;
            switch (input.oper)
            {
                case "del":
                    {
                        _parkAppService.DeleteChannel(input.Id);
                        break;
                    }
                case "edit":
                    {
                        info.Message = _parkAppService. ModifyChannerl(input);
                        break;
                    }
                    
                case "add":
                    {
                        info.Message = _parkAppService.InsertChannel(input);
                        break;
                    }
                   
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }

        /// <summary>
        /// 停车记录
        /// </summary>
        /// <returns></returns>
        public ActionResult ParkRecordManage()
        {
            var parks = _parkAppService.GetBindParkingParks();
            var channels= _parkAppService.GetAllChannel(new GetParkChannelInput { CompanyId=AbpSession.CompanyId, TenantId=AbpSession.TenantId, page=1,rows=int.MaxValue,sidx= "Id", sord="desc" }); ;
            ViewBag.Parks = parks;
            ViewBag.Channel = channels.rows;
            return View();
        }


        /// <summary>
        /// 获取停车记录列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetParkRecordList(GetParkRecordListInput input)
        {
            return Json(_businessAppService.GetParkRecordList(input),JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取停车图片
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public JsonResult GetBusinessDetailsPictureList(Guid Id)
        {
            return this.Json(_businessAppService.GetParkingRecordPicList(Id), JsonRequestBehavior.AllowGet);
        }

    }
}