using Abp.Domain.Repositories;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.Berths;
using P4.Berthsecs;
using P4.Dictionarys;
using P4.Parks;
using P4.Sensors;
using P4.Sensors.Dtos;
using P4.Tenants;
using P4.Transmitters;
using P4.Transmitters.Dtos;
using P4.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{

    /// <summary>
    /// 车检器
    /// </summary>
    [AbpMvcAuthorize]
    public class InspectionDeviceController : P4ControllerBase
    {
        #region Var

        private readonly IBerthsecAppService _berthsecAppService;
        private readonly ISensorAppService _sensorAppService;
        private readonly ITransmitterAppService _transmitterAppService;
        private readonly IDictionarysAppService _dictionarysAppService;
        private readonly ITenantAppService _tenantAppService;
        private readonly IParkAppService _parkAppService;
        private readonly IBerthAppService _berthAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="berthsecAppService"></param>
        /// <param name="sensorAppService"></param>
        /// <param name="transmitterAppService"></param>
        /// <param name="dictionarysAppService"></param>
        /// <param name="tenantAppService"></param>
        /// <param name="parkAppService"></param>
        /// <param name="berthAppService"></param>
        public InspectionDeviceController(IBerthsecAppService berthsecAppService, ISensorAppService sensorAppService,
            ITransmitterAppService transmitterAppService, IDictionarysAppService dictionarysAppService, ITenantAppService tenantAppService, IParkAppService parkAppService, IBerthAppService berthAppService)
        {
            _berthsecAppService = berthsecAppService;
            _sensorAppService = sensorAppService;
            _transmitterAppService = transmitterAppService;
            _dictionarysAppService = dictionarysAppService;
            _tenantAppService = tenantAppService;
            _parkAppService = parkAppService;
            _berthAppService = berthAppService;
        }

        /// <summary>
        /// 车检器管理
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("InspectionSetting")]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 车检器信息
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("SensorInfo")]
        public ActionResult SensorInfo()
        {
            BusinessModel model = new BusinessModel();
            model.parkList = _parkAppService.GetParkAll();
            model.AllOption = alloption;
            return View(model);
        }

        public ActionResult ParkInfoMap()
        {
            return View();
        }
        public JsonResult GetManageTransmitterList(TransmitterInput input)
        {
            return this.Json(_transmitterAppService.GetAllManageTransmitter(input));

        }

        public JsonResult GetSensorsList(SearchSensorsInput input)
        {
            return this.Json(_sensorAppService.GetAllSensorsList(input));

        }

        public JsonResult GetSensorsInfoOnlineOrNot(SearchSensorsInput input)
        {
            input.TenantId = AbpSession.TenantId;//商户
            input.CompanyId = AbpSession.CompanyId;//分公司
            return this.Json(_sensorAppService.GetSensorInfoOnlineOrNot(input));
        }
        /// <summary>
        /// 停车场 车检器在线率
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetSensorsRatio(SearchSensorsInput input)
        {
            input.TenantId = AbpSession.TenantId;
            input.CompanyId = AbpSession.CompanyId;
            return this.Json(_sensorAppService.GetSensorPRatio(input),JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 车检器监控
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public ActionResult Monitor()
        {
            SensorModel sensorModel = new SensorModel();
            sensorModel.berthsecList = _berthsecAppService.GetAllBerthsec();
            if (sensorModel.berthsecList.Count > 0)
                sensorModel.sensorList = _sensorAppService.GetSensorByBerthsecId(sensorModel.berthsecList[0].Id);
            else
                sensorModel.sensorList = new List<SensorDto>();
            return View(sensorModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="berthsecid"></param>
        /// <returns></returns>
        public JsonResult GetSensorByBerthsecId(string berthsecid)
        {
            //return this.Json(_berthAppService.GetBerthList(int.Parse(berthsecid)));
            return this.Json(_sensorAppService.GetSensorByBerthsecId(int.Parse(berthsecid)));
        }
        /// <summary>
        /// 基站监控
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("TransmitterMonitor")]
        public ActionResult TransmitterMonitor()
        {
            return View();
        }
        /// <summary>
        /// 管理基站
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("ManageTransmitter")]
        public ActionResult ManageTransmitter()
        {
            return View();
        }

        public string GetAllTenantName()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _tenantAppService.GetAllTenantName())
            {

                strtemp.AppendFormat(option, model.Id, model.Name);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }
        /// <summary>
        /// 基站地图
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>

        public ActionResult TransmitterMap()
        {
            return View();
        }
        /// <summary>
        /// 基站监控列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult GetAllTransmitter(TransmitterInput entity)
        {
            return this.Json(_transmitterAppService.GetAllTransmitter(entity));
        }

        public JsonResult GetTransmitterById(string id)
        {
            return this.Json(_transmitterAppService.GetTransmitterById(int.Parse(id)));
        }
        public JsonResult GetAllTransmitterList()
        {
            return this.Json(_transmitterAppService.GetAllTransmitterList());
        }
        public JsonResult GetAllParkInfoMap()
        {
            return this.Json(_parkAppService.GetParkInfoMapList());
        }
        
        /// <summary>
        /// 处理区域数据
        /// </summary>
        /// <returns></returns>
        public JsonResult ProcessTransmitters(CreateOrUpdateTransmitterInput input)
        {
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case "del":
                    DeleteTransmitter(input);
                    break;
                case "add":
                    info.Message = InsertTransmitter(input);
                    break;
                case "edit":
                    info.Message = ModifyTransmitter(input);
                    break;
                default:
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }



        [AbpMvcAuthorize("TransmitterMonitor.Insert")]
        public string InsertTransmitter(CreateOrUpdateTransmitterInput input)
        {
            return _transmitterAppService.Insert(input);

        }

        [AbpMvcAuthorize("RegionManagement.Delete")]
        public JsonResult DeleteTransmitter(CreateOrUpdateTransmitterInput input)
        {
            _transmitterAppService.Delete(input.Id);
            return this.Json("");
        }

        [AbpMvcAuthorize("TransmitterMonitor.Update")]
        public string ModifyTransmitter(CreateOrUpdateTransmitterInput input)
        {
            return _transmitterAppService.Modify(input);

        }
        /// <summary>
        /// 获取商户的XY坐标
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTenantX_Y()
        {
            int tenantid =Convert.ToInt32(AbpSession.TenantId);
            
            return this.Json(_tenantAppService.FirstOrDefault(tenantid));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult ProcessManageTransmitters(CreateOrUpdateTransmitterInput input)
        {
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {

                case "edit":
                    info.Message = ModifyManageTransmitter(input);
                    break;
                default:
                    break;

            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }

        [AbpMvcAuthorize("ManageTransmitter.Update")]
        public string ModifyManageTransmitter(CreateOrUpdateTransmitterInput input)
        {
            return _transmitterAppService.ModifyManageTransmitter(input);

        }

    }
}