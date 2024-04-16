using Abp.Web.Mvc.Authorization;
using P4.Sensors;
using P4.Sensors.Dtos;
using P4.Transmitters;
using P4.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 车检器故障控制类
    /// </summary>
    [AbpMvcAuthorize]
    public class SensorsController : P4ControllerBase
    {
        #region Var
        private readonly ISensorAppService _sensorAppService;
        private readonly ITransmitterAppService _transmitterAppService;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sensorAppService"></param>
        public SensorsController(ISensorAppService sensorAppService, ITransmitterAppService transmitterAppService)
        {
            _sensorAppService = sensorAppService;
            _transmitterAppService = transmitterAppService;
        }

        /// <summary>
        ///  车检器在线率图表
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("SensorGraph")]
        public ActionResult SensorGraph()
        {
            return View();
        }

        /// <summary>
        /// 基站通讯日志
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("BTSCommLog")]
        public ActionResult SensorLog()
        {
            return View();
        }

        /// <summary>
        /// 获取基站通讯日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
         [AbpMvcAuthorize("BTSCommLog")]
        public ActionResult GetSensorLogList(SearchSensorLogInput input)
        {
            return this.Json(_sensorAppService.GetSensorLogsList(input));
        }


        /// <summary>
        /// 通过基站编号获取车检器列表
        /// </summary>
        /// <param name="TransmitterId"></param>
        /// <returns></returns>
        public JsonResult GetSensorByTransmitterId(int Id)
        {
            var entity = _transmitterAppService.GetTransmitterById(Id);
            SearchSensorsInput input = new SearchSensorsInput();
            input.filters = "{\"groupOp\":\"AND\",\"rules\":[{\"field\":\"TransmitterNumber\",\"op\":\"eq\",\"data\":\"" + entity.TransmitterNumber + "\"}]}";
            input.page = 1;
            input.rows = 50;
            return this.Json(_sensorAppService.GetAllSensorsList(input));
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <returns></returns>
        public JsonResult SensorBeatEchatData(SearchSensorBeatInput input)
        {
            if (input.OperateDateBegin == DateTime.Parse("0001/1/1 0:00:00"))
            {
                input.OperateDateBegin = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
                input.OperateDateEnd = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            }

            input.OperateDateEnd = input.OperateDateEnd.AddDays(1);
            EcharModel echatModel = EchatInit();

            GetAllSensorsBeatOutput sensorBeatList = _sensorAppService.GetSensorBeatList(input);

            foreach (var temp in sensorBeatList.rows)
            {
                echatModel.categoryList.Add(temp.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"));
                echatModel.seriesObj.data.Add(decimal.Parse(
                    (
                    (decimal)(temp.SensorCount - temp.FaultCount) * 100 
                    /
                    (decimal)temp.SensorCount).ToString("F2")));
            }
            return EchatReturn(echatModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetSensorRunStatusList(SearchSensorRunStatusInput input)
        {
            return this.Json(_sensorAppService.GetSensorRunStatusList(input), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="echarModel"></param>
        /// <returns></returns>
        public JsonResult EchatReturn(EcharModel echarModel)
        { //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类
            List<Series> seriesList = new List<Series>();
            seriesList.Add(echarModel.seriesObj);
           
            var newObj = new
            {
                category = echarModel.categoryList,
                series = seriesList,
                legend = echarModel.legendList
            };
            return this.Json(newObj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetSensorFaultList(SearchSensorFaultInput input)
        {
            if (_sensorAppService.GetFaultSensorList(input) == default(GetAllSensorsFaultOutput))
                return this.Json("");
            return this.Json(_sensorAppService.GetFaultSensorList(input));
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <returns></returns>
        public EcharModel EchatInit()
        {
            List<string> categoryList = new List<string>();
            List<string> legendList = new List<string>();

            legendList.Add("在线率");

            Series seriesObj = new Series();

            seriesObj.name = "在线率";
            seriesObj.type = "line"; //线性图呈现
            seriesObj.data = new List<decimal>();

            EcharModel echarModel = new EcharModel();
            echarModel.categoryList = categoryList;
            echarModel.legendList = legendList;
            echarModel.seriesObj = seriesObj;

            return echarModel;
        }
    }
}