using Abp.Web.Mvc.Authorization;
using P4.AliPay;
using P4.AliPay.Dto;
using P4.Businesses;
using P4.Businesses.Dtos;
using P4.Card;
using P4.Card.Dtos;
using System;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [AbpMvcAuthorize]
    public class ClearingSettlementController : P4ControllerBase
    {

        #region Var
        private readonly IIPassCardAppService _IPassCardAppService;

        private readonly IBusinessAppService _businessAppService;
        private readonly IAliPayOrderAppService _aliPayOrderAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IPassCardAppService"></param>
        /// <param name="businessAppService"></param>
        public ClearingSettlementController(IIPassCardAppService IPassCardAppService, IBusinessAppService businessAppService, IAliPayOrderAppService aliPayOrderAppService)
        {
            _IPassCardAppService = IPassCardAppService;
            _businessAppService = businessAppService;
            _aliPayOrderAppService = aliPayOrderAppService;
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
        /// 微信支付清分结算
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("WeixinSettlement")]
        public ActionResult WeixinSettlement()
        {
            return View();
        }

        /// <summary>
        /// 支付宝清分结算
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("AliPaySettlement")]
        public ActionResult AliPaySettlement()
        {
            return View();
        }

        [AbpMvcAuthorize("AliPaySettlement")]
        public JsonResult GetAliPayOrdersList(SearchAliPayOrdersInput input)
        {
            return this.Json(_aliPayOrderAppService.GetAllAliPayOrderDetail1(input));
        }

        /// <summary>
        /// 分公司清分结算
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("CompanySettlement")]
        public ActionResult CompanySettlement()
        {
            return View();
        }

        /// <summary>
        /// 分公司清分结算
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("CompanySettlement")]
        public JsonResult GetCompanySettlementList(GetEscapeDetailsInput input)
        {
            return this.Json(_businessAppService.GetEscapeDetailsListByCompany(input));
        }

        /// <summary>
        /// 商户清结算功能
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("TenantSettlement")]
        public ActionResult TenantSettlement()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("TenantSettlement")]
        public JsonResult GetTenantSettlementList(GetEscapeDetailsInput input)
        {
            return this.Json(_businessAppService.GetEscapeDetailsListByTenant(input));
        }

        /// <summary>
        /// 结算商户追缴记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("TenantSettlement.Field2")]
        public JsonResult SettlementListData(string ids)
        {
            return this.Json("");
        }

        

        /// <summary>
        /// 市民卡清分结算
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("CardSettlement")]
        public ActionResult CardSettlement()
        {
            return View();
        }

        /// <summary>
        /// 一卡通清结算
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetCardSettlementList(GetAllIPassCardInput input)
        {

            if (input.filters != null && input.filters.Contains("PayDateStr"))
            {
                input.filters = input.filters.Replace("PayDateStr", "PayDate");
            }

            if (input.sidx == "PayDateStr")
                input.sidx = "PayDate";

            return Json(_IPassCardAppService.GetList(input));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("CardSettlement.Field1")]
        public JsonResult UpdateSettlementStatus(int Ids)
        {
            _IPassCardAppService.FreshSettlement(Ids);
            return Json("OK");
        }

        /// <summary>
        /// 账号清分结算
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("AccountSettlement")]
        public ActionResult AccountSettlement()
        {
            return View();
        }
    }
}