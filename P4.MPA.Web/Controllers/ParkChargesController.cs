using Abp.Web.Mvc.Models;
using P4.Berthsecs;
using P4.ParkCharges;
using P4.ParkCharges.Dto;
using P4.Parks;
using P4.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace P4.Web.Controllers
{
    /// <summary>
    /// 停车场收费
    /// </summary>
    public class ParkChargesController : P4ControllerBase
    {
        #region Var
        private readonly IParkChargesAppService _parkChargesAppService;
        private readonly IParkAppService _parkAppService;
        private readonly IBerthsecAppService _berthsecAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parkChargesAppService"></param>
        /// <param name="parkAppService"></param>
        /// <param name="berthsecAppService"></param>
        public ParkChargesController(IParkChargesAppService parkChargesAppService, IParkAppService parkAppService, IBerthsecAppService berthsecAppService)
        {
            _parkChargesAppService = parkChargesAppService;
            _parkAppService = parkAppService;
            _berthsecAppService = berthsecAppService;
        }

        /// <summary>
        ///停车场收费日报
        /// </summary>
        /// <returns></returns>
        public ActionResult ParkChargesDayReport()
        {
            ParkChargesModel model = new ParkChargesModel();
            model.ParkList = _parkAppService.GetParkAll();
            model.AllOption = alloption;
            return View(model);
        }

        /// <summary>
        /// 泊位段日报
        /// </summary>
        /// <returns></returns>
        public ActionResult BerthsecChargesDayReport()
        {
            ParkChargesModel model = new ParkChargesModel();
            model.BerthsecList = _berthsecAppService.GetAllBerthsec();
            model.AllOption = alloption;
            return View(model);
        }

        /// <summary>
        /// 获取Echars数据
        /// </summary>
        /// <returns></returns>
        public JsonResult EcharData(ParkChargeInput input)
        {
            //为了演示效果 这里采用随机数据来代替 后期可以根据自己情况换成访问数据获取数据
            //考虑到图表的category是字符串数组 这里定义一个string的List
            List<string> categoryList = new List<string>();
            //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类
            List<Series> seriesList = new List<Series>();

            //考虑到Echarts图表需要设置legend内的data数组为series的name集合这里需要定义一个legend数组
            List<string> legendList = new List<string>();
            //设置legend数组
            legendList.Add("实收"); //这里的名称必须和series的每一组series的name保持一致
            legendList.Add("未收");
            legendList.Add("现金");
            legendList.Add("账号");
            legendList.Add("应收");

            //定义一个Series对象
            Series seriesObj = new Series();
            seriesObj.id = 0;
            seriesObj.name = "实收";
            seriesObj.type = "line"; //线性图呈现
            seriesObj.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj2 = new Series();
            seriesObj2.id = 1;
            seriesObj2.type = "line";
            seriesObj2.name = "未收";
            seriesObj2.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj3 = new Series();
            seriesObj3.id = 2;
            seriesObj3.type = "line";
            seriesObj3.name = "现金";
            seriesObj3.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj4 = new Series();
            seriesObj4.id = 3;
            seriesObj4.type = "line";
            seriesObj4.name = "账号";
            seriesObj4.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj5 = new Series();
            seriesObj5.id = 4;
            seriesObj5.type = "line";
            seriesObj5.name = "应收";
            seriesObj5.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            //DateTime begindt = DateTime.Parse(operateDateBegin + " 00:00:00");
            //DateTime enddt = DateTime.Parse(operateDateEnd + " 23:59:59");
            //dt = DateTime.Parse("2015-09-28 00:00:00");
            List<ParkChargesDto> parkchargeslist = _parkChargesAppService.GetParkChargesDayReport(input);

            for (var i = 0; i < parkchargeslist.Count; i++)
            {
                categoryList.Add(parkchargeslist[i].ParkName);
                seriesObj.data.Add(parkchargeslist[i].SumFactReceive.Value);
                seriesObj2.data.Add(parkchargeslist[i].SumArrearage.Value);
                seriesObj3.data.Add(Convert.ToDecimal(parkchargeslist[i].XJSumFactReceive));
                seriesObj4.data.Add(Convert.ToDecimal(parkchargeslist[i].SKSumFactReceive));
                seriesObj5.data.Add(Convert.ToDecimal(parkchargeslist[i].SumMoney));
            }
            //for (var i = 0; i < 3; i++)
            //{
            //    categoryList.Add(i.ToString());
            //    seriesObj.data.Add(i*10);
            //    seriesObj2.data.Add((i+1)*20);

            //}
            seriesList.Add(seriesObj);
            seriesList.Add(seriesObj2);
            seriesList.Add(seriesObj3);
            seriesList.Add(seriesObj4);
            seriesList.Add(seriesObj5);


            //最后调用相关函数将List转换为Json
            //因为我们需要返回category和series、legend多个对象 这里我们自己在new一个新的对象来封装这两个对象
            var newObj = new
            {
                category = categoryList,
                series = seriesList,
                legend = legendList
            };

            return this.Json(newObj);
            //Response返回新对象的json数据

            //Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(newObj, Newtonsoft.Json.Formatting.None));

            //Response.End();

        }
        /// <summary>
        /// 获取jqgrid数据
        /// </summary>
        /// <returns></returns>
        public JsonResult JqGridData(ParkChargeInput input)
        {
        
            //DateTime begindt = string.IsNullOrWhiteSpace(operateDateBegin) == true ? DateTime.Now.Date : DateTime.Parse(operateDateBegin + " 00:00:00");
            //DateTime enddt = string.IsNullOrWhiteSpace(operateDateEnd) == true ? DateTime.Now.AddDays(1).Date : DateTime.Parse(operateDateEnd + " 23:59:59");
            //string parkId = parkIdInput == null ? "" : parkIdInput;
            //string parkName = parkNameInput == null ? "" : parkNameInput;

            GetAllParkChargesOutput businessDetaillist = _parkChargesAppService.GetParkChargesDayReport1(input);
            return Json(businessDetaillist);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult BerthsecJqGridData(ParkChargeInput input)
        {
            GetAllParkChargesOutput businessDetaillist = _parkChargesAppService.GetBerthsecChargesDayReport(input);

            return Json(businessDetaillist);
        }

        /// <summary>
        /// 获取Echars数据
        /// </summary>
        /// <returns></returns>
        public JsonResult BerthsecEcharData(ParkChargeInput input)
        {
            //为了演示效果 这里采用随机数据来代替 后期可以根据自己情况换成访问数据获取数据
            //考虑到图表的category是字符串数组 这里定义一个string的List
            List<string> categoryList = new List<string>();
            //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类
            List<Series> seriesList = new List<Series>();

            //考虑到Echarts图表需要设置legend内的data数组为series的name集合这里需要定义一个legend数组
            List<string> legendList = new List<string>();
            //设置legend数组
            legendList.Add("实收"); //这里的名称必须和series的每一组series的name保持一致
            legendList.Add("未收");
            legendList.Add("现金");
            legendList.Add("账号");
            legendList.Add("应收");

            //定义一个Series对象
            Series seriesObj = new Series();
            seriesObj.id = 0;
            seriesObj.name = "实收";
            seriesObj.type = "line"; //线性图呈现
            seriesObj.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj2 = new Series();
            seriesObj2.id = 1;
            seriesObj2.type = "line";
            seriesObj2.name = "未收";
            seriesObj2.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj3 = new Series();
            seriesObj3.id = 2;
            seriesObj3.type = "line";
            seriesObj3.name = "现金";
            seriesObj3.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj4 = new Series();
            seriesObj4.id = 3;
            seriesObj4.type = "line";
            seriesObj4.name = "账号";
            seriesObj4.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj5 = new Series();
            seriesObj5.id = 4;
            seriesObj5.type = "line";
            seriesObj5.name = "应收";
            seriesObj5.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            //DateTime begindt = DateTime.Parse(operateDateBegin + " 00:00:00");
            //DateTime enddt = DateTime.Parse(operateDateEnd + " 23:59:59");
            //dt = DateTime.Parse("2015-09-28 00:00:00");
            List<ParkChargesDto> parkchargeslist = _parkChargesAppService.GetBerthsecChargesDayReportEchar(input);

            for (var i = 0; i < parkchargeslist.Count; i++)
            {
                categoryList.Add(parkchargeslist[i].ParkName);
                seriesObj.data.Add(parkchargeslist[i].SumFactReceive.Value);
                seriesObj2.data.Add(parkchargeslist[i].SumArrearage.Value);
                seriesObj3.data.Add(Convert.ToDecimal(parkchargeslist[i].XJSumFactReceive));
                seriesObj4.data.Add(Convert.ToDecimal(parkchargeslist[i].SKSumFactReceive));
                seriesObj5.data.Add(Convert.ToDecimal(parkchargeslist[i].SumMoney));
            }
            //for (var i = 0; i < 3; i++)
            //{
            //    categoryList.Add(i.ToString());
            //    seriesObj.data.Add(i*10);
            //    seriesObj2.data.Add((i+1)*20);

            //}
            seriesList.Add(seriesObj);
            seriesList.Add(seriesObj2);
            seriesList.Add(seriesObj3);
            seriesList.Add(seriesObj4);
            seriesList.Add(seriesObj5);


            //最后调用相关函数将List转换为Json
            //因为我们需要返回category和series、legend多个对象 这里我们自己在new一个新的对象来封装这两个对象
            var newObj = new
            {
                category = categoryList,
                series = seriesList,
                legend = legendList
            };

            return this.Json(newObj);
            //Response返回新对象的json数据

            //Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(newObj, Newtonsoft.Json.Formatting.None));

            //Response.End();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult ProcessRoles()
        {
            string oper = "add";
            JsonResult returnJson = new JsonResult();
            switch (oper)
            {
                case "add":
                    returnJson = this.Json("12 ");
                    break;
                case "del":
                    returnJson = this.Json("12 ");
                    break;
                case "edit":
                    returnJson = this.Json("12 ");
                    break;
                default:
                    break;

            }
            return this.Json(new MvcAjaxResponse());
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class WorldModel
    {

        private string m_Territory;
        private string m_Country;
        private string m_Year;
        private string m_Stats;

        /// <summary>
        /// 
        /// </summary>
        public string Territory
        {
            get { return m_Territory; }

            set { value = m_Territory; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Country
        {
            get { return m_Country; }

            set { value = m_Country; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Year
        {
            get { return m_Year; }

            set { value = m_Year; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Stats
        {
            get { return m_Stats; }

            set { value = m_Stats; }
        }

        /// <summary>
        /// 
        /// </summary>
        public WorldModel() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="territory"></param>
        /// <param name="country"></param>
        /// <param name="year"></param>
        /// <param name="stats"></param>
        public WorldModel(string territory, string country, string year, string stats)
        {
            m_Territory = territory;
            m_Year = year;
            m_Country = country;
            m_Stats = stats;
        }
    }
}