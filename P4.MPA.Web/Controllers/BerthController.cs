using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.Berths;
using P4.Berths.Dtos;
using P4.Berthsecs;
using P4.Businesses;
using P4.Businesses.Dtos;
using P4.Rates;
using P4.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace P4.Web.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [AbpMvcAuthorize]
    public class BerthController : P4ControllerBase
    {
        #region Var
        private readonly IBerthAppService _berthApplicationService;
        private readonly IBusinessAppService _businessAppService;
        private readonly IBerthsecAppService _berthsecAppService;
        private readonly IRatesAppService _ratesAppService;//停车费用计算
        #endregion

       /// <summary>
       /// 
       /// </summary>
       /// <param name="berthApplicationService"></param>
       /// <param name="businessAppService"></param>
       /// <param name="berthsecAppService"></param>
       /// <param name="ratesAppService"></param>
        public BerthController(IBerthAppService berthApplicationService, IBusinessAppService businessAppService, IBerthsecAppService berthsecAppService, IRatesAppService ratesAppService)
        {
            _berthApplicationService = berthApplicationService;
            _businessAppService = businessAppService;
            _berthsecAppService = berthsecAppService;
            _ratesAppService = ratesAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult MonthBerthsUserOnlyMonth(GetBusinessDetailsInput input)
        {
            MonthBerthsUseModel model = new MonthBerthsUseModel();
            model.MonthBerthsUseList = _businessAppService.GetMonthBerthsUseListOnlyMonth(input);
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("BerthManagement")]
        public ActionResult Berths()
        {
            return View();
        }

        /// <summary>
        /// 批量解除锁定地磁
        /// </summary>
        /// <param name="berthids"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("BerthManagement.Field1")]
        public JsonResult BerthToSensorNolock(List<int> berthids)
        {
            int count = _berthApplicationService.BerthToSensorNolock(berthids);
            if (count > 0)
            {
                return Json(new { a = "OK" });
            }
            else
            {
                return Json(new { a = "NO" });
            }
        }

        /// <summary>
        /// 车辆出场
        /// </summary>
        /// <param name="berthid"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("BerthManagement.Field2")]
        public JsonResult PlatformCarOut(int berthid)
        {
            var berthDto = _berthApplicationService.GetBerthGuidByBerthId(berthid);
            if (berthDto.BerthStatus == "2")
                return this.Json("车辆已出场");
            var CarOutTime = DateTime.Now;
            if (berthDto.guid == berthDto.SensorGuid && berthDto.SensorsOutCarTime != null)
            {
                CarOutTime = Convert.ToDateTime(berthDto.SensorsOutCarTime);
            }
           
            var rate = _ratesAppService.RateCalculate(berthDto.BerthsecId, berthDto.InCarTime.Value, CarOutTime, berthDto.CarType, 0, berthDto.ParkId, berthDto.RelateNumber, berthDto.CompanyId);
            string ispay = "false";
            if (rate.CalculateMoney == 0)
            {
                ispay = "true";
            }
            bool flag = _businessAppService.InsertOtherCarOut(rate.CalculateMoney, "0", rate.CalculateMoney.ToString(), CarOutTime.ToString("yyyy-MM-dd HH:mm:sss"), CarOutTime.ToString("yyyy-MM-dd HH:mm:sss"), AbpSession.UserId.Value.ToString(), berthDto.guid.ToString(), "", "0","0", "0",  ispay, "1", rate.ParkTime.ToString(), rate.CalculateMoney, "", berthDto.BerthsecId.ToString(),"0");
            return this.Json(flag);
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="berthid"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("BerthManagement.Field3")]
        public JsonResult DeleteOrder(int berthid)
        {
            var berthDto = _berthApplicationService.GetBerthGuidByBerthId(berthid);
            if (berthDto.BerthStatus == "2")
                return this.Json("车辆已出场");
            _businessAppService.DeleteBusinessDetial(berthDto.guid);
            return this.Json("OK");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public ActionResult GetExcelData(string file)
        {
            if (string.IsNullOrEmpty(file))
            {
                return null;
            }
            #region 注释代码
            //DataTable dt = new DataTable();
            //List<ContactInfo> list = new List<ContactInfo>();
            //DataTable table = ConvertExcelFileToTable(guid);
            //if (table != null)
            //{
            //    #region 数据转换
            //    int i = 1;
            //    foreach (DataRow dr in table.Rows)
            //    {
            //        string customerName = dr["客户名称"].ToString();
            //        if (string.IsNullOrEmpty(customerName))
            //        {
            //            continue;//客户名称为空，记录跳过
            //        }
            //        CustomerInfo customerInfo = BLLFactory<Customer>.Instance.FindByName(customerName);
            //        if (customerInfo == null)
            //        {
            //            continue;//客户名称不存在，记录跳过
            //        }
            //        ContactInfo info = new ContactInfo();
            //        info.Customer_ID = customerInfo.ID;//客户ID
            //        info.HandNo = dr["编号"].ToString();
            //        info.Name = dr["姓名"].ToString();

            //        info.Data1 = BLLFactory<Customer>.Instance.GetCustomerName(info.Customer_ID);
            //        list.Add(info);
            //    }
            //    #endregion
            //}
            //var result = new { total = list.Count, rows = list };
            //return JsonDate(result);
            #endregion
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult ProcessBerthType(CreatOrUpdateBerthInput input)
        {
            JsonResult returnJson = new JsonResult();
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {

                case P4Consts.JqGridUpdate:
                    info.Message = BerthUpdate(input);
                    break;
                default:
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("DictionaryType.Update")]
        public string BerthUpdate(CreatOrUpdateBerthInput input)
        {
            return _berthApplicationService.BerthUpdate(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetBerths(BerthInput input)
        {
            return this.Json(_berthApplicationService.GetAllBethlist(input));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetBerthByBerthsecId(int Id)
        {
            return this.Json(_berthApplicationService.GetBerthsByBerthsecid(Id), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取详细泊位段detail
        /// </summary>
        /// <param name="BerthsecId"></param>
        /// <param name="CheckInEmployeeId"></param>
        /// <param name="CheckOutEmployeeId"></param>
        /// <returns></returns>
        public JsonResult GetBerthsecGpsDetail(int BerthsecId,long CheckInEmployeeId,long CheckOutEmployeeId)
        {
            return Json(_berthApplicationService.GetBerthsecGpsDetail(BerthsecId, CheckInEmployeeId, CheckOutEmployeeId), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetBerthStatusSelect()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            strtemp.AppendFormat(option, "1", "在停");
            strtemp.AppendFormat(option, "2", "未停");
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("MonthBerthsUse")]
        public ActionResult MonthBerthsUse(GetBusinessDetailsInput input)
        {
            MonthBerthsUseModel model = new MonthBerthsUseModel();
            model.MonthBerthsUseList = _businessAppService.GetMonthBerthsUseList(input);
            return View(model);
        }

        /// <summary>
        /// 泊位监控
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("BerthMonitor")]
        public ActionResult BerthMonitor()
        {
            BerthsModel model = new BerthsModel();
            model.berthsecList = _berthsecAppService.GetAllBerthsec();
            if (model.berthsecList.Count > 0)
                model.berthList = _berthApplicationService.GetBerthsByBerthsecid(model.berthsecList[0].Id);
            else
                model.berthList = new List<BerthDto>();
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="berthsecid"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("BerthMonitor")]
        public JsonResult GetBerthsByBerthsecid(int berthsecid)
        {
            return this.Json(_berthApplicationService.GetBerthsByBerthsecid(berthsecid), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult MonthBerthsUseDataReport(GetBusinessDetailsInput input)
        {
            MonthBerthsUseModel model = new MonthBerthsUseModel();
            model.MonthBerthsUseList = _businessAppService.GetMonthBerthsUseList(input);
            return this.Json(model.MonthBerthsUseList);
        }

        /// <summary>
        /// 月份车位报表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult MonthBerthsUseDataReportOnlyMonth(GetBusinessDetailsInput input)
        {
            MonthBerthsUseModel model = new MonthBerthsUseModel();
            model.MonthBerthsUseList = _businessAppService.GetMonthBerthsUseListOnlyMonth(input);
            return this.Json(model.MonthBerthsUseList);
        }
    }
}