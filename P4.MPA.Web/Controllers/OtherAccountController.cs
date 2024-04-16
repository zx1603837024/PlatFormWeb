using Abp.Domain.Repositories;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using F2.OptionSystem.Service;
using F2.OptionSystem.Service.impl;
using F2.SystemWork.Models;
using F2.SystemWork.Services;
using P4.Employees;
using P4.OtherAccounts;
using P4.OtherAccounts.Dtos;
using P4.OtherPlateNumbers;
using P4.SmsManagements;
using P4.SmsManagements.Dtos;
using P4.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace P4.Web.Controllers
{
    [AbpMvcAuthorize]
    public class OtherAccountController : P4ControllerBase
    {
        #region Var
        private readonly IOtherAccountAppService _otherAccountAppService;
        private readonly IOtherPlateNumberAppService _otherPlateNumberAppService;
        private readonly ISmsManagementAppService _smsManagementAppService;
        private readonly IEmployeeAppService _employeeAppService;
        #endregion

        public OtherAccountController(IOtherAccountAppService otherAccountAppService, IOtherPlateNumberAppService otherPlateNumberAppService, ISmsManagementAppService smsManagementAppService, IEmployeeAppService employeeAppService)
        {
            _otherAccountAppService = otherAccountAppService;
            _otherPlateNumberAppService = otherPlateNumberAppService;
            _smsManagementAppService = smsManagementAppService;
            _employeeAppService = employeeAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("MoneCar")]
        public ActionResult OtherAccounts()
        {
            EmployeeChargesModel model = new EmployeeChargesModel();
            model.EmployeeList = _employeeAppService.GetEmployeeAll();
            model.AllOption = alloption;
            return View(model);
        }
        /// <summary>
        /// Mone消费详情
        /// </summary>
        /// <returns></returns>
        public ActionResult DynamicAccountRecordsReport(SearchDeductionRecordInput input)
        {
            AccountAndDeductionRecordModel model = new AccountAndDeductionRecordModel();
            List<AccountAndDeductionRecordsDto> chen = new List<AccountAndDeductionRecordsDto>();
            AccountAndDeductionRecordsDto aaa = new AccountAndDeductionRecordsDto()
            {
                Id = 0,
                CardNo = "0",
                TheFirstValue = 0,
                TheFirstPrice = 0,
                EarlyMonthBalance = 0,
                StoredValue = 0,
                Price = 0,
                CurrentConsumptionValue = 0,
                TheFinalBalance = 0
            };
            chen.Add(aaa);
            model.AccountList = chen;
            return View(model);
        }


        public ActionResult DynamicAccountOnlyMonth(SearchDeductionRecordInput input)
        {
            //AccountAndDeductionRecordModel model = new AccountAndDeductionRecordModel();
            //List<AccountAndDeductionRecordsDto> chen = new List<AccountAndDeductionRecordsDto>();
            //AccountAndDeductionRecordsDto aaa = new AccountAndDeductionRecordsDto()
            //{
            //    Id = 0,
            //    CardNo = "0",
            //    TheFirstValue = 0,
            //    TheFirstPrice = 0,
            //    EarlyMonthBalance = 0,
            //    StoredValue = 0,
            //    Price = 0,
            //    CurrentConsumptionValue = 0,
            //    TheFinalBalance = 0
            //};
            //chen.Add(aaa);
            //model.AccountList = chen;
            AccountAndDeductionRecordModel model = new AccountAndDeductionRecordModel();
            model.AccountList = _otherAccountAppService.GetAccountAndDeductionRecordsOnlyMonth(input);
            return View(model);
        }

        /// <summary>
        /// 搜索Mone卡消费详情(月)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult AccountRecordsDataReportOnlyMonth(SearchDeductionRecordInput input)
        {
            AccountAndDeductionRecordModel model = new AccountAndDeductionRecordModel();
            model.AccountList = _otherAccountAppService.GetAccountAndDeductionRecordsOnlyMonth(input);
     
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(model.AccountList),
                ContentType = "application/json"
            };
        }

        public ActionResult AccountRecordsDataReport(SearchDeductionRecordInput input)
        {
            AccountAndDeductionRecordModel model = new AccountAndDeductionRecordModel();
            model.AccountList = _otherAccountAppService.GetAccountAndDeductionRecords(input);
            //return this.Json(model.AccountList);
            return new ContentResult
           {
               Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(model.AccountList),
               ContentType = "application/json"
           };
        }

        /// <summary>
        /// Mone消费详情JqGridData
        /// </summary>
        /// <returns></returns>
        public JsonResult AccountRecordsJqGridData(SearchDeductionRecordInput input)
        {
            GetAllDeductionRecordOutput deductionrecord = _otherAccountAppService.GetDynamicReportDeductionRecord(input);
            return this.Json(deductionrecord);
        }
        /// <summary>
        /// Echar数据  （统计Mone时间段商户总消费金额、总余额、总返还、总预付）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult AccountRecordsEchar(SearchDeductionRecordInput input)
        {
            EcharModel echarModel = new EcharModel();
            List<string> categoryList = new List<string>();
            List<string> legendList = new List<string>();
            //设置legend数组

            legendList.Add("总充值");
            legendList.Add("总消费");
            legendList.Add("总预付");
            legendList.Add("总返还");
            legendList.Add("总余额");

            Series seriesObj = new Series();
            // seriesObj2.id = 2;
            seriesObj.type = "bar";
            //seriesObj3.barGap = "0%";
            seriesObj.name = "总充值";
            seriesObj.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj2 = new Series();
            // seriesObj2.id = 2;
            seriesObj2.type = "bar";
            //seriesObj3.barGap = "0%";
            seriesObj2.name = "总消费";
            seriesObj2.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj3 = new Series();
            // seriesObj2.id = 2;
            seriesObj3.type = "bar";
            //seriesObj3.barGap = "0%";
            seriesObj3.name = "总预付";
            seriesObj3.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj4 = new Series();
            // seriesObj2.id = 2;
            seriesObj4.type = "bar";
            //seriesObj4.barGap = "0%";
            seriesObj4.name = "总返还";
            seriesObj4.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            Series seriesObj5 = new Series();
            // seriesObj2.id = 2;
            seriesObj5.type = "bar";
            //seriesObj4.barGap = "0%";
            seriesObj5.name = "总余额";
            seriesObj5.data = new List<decimal>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            //EcharModel echarModel = new EcharModel();
            echarModel.categoryList = categoryList;
            echarModel.seriesObj = seriesObj;
            echarModel.seriesObj2 = seriesObj2;
            echarModel.seriesObj3 = seriesObj3;
            echarModel.seriesObj4 = seriesObj4;
            echarModel.seriesObj5 = seriesObj5;
            echarModel.legendList = legendList;
            List<DeductionRecordDto> Deductionlist = _otherAccountAppService.GetDynamicRDeduStatisEchar(input);

            for (var i = 0; i < Deductionlist.Count; i++)
            {
                echarModel.categoryList.Add(Deductionlist[i].TenancyName);
                echarModel.seriesObj.data.Add(Deductionlist[i].SumCZ_Money);//充值
                echarModel.seriesObj2.data.Add(Deductionlist[i].SumXF_Money);//消费
                echarModel.seriesObj3.data.Add(Deductionlist[i].SumYF_Money);//预付
                echarModel.seriesObj4.data.Add(Deductionlist[i].SumFH_Money);//返还
                echarModel.seriesObj5.data.Add(Deductionlist[i].SumWallet_Money);//余额
            }
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
        /// <param name="input"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public JsonResult GetOtherAccounts(SearchOtherAccountInput input, string searchField, string searchOper,
            string searchString)
        {
            if (!string.IsNullOrEmpty(searchField))
                input.filters =
                    "{'groupOp':'AND','rules':[{'field':'" + searchField + "','op':'" + searchOper + "','data':'" +
                    searchString + "'}]}";
            return this.Json(_otherAccountAppService.GetAllOtherAccountAndPlateNumber(input));
        }

        /// <summary>
        /// 获取卡绑定车牌列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetOtherPlateNumbers(int Id)
        {
            var list = _otherPlateNumberAppService.GetAll(Id).Items;
            return this.Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据主键查看账户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetOtherAccountById(int Id)
        {
            var account = _otherAccountAppService.GetAccountByID(Id);
            return Json(account, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 账户后台充值
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="upMoney"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("MoneCar.Field1")]
        public async Task<JsonResult> OtherAccountVoucher(int Id, decimal upMoney)
        {
            var num = await _otherAccountAppService.UpdateAccountWallet(Id, upMoney);
            if (num == 1)
            {
                return Json(new MvcAjaxResponse(true));
            }
            else
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("充值失败！")));
            }
        }




        /// <summary>
        /// 解除车牌绑定
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("MoneCar.Field2")]
        public JsonResult OtherAccountAbsolvePlatNumber(int Id)
        {
            if (_otherAccountAppService.AbsolvePlatNumber(Id) == 1)
            {
                return Json(new MvcAjaxResponse(true));
            }
            else
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("解除绑定失败！")));
            }
        }

        /// <summary>
        /// 激活账户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("MoneCar.Field1")]
        public JsonResult OtherAccountIsEnable(int Id, string IsEnableAccountName, int employeeIdInput)
        {
            if (_otherAccountAppService.OtherAccountIsEnabnle(Id, IsEnableAccountName, employeeIdInput) == 1)
            {
                return Json(new MvcAjaxResponse(true));
            }
            else
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("账户激活失败！")));
            }
        }

        public JsonResult ProcessOtherAccount(CreateOrUpdateOtherAccountInput input)
        {
            switch (input.oper)
            {
                case "add":
                    _otherAccountAppService.Insert(input);
                    break;
                case "del":
                    _otherAccountAppService.Delete(input.Id);
                    break;
                case "edit":
                    _otherAccountAppService.Update(input);
                    break;
            }
            return this.Json(new MvcAjaxResponse());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("CreditCardDetails")]
        public ActionResult DeductionRecords()
        {
            return View();
        }

        /// <summary>
        /// 获取钱包明细记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 
        public JsonResult GetAllDeductionRecords(SearchDeductionRecordInput input)
        {
            if (input.filters != null)
            {
                input.filters = input.filters.Replace("OperTypeName", "OperType");
            }
            return this.Json(_otherAccountAppService.GetAllDeductionRecord(input));
        }
    }
}