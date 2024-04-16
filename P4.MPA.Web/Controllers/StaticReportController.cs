
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Web.Mvc.Authorization;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using P4.AliPay;
using P4.AliPay.Dto;
using P4.Berthsecs;
using P4.Berthsecs.Dto;
using P4.Businesses;
using P4.Businesses.Dtos;
using P4.Card;
using P4.Card.Dtos;
using P4.Collectors.Dtos;
using P4.Companys;
using P4.EmployeeCharges;
using P4.EmployeeCharges.Dto;
using P4.Employees;
using P4.ExportCommon;
using P4.ExportCommon.Dtos;
using P4.InspectorCharges;
using P4.InspectorCharges.Dtos;
using P4.MonthlyCars;
using P4.MonthlyCars.Dtos;
using P4.OtherAccounts;
using P4.OtherAccounts.Dtos;
using P4.ParkCharges;
using P4.ParkCharges.Dto;
using P4.Parks;
using P4.Sensors;
using P4.Sensors.Dtos;
using P4.StaticReport;
using P4.StaticReport.Dtos;
using P4.SubscribeManager;
using P4.Web.Models;
using P4.Weixin;
using P4.Weixin.Dtos;
using P4.WorkGroups;
using P4.WorkGroups.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 数据导出
    /// </summary>
    [AbpMvcAuthorize]
    public class StaticReportController : P4ControllerBase
    {
        #region Var
        private readonly IRepository<Sensor> _sensorRepository;
        private readonly IRepository<Park> _parkRepository;
        private readonly IRepository<Berths.Berth, long> _berthRepository;
        private readonly IStaticReportAppService _staticReportAppService;
        private readonly IParkAppService _parkAppService;
        private readonly IRepository<Berthsec> _abpBerthsecRepository;
        private readonly IWorkGroupRepository _workGroupRepository;
        private readonly IParkChargesAppService _parkChargesAppService;
        private readonly IEmployeeChargesAppService _employeeChargesAppService;
        private readonly IOtherAccountAppService _otherAccountAppService;
        private readonly IBusinessAppService _businessAppService;
        private readonly IEmployeeAppService _employeeAppService;
        private readonly ICompanyAppService _companyAppService;
        private readonly IInspectorChargesAppService _inspectorChargesAppService;
        private readonly IMonthlyCarAppService _monthlyCarAppService;
        private readonly IExportCodeAppService _exportCodeAppService;
        private readonly IIPassCardAppService _IPassCardAppService;
        private readonly IWeixinAppService _weixinAppService;
        private readonly ISubscribeAppService _subscribeAppService;
        private readonly IAliPayOrderAppService _aliPayOrderAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staticReportAppService"></param>
        /// <param name="parkAppService"></param>
        /// <param name="abpBerthsecRepository"></param>
        /// <param name="workGroupRepository"></param>
        /// <param name="parkChargesAppService"></param>
        /// <param name="employeeChargesAppService"></param>
        /// <param name="otherAccountAppService"></param>
        /// <param name="businessAppService"></param>
        /// <param name="employeeAppService"></param>
        /// <param name="companyAppService"></param>
        /// <param name="inspectorChargesAppService"></param>
        /// <param name="monthlyCarAppService"></param>
        /// <param name="exportCodeAppService"></param>
        /// <param name="iIPassCardAppService"></param>
        /// <param name="weixinAppService"></param>
        /// <param name="subscribeAppService"></param>
        public StaticReportController(IRepository<Sensor> sensorRepository, IRepository<Park> parkRepository, IRepository<Berths.Berth, long> berthRepository, IStaticReportAppService staticReportAppService, IParkAppService parkAppService, IRepository<Berthsec> abpBerthsecRepository,
            IWorkGroupRepository workGroupRepository, IParkChargesAppService parkChargesAppService, IEmployeeChargesAppService employeeChargesAppService,
            IOtherAccountAppService otherAccountAppService, IBusinessAppService businessAppService, IEmployeeAppService employeeAppService,
            ICompanyAppService companyAppService, IInspectorChargesAppService inspectorChargesAppService, IMonthlyCarAppService monthlyCarAppService,
            IExportCodeAppService exportCodeAppService, IIPassCardAppService iIPassCardAppService, IWeixinAppService weixinAppService, ISubscribeAppService subscribeAppService, IAliPayOrderAppService aliPayOrderAppService)
        {
            _sensorRepository = sensorRepository;
            _parkRepository = parkRepository;
            _berthRepository = berthRepository;
            _staticReportAppService = staticReportAppService;
            _parkAppService = parkAppService;
            _abpBerthsecRepository = abpBerthsecRepository;
            _workGroupRepository = workGroupRepository;
            _parkChargesAppService = parkChargesAppService;
            _employeeChargesAppService = employeeChargesAppService;
            _otherAccountAppService = otherAccountAppService;
            _businessAppService = businessAppService;
            _employeeAppService = employeeAppService;
            _companyAppService = companyAppService;
            _inspectorChargesAppService = inspectorChargesAppService;
            _monthlyCarAppService = monthlyCarAppService;
            _exportCodeAppService = exportCodeAppService;
            _IPassCardAppService = iIPassCardAppService;
            _weixinAppService = weixinAppService;
            _subscribeAppService = subscribeAppService;
            _aliPayOrderAppService = aliPayOrderAppService;
        }

        /// <summary>
        /// 泊位段报表
        /// </summary>
        /// <returns></returns>
        public ActionResult BerthsecStaticReport()
        {
            BerthsecModel model = new BerthsecModel();
            model.BerthsecList = _abpBerthsecRepository.GetAllList().MapTo<List<BerthsecDto>>();
            model.AllOption = alloption;
            return View(model);
        }
        /// <summary>
        /// 停车场报表
        /// </summary>
        /// <returns></returns>
        public ActionResult ParkStaticReport()
        {
            ParkChargesModel model = new ParkChargesModel();
            model.ParkList = _parkAppService.GetParkAll();
            model.AllOption = alloption;
            return View(model);
        }
        /// <summary>
        /// 时间段停车场报表
        /// </summary>
        /// <returns></returns>
        public ActionResult TimeBucketParkReport()
        {
            ParkChargesModel model = new ParkChargesModel();
            model.ParkList = _parkAppService.GetParkAll();
            model.AllOption = alloption;
            return View(model);
        }
        /// <summary>
        /// 时间段泊位段报表
        /// </summary>
        /// <returns></returns>
        public ActionResult TBucketBerthsecStaticR()
        {
            BerthsecModel model = new BerthsecModel();
            model.BerthsecList = _abpBerthsecRepository.GetAllList().MapTo<List<BerthsecDto>>();
            model.AllOption = alloption;
            return View(model);
        }
        /// <summary>
        /// 收费员报表
        /// </summary>
        /// <returns></returns>
        public ActionResult EmployeeStaticReport()
        {
            EmployeeChargesModel model = new EmployeeChargesModel();
            model.EmployeeList = _employeeAppService.GetEmployeeAll();
            model.AllOption = alloption;
            return View(model);
        }
        /// <summary>
        /// 公司报表
        /// </summary>
        /// <returns></returns>
        public ActionResult OperatorsCompanyStaticReport()
        {
            CompanyModel model = new CompanyModel();
            model.CompanyDtoList = _companyAppService.GetAllCompanyName();
            model.AllOption = alloption;
            return View(model);
        }
        /// <summary>
        /// 工作组报表
        /// </summary>
        /// <returns></returns>
        public ActionResult WorkGroupReport()
        {
            return View();
        }
        public EcharModel EcharIni()
        {
            //为了演示效果 这里采用随机数据来代替 后期可以根据自己情况换成访问数据获取数据
            //考虑到图表的category是字符串数组 这里定义一个string的List
            List<string> categoryList = new List<string>();

            //考虑到Echarts图表需要设置legend内的data数组为series的name集合这里需要定义一个legend数组
            List<string> legendList = new List<string>();
            //设置legend数组
            legendList.Add("实收"); //这里的名称必须和series的每一组series的name保持一致
            legendList.Add("未收");
            //legendList.Add("现金");
            //legendList.Add("刷卡");
            //legendList.Add("应收");

            //定义一个Series对象

            Series seriesObj = new Series();
            //  seriesObj.id = 1;
            seriesObj.name = "实收";
            seriesObj.type = "line"; //线性图呈现
            seriesObj.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj2 = new Series();
            // seriesObj2.id = 2;
            seriesObj2.type = "line";
            seriesObj2.name = "未收";
            seriesObj2.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            //Series seriesObj3 = new Series();
            //// seriesObj2.id = 2;
            //seriesObj3.type = "line";
            //seriesObj3.name = "现金";
            //seriesObj3.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            //Series seriesObj4 = new Series();
            //// seriesObj2.id = 2;
            //seriesObj4.type = "line";
            //seriesObj4.name = "刷卡";
            //seriesObj4.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            //Series seriesObj5 = new Series();
            //// seriesObj2.id = 2;
            //seriesObj5.type = "line";
            //seriesObj5.name = "应收";
            //seriesObj5.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            EcharModel echarModel = new EcharModel();
            echarModel.categoryList = categoryList;
            echarModel.seriesObj = seriesObj;
            echarModel.seriesObj2 = seriesObj2;
            //echarModel.seriesObj3 = seriesObj3;
            //echarModel.seriesObj4 = seriesObj4;
            //echarModel.seriesObj5 = seriesObj5;
            echarModel.legendList = legendList;
            return echarModel;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public EcharModel EcharIniBerthsec()
        {
            //为了演示效果 这里采用随机数据来代替 后期可以根据自己情况换成访问数据获取数据
            //考虑到图表的category是字符串数组 这里定义一个string的List
            List<string> categoryList = new List<string>();

            //考虑到Echarts图表需要设置legend内的data数组为series的name集合这里需要定义一个legend数组
            List<string> legendList = new List<string>();
            //设置legend数组
            legendList.Add("实收"); //这里的名称必须和series的每一组series的name保持一致
            legendList.Add("未收");
            legendList.Add("现金");
            legendList.Add("刷卡");
            legendList.Add("应收");

            //定义一个Series对象

            Series seriesObj = new Series();
            //  seriesObj.id = 1;
            seriesObj.name = "实收";
            seriesObj.type = "line"; //线性图呈现
            seriesObj.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj2 = new Series();
            // seriesObj2.id = 2;
            seriesObj2.type = "line";
            seriesObj2.name = "未收";
            seriesObj2.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj3 = new Series();
            // seriesObj2.id = 2;
            seriesObj3.type = "line";
            seriesObj3.name = "现金";
            seriesObj3.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj4 = new Series();
            // seriesObj2.id = 2;
            seriesObj4.type = "line";
            seriesObj4.name = "刷卡";
            seriesObj4.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj5 = new Series();
            // seriesObj2.id = 2;
            seriesObj5.type = "line";
            seriesObj5.name = "应收";
            seriesObj5.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            EcharModel echarModel = new EcharModel();
            echarModel.categoryList = categoryList;
            echarModel.seriesObj = seriesObj;
            echarModel.seriesObj2 = seriesObj2;
            echarModel.seriesObj3 = seriesObj3;
            echarModel.seriesObj4 = seriesObj4;
            echarModel.seriesObj5 = seriesObj5;
            echarModel.legendList = legendList;
            return echarModel;
        }


        public EcharModel TBucketBerthsecEcharIni()
        {
            //为了演示效果 这里采用随机数据来代替 后期可以根据自己情况换成访问数据获取数据
            //考虑到图表的category是字符串数组 这里定义一个string的List
            List<string> categoryList = new List<string>();
            List<string> legendList = new List<string>();
            //设置legend数组
            legendList.Add("预付"); //这里的名称必须和series的每一组series的name保持一致
            legendList.Add("应收");
            legendList.Add("实收"); //这里的名称必须和series的每一组series的name保持一致
            legendList.Add("欠费");
            legendList.Add("现金"); //这里的名称必须和series的每一组series的name保持一致
            legendList.Add("刷卡");

            //定义一个Series对象
            Series seriesObj = new Series();
            seriesObj.name = "预付";
            seriesObj.type = "line"; //线性图呈现
            seriesObj.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj2 = new Series();
            seriesObj2.name = "应收";
            seriesObj2.type = "line"; //线性图呈现
            seriesObj2.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj3 = new Series();
            seriesObj3.name = "实收";
            seriesObj3.type = "line"; //线性图呈现
            seriesObj3.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj4 = new Series();
            seriesObj4.name = "欠费";
            seriesObj4.type = "line"; //线性图呈现
            seriesObj4.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj5 = new Series();
            seriesObj5.name = "现金";
            seriesObj5.type = "bar"; //线性图呈现
            seriesObj5.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj6 = new Series();
            seriesObj6.name = "刷卡";
            seriesObj6.type = "bar"; //线性图呈现
            seriesObj6.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            EcharModel echarModel = new EcharModel();
            echarModel.categoryList = categoryList;
            echarModel.legendList = legendList;
            echarModel.seriesObj = seriesObj;
            echarModel.seriesObj2 = seriesObj2;
            echarModel.seriesObj3 = seriesObj3;
            echarModel.seriesObj4 = seriesObj4;
            echarModel.seriesObj5 = seriesObj5;
            echarModel.seriesObj6 = seriesObj6;
            return echarModel;
        }
        /// <summary>
        /// 时间段停车场EcharModel
        /// </summary>
        /// <returns></returns>
        public EcharModel EcharTimeBucketPark()
        {
            List<string> categoryList = new List<string>();
            List<string> legendList = new List<string>();
            //设置legend数组

            legendList.Add("停车次数");
            legendList.Add("应收");
            legendList.Add("实收");
            legendList.Add("欠费");
            legendList.Add("现金");
            legendList.Add("刷卡");
            //定义一个Series对象



            Series seriesObj = new Series();
            // seriesObj2.id = 2;
            seriesObj.type = "line";
            seriesObj.name = "停车次数";
            seriesObj.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj2 = new Series();
            // seriesObj2.id = 2;
            seriesObj2.type = "line";
            seriesObj2.name = "应收";
            seriesObj2.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj3 = new Series();
            // seriesObj2.id = 2;
            seriesObj3.type = "line";
            seriesObj3.name = "实收";
            seriesObj3.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj4 = new Series();
            // seriesObj2.id = 2;
            seriesObj4.type = "line";
            seriesObj4.name = "欠费";
            seriesObj4.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj5 = new Series();
            // seriesObj2.id = 2;
            seriesObj5.type = "bar";
            seriesObj5.name = "现金";
            seriesObj5.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj6 = new Series();
            // seriesObj2.id = 2;
            seriesObj6.type = "bar";
            seriesObj6.name = "刷卡";
            seriesObj6.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            EcharModel echarModel = new EcharModel();
            echarModel.categoryList = categoryList;
            echarModel.seriesObj = seriesObj;
            echarModel.seriesObj2 = seriesObj2;
            echarModel.seriesObj3 = seriesObj3;
            echarModel.seriesObj4 = seriesObj4;
            echarModel.seriesObj5 = seriesObj5;
            echarModel.seriesObj6 = seriesObj6;
            echarModel.legendList = legendList;
            return echarModel;
        }
        public JsonResult EcharReturn(EcharModel echarModel)
        { //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类
            List<Series> seriesList = new List<Series>();
            seriesList.Add(echarModel.seriesObj);
            seriesList.Add(echarModel.seriesObj2);
            //seriesList.Add(echarModel.seriesObj3);
            //seriesList.Add(echarModel.seriesObj4);
            //seriesList.Add(echarModel.seriesObj5);
            //最后调用相关函数将List转换为Json
            //因为我们需要返回category和series、legend多个对象 这里我们自己在new一个新的对象来封装这两个对象
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
        /// <param name="echarModel"></param>
        /// <returns></returns>
        public JsonResult EcharReturnBerthsec(EcharModel echarModel)
        { //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类
            List<Series> seriesList = new List<Series>();
            seriesList.Add(echarModel.seriesObj);
            seriesList.Add(echarModel.seriesObj2);
            seriesList.Add(echarModel.seriesObj3);
            seriesList.Add(echarModel.seriesObj4);
            seriesList.Add(echarModel.seriesObj5);
            //最后调用相关函数将List转换为Json
            //因为我们需要返回category和series、legend多个对象 这里我们自己在new一个新的对象来封装这两个对象
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
        /// <param name="echarModel"></param>
        /// <returns></returns>
        public JsonResult TimeBucketParkEcharReturn(EcharModel echarModel)
        {
            List<Series> seriesList = new List<Series>();
            seriesList.Add(echarModel.seriesObj);
            seriesList.Add(echarModel.seriesObj2);
            seriesList.Add(echarModel.seriesObj3);
            seriesList.Add(echarModel.seriesObj4);
            seriesList.Add(echarModel.seriesObj5);
            seriesList.Add(echarModel.seriesObj6);
            //最后调用相关函数将List转换为Json
            //因为我们需要返回category和series、legend多个对象 这里我们自己在new一个新的对象来封装这两个对象
            var newObj = new
            {
                category = echarModel.categoryList,
                series = seriesList,
                legend = echarModel.legendList
            };

            return this.Json(newObj);
        }
        /// <summary>
        /// 获取收费员Echars数据
        /// </summary>
        /// <returns></returns>
        public JsonResult EmployeeEcharData(StaticReportInput input)
        {
            //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类
            //List<Series> seriesList = new List<Series>();
            EcharModel echarModel = EcharIni();
            //DateTime begindt = DateTime.Parse(operateDateBegin + " 00:00:00");
            //DateTime enddt = DateTime.Parse(operateDateEnd + " 23:59:59");

            List<GetEmployeeReportTotalOutput> employeechargeslist = _staticReportAppService.GetEmployeeReportEchar(input);

            for (var i = 0; i < employeechargeslist.Count; i++)
            {
                echarModel.categoryList.Add(employeechargeslist[i].EmployeeName);
                echarModel.seriesObj.data.Add(employeechargeslist[i].FactReceive);
                echarModel.seriesObj2.data.Add(employeechargeslist[i].Arrearage);
            }
            return EcharReturn(echarModel);
            //seriesList.Add(echarModel.seriesObj);
            //seriesList.Add(echarModel.seriesObj2);


            ////最后调用相关函数将List转换为Json
            ////因为我们需要返回category和series、legend多个对象 这里我们自己在new一个新的对象来封装这两个对象
            //var newObj = new
            //{
            //    category = echarModel.categoryList,
            //    series = seriesList,
            //    legend = echarModel.legendList
            //};

            //return this.Json(newObj);

        }
        /// <summary>
        /// 获取收费员jqgrid数据
        /// </summary>
        /// <returns></returns>
        public JsonResult EmployeeJqGridData(StaticReportInput input)
        {
            //DateTime begindt = string.IsNullOrWhiteSpace(operateDateBegin) == true ? DateTime.Now.Date : DateTime.Parse(operateDateBegin + " 00:00:00");
            //DateTime enddt = string.IsNullOrWhiteSpace(operateDateEnd) == true ? DateTime.Now.AddDays(1).Date : DateTime.Parse(operateDateEnd + " 23:59:59");

            GetAllEmployeeReportOutput businessDetaillist = _staticReportAppService.GetEmployeeReportGrid(input);
            return this.Json(businessDetaillist);
        }

        /// <summary>
        /// 收费员导出
        /// </summary>
        /// <param name="input"></param>
        public void EmployeeCargoInfoExport(StaticReportInput input)
        {
            input.page = 1;
            input.rows = 1000;
            input.operateDateBegin = Request.Params["operateDateBegin"];
            input.operateDateEnd = Request.Params["operateDateEnd"];
            input.employeeIdInput = Request.Params["EmployeeId"];
            // public List<OperatorsCompany> BerthsecReportList { get; set; }
            GetAllEmployeeReportOutput businessDetaillist = _staticReportAppService.GetEmployeeReportGrid(input);

            if (businessDetaillist != null)
            {
                List<GetEmployeeDto> ListRTdto = new List<GetEmployeeDto>();
                for (int i = 0; i < businessDetaillist.rows.Count; i++)
                {
                    GetEmployeeDto emdto = new GetEmployeeDto();
                    emdto.EmployeeName = businessDetaillist.rows[i].EmployeeName;
                    emdto.Repayment = businessDetaillist.rows[i].Repayment;
                    emdto.Arrearage = businessDetaillist.rows[i].Arrearage;
                    emdto.FactReceive = businessDetaillist.rows[i].FactReceive;
                    ListRTdto.Add(emdto);
                }
                DataTable dt = ListToDataTable<GetEmployeeDto>(ListRTdto);
                dt.TableName = "收费员";
                Export1(dt);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void AccountRecordsDataExcelReport(SearchDeductionRecordInput input)
        {
            var model = _otherAccountAppService.GetAccountAndDeductionRecords(input);
            DataTable dt = ListToDataTable<AccountAndDeductionRecordsDto>(model);
            dt.TableName = "月份储值卡使用情况表";
            Export1(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void AccountRecordsDataExcelReportOnlyMonth(SearchDeductionRecordInput input)
        {
            var model = _otherAccountAppService.GetAccountAndDeductionRecordsOnlyMonth(input);
            List<AccountAndDeductionRecordsDto> AA = new List<AccountAndDeductionRecordsDto>();
            DataTable dt = ListToDataTable<AccountAndDeductionRecordsDto>(model);

            //删除列  
            dt.Columns.Remove("Id");

            dt.TableName = "月份储值卡使用情况表";
            Export1(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void MonthBerthsUseExport(GetBusinessDetailsInput input)
        {
            input.page = 1;
            input.rows = 1000;
            input.operateDateBegin = Request.Params["operateDateBegin"];
            input.operateDateEnd = Request.Params["operateDateEnd"];

            List<MonthBerthsUseDto> mbuList = _businessAppService.GetMonthBerthsUseList(input);
            List<MonthBerthsUseExcelDto> ListRTdto = new List<MonthBerthsUseExcelDto>();
            decimal ReceivableTotal = 0;
            decimal CashCollectionTotal = 0;
            decimal PaybyCardValueTotal = 0;
            decimal TotalConsumptionTotal = 0;
            decimal ArrearageTotal = 0;
            decimal ByCashRepayTotal = 0;
            decimal ByCardRepayTotal = 0;
            decimal TotalXJ1 = 0;
            decimal TotalXJ2 = 0;
            for (int i = 0; i < mbuList.Count; i++)
            {
                MonthBerthsUseExcelDto Mbuedto = new MonthBerthsUseExcelDto();
                Mbuedto.MBUEId = mbuList[i].Id.ToString();
                Mbuedto.MBUEBerthNumber = mbuList[i].BerthNumber;
                Mbuedto.MBUEReceivable = mbuList[i].Receivable;
                Mbuedto.MBUECashCollection = mbuList[i].CashCollection;
                Mbuedto.MBUEPaybyCardValue = mbuList[i].PaybyCardValue;
                Mbuedto.MBUETotalConsumption = mbuList[i].CashCollection + mbuList[i].PaybyCardValue + mbuList[i].ByCashRepay + mbuList[i].ByCardRepay;
                Mbuedto.MBUEArrearage = mbuList[i].Arrearage;
                Mbuedto.MBUEPByCashRepay = mbuList[i].ByCashRepay;
                Mbuedto.MBUEPByCardRepay = mbuList[i].ByCardRepay;
                Mbuedto.XJ1 = mbuList[i].CashCollection + mbuList[i].PaybyCardValue; //合计1
                Mbuedto.XJ2 = mbuList[i].ByCashRepay + mbuList[i].ByCardRepay;//合计2
                ListRTdto.Add(Mbuedto);

                ReceivableTotal += mbuList[i].Receivable;
                CashCollectionTotal += mbuList[i].CashCollection;
                PaybyCardValueTotal += mbuList[i].PaybyCardValue;
                TotalXJ1 += mbuList[i].CashCollection + mbuList[i].PaybyCardValue;
                TotalXJ2 += mbuList[i].ByCashRepay + mbuList[i].ByCardRepay;
                TotalConsumptionTotal += mbuList[i].CashCollection + mbuList[i].PaybyCardValue + mbuList[i].ByCashRepay + mbuList[i].ByCardRepay;
                ArrearageTotal += mbuList[i].Arrearage;
                ByCashRepayTotal += mbuList[i].ByCashRepay;
                ByCardRepayTotal += mbuList[i].ByCardRepay;
            }
            MonthBerthsUseExcelDto HJdto = new MonthBerthsUseExcelDto();
            HJdto.MBUEId = "合计";
            HJdto.MBUEBerthNumber = "";
            HJdto.MBUEReceivable = ReceivableTotal;
            HJdto.MBUECashCollection = CashCollectionTotal;
            HJdto.MBUEPaybyCardValue = PaybyCardValueTotal;
            HJdto.MBUETotalConsumption = TotalConsumptionTotal;
            HJdto.MBUEArrearage = ArrearageTotal;
            HJdto.MBUEPByCashRepay = ByCashRepayTotal;
            HJdto.MBUEPByCardRepay = ByCardRepayTotal;
            HJdto.XJ1 = TotalXJ1;
            HJdto.XJ2 = TotalXJ2;
            ListRTdto.Add(HJdto);
            DataTable dt = ListToDataTable<MonthBerthsUseExcelDto>(ListRTdto);
            dt.TableName = "车位使用及消费";
            ExportOnlyMonth(dt);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void MonthBerthsUseExportOnlyMonth(GetBusinessDetailsInput input)
        {
            input.page = 1;
            input.rows = 1000;
            input.operateDateBegin = Request.Params["operateDateBegin"];
            input.operateDateEnd = Request.Params["operateDateEnd"];

            List<MonthBerthsUseDto> mbuList = _businessAppService.GetMonthBerthsUseListOnlyMonth(input);
            List<MonthBerthsUseExcelDto> ListRTdto = new List<MonthBerthsUseExcelDto>();
            decimal ReceivableTotal = 0;
            decimal CashCollectionTotal = 0;
            decimal PaybyCardValueTotal = 0;
            decimal TotalConsumptionTotal = 0;
            decimal ArrearageTotal = 0;
            decimal ByCashRepayTotal = 0;
            decimal ByCardRepayTotal = 0;
            decimal TotalXJ1 = 0;
            decimal TotalXJ2 = 0;
            for (int i = 0; i < mbuList.Count; i++)
            {
                MonthBerthsUseExcelDto Mbuedto = new MonthBerthsUseExcelDto();
                Mbuedto.MBUEId = mbuList[i].YearMonth.ToString();  //月份
                Mbuedto.MBUEBerthNumber = "";  //车位
                Mbuedto.MBUEReceivable = mbuList[i].Receivable;//应收车位费
                Mbuedto.MBUECashCollection = mbuList[i].CashCollection;//现金收款
                Mbuedto.MBUEPByCashRepay = mbuList[i].ByCashRepay;//现金补缴
                Mbuedto.XJ1 = mbuList[i].CashCollection + mbuList[i].PaybyCardValue; //小计1
                Mbuedto.MBUEPaybyCardValue = mbuList[i].PaybyCardValue;//刷卡消费
                Mbuedto.MBUEPByCardRepay = mbuList[i].ByCardRepay;//刷卡补缴
                Mbuedto.XJ2 = mbuList[i].ByCashRepay + mbuList[i].ByCardRepay;//小计2
                Mbuedto.MBUETotalConsumption = mbuList[i].CashCollection + mbuList[i].ByCashRepay + mbuList[i].PaybyCardValue + mbuList[i].ByCardRepay;//消费小计
                Mbuedto.MBUEArrearage = mbuList[i].Arrearage;//未收款金额

                ListRTdto.Add(Mbuedto);
                ReceivableTotal += mbuList[i].Receivable;
                CashCollectionTotal += mbuList[i].CashCollection;
                PaybyCardValueTotal += mbuList[i].PaybyCardValue;
                TotalXJ1 += mbuList[i].CashCollection + mbuList[i].PaybyCardValue;
                TotalXJ2 += mbuList[i].ByCashRepay + mbuList[i].ByCardRepay;
                TotalConsumptionTotal += mbuList[i].PaybyCardValue + mbuList[i].ByCardRepay + mbuList[i].CashCollection + mbuList[i].ByCashRepay;
                ArrearageTotal += mbuList[i].Arrearage;
                ByCashRepayTotal += mbuList[i].ByCashRepay;
                ByCardRepayTotal += mbuList[i].ByCardRepay;
            }
            MonthBerthsUseExcelDto HJdto = new MonthBerthsUseExcelDto();
            HJdto.MBUEId = "合计";
            HJdto.MBUEBerthNumber = "";
            HJdto.MBUEReceivable = ReceivableTotal;
            HJdto.MBUECashCollection = CashCollectionTotal;
            HJdto.MBUEPaybyCardValue = PaybyCardValueTotal;
            HJdto.MBUETotalConsumption = TotalConsumptionTotal;
            HJdto.MBUEArrearage = ArrearageTotal;
            HJdto.MBUEPByCashRepay = ByCashRepayTotal;
            HJdto.MBUEPByCardRepay = ByCardRepayTotal;
            HJdto.XJ1 = TotalXJ1;
            HJdto.XJ2 = TotalXJ2;
            ListRTdto.Add(HJdto);
            DataTable dt = ListToDataTable<MonthBerthsUseExcelDto>(ListRTdto);
            dt.TableName = "月份车位使用及消费";
            ExportOnlyMonth(dt);
        }

        /// <summary>
        /// 泊位段动态导出报表
        /// </summary>
        /// <param name="input"></param>
        public void ExportBerthsecReport(ParkChargeInput input)
        {
            input.page = 1;
            input.rows = 1000;
            input.operateDateBegin = Request.Params["operateDateBegin"];
            input.operateDateEnd = Request.Params["operateDateEnd"];
            input.parkIdInput = Request.Params["parkIdInput"];
            GetAllParkChargesOutput businessDetaillist = _parkChargesAppService.GetBerthsecChargesDayReport(input);
            List<BerthsecChargesExcelDto> ListRTdto = new List<BerthsecChargesExcelDto>();
            for (int i = 0; i < businessDetaillist.rows.Count; i++)
            {
                BerthsecChargesExcelDto Pcedto = new BerthsecChargesExcelDto
                {
                    BerthsecName = businessDetaillist.rows[i].ParkName,
                    SumFactReceive = businessDetaillist.rows[i].SumFactReceive.Value,
                    XJSumFactReceive = businessDetaillist.rows[i].XJSumFactReceive,
                    SKSumFactReceive = businessDetaillist.rows[i].SKSumFactReceive,
                    SumArrearage = businessDetaillist.rows[i].SumArrearage.Value,
                    SumRepayment = businessDetaillist.rows[i].SumRepayment.Value,
                    OnlineSumFactReceive = businessDetaillist.rows[i].OnlineSumFactReceive.Value,
                    SensorSumReceivable = businessDetaillist.rows[i].SensorSumReceivable.Value,
                    SumMoney = businessDetaillist.rows[i].SumMoney.Value,
                    PosInTimes = businessDetaillist.rows[i].PosInTimes,
                    PosOutTimes = businessDetaillist.rows[i].PosOutTimes,
                    SensorTimes = businessDetaillist.rows[i].SensorTimes,
                    Yield = businessDetaillist.rows[i].Yield
                };
                ListRTdto.Add(Pcedto);
            }

            if (businessDetaillist.rows.Count > 0)
            {
                ListRTdto.Add(new BerthsecChargesExcelDto()
                {
                    BerthsecName = "合计",
                    SKSumFactReceive = businessDetaillist.rows.Sum(entity => entity.SKSumFactReceive),
                    XJSumFactReceive = businessDetaillist.rows.Sum(entity => entity.XJSumFactReceive),
                    PosInTimes = businessDetaillist.rows.Sum(entity => entity.PosInTimes),
                    PosOutTimes = businessDetaillist.rows.Sum(entity => entity.PosOutTimes),
                    SensorTimes = businessDetaillist.rows.Sum(entity => entity.SensorTimes),
                    SumFactReceive = businessDetaillist.rows.Sum(entity => entity.SumFactReceive.Value),
                    SumArrearage = businessDetaillist.rows.Sum(entity => entity.SumArrearage.Value),
                    SumRepayment = businessDetaillist.rows.Sum(entity => entity.SumRepayment.Value),
                    SumMoney = businessDetaillist.rows.Sum(entity => entity.SumMoney.Value),
                    OnlineSumFactReceive = businessDetaillist.rows.Sum(entity => entity.OnlineSumFactReceive.Value),
                    SensorSumReceivable = businessDetaillist.rows.Sum(entity => entity.SensorSumReceivable.Value),
                    Yield = (businessDetaillist.rows.Sum(entry => decimal.Parse(entry.Yield.Replace("%", ""))) / businessDetaillist.rows.Count).ToString("#0.00") + "%",
                });
            }
            else
            {
                ListRTdto.Add(new BerthsecChargesExcelDto() { BerthsecName = "合计", Yield = "0.00%" });
            }

            DataTable dt = ListToDataTable<BerthsecChargesExcelDto>(ListRTdto);
            dt.TableName = "泊位段收费报表";
            Export1(dt);
        }


        /// <summary>
        /// 欠费动态导出报表
        /// </summary>
        /// <param name="input"></param>
        public void ExportOwnReport(ParkChargeInput input)
        {
            input.page = 1;
            input.rows = 1000;
            input.operateDateBegin = Request.Params["operateDateBegin"];
            input.operateDateEnd = Request.Params["operateDateEnd"];
            input.parkIdInput = Request.Params["parkIdInput"];
            GetAllParkChargesOutput businessDetaillist = _parkChargesAppService.GetBerthescOwnOutput(input);

            List<OwnChargesExcelDto> ListRTdto = new List<OwnChargesExcelDto>();
            for (int i = 0; i < businessDetaillist.rows.Count; i++)
            {
                OwnChargesExcelDto Pcedto = new OwnChargesExcelDto
                {
                    ParkName = businessDetaillist.rows[i].ParkName,
                    SumOwn = businessDetaillist.rows[i].SumOwn,
                    SumRecovered = businessDetaillist.rows[i].SumRecovered,
                    SpanSumRecovered = businessDetaillist.rows[i].SpanSumRecovered,
                    DelSumRecovered = businessDetaillist.rows[i].DelSumRecovered,
                };
                if(Pcedto.SumRecovered == null)
                {
                    Pcedto.SumRecovered = 0;
                }
                if (Pcedto.SumOwn == null)
                {
                    Pcedto.SumOwn = 0;
                }
                if (Pcedto.SpanSumRecovered == null)
                {
                    Pcedto.SpanSumRecovered = 0;
                }
                if (Pcedto.DelSumRecovered == null)
                {
                    Pcedto.DelSumRecovered = 0;
                }
                ListRTdto.Add(Pcedto);
            }

            if (businessDetaillist.rows.Count > 0)
            {
                ListRTdto.Add(new OwnChargesExcelDto()
                {
                    ParkName = "合计",
                    SumOwn = businessDetaillist.rows.Sum(entity => entity.SumOwn).GetValueOrDefault(),
                    SumRecovered = businessDetaillist.rows.Sum(entity => entity.SumRecovered).GetValueOrDefault(),
                    SpanSumRecovered = businessDetaillist.rows.Sum(entity => entity.SpanSumRecovered).GetValueOrDefault(),
                    DelSumRecovered = businessDetaillist.rows.Sum(entity => entity.DelSumRecovered).GetValueOrDefault(),
                });
            }
            else
            {
                ListRTdto.Add(new OwnChargesExcelDto() { ParkName = "合计"});
            }

            DataTable dt = ListToDataTable(ListRTdto);
            dt.TableName = "泊位段欠费报表";
            Export1(dt);
        }

        /// <summary>
        /// 停车场动态导出报表
        /// </summary>
        /// <returns></returns>
        public void ExportParkReport(ParkChargeInput input)
        {
            input.page = 1;
            input.rows = 1000;
            input.operateDateBegin = Request.Params["operateDateBegin"];
            input.operateDateEnd = Request.Params["operateDateEnd"];
            input.parkIdInput = Request.Params["parkIdInput"];
            GetAllParkChargesOutput businessDetaillist = _parkChargesAppService.GetParkChargesDayReport1(input);
            List<ParkChargesExcelDto> ListRTdto = new List<ParkChargesExcelDto>();
            for (int i = 0; i < businessDetaillist.rows.Count; i++)
            {
                ParkChargesExcelDto Pcedto = new ParkChargesExcelDto();
                Pcedto.ParkName = businessDetaillist.rows[i].ParkName;
                Pcedto.SumFactReceive = businessDetaillist.rows[i].SumFactReceive.Value;
                Pcedto.XJSumFactReceive = businessDetaillist.rows[i].XJSumFactReceive;
                Pcedto.SKSumFactReceive = businessDetaillist.rows[i].SKSumFactReceive;
                Pcedto.SumArrearage = businessDetaillist.rows[i].SumArrearage.Value;
                Pcedto.SumRepayment = businessDetaillist.rows[i].SumRepayment.Value;
                Pcedto.OnlineSumFactReceive = businessDetaillist.rows[i].OnlineSumFactReceive.Value;
                Pcedto.SensorSumReceivable = businessDetaillist.rows[i].SensorSumReceivable.Value;
                Pcedto.SumMoney = businessDetaillist.rows[i].SumMoney.Value;
                Pcedto.Yield = businessDetaillist.rows[i].Yield;
                ListRTdto.Add(Pcedto);
            }

            if (businessDetaillist.rows.Count > 0)
            {
                ListRTdto.Add(new ParkChargesExcelDto()
                {
                    ParkName = "合计",
                    SKSumFactReceive = businessDetaillist.rows.Sum(entity => entity.SKSumFactReceive),
                    XJSumFactReceive = businessDetaillist.rows.Sum(entity => entity.XJSumFactReceive),
                    SumFactReceive = businessDetaillist.rows.Sum(entity => entity.SumFactReceive.Value),
                    SumArrearage = businessDetaillist.rows.Sum(entity => entity.SumArrearage.Value),
                    SumRepayment = businessDetaillist.rows.Sum(entity => entity.SumRepayment.Value),
                    SumMoney = businessDetaillist.rows.Sum(entity => entity.SumMoney.Value),
                    OnlineSumFactReceive = businessDetaillist.rows.Sum(entity => entity.OnlineSumFactReceive.Value),
                    SensorSumReceivable = businessDetaillist.rows.Sum(entity => entity.SensorSumReceivable.Value),
                    Yield = (businessDetaillist.rows.Sum(entry => decimal.Parse(entry.Yield.Replace("%", ""))) / businessDetaillist.rows.Count).ToString("#0.00") + "%",
                });
            }
            else
            {
                ListRTdto.Add(new ParkChargesExcelDto() { ParkName = "合计", Yield = "0.00%" });
            }

            DataTable dt = ListToDataTable<ParkChargesExcelDto>(ListRTdto);
            dt.TableName = "停车场收费报表";
            Export1(dt);
        }

        /// <summary>
        /// 动态收费员导出报表
        /// </summary>
        /// <param name="input"></param>
        public void ExportEmployeeReport(EmployeeChargeInput input)
        {
            input.page = 1;
            input.rows = 1000;
            string strdate = Convert.ToDateTime(Request.Params["operateDateBegin"]).ToString("yyyy年MM月dd日HH时mm分");
            string eddate = Convert.ToDateTime(Request.Params["operateDateEnd"]).ToString("yyyy年MM月dd日HH时mm分");
            input.operateDateBegin = Request.Params["operateDateBegin"];
            input.operateDateEnd = Request.Params["operateDateEnd"];
            //gcj  add 逃逸时间检查text
            input.operateDateBegin = Request.Params["EscapeTimeDateBegin"];
            input.operateDateEnd = Request.Params["EscapeTimeDateEnd"];
            input.employeeIdInput = Request.Params["employeeIdInput"];
            input.TenantId = AbpSession.TenantId;
            input.CompanyId = AbpSession.CompanyId;

            foreach (int v in AbpSession.BerthsecIds)
            {
                input.BerthsecIds += v + ",";
            }
            input.BerthsecIds = input.BerthsecIds.Substring(0, input.BerthsecIds.Length - 1);

            GetAllEmployeeChargesOutput businessDetaillist = _employeeChargesAppService.GetEmployeeChargesDayReport1(input);
            List<EmployeeChargesExcelDto> ListRTdto = new List<EmployeeChargesExcelDto>();

            for (int i = 0; i < businessDetaillist.rows.Count; i++)
            {
                EmployeeChargesExcelDto Ecedto = new EmployeeChargesExcelDto
                {
                    UserName = businessDetaillist.rows[i].UserName,  //收费员工号
                    ChargeOperaName = businessDetaillist.rows[i].ChargeOperaName,
                    SumMoney = businessDetaillist.rows[i].SumMoney,
                    SumArrearage = businessDetaillist.rows[i].SumArrearage,
                    SumFactReceive = businessDetaillist.rows[i].SumFactReceive,
                    XJSumFactReceive = businessDetaillist.rows[i].XJSumFactReceive,
                    SKSumFactReceive = businessDetaillist.rows[i].SKSumFactReceive,
                    WxSumFactReceive = businessDetaillist.rows[i].WxSumFactReceive,
                    CardMoney = businessDetaillist.rows[i].CardMoney,
                    CarInCount = Convert.ToDecimal( businessDetaillist.rows[i].CarInCount),
                    CarOutCount = Convert.ToDecimal(businessDetaillist.rows[i].CarOutCount),
                    XJSumRepayment = businessDetaillist.rows[i].XJSumRepayment,
                    SKSumRepayment = businessDetaillist.rows[i].SKSumRepayment,
                    WxSumRepayment = businessDetaillist.rows[i].WXSumRepayment,
                    SumIncomePlusBack = businessDetaillist.rows[i].SumIncomePlusBack,//总收入
                    SumRepayment = businessDetaillist.rows[i].SumRepayment,
                    Yield = businessDetaillist.rows[i].Yield,
                    AllYield = businessDetaillist.rows[i].AllYield
                };
                ListRTdto.Add(Ecedto);
            }

            if (ListRTdto.Count > 0)
            {
                ListRTdto.Add(new EmployeeChargesExcelDto()
                {
                    UserName = "合计",
                    ChargeOperaName = businessDetaillist.rows.Count.ToString(),
                    SumMoney = businessDetaillist.rows.Sum(entry => entry.SumMoney),
                    SumArrearage = businessDetaillist.rows.Sum(entry => entry.SumArrearage),
                    SumFactReceive = businessDetaillist.rows.Sum(entry => entry.SumFactReceive),
                    XJSumFactReceive = businessDetaillist.rows.Sum(entry => entry.XJSumFactReceive),
                    SKSumFactReceive = businessDetaillist.rows.Sum(entry => entry.SKSumFactReceive),
                    WxSumFactReceive = businessDetaillist.rows.Sum(entry => entry.WxSumFactReceive),
                    CardMoney = businessDetaillist.rows.Sum(entry => entry.CardMoney),     //充值支付
                    CarInCount = businessDetaillist.rows.Sum(entry => entry.CarInCount),
                    CarOutCount = businessDetaillist.rows.Sum(entry => entry.CarOutCount),
                    XJSumRepayment = businessDetaillist.rows.Sum(entry => entry.XJSumRepayment),
                    SKSumRepayment = businessDetaillist.rows.Sum(entry => entry.SKSumRepayment),
                    WxSumRepayment = businessDetaillist.rows.Sum(entry => entry.WXSumRepayment),               
                    SumIncomePlusBack = businessDetaillist.rows.Sum(entry => entry.SumIncomePlusBack),
                    SumRepayment = businessDetaillist.rows.Sum(entry => entry.SumRepayment),
                    Yield = (businessDetaillist.rows.Sum(entry => decimal.Parse(entry.Yield.Replace("%", ""))) / businessDetaillist.rows.Count).ToString("#0.00") + "%",
                    AllYield = (businessDetaillist.rows.Sum(entry => decimal.Parse(entry.AllYield.Replace("%", ""))) / businessDetaillist.rows.Count).ToString("#0.00") + "%"
                });
            }
            else
            {
                ListRTdto.Add(new EmployeeChargesExcelDto() { UserName = "合计", ChargeOperaName = "0", Yield = "0.00%", AllYield = "0.00%" });
            }
            DataTable dt = ListToDataTable(ListRTdto);
            dt.TableName = "收费员报表" + strdate + "到" + eddate;
            Export1(dt);
        }

        /// <summary>
        /// 动态巡查员导出报表
        /// </summary>
        /// <param name="input"></param>
        public void ExportInspectorReport(InspectorChargeInput input)
        {
            input.page = 1;
            input.rows = 1000;
            input.operateDateBegin = Request.Params["operateDateBegin"];
            input.operateDateEnd = Request.Params["operateDateEnd"];
            input.employeeIdInput = Request.Params["employeeIdInput"];
            input.TenantId = AbpSession.TenantId;
            input.CompanyId = AbpSession.CompanyId;
            GetAllInspectorChargesOutput businessDetaillist = _inspectorChargesAppService.GetInspectorChargesDayReport(input);
            List<InspectorChargesExcelDto> ListRTdto = new List<InspectorChargesExcelDto>();

            for (int i = 0; i < businessDetaillist.rows.Count; i++)
            {
                InspectorChargesExcelDto Ecedto = new InspectorChargesExcelDto
                {
                    UserName = businessDetaillist.rows[i].UserName,  //收费员工号
                    ChargeOperaName = businessDetaillist.rows[i].ChargeOperaName,
                    SumFactReceive = businessDetaillist.rows[i].SumFactReceive,
                    XJSumFactReceive = businessDetaillist.rows[i].XJSumFactReceive,
                    SKSumFactReceive = businessDetaillist.rows[i].SKSumFactReceive,
                    SumArrearage = businessDetaillist.rows[i].SumArrearage,
                    SumMoney = businessDetaillist.rows[i].SumMoney,
                    CardMoney = businessDetaillist.rows[i].CardMoney,
                    SKSumRepayment = businessDetaillist.rows[i].SKSumRepayment,
                    XJSumRepayment = businessDetaillist.rows[i].XJSumRepayment,
                    SumIncomePlusBack = businessDetaillist.rows[i].SumIncomePlusBack//总收入
                };
                ListRTdto.Add(Ecedto);
            }

            if (ListRTdto.Count > 0)
            {
                ListRTdto.Add(new InspectorChargesExcelDto()
                {
                    UserName = "合计",
                    ChargeOperaName = businessDetaillist.rows.Count.ToString(),
                    SumFactReceive = businessDetaillist.rows.Sum(entry => entry.SumFactReceive),
                    XJSumFactReceive = businessDetaillist.rows.Sum(entry => entry.XJSumFactReceive),
                    SKSumFactReceive = businessDetaillist.rows.Sum(entry => entry.SKSumFactReceive),
                    SumArrearage = businessDetaillist.rows.Sum(entry => entry.SumArrearage),
                    SumMoney = businessDetaillist.rows.Sum(entry => entry.SumMoney),
                    SKSumRepayment = businessDetaillist.rows.Sum(entry => entry.SKSumRepayment),
                    XJSumRepayment = businessDetaillist.rows.Sum(entry => entry.XJSumRepayment),
                    SumIncomePlusBack = businessDetaillist.rows.Sum(entry => entry.SumIncomePlusBack),
                    CardMoney = businessDetaillist.rows.Sum(entity => entity.CardMoney)
                });
            }
            else
            {
                ListRTdto.Add(new InspectorChargesExcelDto() { UserName = "合计", ChargeOperaName = "0" });
            }

            DataTable dt = ListToDataTable<InspectorChargesExcelDto>(ListRTdto);
            dt.TableName = "巡查员收费报表";
            Export1(dt);
        }

        /// <summary>
        /// 动态Mone消费详情报表导出
        /// </summary>
        /// <param name="input"></param>
        public void ExportOtherAccount(SearchDeductionRecordInput input)
        {
            input.page = 1;
            input.rows = 1000;
            input.operateDateBegin = Request.Params["operateDateBegin"];
            input.operateDateEnd = Request.Params["operateDateEnd"];
            GetAllDeductionRecordOutput deductionrecord = _otherAccountAppService.GetDynamicReportDeductionRecord(input);
            List<AccountRecordsExcelDto> ListRTdto = new List<AccountRecordsExcelDto>();
            for (int i = 0; i < deductionrecord.rows.Count; i++)
            {
                AccountRecordsExcelDto Aredto = new AccountRecordsExcelDto();
                Aredto.UserName = deductionrecord.rows[i].UserName;
                Aredto.Name = deductionrecord.rows[i].Name;
                Aredto.InTimeStr = deductionrecord.rows[i].InTimeStr;
                Aredto.CardNo = deductionrecord.rows[i].CardNo;
                Aredto.TelePhone = deductionrecord.rows[i].TelePhone;
                Aredto.Remark = deductionrecord.rows[i].Remark;
                Aredto.Money = deductionrecord.rows[i].Money;
                Aredto.Wallet = deductionrecord.rows[i].Wallet;
                ListRTdto.Add(Aredto);
            }
            DataTable dt = ListToDataTable<AccountRecordsExcelDto>(ListRTdto);
            dt.TableName = "Mone消费详情";
            Export1(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void ExportOtherTotalAccount(SearchDeductionRecordInput input)
        {
            input.page = 1;
            input.rows = 1000;
            input.operateDateBegin = Request.Params["operateDateBegin"];
            input.operateDateEnd = Request.Params["operateDateEnd"];
            GetAllDeductionRecordOutput deductionrecord = _otherAccountAppService.GetDynamicReportDeductionRecord(input);
            List<AccountRecordsExcelDto> ListRTdto = new List<AccountRecordsExcelDto>();
            for (int i = 0; i < deductionrecord.rows.Count; i++)
            {
                AccountRecordsExcelDto Aredto = new AccountRecordsExcelDto();
                Aredto.UserName = deductionrecord.rows[i].UserName;
                Aredto.Name = deductionrecord.rows[i].Name;
                Aredto.InTimeStr = deductionrecord.rows[i].InTimeStr;
                Aredto.CardNo = deductionrecord.rows[i].CardNo;
                Aredto.TelePhone = deductionrecord.rows[i].TelePhone;
                Aredto.Remark = deductionrecord.rows[i].Remark;
                Aredto.Money = deductionrecord.rows[i].Money;
                Aredto.Wallet = deductionrecord.rows[i].Wallet;
                ListRTdto.Add(Aredto);
            }
            DataTable dt = ListToDataTable<AccountRecordsExcelDto>(ListRTdto);
            dt.TableName = "Mone消费详情";
            Export1(dt);
        }

        /// <summary>
        /// 静态时间段停车场报表导出
        /// </summary>
        /// <param name="input"></param>
        public void ExportTimeBucketParkReport(StaticReportInput input)
        {
            input.page = 1;
            input.rows = 1000;
            input.operateDateBegin = Request.Params["operateDateBegin"];
            input.parkIdInput = Request.Params["parkIdInput"];
            GetAllParkReportOutput GAPRO = _staticReportAppService.GetTimeBucketParkReport(input);
            List<TimeBucketParkReportExcelDto> ListRTdto = new List<TimeBucketParkReportExcelDto>();
            for (int i = 0; i < GAPRO.rows.Count; i++)
            {
                TimeBucketParkReportExcelDto Tbprdto = new TimeBucketParkReportExcelDto();
                Tbprdto.ParkName = GAPRO.rows[i].ParkName;
                Tbprdto.Time = GAPRO.rows[i].Time;
                Tbprdto.StopTimes = GAPRO.rows[i].StopTimes;
                Tbprdto.Receivable = GAPRO.rows[i].Receivable;
                Tbprdto.FactReceive = GAPRO.rows[i].FactReceive;
                Tbprdto.Cash = GAPRO.rows[i].Cash;
                Tbprdto.ByCard = GAPRO.rows[i].ByCard;
                Tbprdto.Arrearage = GAPRO.rows[i].Arrearage;
                ListRTdto.Add(Tbprdto);
            }
            DataTable dt = ListToDataTable<TimeBucketParkReportExcelDto>(ListRTdto);
            dt.TableName = "时间段停车场";
            Export1(dt);
        }

        /// <summary>
        /// 静态时间段泊位段报表导出
        /// </summary>
        /// <param name="input"></param>
        public void ExportTBucketBertsecStaticR(StaticReportInput input)
        {
            input.page = 1;
            input.rows = 1000;
            input.operateDateBegin = Request.Params["operateDateBegin"];
            input.parkIdInput = Request.Params["parkIdInput"];
            GetAllBerthsecReportOutput TBucketbusinessDetaillist = _staticReportAppService.GetTBucketBerthsecGrid(input);
            List<TbucketBerthsecStaticRExcelDto> ListRTdto = new List<TbucketBerthsecStaticRExcelDto>();
            for (int i = 0; i < TBucketbusinessDetaillist.rows.Count; i++)
            {
                TbucketBerthsecStaticRExcelDto Tbprdto = new TbucketBerthsecStaticRExcelDto();
                Tbprdto.BerthsecName = TBucketbusinessDetaillist.rows[i].BerthsecName;
                Tbprdto.Time = TBucketbusinessDetaillist.rows[i].Time;
                Tbprdto.StopTime = TBucketbusinessDetaillist.rows[i].StopTime;
                Tbprdto.Prepaid = TBucketbusinessDetaillist.rows[i].Prepaid;
                Tbprdto.Receivable = TBucketbusinessDetaillist.rows[i].Receivable;
                Tbprdto.FactReceive = TBucketbusinessDetaillist.rows[i].FactReceive;
                Tbprdto.Cash = TBucketbusinessDetaillist.rows[i].Cash;
                Tbprdto.ByCard = TBucketbusinessDetaillist.rows[i].ByCard;
                Tbprdto.Arrearage = TBucketbusinessDetaillist.rows[i].Arrearage;
                ListRTdto.Add(Tbprdto);
            }
            DataTable dt = ListToDataTable<TbucketBerthsecStaticRExcelDto>(ListRTdto);
            dt.TableName = "时间段泊位段";
            Export1(dt);
        }

        /// <summary>
        /// 静态工作组报表导出
        /// </summary>
        /// <param name="input"></param>
        public void ExportWorkGroupReport(WorkGroupInput input)
        {
            input.page = 1;
            input.rows = 1000;
            input.operateDateBegin = Request.Params["operateDateBegin"];
            input.operateDateEnd = Request.Params["operateDateEnd"];
            GetAllWorkGroupReport workGroupR = _workGroupRepository.GetWorkGroupReportJqGrid(input);
            List<WorkGroupReportExcelDto> ListRTdto = new List<WorkGroupReportExcelDto>();
            for (int i = 0; i < workGroupR.rows.Count; i++)
            {
                WorkGroupReportExcelDto Wgrdto = new WorkGroupReportExcelDto();
                Wgrdto.WorkGroupName = workGroupR.rows[i].WorkGroupName;
                Wgrdto.WorkGroupPrepaid = workGroupR.rows[i].WorkGroupPrepaid;
                Wgrdto.WorkGroupReceivable = workGroupR.rows[i].WorkGroupReceivable;
                Wgrdto.WorkGroupFactReceive = workGroupR.rows[i].WorkGroupFactReceive;
                Wgrdto.WorkGroupCash = workGroupR.rows[i].WorkGroupCash;
                Wgrdto.WorkGroupByCard = workGroupR.rows[i].WorkGroupByCard;
                Wgrdto.WorkGroupArrearage = workGroupR.rows[i].WorkGroupArrearage;
                ListRTdto.Add(Wgrdto);
            }
            DataTable dt = ListToDataTable<WorkGroupReportExcelDto>(ListRTdto);
            dt.TableName = "工作组";
            Export1(dt);
        }


        /// <summary>
        /// 静态报表泊位段导出
        /// </summary>
        /// <param name="input"></param>
        public void BerthsecCargoInfoExport(StaticReportInput input)
        {
            input.page = 1;
            input.rows = 1000;
            input.operateDateBegin = Request.Params["operateDateBegin"];
            input.operateDateEnd = Request.Params["operateDateEnd"];
            input.BerthsecIdInput = Request.Params["BerthsecId"];
            // public List<OperatorsCompany> BerthsecReportList { get; set; }
            GetAllBerthsecReportOutput businessDetaillist = _staticReportAppService.GetBerthsecReportGrid(input);
            if (businessDetaillist.rows.Count != 0)
            {
                List<GetBerthsecDto> ListRTdto = new List<GetBerthsecDto>();
                for (int i = 0; i < businessDetaillist.rows.Count; i++)
                {
                    GetBerthsecDto emdto = new GetBerthsecDto();
                    emdto.Receivable = businessDetaillist.rows[i].Receivable;
                    emdto.BerthsecName = businessDetaillist.rows[i].BerthsecName;
                    emdto.FactPercent = businessDetaillist.rows[i].FactPercent;
                    emdto.CarInCount = businessDetaillist.rows[i].CarInCount;
                    emdto.CarOutCount = businessDetaillist.rows[i].CarOutCount;
                    emdto.Arrearage = businessDetaillist.rows[i].Arrearage;
                    emdto.Cash = businessDetaillist.rows[i].Cash;
                    emdto.ByCardPercent = businessDetaillist.rows[i].ByCardPercent;
                    emdto.FactReceive = businessDetaillist.rows[i].FactReceive;
                    emdto.BerthsecByCard = businessDetaillist.rows[i].BerthsecByCard;
                    ListRTdto.Add(emdto);
                }
                DataTable dt = ListToDataTable<GetBerthsecDto>(ListRTdto);
                dt.TableName = "泊位段";
                Export1(dt);
            }
        }

        /// <summary>
        /// 静态报表分公司导出
        /// </summary>
        /// <param name="input"></param>
        public void OperatorsCompanyCargoInfoExport(StaticReportInput input)
        {
            input.page = 1;
            input.rows = 1000;
            input.operateDateBegin = Request.Params["operateDateBegin"];
            input.operateDateEnd = Request.Params["operateDateEnd"];
            input.CompanyIdInput = Request.Params["CompanyId"];
            // public List<OperatorsCompany> BerthsecReportList { get; set; }
            GetAllOperatorsCompanyReportOutput businessDetaillist = _staticReportAppService.GetOperatorsCompanyReportGrid(input);
            if (businessDetaillist != null)
            {
                List<GetOperatorCompanyDto> ListRTdto = new List<GetOperatorCompanyDto>();
                for (int i = 0; i < businessDetaillist.rows.Count; i++)
                {
                    GetOperatorCompanyDto emdto = new GetOperatorCompanyDto();
                    emdto.Receivable = businessDetaillist.rows[i].Receivable;
                    emdto.CompanyName = businessDetaillist.rows[i].CompanyName;
                    emdto.Arrearage = businessDetaillist.rows[i].Arrearage;
                    emdto.FactReceive = businessDetaillist.rows[i].FactReceive;
                    emdto.Repayment = businessDetaillist.rows[i].Repayment;
                    emdto.ByCard = businessDetaillist.rows[i].ByCard;
                    emdto.Cash = businessDetaillist.rows[i].Cash;
                    ListRTdto.Add(emdto);
                }
                DataTable dt = ListToDataTable<GetOperatorCompanyDto>(ListRTdto);
                dt.TableName = "分公司";
                Export1(dt);
            }


        }
        //静态报表停车场导出

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void ParkCargoInfoExport(StaticReportInput input)
        {
            input.page = 1;
            input.rows = 1000;
            input.operateDateBegin = Request.Params["operateDateBegin"];
            input.operateDateEnd = Request.Params["operateDateEnd"];
            input.parkIdInput = Request.Params["ParkId"];
            // public List<OperatorsCompany> BerthsecReportList { get; set; }
            GetAllParkReportOutput businessDetaillist = _staticReportAppService.GetParkReportGrid(input);

            List<GetParkDto> ListRTdto = new List<GetParkDto>();
            for (int i = 0; i < businessDetaillist.rows.Count; i++)
            {
                GetParkDto emdto = new GetParkDto();
                emdto.Arrearage = businessDetaillist.rows[i].Arrearage;
                emdto.FactReceive = businessDetaillist.rows[i].FactReceive;
                emdto.ParkName = businessDetaillist.rows[i].ParkName;
                emdto.Receivable = businessDetaillist.rows[i].Receivable;
                emdto.Cash = businessDetaillist.rows[i].Cash;
                emdto.ByCard = businessDetaillist.rows[i].ByCard;
                ListRTdto.Add(emdto);
            }
            DataTable dt = ListToDataTable<GetParkDto>(ListRTdto);
            dt.TableName = "停车场";
            Export1(dt);
        }

        /// <summary>
        /// 导出包月卡
        /// </summary>
        [AbpMvcAuthorize("MonthCars.Field1")]
        public void ExportMonth(MonthlyCarInput input)
        {
            input.page = 1;
            input.rows = 300000;
            input.sidx = "id";
            input.sord = "desc";
            var list = _monthlyCarAppService.GetMonthlyCarList(input);
            DataTable dt = ListToDataTable<MonthlyCarExportDto>(list.rows.MapTo<List<MonthlyCarExportDto>>());
            dt.TableName = "包月卡列表";
            Export1(dt);
            _subscribeAppService.SendMessage("MonthCarsManagement", "导出包月车辆记录", "");

        }

        /// <summary>
        /// 导出包月卡明细
        /// </summary>
        [AbpMvcAuthorize("MonthCars.Field2")]
        public void ExportMonthDetail(MonthlyCarHistoryInput input)
        {
            input.page = 1;
            input.rows = 300000;
            input.sidx = "id";
            input.sord = "desc";
            var list = _monthlyCarAppService.GetMonthlyCarHistoryAll(input);
            DataTable dt = new DataTable();
            if (list != null)
            {
                dt = ListToDataTable<MonthlyCarHistoryExportDto>(list.rows.MapTo<List<MonthlyCarHistoryExportDto>>());
            }
            dt.TableName = "包月卡明细列表";
            Export1(dt);
            _subscribeAppService.SendMessage("MonthCarsManagement", "导出包月车辆明细记录", "");

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        [AbpMvcAuthorize("EscapeRank.Field1")]
        public void ExportEscapeRank(GetEscapeRankInput input)
        {
            input.page = 1;
            input.rows = int.MaxValue;
            input.sidx = "Money";
            input.sord = "desc";
            GetEscapeRankOutput output = _businessAppService.GetEscapeRankList(input);
            DataTable dt = ListToDataTable<EscapeRankDto>(output.rows);
            dt.TableName = "欠费排名导出";
            Export1(dt);
        }

        /// <summary>
        /// 收费明细导出
        /// </summary>
        /// <param name="input"></param>
        [AbpMvcAuthorize("BusinessDetails.Field1")]
        public void ExportBusinessDetails(GetBusinessDetailsInput input)
        {
            input.page = 1;
            input.rows = 300000;
            input.RegionId = int.Parse(Request.Params["RegionId"]);
            input.ParkId = int.Parse(Request.Params["ParkId"]);
            input.BerthsecId = int.Parse(Request.Params["BerthsecId"]);
            input.BerthNumber = Request.Params["BerthNumber"];
            input.PlateNumber = Request.Params["PlateNumber"];
            input.operateDateBegin = Request.Params["operateDateBegin"];
            input.operateDateEnd = Request.Params["operateDateEnd"];
            input.StopType = short.Parse(Request.Params["StopType"]);
            input.PayStatus = short.Parse(Request.Params["PayStatus"]);
            input.sidx = "CarPayTime";
            input.sord = "asc";
            GetBusinessDetailsOutput Gbdo = _businessAppService.GetBusinessDetailsList(input);
            List<BusinessDetailsExcelDto> ListRTdto = new List<BusinessDetailsExcelDto>();
            for (int i = 0; i < Gbdo.rows.Count; i++)
            {
                BusinessDetailsExcelDto Bddto = new BusinessDetailsExcelDto
                {
                    BdeTenant = Gbdo.rows[i].Tenant,
                    BdeRegionName = Gbdo.rows[i].RegionName,
                    BdeParkName = Gbdo.rows[i].ParkName,
                    BdeBerthsecName = Gbdo.rows[i].BerthsecName,
                    BdeBerthNumber = Gbdo.rows[i].BerthNumber,
                    BdePlateNumber = Gbdo.rows[i].PlateNumber,
                    BdeCarInTimeString = Gbdo.rows[i].CarInTimeString,
                    BdeCarOutTimeString = Gbdo.rows[i].CarOutTimeString,
                    BdeStopTimes = Gbdo.rows[i].StopTimes,
                    BdePrepaid = Gbdo.rows[i].Prepaid,
                    BdeReceivable = Gbdo.rows[i].Receivable,
                    BdeFactReceive = Gbdo.rows[i].FactReceive,
                    BdeArrearage = Gbdo.rows[i].Arrearage,
                    BdeStatusName = Gbdo.rows[i].StatusName,
                    BdeStopTypeName = Gbdo.rows[i].StopTypeName,
                    BdeInEmployeeName = Gbdo.rows[i].InEmployeeName,
                    BdeInDeviceCode = Gbdo.rows[i].InDeviceCode,
                    BdeOutEmployeeName = Gbdo.rows[i].OutEmployeeName,
                    BdeOutDeviceCode = Gbdo.rows[i].OutDeviceCode,
                    BdeChargeEmployeeName = Gbdo.rows[i].ChargeEmployeeName,
                    BdeChargeDeviceCode = Gbdo.rows[i].ChargeDeviceCode,
                    BdSensorsInCarTimeString = Gbdo.rows[i].SensorsInCarTimeString,
                    BdSensorsOutCarTimeString = Gbdo.rows[i].SensorsOutCarTimeString,
                    BdSensorsStopTimes = Gbdo.rows[i].SensorsStopTimes,
                    BdSensorsReceivable = Gbdo.rows[i].SensorsReceivable
                };
                ListRTdto.Add(Bddto);
            }
            DataTable dt = ListToDataTable<BusinessDetailsExcelDto>(ListRTdto);
            dt.TableName = "收费明细";
            Export1(dt);
        }

        /// <summary>
        /// 逃逸明细导出
        /// </summary>
        /// <param name="input"></param>
        [AbpMvcAuthorize("EscapeDetails.Field1")]
        public void ExportEscapeDetails(GetEscapeDetailsInput input)
        {
            input.page = 1;
            input.rows = 300000;
            input.RegionId = Convert.ToInt32(Request.QueryString["RegionId"]);
            input.ParkId = Convert.ToInt32(Request.QueryString["ParkId"]);
            input.BerthsecId = Convert.ToInt32(Request.QueryString["BerthsecId"]);
            input.BerthNumber = Request.QueryString["BerthNumber"];
            input.operateDateBegin = Request.QueryString["operateDateBegin"];
            input.operateDateEnd = Request.QueryString["operateDateEnd"];
            input.PlateNumber = Request.QueryString["PlateNumber"];
            input.EmployeeId = Convert.ToInt32(Request.QueryString["EmployeeId"]);
            input.TenantId = AbpSession.TenantId;
            input.CompanyId = AbpSession.CompanyId;
            GetEscapeDetailsOutput Getoutput = _businessAppService.GetEscapeDetailsList(input);
            List<EscapeDetailsExcelDto> ListRTdto = new List<EscapeDetailsExcelDto>();
            for (int i = 0; i < Getoutput.rows.Count; i++)
            {
                EscapeDetailsExcelDto Bddto = new EscapeDetailsExcelDto
                {
                    EdeTenant = Getoutput.rows[i].Tenant,
                    EdeRegionName = Getoutput.rows[i].RegionName,
                    EdeParkName = Getoutput.rows[i].ParkName,
                    EdeBerthsecName = Getoutput.rows[i].BerthsecName,
                    EdeBerthNumber = Getoutput.rows[i].BerthNumber,
                    EdePlateNumber = Getoutput.rows[i].PlateNumber,
                    EdeCarInTimeString = Getoutput.rows[i].CarInTimeString,
                    EdeCarOutTimeString = Getoutput.rows[i].CarOutTimeString,
                    EdeStopTimes = Getoutput.rows[i].StopTimes,
                    EdePrepaid = Getoutput.rows[i].Prepaid,
                    EdeReceivable = Getoutput.rows[i].Receivable,
                    EdeArrearage = Getoutput.rows[i].Arrearage,
                    EdeInEmployeeName = Getoutput.rows[i].InEmployeeName,
                    EdeInDeviceCode = Getoutput.rows[i].InDeviceCode,
                    EdeOutEmployeeName = Getoutput.rows[i].OutEmployeeName,
                    EdeOutDeviceCode = Getoutput.rows[i].OutDeviceCode,
                    EdeEscapeEmployeeName = Getoutput.rows[i].EscapeEmployeeName,
                    EdeEscapeDeviceCode = Getoutput.rows[i].EscapeDeviceCode,
                    EdeStatusName = Getoutput.rows[i].StatusName,
                    EdeRepayment = Getoutput.rows[i].Repayment,
                    EdeCarRepaymentTime = Getoutput.rows[i].CarRepaymentTime
                };
                ListRTdto.Add(Bddto);
            }
            DataTable dt = ListToDataTable<EscapeDetailsExcelDto>(ListRTdto);
            dt.TableName = "逃逸明细";
            Export1(dt);
        }

        /// <summary>
        /// 导出刷卡记录数据
        /// </summary>
        [AbpMvcAuthorize("CardSettlement.Field2")]
        public void ExportCardSettlement()
        {
            List<IPassCardDto> output = _IPassCardAppService.GetList(new Card.Dtos.GetAllIPassCardInput()
            {
                rows = int.MaxValue,
                page = 1,
                sidx = "PayDate",
                sord = "asc"
            }).rows;
            DataTable dt = ListToDataTable<IPassCardDto>(output);
            dt.TableName = "刷卡明细";
            Export1(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        [AbpMvcAuthorize("WeixinSettlement.Field1")]
        public void ExportWeixinSettlement()
        {
            List<WeixinOrdersDto> output = _weixinAppService.GetWeixinOrderList(new Weixin.Dtos.SearchWeixinOrdersInput()
            {
                rows = int.MaxValue,
                page = 1,
                sidx = "time_end",
                sord = "desc"
            }).rows;
            DataTable dt = ListToDataTable<WeixinOrdersDto>(output);
            dt.TableName = "微信支付明细";
            Export1(dt);
        }

        [AbpMvcAuthorize("AliPaySettlement.Field1")]
        public void ExportAliPaySettlement(SearchAliPayOrdersInput input)
        {
            input.rows = int.MaxValue;
            input.page = 1;
            input.sidx = "gmt_create";
            input.sord = "desc";
            List<AliPayOrdersDto> output = _aliPayOrderAppService.GetAllAliPayOrderDetail1(input).rows;
            DataTable dt = ListToDataTable<AliPayOrdersDto>(output);
            dt.TableName = "支付宝支付明细";
            Export1(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        [AbpMvcAuthorize("TenantSettlement.Field1")]
        public void ExportTenantSettlement(GetEscapeDetailsInput input)
        {
            input.rows = int.MaxValue;
            input.page = 1;
            input.sidx = "CarRepaymentTime";
            input.sord = "desc";
            List<EscapeDetailsDto> output = _businessAppService.GetEscapeDetailsListByTenant(input).rows;
            DataTable dt = ListToDataTable<EscapeDetailsDto>(output);
            dt.TableName = "商户追缴明细";
            Export1(dt);
        }

        /// <summary>
        /// 获取停车场Echars数据
        /// </summary>
        /// <returns></returns>
        public JsonResult ParkEcharData(StaticReportInput input)
        {
            //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类

            EcharModel echarModel = EcharIniBerthsec();
            //DateTime begindt = DateTime.Parse(operateDateBegin +" 00:00:00");
            //DateTime enddt = DateTime.Parse(operateDateEnd + " 23:59:59");

            List<GetParkTotalOutput> parklist = _staticReportAppService.GetParkReportEchar(input);

            for (var i = 0; i < parklist.Count; i++)
            {
                echarModel.categoryList.Add(parklist[i].ParkName);
                echarModel.seriesObj.data.Add(parklist[i].FactReceive);
                echarModel.seriesObj2.data.Add(parklist[i].Arrearage);
                echarModel.seriesObj3.data.Add(parklist[i].Cash);
                echarModel.seriesObj4.data.Add(parklist[i].ByCard);
                echarModel.seriesObj5.data.Add(parklist[i].Receivable);
            }
            return EcharReturnBerthsec(echarModel);
        }

        /// <summary>
        /// 获取时间段停车场Echars数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult TimeBucketParkEcharData(StaticReportInput input)
        {
            EcharModel echarModel = EcharTimeBucketPark();
            //DateTime begindt = DateTime.Parse(operateDateBegin +" 00:00:00");
            //DateTime enddt = DateTime.Parse(operateDateEnd + " 23:59:59");

            List<GetParkTotalOutput> parklist = _staticReportAppService.GetTimeBucketParkReportEchar(input);
            for (var i = 0; i < parklist.Count; i++)
            {
                echarModel.categoryList.Add(parklist[i].Time + "\n" + parklist[i].ParkName);
                echarModel.seriesObj.data.Add(parklist[i].StopTimes);//停产次数
                echarModel.seriesObj2.data.Add(parklist[i].Receivable);//应收
                echarModel.seriesObj3.data.Add(parklist[i].FactReceive);//实收
                echarModel.seriesObj4.data.Add(parklist[i].Arrearage);//欠费
                echarModel.seriesObj5.data.Add(parklist[i].Cash);//欠费
                echarModel.seriesObj6.data.Add(parklist[i].ByCard);//欠费
            }
            return TimeBucketParkEcharReturn(echarModel);
        }

        /// <summary>
        /// 获取停车场jqgrid数据
        /// </summary>
        /// <returns></returns>
        public JsonResult ParkJqGridData(StaticReportInput input)
        {
            //DateTime begindt = string.IsNullOrWhiteSpace(input.operateDateBegin) == true ? DateTime.Now.Date : DateTime.Parse(input.operateDateBegin + " 00:00:00");
            //DateTime enddt = string.IsNullOrWhiteSpace(input.operateDateEnd) == true ? DateTime.Now.AddDays(1).Date : DateTime.Parse(input.operateDateEnd + " 23:59:59");

            GetAllParkReportOutput businessDetaillist = _staticReportAppService.GetParkReportGrid(input);
            return this.Json(businessDetaillist);
        }

        /// <summary>
        ///获取时间段停车场jqgrid数据
        /// </summary>
        /// <returns></returns>
        public JsonResult TimeBucketParkJsonData(StaticReportInput input)
        {
            GetAllParkReportOutput GAPRO = _staticReportAppService.GetTimeBucketParkReport(input);
            return this.Json(GAPRO);
        }

        /// <summary>
        /// 获取泊位段Echars数据
        /// </summary>
        /// <returns></returns>
        public JsonResult BerthsecEcharData(StaticReportInput input)
        {
            //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类

            EcharModel echarModel = EcharIniBerthsec();
            //DateTime begindt = DateTime.Parse(input.operateDateBegin + " 00:00:00");
            //DateTime enddt = DateTime.Parse(input.operateDateEnd + " 23:59:59");

            List<GetBerthsecReportTotalOutput> parklist = _staticReportAppService.GetBerthsecReportEchar(input);

            for (var i = 0; i < parklist.Count; i++)
            {
                echarModel.categoryList.Add(parklist[i].BerthsecName);
                echarModel.seriesObj.data.Add(parklist[i].FactReceive);
                echarModel.seriesObj2.data.Add(parklist[i].Arrearage);
                echarModel.seriesObj3.data.Add(parklist[i].Cash);
                echarModel.seriesObj4.data.Add(parklist[i].BerthsecByCard);
                echarModel.seriesObj5.data.Add(parklist[i].Receivable);
            }
            //echarModel.series.Add(echarModel.seriesObj);
            //echarModel.series.Add(echarModel.seriesObj2);
            //echarModel.series.Add(echarModel.seriesObj3);
            //echarModel.series.Add(echarModel.seriesObj4);
            //echarModel.series.Add(echarModel.seriesObj5);
            return EcharReturnBerthsec(echarModel);
        }

        /// <summary>
        /// 获取泊位段jqgrid数据
        /// </summary>
        /// <returns></returns>
        public JsonResult BerthsecJqGridData(StaticReportInput input)
        {
            GetAllBerthsecReportOutput businessDetaillist = _staticReportAppService.GetBerthsecReportGrid(input);
            return this.Json(businessDetaillist);
        }

        /// <summary>
        /// 获取时间段泊位段jqgrid数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult TBucketBerthsecJqGridData(StaticReportInput input)
        {
            GetAllBerthsecReportOutput TBucketbusinessDetaillist = _staticReportAppService.GetTBucketBerthsecGrid(input);
            return this.Json(TBucketbusinessDetaillist);
        }

        /// <summary>
        /// 工作组报表JqGrid
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult WorkGroupReportJqGrid(WorkGroupInput input)
        {
            GetAllWorkGroupReport workGroupR = _workGroupRepository.GetWorkGroupReportJqGrid(input);
            return this.Json(workGroupR);
        }

        /// <summary>
        /// 工作组报表Echars数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult WorkGroupReportEchars(WorkGroupInput input)
        {
            EcharModel echarModel = new EcharModel();
            List<string> categoryList = new List<string>();
            List<string> legendList = new List<string>();
            //设置legend数组

            legendList.Add("总预付");
            legendList.Add("总应收");
            legendList.Add("总实收");
            legendList.Add("总欠费");
            legendList.Add("总现金");
            legendList.Add("总刷卡");
            Series seriesObj = new Series();
            // seriesObj2.id = 2;
            seriesObj.type = "line";
            //seriesObj3.barGap = "0%";
            seriesObj.name = "总预付";
            seriesObj.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报
            Series seriesObj2 = new Series();
            // seriesObj2.id = 2;
            seriesObj2.type = "line";
            //seriesObj3.barGap = "0%";
            seriesObj2.name = "总应收";
            seriesObj2.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj3 = new Series();
            // seriesObj2.id = 2;
            seriesObj3.type = "bar";
            //seriesObj3.barGap = "0%";
            seriesObj3.name = "总实收";
            seriesObj3.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj4 = new Series();
            // seriesObj2.id = 2;
            seriesObj4.type = "bar";
            //seriesObj4.barGap = "0%";
            seriesObj4.name = "总欠费";
            seriesObj4.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj5 = new Series();
            // seriesObj2.id = 2;
            seriesObj5.type = "line";
            //seriesObj4.barGap = "0%";
            seriesObj5.name = "总现金";
            seriesObj5.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj6 = new Series();
            // seriesObj2.id = 2;
            seriesObj6.type = "line";
            //seriesObj4.barGap = "0%";
            seriesObj6.name = "总刷卡";
            seriesObj6.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            //Series seriesObj5 = new Series();
            //// seriesObj2.id = 2;
            //seriesObj5.type = "bar";
            ////seriesObj4.barGap = "0%";
            //seriesObj5.name = "总余额";
            //seriesObj5.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            //EcharModel echarModel = new EcharModel();
            echarModel.categoryList = categoryList;
            echarModel.seriesObj = seriesObj;
            echarModel.seriesObj2 = seriesObj2;
            echarModel.seriesObj3 = seriesObj3;
            echarModel.seriesObj4 = seriesObj4;
            echarModel.seriesObj5 = seriesObj5;
            echarModel.seriesObj6 = seriesObj6;
            //echarModel.seriesObj5 = seriesObj5;
            echarModel.legendList = legendList;
            List<WorkGroupEcharsDto> WorkGrouplist = _workGroupRepository.GetWorkGroupReportToList(input);

            for (var i = 0; i < WorkGrouplist.Count; i++)
            {
                echarModel.categoryList.Add(WorkGrouplist[i].WorkGroupName);
                echarModel.seriesObj.data.Add(Convert.ToDecimal(WorkGrouplist[i].WorkGroupPrepaid));//充值
                echarModel.seriesObj2.data.Add(Convert.ToDecimal(WorkGrouplist[i].WorkGroupReceivable));//消费
                echarModel.seriesObj3.data.Add(Convert.ToDecimal(WorkGrouplist[i].WorkGroupFactReceive));//预付
                echarModel.seriesObj4.data.Add(Convert.ToDecimal(WorkGrouplist[i].WorkGroupArrearage));//返还
                echarModel.seriesObj5.data.Add(Convert.ToDecimal(WorkGrouplist[i].WorkGroupCash));//预付
                echarModel.seriesObj6.data.Add(Convert.ToDecimal(WorkGrouplist[i].WorkGroupByCard));//返还
                //echarModel.seriesObj5.data.Add(WorkGrouplist[i].SumWallet_Money);//余额
            }
            List<Series> seriesList = new List<Series>
            {
                echarModel.seriesObj,
                echarModel.seriesObj2,
                echarModel.seriesObj3,
                echarModel.seriesObj4,
                echarModel.seriesObj5,
                echarModel.seriesObj6
            };
            //seriesList.Add(echarModel.seriesObj5);

            //最后调用相关函数将List转换为Json
            //因为我们需要返回category和series、legend多个对象 这里我们自己在new一个新的对象来封装这两个对象
            var newObj = new
            {
                category = echarModel.categoryList,
                series = seriesList,
                legend = echarModel.legendList
            };

            return this.Json(newObj);
        }

        /// <summary>
        /// 获取时间段泊位段Echars数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult TBucketBerthsecEcharData(StaticReportInput input)
        {
            //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类

            EcharModel echarModel = TBucketBerthsecEcharIni();
            //DateTime begindt = DateTime.Parse(input.operateDateBegin + " 00:00:00");
            //DateTime enddt = DateTime.Parse(input.operateDateEnd + " 23:59:59");

            List<GetBerthsecReportTotalOutput> parklist = _staticReportAppService.GetTBucketBerthsecEchar(input);

            for (var i = 0; i < parklist.Count; i++)
            {
                echarModel.categoryList.Add(parklist[i].Time + "\n" + parklist[i].BerthsecName);
                echarModel.seriesObj.data.Add(parklist[i].Prepaid);//预付
                echarModel.seriesObj2.data.Add(parklist[i].Receivable);//应收
                echarModel.seriesObj3.data.Add(parklist[i].FactReceive);//实收
                echarModel.seriesObj4.data.Add(parklist[i].Arrearage);//欠费
                echarModel.seriesObj5.data.Add(parklist[i].Cash);//现金
                echarModel.seriesObj6.data.Add(parklist[i].ByCard);//刷卡
            }
            List<Series> seriesList = new List<Series>
            {
                echarModel.seriesObj,
                echarModel.seriesObj2,
                echarModel.seriesObj3,
                echarModel.seriesObj4,
                echarModel.seriesObj5,
                echarModel.seriesObj6
            };
            //最后调用相关函数将List转换为Json
            //因为我们需要返回category和series、legend多个对象 这里我们自己在new一个新的对象来封装这两个对象
            var newObj = new
            {
                category = echarModel.categoryList,
                series = seriesList,
                legend = echarModel.legendList
            };
            return this.Json(newObj);
            // return EcharReturn(echarModel);
        }

        /// <summary>
        /// 获取公司Echars数据
        /// </summary>
        /// <returns></returns>
        public JsonResult OperatorsCompanyEcharData(StaticReportInput input)
        {
            //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类

            EcharModel echarModel = EcharIniBerthsec();
            //DateTime begindt = DateTime.Parse(operateDateBegin + " 00:00:00");
            //DateTime enddt = DateTime.Parse(operateDateEnd + " 23:59:59");

            List<GetOperatorsCompanyTotalOutput> parklist = _staticReportAppService.GetOperatorsCompanyReportEchar(input);

            for (var i = 0; i < parklist.Count; i++)
            {
                echarModel.categoryList.Add(parklist[i].CompanyName);
                echarModel.seriesObj.data.Add(parklist[i].FactReceive);
                echarModel.seriesObj2.data.Add(parklist[i].Arrearage);
                echarModel.seriesObj3.data.Add(parklist[i].Cash);
                echarModel.seriesObj4.data.Add(parklist[i].ByCard);
                echarModel.seriesObj5.data.Add(parklist[i].Receivable);
            }
            return EcharReturnBerthsec(echarModel);

        }

        /// <summary>
        /// 获取公司jqgrid数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult OperatorsCompanyJqGridData(StaticReportInput input)
        {
            //DateTime begindt = string.IsNullOrWhiteSpace(operateDateBegin) == true ? DateTime.Now.Date : DateTime.Parse(operateDateBegin + " 00:00:00");
            //DateTime enddt = string.IsNullOrWhiteSpace(operateDateEnd) == true ? DateTime.Now.AddDays(1).Date : DateTime.Parse(operateDateEnd + " 23:59:59");

            GetAllOperatorsCompanyReportOutput businessDetaillist = _staticReportAppService.GetOperatorsCompanyReportGrid(input);

            return this.Json(businessDetaillist);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> entitys)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                return new DataTable();
                //throw new Exception("需转换的集合为空");
            }
            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                dt.Columns.Add(entityProperties[i].Name);
            }
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;

        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="rowstart"></param>
        /// <param name="rowend"></param>
        /// <param name="colstart"></param>
        /// <param name="colend"></param>
        public static void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
        {
            CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
            sheet.AddMergedRegion(cellRangeAddress);
        }

        /// <summary>
        /// 处理excel
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static MemoryStream RenderToExcel(DataTable table)
        {
            MemoryStream ms = new MemoryStream();

            using (table)
            {
                IWorkbook workbook = new HSSFWorkbook();
                ISheet sheet = workbook.CreateSheet(table.TableName);
                sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, table.Columns.Count));
                //处理表头
                IRow fieldRow = sheet.CreateRow(0);
                fieldRow.CreateCell(0).SetCellValue(table.TableName);//If Caption not set, returns the ColumnName value
                ICellStyle fieldStyle = workbook.CreateCellStyle();
                fieldStyle.Alignment = HorizontalAlignment.Center;
                IFont fieldfont = workbook.CreateFont();
                fieldfont.FontHeightInPoints = 20;
                fieldfont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                fieldStyle.SetFont(fieldfont);
                fieldStyle.FillBackgroundColor = HSSFColor.BlueGrey.Index;
                fieldRow.GetCell(0).CellStyle = fieldStyle;
                IRow headerRow = sheet.CreateRow(1);
                #region 字段
                // handling header.
                foreach (DataColumn column in table.Columns)
                {
                    string caption = column.Caption.ToString();//SumMoney
                    #region 导出字段转换
                    if (column.Caption.ToString() == "CreationTime")
                        caption = "创建时间";
                    if (column.Caption.ToString() == "SumOwn")
                    {
                        caption = "欠费总额";
                    }
                    if (column.Caption.ToString() == "SumRecovered")
                    {
                        caption = "已追缴";
                    }
                    if (column.Caption.ToString() == "SpanSumRecovered")
                    {
                        caption = "跨月追缴";
                    }
                    if (column.Caption.ToString() == "DelSumRecovered")
                    {
                        caption = "删除历史欠费";
                    }
                    if (column.Caption.ToString() == "SumMoney")
                    {
                        caption = "应收总和";
                    }
                    if (column.Caption.ToString() == "SKSumRepayment")
                    {
                        caption = "刷卡补缴";
                    }
                    if (column.Caption.ToString() == "XJSumRepayment")
                    {
                        caption = "现金补缴";
                    }

                    if (column.Caption.ToString() == "SumRepayment")
                    {
                        caption = "补缴金额";
                    }
                    if (column.Caption.ToString() == "ChargeOperaName")
                    {
                        caption = "收费员名称";
                    }
                    if (column.Caption.ToString() == "EmployeeName")
                    {
                        caption = "收费员名称";
                    }
                    if (column.Caption.ToString() == "FactReceive")
                    {
                        caption = "实收";
                    }
                    if (column.Caption.ToString() == "Arrearage")
                    {
                        caption = "欠费";
                    }
                    if (column.Caption.ToString() == "Repayment")
                    {
                        caption = "欠费补缴";
                    }
                    if (column.Caption.ToString() == "BerthsecName")
                    {
                        caption = "泊位段名称";
                    }
                    if (column.Caption.ToString() == "CarInCount")
                    {
                        caption = "进场车辆数";
                    }
                    if (column.Caption.ToString() == "CarOutCount")
                    {
                        caption = "出场车辆数";
                    }
                    if (column.Caption.ToString() == "Receivable")
                    {
                        caption = "应收";
                    }
                    if (column.Caption.ToString() == "BerthsecByCard")
                    {
                        caption = "刷卡";
                    }
                    if (column.Caption.ToString() == "ByCard")
                    {
                        caption = "刷卡";
                    }
                    if (column.Caption.ToString() == "Cash")
                    {
                        caption = "现金";
                    }
                    if (column.Caption.ToString() == "FactPercent")
                    {
                        caption = "实收比";
                    }
                    if (column.Caption.ToString() == "ByCardPercent")
                    {
                        caption = "刷卡比";
                    }
                    if (column.Caption.ToString() == "CompanyName")
                    {
                        caption = "公司名称";
                    }
                    if (column.Caption.ToString() == "ParkName")
                    {
                        caption = "停车场名称";
                    }
                    if (column.Caption.ToString() == "SumFactReceive")
                    {
                        caption = "实收总和";
                    }
                    if (column.Caption.ToString() == "SumArrearage")
                    {
                        caption = "欠费总和";
                    }
                    if (column.Caption.ToString() == "Time")
                    {
                        caption = "停车时间";
                    }
                    if (column.Caption.ToString() == "StopTimes")
                    {
                        caption = "停车次数";
                    }
                    if (column.Caption.ToString() == "StopTime")
                    {
                        caption = "停车时长";
                    }
                    if (column.Caption.ToString() == "Prepaid")
                    {
                        caption = "预付";
                    }
                    if (column.Caption.ToString() == "WorkGroupName")
                    {
                        caption = "工作组名";
                    }

                    if (column.Caption.ToString() == "OnlineSumFactReceive")
                    {
                        caption = "在线支付";
                    }

                    if (column.Caption.ToString() == "SensorSumReceivable")
                    {
                        caption = "车检器应收";
                    }
                    if (column.Caption.ToString() == "PosInTimes")
                    {
                        caption = "Pos进场次数";
                    }
                    if (column.Caption.ToString() == "PosOutTimes")
                    {
                        caption = "Pos出场次数";
                    }
                    if (column.Caption.ToString() == "SensorTimes")
                    {
                        caption = "地磁停车次数";
                    }
                    if (column.Caption.ToString() == "WorkGroupPrepaid")
                    {
                        caption = "总预付";
                    }
                    if (column.Caption.ToString() == "WorkGroupReceivable")
                    {
                        caption = "总应收";
                    }
                    if (column.Caption.ToString() == "WorkGroupFactReceive")
                    {
                        caption = "总实收";
                    }
                    if (column.Caption.ToString() == "WorkGroupArrearage")
                    {
                        caption = "总欠费";
                    }
                    if (column.Caption.ToString() == "UserName")
                    {
                        caption = "用户名";
                    }
                    if (column.Caption.ToString() == "Name")
                    {
                        caption = "姓名";
                    }
                    if (column.Caption.ToString() == "InTimeStr")
                    {
                        caption = "时间";
                    }
                    if (column.Caption.ToString() == "YearMonth")
                    {
                        caption = "序号";
                    }
                    if (column.Caption.ToString() == "CardNo")
                    {
                        caption = "卡号";
                    }
                    if (column.Caption.ToString() == "TelePhone")
                    {
                        caption = "手机号码";
                    }
                    if (column.Caption.ToString() == "Remark")
                    {
                        caption = "消费类型";
                    }
                    if (column.Caption.ToString() == "Money")
                    {
                        caption = "消费金额";
                    }
                    if (column.Caption.ToString() == "Wallet")
                    {
                        caption = "钱包余额";
                    }
                    if (column.Caption.ToString() == "ChargeOperaName")
                    {
                        caption = "收费员";
                    }
                    if (column.Caption.ToString() == "BdeTenant")
                    {
                        caption = "商户";
                    }
                    if (column.Caption.ToString() == "BdeRegionName")
                    {
                        caption = "区域";
                    }
                    if (column.Caption.ToString() == "BdeParkName")
                    {
                        caption = "停车场";
                    }
                    if (column.Caption.ToString() == "BdeBerthsecName")
                    {
                        caption = "泊位段";
                    }
                    if (column.Caption.ToString() == "BdeBerthNumber")
                    {
                        caption = "泊位号";
                    }
                    if (column.Caption.ToString() == "BdePlateNumber")
                    {
                        caption = "车牌号";
                    }
                    if (column.Caption.ToString() == "BdeCarInTimeString")
                    {
                        caption = "入场时间";
                    }
                    if (column.Caption.ToString() == "BdeCarOutTimeString")
                    {
                        caption = "离场时间";
                    }
                    if (column.Caption.ToString() == "BdeStopTimes")
                    {
                        caption = "停车时长";
                    }
                    if (column.Caption.ToString() == "BdePrepaid")
                    {
                        caption = "预付款";
                    }
                    if (column.Caption.ToString() == "BdeReceivable")
                    {
                        caption = "应收";
                    }
                    if (column.Caption.ToString() == "BdeFactReceive")
                    {
                        caption = "实收";
                    }
                    if (column.Caption.ToString() == "BdeArrearage")
                    {
                        caption = "欠费金额";
                    }
                    if (column.Caption.ToString() == "BdeStatusName")
                    {
                        caption = "状态";
                    }
                    if (column.Caption.ToString() == "BdeStopTypeName")
                    {
                        caption = "停车类型";
                    }
                    if (column.Caption.ToString() == "BdeInEmployeeName")
                    {
                        caption = "入场收费员";
                    }
                    if (column.Caption.ToString() == "BdeInDeviceCode")
                    {
                        caption = "入场设备";
                    }
                    if (column.Caption.ToString() == "BdeOutEmployeeName")
                    {
                        caption = "出场收费员";
                    }
                    if (column.Caption.ToString() == "BdeOutDeviceCode")
                    {
                        caption = "出场设备";
                    }
                    if (column.Caption.ToString() == "BdeChargeEmployeeName")
                    {
                        caption = "收费收费员";
                    }
                    if (column.Caption.ToString() == "BdeChargeDeviceCode")
                    {
                        caption = "收费设备";
                    }
                    if (column.Caption.ToString() == "EdeTenant")
                    {
                        caption = "商户";
                    }
                    if (column.Caption.ToString() == "EdeRegionName")
                    {
                        caption = "区域";
                    }
                    if (column.Caption.ToString() == "EdeParkName")
                    {
                        caption = "停车场";
                    }
                    if (column.Caption.ToString() == "EdeBerthsecName")
                    {
                        caption = "泊位段";
                    }
                    if (column.Caption.ToString() == "EdeBerthNumber")
                    {
                        caption = "泊位号";
                    }
                    if (column.Caption.ToString() == "EdePlateNumber")
                    {
                        caption = "车牌";
                    }
                    if (column.Caption.ToString() == "EdeCarInTimeString")
                    {
                        caption = "入场时间";
                    }
                    if (column.Caption.ToString() == "EdeCarOutTimeString")
                    {
                        caption = "离场时间";
                    }
                    if (column.Caption.ToString() == "EdeStopTimes")
                    {
                        caption = "停车时长";
                    }
                    if (column.Caption.ToString() == "EdePrepaid")
                    {
                        caption = "预支付";
                    }
                    if (column.Caption.ToString() == "EdeReceivable")
                    {
                        caption = "应收";
                    }
                    if (column.Caption.ToString() == "EdeArrearage")
                    {
                        caption = "欠费金额";
                    }
                    if (column.Caption.ToString() == "EdeInEmployeeName")
                    {
                        caption = "入场收费员";
                    }
                    if (column.Caption.ToString() == "EdeInDeviceCode")
                    {
                        caption = "入场设备";
                    }
                    if (column.Caption.ToString() == "EdeOutEmployeeName")
                    {
                        caption = "离场收费员";
                    }
                    if (column.Caption.ToString() == "EdeOutDeviceCode")
                    {
                        caption = "离场设备";
                    }
                    if (column.Caption.ToString() == "EdeEscapeEmployeeName")
                    {
                        caption = "追缴收费员";
                    }
                    if (column.Caption.ToString() == "EdeEscapeDeviceCode")
                    {
                        caption = "追缴设备";
                    }
                    if (column.Caption.ToString() == "EdeStatusName")
                    {
                        caption = "补缴类型";
                    }
                    if (column.Caption.ToString() == "EdeRepayment")
                    {
                        caption = "补缴金额";
                    }
                    if (column.Caption.ToString() == "EdeCarRepaymentTime")
                    {
                        caption = "补缴时间";
                    }
                    if (column.Caption.ToString() == "MBUEId")
                    {
                        caption = "序号";
                    }
                    if (column.Caption.ToString() == "MBUEBerthNumber")
                    {
                        caption = "车位号";
                    }
                    if (column.Caption.ToString() == "MBUEReceivable")
                    {
                        caption = "应收车位费";
                    }
                    if (column.Caption.ToString() == "MBUECashCollection")
                    {
                        caption = "现金收款";
                    }
                    if (column.Caption.ToString() == "MBUEPaybyCardValue")
                    {
                        caption = "刷卡额";
                    }
                    if (column.Caption.ToString() == "MBUETotalConsumption")
                    {
                        caption = "消费小计";
                    }
                    if (column.Caption.ToString() == "MBUEArrearage")
                    {
                        caption = "未收款金额";
                    }
                    if (column.Caption.ToString() == "XJSumFactReceive")
                    {
                        caption = "现金收入";
                    }
                    if (column.Caption.ToString() == "MBUEPByCashRepay")
                    {
                        caption = "现金补缴金额";
                    }
                    if (column.Caption.ToString() == "MBUEPByCardRepay")
                    {
                        caption = "刷卡补缴金额";
                    }
                    if (column.Caption.ToString() == "SKSumFactReceive")
                    {
                        caption = "刷卡收入";
                    }
                    if (column.Caption.ToString() == "Cash")
                    {
                        caption = "现金收入";
                    }
                    if (column.Caption.ToString() == "ByCard")
                    {
                        caption = "刷卡收入";
                    }

                    if (column.Caption.ToString() == "WxSumFactReceive")
                    {
                        caption = "微信收入";
                    }

                    if(column.Caption.ToString() == "WxSumRepaymentReceive")
                    {
                        caption = "微信补缴";
                    }

                    if (column.Caption.ToString() == "WorkGroupCash")
                    {
                        caption = "现金收入";
                    }
                    if (column.Caption.ToString() == "WorkGroupByCard")
                    {
                        caption = "刷卡收入";
                    }
                    if (column.Caption.ToString() == "BdSensorsInCarTimeString")
                    {
                        caption = "车检器入场时间";
                    }
                    if (column.Caption.ToString() == "BdSensorsOutCarTimeString")
                    {
                        caption = "车检器出场时间";
                    }
                    if (column.Caption.ToString() == "BdSensorsStopTimes")
                    {
                        caption = "车检器停车时长";
                    }
                    if (column.Caption.ToString() == "BdSensorsReceivable")
                    {
                        caption = "车检器应收";
                    }

                    if (column.Caption.ToString() == "TheFirstValue")
                    {
                        caption = "首发面值";
                    }


                    if (column.Caption.ToString() == "TheFirstPrice")
                    {
                        caption = "首发售价";
                    }

                    if (column.Caption.ToString() == "EarlyMonthBalance")
                    {
                        caption = "月初结存额";
                    }

                    if (column.Caption.ToString() == "StoredValue")
                    {
                        caption = "储值额";
                    }

                    if (column.Caption.ToString() == "Price")
                    {
                        caption = "售价";
                    }

                    if (column.Caption.ToString() == "CurrentConsumptionValue")
                    {
                        caption = "本期消费额";
                    }

                    if (column.Caption.ToString() == "TheFinalBalance")
                    {
                        caption = "期末结存额";
                    }
                    if (column.Caption.ToString() == "SumIncomePlusBack")
                    {
                        caption = "总收入(收入+补缴)";
                    }

                    if (column.Caption.ToString() == "WxSumRepayment")
                    {
                        caption = "在线补缴";
                    }

                    if (column.Caption.ToString() == "CardMoney")
                    {
                        caption = "账号充值";
                    }

                    if (column.Caption.ToString() == "Yield")
                    {
                        caption = "实收率";
                    }

                    if (column.Caption.ToString() == "AllYield")
                    {
                        caption = "总实收率";
                    }

                    if (column.Caption.ToString() == "VehicleOwner")
                    {
                        caption = "车主姓名";
                    }

                    if (column.Caption.ToString() == "Telphone")
                    {
                        caption = "手机号码";
                    }

                    if (column.Caption.ToString() == "PlateNumber")
                    {
                        caption = "车牌号";
                    }

                    if (column.Caption.ToString() == "Id")
                    {
                        caption = "序号";
                    }

                    if (column.Caption.ToString() == "BeginTimeStr")
                    {
                        caption = "生效日期";
                    }

                    if (column.Caption.ToString() == "EndTimeStr")
                    {
                        caption = "失效日期";
                    }

                    if (column.Caption.ToString() == "MonthyType")
                    {
                        caption = "包月时段";
                    }

                    if (column.Caption.ToString() == "BeginTime")
                    {
                        caption = "生效日期";
                    }

                    if (column.Caption.ToString() == "EndTime")
                    {
                        caption = "失效日期";
                    }

                    if (column.Caption.ToString() == "total_amount")
                    {
                        caption = "交易金额";
                    }

                    if (column.Caption.ToString() == "buyer_logon_id")
                    {
                        caption = "用户名称";
                    }

                    if (column.Caption.ToString() == "body")
                    {
                        caption = "交易主题";
                    }

                    if (column.Caption.ToString() == "subject")
                    {
                        caption = "主题";
                    }

                    if (column.Caption.ToString() == "trade_no")
                    {
                        caption = "交易流水号";
                    }

                    if (column.Caption.ToString() == "gmt_create")
                    {
                        caption = "创建时间";
                    }

                    if (column.Caption.ToString() == "gmt_payment")
                    {
                        caption = "支付时间";
                    }

                    if (column.Caption.ToString() == "trade_status")
                    {
                        caption = "支付状态";
                    }

                    if (column.Caption.ToString() == "PayType")
                    {
                        caption = "业务类型";
                    }

                    if (column.Caption.ToString() == "out_trade_no")
                    {
                        caption = "商户网站唯一订单号";
                    }

                    if (column.Caption.ToString() == "CompanyId")
                    {
                        caption = "分公司编号";
                    }

                    if (column.Caption.ToString() == "TenantId")
                    {
                        caption = "商户编号";
                    }



                    #endregion
                    headerRow.CreateCell(column.Ordinal).SetCellValue(caption);
                    ICellStyle headStyle = workbook.CreateCellStyle();
                    headStyle.Alignment = HorizontalAlignment.Center;
                    headStyle.IsLocked = true;
                    IFont font = workbook.CreateFont();
                    font.FontHeightInPoints = 10;
                    font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                    headStyle.SetFont(font);
                    headerRow.GetCell(column.Ordinal).CellStyle = headStyle;
                }
                #endregion

                // handling value.
                int rowIndex = 2;
                int tempIndex = 2;
                //int sheetNum = 1;

                foreach (DataRow row in table.Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in table.Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }
                    if (tempIndex == 65535)
                    {
                        //sheetNum++;
                        sheet = workbook.CreateSheet();
                        tempIndex = 1;
                        rowIndex = 1;
                    }
                    rowIndex++;
                    tempIndex++;
                }
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
            }
            return ms;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public MemoryStream RenderToExcelOnlyMonth(DataTable table)
        {
            MemoryStream ms = new MemoryStream();
            using (table)
            {
                IWorkbook workbook = new HSSFWorkbook();
                ISheet sheet = workbook.CreateSheet();
                IRow headerRow = sheet.CreateRow(0);
                foreach (DataColumn column in table.Columns)
                {
                    string caption = column.Caption.ToString();//SumMoney
                    var entity = _exportCodeAppService.GetExportCodeDefault(caption);
                    if (entity != default(ExportCodeDto))
                        caption = _exportCodeAppService.GetExportCodeDefault(caption).Value;
                    headerRow.CreateCell(column.Ordinal).SetCellValue(caption);//If Caption not set, returns the ColumnName value
                    ICellStyle headStyle = workbook.CreateCellStyle();
                    headStyle.Alignment = HorizontalAlignment.Center;
                    IFont font = workbook.CreateFont();
                    font.FontHeightInPoints = 10;
                    font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                    headStyle.SetFont(font);
                    headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                }

                // handling value.
                int rowIndex = 1;
                int tempIndex = 1;
                //int sheetNum = 1;

                foreach (DataRow row in table.Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in table.Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }
                    if (tempIndex == 65535)
                    {
                        //sheetNum++;
                        sheet = workbook.CreateSheet();
                        tempIndex = 1;
                        rowIndex = 1;
                    }
                    rowIndex++;
                    tempIndex++;
                }
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
            }
            return ms;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="fileName"></param>
        static void SaveToFile(MemoryStream ms, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
                data = null;
            }
        }

        /// <summary>
        /// Excel导出
        /// </summary>
        /// <param name="dt"></param>
        public void Export1(DataTable dt)
        {
            string filename = dt.TableName;
            MemoryStream ms = RenderToExcel(dt);
            //如果不存在就创建file文件夹
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Report/") == false)
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Report/");
            }
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "/Report/", Path.GetFileName(filename + ".xls"));
            SaveToFile(ms, path);
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-download";
            FileInfo file = new FileInfo(path);
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.AddHeader("Content-Type", "application/octet-stream");
            System.Web.HttpContext.Current.Response.Charset = "utf-8";
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition",
                "attachment;filename=" + System.Web.HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8));
            System.Web.HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            System.Web.HttpContext.Current.Response.WriteFile(file.FullName);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.End();
            //return "/UploadFiles/ExportExcel/" + filename + ".xls";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        public void ExportOnlyMonth(DataTable dt)
        {
            string filename = dt.TableName + "报表" + DateTime.Now.ToFileTimeUtc().ToString();
            MemoryStream ms = RenderToExcelOnlyMonth(dt);
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "/Report/",
                Path.GetFileName(filename + ".xls"));
            SaveToFile(ms, path);
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-download";
            FileInfo file = new FileInfo(path);
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.AddHeader("Content-Type", "application/octet-stream");
            System.Web.HttpContext.Current.Response.Charset = "utf-8";
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8));
            System.Web.HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            System.Web.HttpContext.Current.Response.WriteFile(file.FullName);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.End();
            //return "/UploadFiles/ExportExcel/" + filename + ".xls";
        }

        /// <summary>
        /// 下载地磁数据导入模板
        /// </summary>
        [AbpMvcAuthorize("InspectionSetting.Field1")]
        public void DownLoadExcel()
        {
            IWorkbook workbook = new NPOI.XSSF.UserModel.XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("地磁数据导入模板"); //创建sheet
            IRow row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("序号");     //创建单元格并给单元格赋值
            row.CreateCell(1).SetCellValue("停车场编号");
            row.CreateCell(2).SetCellValue("停车场名称");
            row.CreateCell(3).SetCellValue("泊位段编号");
            row.CreateCell(4).SetCellValue("泊位段名称");
            row.CreateCell(5).SetCellValue("泊位号");
            row.CreateCell(6).SetCellValue("地磁编号");
            MemoryStream memoryStream = new MemoryStream();//创建内存流
            workbook.Write(memoryStream);//npoi将创建好的工作簿写入到内存流
            System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + "地磁数据导入模板.xlsx");
            System.Web.HttpContext.Current.Response.BinaryWrite(memoryStream.ToArray());
            System.Web.HttpContext.Current.Response.End();
            memoryStream.Dispose();
        }

        /// <summary>
        /// 上载地磁数据
        /// </summary>
        [AbpMvcAuthorize("InspectionSetting.Field2")]
        public string UpLoadExcel()
        {
            HttpContext context = System.Web.HttpContext.Current;
            HttpPostedFile uploadFile = context.Request.Files["file"];
            string savePath = AppDomain.CurrentDomain.BaseDirectory + "/Excel/上载Excel文件/";
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            string fileName = uploadFile.FileName.Replace(".xlsx", DateTime.Now.ToFileTimeUtc().ToString() + ".xlsx");
            string path = Path.Combine(savePath, fileName);
            uploadFile.SaveAs(path);             //保存上载文件

            IWorkbook workbook = null;
            FileStream fileStream = null;
            ISheet sheet = null;
            int startRow = 0;
            fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            workbook = new NPOI.XSSF.UserModel.XSSFWorkbook(fileStream);
            sheet = workbook.GetSheetAt(0);
            IRow firstRow = sheet.GetRow(0);
            ICell cellvalue = firstRow.CreateCell(7);//在行中添加一列
            cellvalue.SetCellValue("错误消息详情");//设置列的内容
            int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数         
            startRow = sheet.FirstRowNum + 1;
            //最后一行的标号
            int rowCount = sheet.LastRowNum;
            for (int i = startRow; i <= rowCount; ++i)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) continue;      //没有数据的行默认是null　　　
                ICell errValue = row.CreateCell(7);//在行中添加一列
                int RegionId = 0;
                long BerthsId = 0;
                int BerthsesId = 0;
                int parkId = 0;
                string BerthNumber = "0";
                string ErrorMsg = "";
                for (int j = row.FirstCellNum; j < cellCount - 1; ++j)
                {
                    var columnName = firstRow.Cells[j].ToString();
                    if (row.GetCell(j) != null && !string.IsNullOrEmpty(row.GetCell(j).ToString()))
                    {
                        var number = row.GetCell(j).ToString();
                        if (columnName == "停车场编号")
                        {
                            parkId = Convert.ToInt32(number);
                            if (_parkRepository.Count(entity => entity.Id == parkId) == 0)
                            {
                                ErrorMsg = "停车场编号错误；";
                            }
                        }
                        else if (columnName == "泊位段编号")
                        {
                            var berthId = Convert.ToInt32(number);
                            if (_abpBerthsecRepository.Count(entity => entity.Id == berthId) == 0)
                            {
                                ErrorMsg = ErrorMsg + "泊位段编号错误；";
                            }
                        }
                        else if (columnName == "泊位号")
                        {
                            var model = _berthRepository.FirstOrDefault(entity => entity.BerthNumber == number);
                            if (model == null)
                            {
                                ErrorMsg = ErrorMsg + "泊位号错误；";
                            }
                            else
                            {
                                BerthsId = model.Id;
                                BerthsesId = model.BerthsecId;
                                BerthNumber = model.BerthNumber;
                                RegionId = model.RegionId;
                            }
                        }
                        else if (columnName == "地磁编号" && ErrorMsg == "")
                        {
                            number = number.Replace("'", "");
                            var sensorsModel = _sensorRepository.FirstOrDefault(entity => entity.SensorNumber == number);
                            if (sensorsModel == null)     
                            {
                                Sensor sensorDto = new Sensor
                                {
                                    TenantId = AbpSession.TenantId.Value,
                                    CompanyId = AbpSession.CompanyId.Value,
                                    RegionId = RegionId,
                                    ParkId = parkId,
                                    BerthsecId = BerthsesId,
                                    Magnetism = 1,
                                    TransmitterNumber = "1",
                                    SensorNumber = number,
                                    CreationTime = DateTime.Now,
                                    BerthNumber = BerthNumber,
                                    ParkStatus = 0,
                                    Receivable = 0,

                                };
                                _sensorRepository.Insert(sensorDto);
                            }
                            _berthRepository.Update(BerthsId, entry => entry.SensorNumber = number);
                            ErrorMsg = ErrorMsg + "成功上传；";
                        }
                    }
                    else
                    {
                        ErrorMsg = ErrorMsg + "这一行有空值；";
                    }
                }
                errValue.SetCellValue(ErrorMsg);

            }

            string errPath = AppDomain.CurrentDomain.BaseDirectory + "/Excel/返回Excel文件/";
            if (!Directory.Exists(errPath))
            {
                Directory.CreateDirectory(errPath);
            }
            string errMsgPath = Path.Combine(errPath, Path.GetFileName(fileName));
            FileStream fs = new FileStream(errMsgPath, FileMode.Create, FileAccess.Write);
            workbook.Write(fs);//向打开的这个Excel文件中写入表单并保存。  
            fs.Close();
            return fileName;
        }


        public void Downloadmes(string Result)
        {
            string path = Result;// Result.Substring(0, Result.Length);
            var FilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/Excel/返回Excel文件/" + path);
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-download";
            FileInfo file = new FileInfo(FilePath);
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.AddHeader("Content-Type", "application/octet-stream");
            System.Web.HttpContext.Current.Response.Charset = "utf-8";
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8));
            System.Web.HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            System.Web.HttpContext.Current.Response.WriteFile(file.FullName);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.End();
        }
    }
}


