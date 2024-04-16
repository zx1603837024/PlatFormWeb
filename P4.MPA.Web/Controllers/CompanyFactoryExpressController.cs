using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.CompanyCustomerExpressManagement;
using P4.CompanyCustomerExpressManagement.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [AbpMvcAuthorize]
    public class CompanyFactoryExpressController : P4ControllerBase
    {
        private readonly ICompanyFactoryExpressAppService _companyFactoryExpressAppService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="companyFactoryExpressAppService"></param>
        public CompanyFactoryExpressController(ICompanyFactoryExpressAppService companyFactoryExpressAppService)
        {
            _companyFactoryExpressAppService = companyFactoryExpressAppService;
        }

        //
        // GET: /CompanyFactoryExpress/
        [AbpMvcAuthorize("CompanyFactoryExpress")]
        public ActionResult CompanyFactoryExpressDetail()
        {
            return View();
        }

        /// <summary>
        /// 查询公司工厂快递信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("CompanyFactoryExpress")]
        public JsonResult GetCompanyFactoryExpressList(SearchCompanyFactoryExpress input)
        {
            return this.Json(_companyFactoryExpressAppService.GetAllCompanyCustomerExpressList(input));
        }


        /// <summary>
        /// 删除发货详情
        /// </summary>
        /// <param name="input"></param>
        [AbpMvcAuthorize("CompanyFactoryExpress.Delete")]
        public void Delete(CreateOrUpdateCompanyFactoryExpress input)
        {
            _companyFactoryExpressAppService.Delete(input);
        }

        /// <summary>
        /// 修改发货详情
        /// </summary>
        /// <param name="input"></param>
        [AbpMvcAuthorize("CompanyFactoryExpress.Update")]
        public void Update(CreateOrUpdateCompanyFactoryExpress input)
        {
            _companyFactoryExpressAppService.Update(input);
        }

        /// <summary>
        /// Jqgrid调用操作方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("CompanyFactoryExpress")]
        public JsonResult ProcessCompanyFactoryExpress(CreateOrUpdateCompanyFactoryExpress input)
        {
            switch (input.oper)
            {
                //case "add":
                //    Insert(input);
                //    break;
                case "del":
                    Delete(input);
                    break;
                case "edit":
                    Update(input);
                    break;
                default:
                    break;
            }
            return this.Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 工厂确认收货
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult CompanyFactoryExpressConfirm(int Id)
        {
            if (_companyFactoryExpressAppService.CompanyFactoryExpressConfirm(Id) == 1)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("该批次已收货！")));
            }
            if (_companyFactoryExpressAppService.CompanyFactoryExpressConfirm(Id) == 2)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("请选择正确批次！")));
            }
            else
            {
                _companyFactoryExpressAppService.CompanyFactoryExpressConfirm(Id);
                return Json(new MvcAjaxResponse(true));
            }
        }
        /// <summary>
        /// 公司确认收(工厂)货
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult CompanyConfirm(int id)
        {
            if (_companyFactoryExpressAppService.CompanyConfirm(id) == 1)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("该批次已收货！")));
            }
            if (_companyFactoryExpressAppService.CompanyConfirm(id) == 2)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("请选择正确批次！")));
            }
            else
            {
                _companyFactoryExpressAppService.CompanyConfirm(id);
                return Json(new MvcAjaxResponse(true));
            }
        }
        /// <summary>
        /// 通过id获取返修批次号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetBatchNumById(int id)
        {
            return _companyFactoryExpressAppService.GetBatchNumById(id).ToString();
        }

        /// <summary>
        /// 添加公司工厂快递表(返修发货)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult CompanyFactoryInsert(CreateOrUpdateCompanyFactoryExpress input, int selectedId)
        {
            input.CompanyFactoryExpressState = "工厂已发货";
            input.EquipmentDeliveryType = "1";
            if (_companyFactoryExpressAppService.FactoryInsert(input, selectedId) == 0)
            {
                return Json(new MvcAjaxResponse(true));
            }
            else if (_companyFactoryExpressAppService.FactoryInsert(input, selectedId) == 1)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("该流水号已存在！")));
            }
            else if (_companyFactoryExpressAppService.FactoryInsert(input, selectedId) == 3)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("该批次还未收货！")));
            }
            else
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("该批次已发货给公司！")));
            }
        }
	}
}