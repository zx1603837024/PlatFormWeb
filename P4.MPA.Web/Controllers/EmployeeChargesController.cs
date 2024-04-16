using Abp.Web.Mvc.Models;
using P4.EmployeeCharges;
using P4.EmployeeCharges.Dto;
using P4.Employees;
using P4.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeChargesController : P4ControllerBase
    {
         #region Var
        private readonly IEmployeeChargesAppService _employeeChargesAppService;
        private readonly IEmployeeAppService _employeeAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeChargesAppService"></param>
        /// <param name="employeeAppService"></param>
        public EmployeeChargesController(IEmployeeChargesAppService employeeChargesAppService, IEmployeeAppService employeeAppService)
        {
            _employeeChargesAppService = employeeChargesAppService;
            _employeeAppService = employeeAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: EmployeeCharges
        public ActionResult EmployeeChargesDayReport()
        {
            EmployeeChargesModel model = new EmployeeChargesModel();
            model.EmployeeList = _employeeAppService.GetEmployeeAll();
            model.AllOption = alloption;
            return View(model);
        }
        public ActionResult ChargeRateDayReport()
        {
            EmployeeChargesModel model = new EmployeeChargesModel();
            model.EmployeeList = _employeeAppService.GetEmployeeAll();
            model.AllOption = alloption;
            return View(model);
        }
        

        /// <summary>
        /// 获取Echars数据
        /// </summary>
        /// <returns></returns>
        public JsonResult EcharData(EmployeeChargeInput input)
        {
            try
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

                //DateTime begindt = DateTime.Parse(operateDateBegin + " 00:00:00");
                //DateTime enddt = DateTime.Parse(operateDateEnd + " 23:59:59");
                //string employeeId = employeeIdInput == null ? "" : employeeIdInput;
                //string employeeName = employeeNameInput == null ? "" : employeeNameInput;
                //dt = DateTime.Parse("2015-09-28 00:00:00");
                List<EmployeeChargesDto> employeechargeslist = _employeeChargesAppService.GetEmployeeChargesDayReport(input);

                for (var i = 0; i < employeechargeslist.Count; i++)
                {
                    categoryList.Add(employeechargeslist[i].ChargeOperaName);
                    seriesObj.data.Add(employeechargeslist[i].SumFactReceive);
                    seriesObj2.data.Add(employeechargeslist[i].SumArrearage);
                    seriesObj3.data.Add(Convert.ToDecimal(employeechargeslist[i].XJSumFactReceive));
                    seriesObj4.data.Add(Convert.ToDecimal(employeechargeslist[i].SKSumFactReceive));
                    seriesObj5.data.Add(Convert.ToDecimal(employeechargeslist[i].SumMoney));

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
            }
            catch (Exception ex)
            {
                return this.Json(ex.Message);
            }
        }
        /// <summary>
        /// 获取jqgrid数据 收费员报表
        /// </summary>
        /// <returns></returns>
        public JsonResult JqGridData(EmployeeChargeInput input)
        {
            //DateTime begindt = string.IsNullOrWhiteSpace(operateDateBegin) == true ? DateTime.Now.Date : DateTime.Parse(operateDateBegin + " 00:00:00");
            //DateTime enddt = string.IsNullOrWhiteSpace(operateDateEnd) == true ? DateTime.Now.AddDays(1).Date : DateTime.Parse(operateDateEnd + " 23:59:59");
            //string employeeId = employeeIdInput == null ? "" : employeeIdInput;
            //string employeeName = employeeNameInput == null ? "" : employeeNameInput;
            input.TenantId = AbpSession.TenantId;
            input.CompanyId = AbpSession.CompanyId;
            input.BerthsecIds = string.Join(",",AbpSession.BerthsecIds??new List<int>());
            GetAllEmployeeChargesOutput businessDetaillist = _employeeChargesAppService.GetEmployeeChargesDayReport1(input);
            return this.Json(businessDetaillist);
        }


        public JsonResult RateEcharData(EmployeeChargeInput input)
        {
            try
            {
                //为了演示效果 这里采用随机数据来代替 后期可以根据自己情况换成访问数据获取数据
                //考虑到图表的category是字符串数组 这里定义一个string的List
                List<string> categoryList = new List<string>();
                //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类
                List<EchartsData> seriesList = new List<EchartsData>();

                //考虑到Echarts图表需要设置legend内的data数组为series的name集合这里需要定义一个legend数组
                List<string> legendList = new List<string>();
                //设置legend数组
                legendList.Add("应收"); //这里的名称必须和series的每一组series的name保持一致
                legendList.Add("未收");
                legendList.Add("PDA入场次数");
                legendList.Add("NB地磁入场次数");
                legendList.Add("取证收费率");

                //定义一个Series对象
                EchartsData seriesObj = new EchartsData();
                //  seriesObj.id = 1;
                seriesObj.name = "应收";
                seriesObj.type = "line"; //线性图呈现
                seriesObj.data = new List<string>(); //先初始化 不初始化后面直直接data.Add(x)会报错

                EchartsData seriesObj2 = new EchartsData();
                // seriesObj2.id = 2;
                seriesObj2.type = "line";
                seriesObj2.name = "未收";
                seriesObj2.data = new List<string>(); //先初始化 不初始化后面直直接data.Add(x)会报错

                EchartsData seriesObj3 = new EchartsData();
                // seriesObj2.id = 2;
                seriesObj3.type = "line";
                seriesObj3.name = "PDA入场次数";
                seriesObj3.data = new List<string>(); //先初始化 不初始化后面直直接data.Add(x)会报错

                EchartsData seriesObj4 = new EchartsData();
                // seriesObj2.id = 2;
                seriesObj4.type = "line";
                seriesObj4.name = "NB地磁入场次数";
                seriesObj4.data = new List<string>(); //先初始化 不初始化后面直直接data.Add(x)会报错

                EchartsData seriesObj5 = new EchartsData();
                // seriesObj2.id = 2;
                seriesObj5.type = "line";
                seriesObj5.name = "取证收费率";
                seriesObj5.data = new List<string>(); //先初始化 不初始化后面直直接data.Add(x)会报错

                //DateTime begindt = DateTime.Parse(operateDateBegin + " 00:00:00");
                //DateTime enddt = DateTime.Parse(operateDateEnd + " 23:59:59");
                //string employeeId = employeeIdInput == null ? "" : employeeIdInput;
                //string employeeName = employeeNameInput == null ? "" : employeeNameInput;
                //dt = DateTime.Parse("2015-09-28 00:00:00");
                List<EmployeeChargesDto> employeechargeslist = _employeeChargesAppService.GetChargesRateDayEcharReport(input);

                for (var i = 0; i < employeechargeslist.Count; i++)
                {
                    categoryList.Add(employeechargeslist[i].Date);
                    seriesObj.data.Add(employeechargeslist[i].SumMoney.ToString());
                    seriesObj2.data.Add(employeechargeslist[i].SumArrearage.ToString());
                    seriesObj3.data.Add(employeechargeslist[i].CarInCount.ToString());
                    seriesObj4.data.Add(employeechargeslist[i].CarOutCount.ToString());
                    seriesObj5.data.Add(employeechargeslist[i].Teaching);

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
            }
            catch (Exception ex)
            {
                return this.Json(ex.Message);
            }
        }
        /// <summary>
        /// 获取jqgrid数据 收费率报表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetRateGridList(EmployeeChargeInput input)
        {
            //DateTime begindt = string.IsNullOrWhiteSpace(operateDateBegin) == true ? DateTime.Now.Date : DateTime.Parse(operateDateBegin + " 00:00:00");
            //DateTime enddt = string.IsNullOrWhiteSpace(operateDateEnd) == true ? DateTime.Now.AddDays(1).Date : DateTime.Parse(operateDateEnd + " 23:59:59");
            //string employeeId = employeeIdInput == null ? "" : employeeIdInput;
            //string employeeName = employeeNameInput == null ? "" : employeeNameInput;
            input.TenantId = AbpSession.TenantId;
            input.CompanyId = AbpSession.CompanyId;
            input.BerthsecIds = "";
            foreach (int v in AbpSession.BerthsecIds)
            {
                input.BerthsecIds += v + ",";
            }
            input.BerthsecIds = input.BerthsecIds.Substring(0, input.BerthsecIds.Length - 1);
            GetAllEmployeeChargesOutput businessDetaillist = _employeeChargesAppService.GetChargeRateDayReport(input);
            return this.Json(businessDetaillist);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult EmployeeChargesDetail(string Id, EmployeeChargeInput input)
        {
            input.TenantId = AbpSession.TenantId;
            input.CompanyId = AbpSession.CompanyId;
            input.BerthsecIds = "";
            foreach (int v in (AbpSession.BerthsecIds??new List<int>()))
            {
                input.BerthsecIds += v + ",";
            }
            if (!string.IsNullOrWhiteSpace(input.BerthsecIds))
            {
                input.BerthsecIds = input.BerthsecIds.Substring(0, input.BerthsecIds.Length - 1);
            }
            GetAllEmployeeChargesOutput businessDetaillist = _employeeChargesAppService.GetEmployeeChargesDayReportDetail(input);
            return Json(businessDetaillist);
        }


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
  
}