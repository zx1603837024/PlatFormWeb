using Abp.Web.Mvc.Authorization;
using P4.Web.Models.Layout;
using P4.Businesses;
using System.Web.Mvc;
using P4.Sensors.Dtos;
using P4.Sensors;
using P4.BigScreen;
using P4.Web.Models;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 大屏展示
    /// </summary>
    [AbpMvcAuthorize]
    public class BigScreenController : P4ControllerBase
    {
        #region Var
        private readonly IBigScreenAppService _bigScreenAppService;
        private readonly IBusinessAppService _businessAppService;
        private readonly ISensorAppService _sensorAppService;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sensorAppService"></param>
        /// <param name="businessAppService"></param>
        /// <param name="bigScreenAppService"></param>
        public BigScreenController(ISensorAppService sensorAppService, IBusinessAppService businessAppService, IBigScreenAppService bigScreenAppService)
        {
            _bigScreenAppService = bigScreenAppService;
            _businessAppService = businessAppService;
            _sensorAppService = sensorAppService;
        }

        /// <summary>
        /// 大屏展示
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            BigScreenModel model = new BigScreenModel();
            model.TenantId = AbpSession.TenantId;
            model.UserId = AbpSession.UserId.Value;
            var stoptimesModel = _bigScreenAppService.GetStatisticsInfo();
            model.Today = stoptimesModel.Today;
            model.AllStopTimes = stoptimesModel.AllStopTimes;
            model.AverageStopTimes = stoptimesModel.AverageStopTimes;
            model.BerthsecStatisticsList = stoptimesModel.berthsecList;

            model.EmployeeChargesList = stoptimesModel.TodayFactReceiveList;

            model.SumReceivable = stoptimesModel.SumReceivable;
            model.SumArrearage = stoptimesModel.SumArrearage;
            model.SumFactReceive = stoptimesModel.SumFactReceive;
            model.SumRepayment = stoptimesModel.SumRepayment;
            return View(model);
        }

        /// <summary>
        /// 泊位段今日金额
        /// </summary>
        /// <returns></returns>
        public JsonResult GetBerthsecStatistics(int BerthsecId)
        {
            
            return Json(_bigScreenAppService.GetBerthsecStatisticsInfo(BerthsecId));
        }

        /// <summary>
        /// 今日出场收费
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public JsonResult GetTodayFactReceiveList(int EmployeeId)
        {
            return Json(_bigScreenAppService.GetTodayFactReceiveList(EmployeeId));
        }

        /// <summary>
        /// 展示地磁信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSensorsDetails()
        {
            var model = _bigScreenAppService.GetSensorsDetail();
            return this.Json(model);
        }

        /// <summary>
        /// 获取泊位使用情况
        /// </summary>
        /// <returns></returns>
        [Abp.Auditing.DisableAuditing]
        public JsonResult GetBerthsecsUtilization()
        {
            return this.Json(_bigScreenAppService.GetBerthsecsUtilization());
        }

        /// <summary>
        /// 获取环比金额
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMoneyChain()
        {
           return this.Json(_bigScreenAppService.GetMoneyChain());
        }

        /// <summary>
        /// 获取泊位段坐标
        /// </summary>
        /// <returns></returns>
        public JsonResult GetBerthsecsPoint()
        {
            return this.Json(_bigScreenAppService.GetBerthsecsPoint());
        }

        /// <summary>
        /// 获取人员坐标
        /// </summary>
        /// <returns></returns>
        public JsonResult GetEmpolyeePoint()
        {
            return this.Json(_bigScreenAppService.GetEmpolyeePoint());
        }
        /// <summary>
        /// 获取单个人员坐标
        /// </summary>
        /// <returns></returns>
        public JsonResult GetEmpolyeePointModel(int Employeeid)
        {
            return this.Json(_bigScreenAppService.GetEmpolyeePointModel(Employeeid));
        }

        /// <summary>
        /// 统计
        /// </summary>
        /// <returns></returns>
        public ActionResult Statistics()
        {
            BigScreenModel model = new BigScreenModel();
            model.TenantId = AbpSession.TenantId;
            model.UserId = AbpSession.UserId.Value;
            var stoptimesModel = _bigScreenAppService.GetStatisticsInfo();
            model.Today = stoptimesModel.Today;
            model.AllStopTimes = stoptimesModel.AllStopTimes;
            model.AverageStopTimes = stoptimesModel.AverageStopTimes;
            model.BerthsecStatisticsList = stoptimesModel.berthsecList;       
            return View(model);
        }

        /// <summary>
        /// 全局收入统计
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTodayStatistics(int Status)
        {
            return Json(_bigScreenAppService.GetStatisticsMoneyInfo(Status));
        }

        /// <summary>
        /// 停车车次信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetStopTimesRank()
        {
            return Json(_bigScreenAppService.StopTimesRank());
        }

        /// <summary>
        /// 微信注册用户概况
        /// </summary>
        /// <returns></returns>
        public JsonResult GetWeixinTuser()
        {
            return Json(_bigScreenAppService.WeixinTuser());
        }

        /// <summary>
        /// 获取包月车辆
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMonthlyCar()
        {
            return Json(_bigScreenAppService.MonthlyCar());
        }
    }
}