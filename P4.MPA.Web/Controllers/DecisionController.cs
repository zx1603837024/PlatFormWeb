using Abp.Helper;
using Abp.Web.Mvc.Authorization;
using P4.Berthsecs;
using P4.Businesses;
using P4.DecisionAnalysis;
using P4.DecisionAnalysis.Dtos;
using P4.MonthlyCars;
using P4.Parks;
using P4.Rates;
using P4.Sensors;
using P4.Sensors.Dtos;
using P4.Web.Models;
using P4.Weixin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 决策分析
    /// </summary>
    [AbpMvcAuthorize]
    public class DecisionController : P4ControllerBase
    {

        #region Var
        private readonly IPeakPeriodAppService _peakPeriodAppService;
        private readonly IBerthsecAppService _berthsecAppService;
        private readonly IParkAppService _parkAppService;
        private readonly ISensorAppService _sensorAppService;
        private readonly IMonthlyCarAppService _monthlyCarAppService;
        private readonly IWeixinAppService _weixinAppService;
        private readonly IBusinessAppService _businessAppService;
        private readonly IRatesAppService _ratesAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="peakPeriodAppService"></param>
        /// <param name="berthsecAppService"></param>
        /// <param name="parkAppService"></param>
        /// <param name="sensorAppService"></param>
        /// <param name="monthlyCarAppService"></param>
        /// <param name="weixinAppService"></param>
        /// <param name="businessAppService"></param>
        public DecisionController(IPeakPeriodAppService peakPeriodAppService, IBerthsecAppService berthsecAppService, IParkAppService parkAppService, ISensorAppService sensorAppService, IMonthlyCarAppService monthlyCarAppService, IWeixinAppService weixinAppService, IBusinessAppService businessAppService, IRatesAppService ratesAppService)
        {
            _peakPeriodAppService = peakPeriodAppService;
            _berthsecAppService = berthsecAppService;
            _parkAppService = parkAppService;
            _sensorAppService = sensorAppService;
            _monthlyCarAppService = monthlyCarAppService;
            _weixinAppService = weixinAppService;
            _businessAppService = businessAppService;
            _ratesAppService = ratesAppService;
        }


        /// <summary>
        /// 收费决策
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("FeesDecision")]
        public ActionResult FeesDecision()
        {
            UtilizationModel model = new UtilizationModel();
            //model.berthsecList = _berthsecAppService.GetAllBerthsec();
            model.parkList = _parkAppService.GetParkAll();
            model.AllOption = alloption;
            return View(model);
        }

        /// <summary>
        /// 运维决策
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("DevOpsDecision")]
        public ActionResult DevOpsDecision()
        {
            DecisionModel model = new DecisionModel();

            var entity = _monthlyCarAppService.GetDecisionModel(DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00")), DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59")));
            var entry = _weixinAppService.GetDecisionWeixinModel(DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00")), DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59")));

            var businessentity = _businessAppService.GetBusinessDecisionDto(DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00")), DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59")));
            model.PeakPeriodList = _peakPeriodAppService.GetTopPeakPeriodList(5, false);
            model.AllMonthyCount = entity.AllMonthyCount;
            model.AllMonthyMoney = entity.AllMonthyMoney;
            model.NewMonthyCount = entity.NewMonthyCount;
            model.NewMonthyMoney = entity.NewMonthyMoney;
            model.WeixinIdeaCount = entry.WeixinIdeaCount;
            model.WeixinNewRegisterCount = entry.WeixinNewRegisterCount;
            model.WeixinRegisterCount = entry.WeixinRegisterCount;
            model.WeixinRelyIdeaCount = entry.WeixinRelyIdeaCount;

            model.AllMoney = businessentity.AllMoney;
            model.AllArrearage = businessentity.AllArrearage;
            model.AllFactReceive = businessentity.AllFactReceive;
            model.AllRepayment = businessentity.AllRepayment;
            model.AllStopTime = businessentity.AllStopTime;
            model.AllStopTimes = businessentity.AllStopTimes;
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("DevOpsDecision")]
        public JsonResult GetDecisionModel(DateTime BeginTime, DateTime EndTime)
        {
            DecisionModel model = new DecisionModel();
            var entity = _monthlyCarAppService.GetDecisionModel(BeginTime, EndTime);
            var entry = _weixinAppService.GetDecisionWeixinModel(BeginTime, EndTime);
            var businessentity = _businessAppService.GetBusinessDecisionDto(BeginTime, EndTime);
            model.PeakPeriodList = _peakPeriodAppService.GetTopPeakPeriodList(5, false);
            model.AllMonthyCount = entity.AllMonthyCount;
            model.AllMonthyMoney = entity.AllMonthyMoney;
            model.NewMonthyCount = entity.NewMonthyCount;
            model.NewMonthyMoney = entity.NewMonthyMoney;
            model.WeixinIdeaCount = entry.WeixinIdeaCount;
            model.WeixinNewRegisterCount = entry.WeixinNewRegisterCount;
            model.WeixinRegisterCount = entry.WeixinRegisterCount;
            model.WeixinRelyIdeaCount = entry.WeixinRelyIdeaCount;

            model.AllMoney = businessentity.AllMoney;
            model.AllArrearage = businessentity.AllArrearage;
            model.AllFactReceive = businessentity.AllFactReceive;
            model.AllRepayment = businessentity.AllRepayment;
            model.AllStopTime = businessentity.AllStopTime;
            model.AllStopTimes = businessentity.AllStopTimes;
            return Json(model);
        }

        [AbpMvcAuthorize("PeakPeriod")]
        public ActionResult PeakPeriod()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetPeakPeriodList(SearchPeakPriodInput input)
        {
            return this.Json(_peakPeriodAppService.GetAllPeakPeriodList(input));
        }


        [AbpMvcAuthorize("UtilizationHour")]
        public ActionResult UtilizationHour()
        {
            UtilizationModel model = new UtilizationModel();
            model.berthsecList = _berthsecAppService.GetAllBerthsec();
            model.AllOption = alloption;
            return View(model);
        }

        /// <summary>
        /// 新添加曲线图
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult UtilizationHourEcharData(SearchUtilizationInput input)
        {
            //为了演示效果 这里采用随机数据来代替 后期可以根据自己情况换成访问数据获取数据
            //考虑到图表的category(种类)是字符串数组 这里定义一个string的List
            List<string> categoryList = new List<string>();
            //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类
            List<Series> seriesList = new List<Series>();
            //考虑到Echarts图表需要设置legend内的data数组为series的name集合这里需要定义一个legend数组      
            var beginTime = input.begintime;
            var endTime = input.endtime;
            List<string> legendList = new List<string>();
            if (beginTime != null)
            {
                for (var time = beginTime; time <= endTime; time = time.Value.AddDays(1))
                {
                    // legendList.Add(time.ToString().Substring(0, 9));
                    legendList.Add(time.ToString().Substring(0, 9) + "利用率");
                    legendList.Add(time.ToString().Substring(0, 9) + "周转次数");
                }
            }
            List<Series> series = new List<Series>();
            for (int i = 0; i < legendList.Count; i++)
            {
                //定义一个Series对象
                Series seriesObj = new Series();
                seriesObj.id = i;
                seriesObj.name = legendList[i];
                seriesObj.type = "line"; //线性图呈现
                seriesObj.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错
                series.Add(seriesObj);
            }

            GetUtilizationHourListOutput utilizationHourDtos = _peakPeriodAppService.GetUtilizationHourList(input);
            for (int a = 0; a < legendList.Count; a++)
            {
                #region 初始化
                if (utilizationHourDtos.rows[a].Utilization0 == "") utilizationHourDtos.rows[a].Utilization0 = "0";
                if (utilizationHourDtos.rows[a].Utilization1 == "") utilizationHourDtos.rows[a].Utilization1 = "0";
                if (utilizationHourDtos.rows[a].Utilization2 == "") utilizationHourDtos.rows[a].Utilization2 = "0";
                if (utilizationHourDtos.rows[a].Utilization3 == "") utilizationHourDtos.rows[a].Utilization3 = "0";
                if (utilizationHourDtos.rows[a].Utilization4 == "") utilizationHourDtos.rows[a].Utilization4 = "0";
                if (utilizationHourDtos.rows[a].Utilization5 == "") utilizationHourDtos.rows[a].Utilization5 = "0";
                if (utilizationHourDtos.rows[a].Utilization6 == "") utilizationHourDtos.rows[a].Utilization6 = "0";
                if (utilizationHourDtos.rows[a].Utilization7 == "") utilizationHourDtos.rows[a].Utilization7 = "0";
                if (utilizationHourDtos.rows[a].Utilization8 == "") utilizationHourDtos.rows[a].Utilization8 = "0";
                if (utilizationHourDtos.rows[a].Utilization9 == "") utilizationHourDtos.rows[a].Utilization9 = "0";
                if (utilizationHourDtos.rows[a].Utilization10 == "") utilizationHourDtos.rows[a].Utilization10 = "0";
                if (utilizationHourDtos.rows[a].Utilization11 == "") utilizationHourDtos.rows[a].Utilization11 = "0";
                if (utilizationHourDtos.rows[a].Utilization12 == "") utilizationHourDtos.rows[a].Utilization12 = "0";
                if (utilizationHourDtos.rows[a].Utilization13 == "") utilizationHourDtos.rows[a].Utilization13 = "0";
                if (utilizationHourDtos.rows[a].Utilization14 == "") utilizationHourDtos.rows[a].Utilization14 = "0";
                if (utilizationHourDtos.rows[a].Utilization15 == "") utilizationHourDtos.rows[a].Utilization15 = "0";
                if (utilizationHourDtos.rows[a].Utilization16 == "") utilizationHourDtos.rows[a].Utilization16 = "0";
                if (utilizationHourDtos.rows[a].Utilization17 == "") utilizationHourDtos.rows[a].Utilization17 = "0";
                if (utilizationHourDtos.rows[a].Utilization18 == "") utilizationHourDtos.rows[a].Utilization18 = "0";
                if (utilizationHourDtos.rows[a].Utilization19 == "") utilizationHourDtos.rows[a].Utilization19 = "0";
                if (utilizationHourDtos.rows[a].Utilization20 == "") utilizationHourDtos.rows[a].Utilization20 = "0";
                if (utilizationHourDtos.rows[a].Utilization21 == "") utilizationHourDtos.rows[a].Utilization21 = "0";
                if (utilizationHourDtos.rows[a].Utilization22 == "") utilizationHourDtos.rows[a].Utilization22 = "0";
                if (utilizationHourDtos.rows[a].Utilization23 == "") utilizationHourDtos.rows[a].Utilization23 = "0";
                #endregion

                #region 赋值
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization0));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization1));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization2));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization3));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization4));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization5));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization6));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization7));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization8));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization9));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization10));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization11));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization12));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization13));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization14));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization15));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization16));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization17));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization18));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization19));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization20));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization21));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization22));
                series[a].data.Add(Convert.ToDecimal(utilizationHourDtos.rows[a].Utilization23));
                #endregion
            }

            //最后调用相关函数将List转换为Json
            //因为我们需要返回category和series、legend多个对象 这里我们自己在new一个新的对象来封装这两个对象
            var newObj = new
            {
                series,
                legend = legendList
            };


            return this.Json(newObj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetBerthsecInfoList(SearchBerthsecAnalysisResultInput input)
        {
            return this.Json(_peakPeriodAppService.GetBerthsecAnalysisResultList(input));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetUtilizationHourList(SearchUtilizationInput input)
        {
            var model = _peakPeriodAppService.GetUtilizationHourList(input);
            for (int i = 0; i < model.rows.Count; i++)
                model.rows[i].berthsecid = i;
            return this.Json(model);
          //  return this.Json(_peakPeriodAppService.GetUtilizationHourList(input));

            
        }

        /// <summary>
        /// 地磁POS对照
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("SensorPOSContrast")]
        public ActionResult SensorPOSContrast()
        {
            return View();
        }

        /// <summary>
        /// 地磁pos对照表
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("SensorPOSContrast")]
        public JsonResult SensorPOSContrastList(SearchSensorPosInput input)
        {
            GetSensorPosOutPut entity = _sensorAppService.GetSensorPosList(input);
            //地磁重复时间错误问题修改20220808
            try
            {
                List<String> IdListA = new List<String>();
                List<String> IdList = new List<String>();
                foreach (SearchSensorPosDto element in entity.rows) {
                    if (!IdListA.Contains(element.guid))
                    {
                        IdListA.Add(element.guid);
                    }
                    else
                    {
                        IdList.Add(element.guid);
                    }
                }
                foreach (SearchSensorPosDto element in entity.rows)
                {
                    if (IdList.Contains(element.guid))
                    {
                        var SensorStopTime = 0;
                        decimal Receivable = 0;
                        var StrSensorCarOutTime = "";
                        DateTime SensorCarInTime = new DateTime();
                        DateTime SensorCarOutTime = new DateTime();
                        if (element.SensorCarInTime != null && element.SensorCarInTime != "" && element.SensorCarInTime != "null")
                        {
                            SensorCarInTime = Convert.ToDateTime(element.SensorCarInTime.Substring(0, 19));
                        }
                        if (element.SensorCarOutTime != null && element.SensorCarOutTime != "" && element.SensorCarOutTime != "null")
                        {
                            SensorCarOutTime = Convert.ToDateTime(element.SensorCarOutTime.Substring(0, 19));
                        }
                        if (SensorCarInTime.Year > 1 && SensorCarOutTime.Year > 1)
                        {
                            StrSensorCarOutTime = element.SensorCarOutTime;
                            SensorStopTime = (int)((SensorCarOutTime - SensorCarInTime).TotalSeconds) / 60;
                        }
                        element.SensorCarOutTime = StrSensorCarOutTime;
                        element.SensorStopTime = SensorStopTime;
                        if (SensorStopTime>0) {
                            //地磁重新计算费用
                            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select CarInTime, BerthsecId, PlateNumber, ParkId, CompanyId from AbpSensorBusinessDetail with(nolock) where guid = '" + element.guid + "'").Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                var model = _ratesAppService.RateCalculate(int.Parse(dt.Rows[0]["BerthsecId"].ToString()), DateTime.Parse(element.SensorCarInTime), DateTime.Parse(element.SensorCarOutTime), 2, 0, int.Parse(dt.Rows[0]["ParkId"].ToString()), dt.Rows[0]["PlateNumber"].ToString(), int.Parse(dt.Rows[0]["CompanyId"].ToString()));
                                Receivable = model.CalculateMoney;
                            }
                        }
                        element.SensorMoney = Receivable;
                    }
                }
                return Json(entity);
            }
            catch (Exception e)
            {
                return Json(entity);
            }
        }
    }
}