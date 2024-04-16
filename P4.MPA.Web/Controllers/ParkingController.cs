using Abp.Domain.Uow;
using Abp.Web.Mvc.Authorization;
using P4.Businesses;
using P4.Businesses.Dtos;
using P4.Collectors;
using P4.DecisionAnalysis;
using P4.Inspectors;
using P4.Web.Models;
using P4.Weixin;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [AbpMvcAuthorize]
    public class ParkingController : P4ControllerBase
    {
        #region Var
        private readonly IBusinessAppService _businessAppService;
        private readonly ICollectorAppService _collectorAppService;
        private readonly IInspectorAppService _inspectorAppService;
        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        private readonly IPeakPeriodAppService _peakPeriodAppService;
        private readonly IWeixinAppService _weixinAppService;
        #endregion


        public ParkingController(IBusinessAppService businessAppService, ICollectorAppService collectorAppService, IInspectorAppService inspectorAppService, IPeakPeriodAppService peakPeriodAppService, IWeixinAppService weixinAppService)
        {
            _businessAppService = businessAppService;
            _collectorAppService = collectorAppService;
            _inspectorAppService = inspectorAppService;
            _peakPeriodAppService = peakPeriodAppService;
            _weixinAppService = weixinAppService;
        }
        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("Control")]
        [UnitOfWork(false)]
        public async Task<ActionResult> Index()
        {
            IndexModel model = new IndexModel();
            //收费金额
            var moneyentity = _businessAppService.IndexMoneyTotal(new GetMoneyInput() { BeginTime = DateTime.Now.AddDays(-1), EndTime = DateTime.Now });
            model.ArrearageTotal = moneyentity.ArrearageTotal;
            model.FactReceiveTotal = moneyentity.FactReceiveTotal;
            model.PrepaidTotal = moneyentity.PrepaidTotal;
            model.ReceivableTotal = moneyentity.ReceivableTotal;
            model.RepaymentTotal = moneyentity.RepaymentTotal;
            //收费员签到
            model.CheckTotal = _collectorAppService.GetCollectorCheckCount();     
            //车辆在停表
            model.ParkCount = _collectorAppService.GetBerthCheckTotalCount();
            //车检器在线率
            model.SensorCount = _collectorAppService.GetSensorCheckTotalCount();
            //model.SensorCount.SensorOnLineCount=0;
            //泊位段收费统计
            model.BerthsecCount = _collectorAppService.GetBerthsecBussinessCount();
            //收费员收费统计
            model.EmployeeCountList = _collectorAppService.GetEmployeeBussinessCount();
            //停车场收费统计
            model.ParkTotalListJson = jsSer.Serialize(_collectorAppService.GetParkBussinessCount());
            //当月收费统计
            model.MonthTotalListJson = jsSer.Serialize(_collectorAppService.GetMonthBussinessCount());
            //当年收费统计
           // model.YearTotal = _collectorAppService.GetYearBussinessCount();
            //获取巡查员任务列表
           // model.inspectorTaskList = _inspectorAppService.GetAllInspectorTask();
            model.PeakPeriodList = _peakPeriodAppService.GetTopPeakPeriodList(5, false);
            model.WeixinCount = _collectorAppService.GetWeixinCount();
            model.WeixinIdeas = _weixinAppService.GetWeixinIdeaToTop(10);
            ViewBag.Company = "智能科技系统";


            //当日金额
            model.TodayMoney = _collectorAppService.TitleModelDto().TodayMoney;
            //当月金额
            model.MonthMoney = _collectorAppService.TitleModelDto().MonthMoney;
            //当日订单量
            model.TodayOrderSize = _collectorAppService.TitleModelDto().TodayOrderSize;
            //当月订单量
            model.MonthOrderSize = _collectorAppService.TitleModelDto().MonthOrderSize;
            //道闸在线率
            model.SignoCount = _collectorAppService.GetsignoOnlineOutput();
            //包月车辆
            model.MonthCarChain = _collectorAppService.GetMonthCarCountChainDto();



            return View(model);
        }
    }
}