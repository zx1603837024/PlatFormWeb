using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.CompanyCustomerExpressManagement;
using P4.CompanyCustomerExpressManagement.Dto;
using P4.Companys.Dtos;
using P4.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    [AbpMvcAuthorize]
    public class CompanyCustomerExpressController : P4ControllerBase
    {

        private readonly ICompanyCustomerExpressAppService _companyCustomerExpressAppService;
        private readonly ICompanyCustomerExpressRepository _companyCustomerExpressRepository;
        private readonly ICompanyFactoryExpressAppService _companyFactoryExpressAppService;
        
        //
        // GET: /CompanyCustomerExpress/
        /// <summary>
        /// 构造函数
        /// </summary>
        public CompanyCustomerExpressController(ICompanyCustomerExpressAppService companyCustomerExpressAppService, ICompanyCustomerExpressRepository companyCustomerExpressRepository, ICompanyFactoryExpressAppService companyFactoryExpressAppService)
        {
            _companyCustomerExpressAppService = companyCustomerExpressAppService;
            _companyCustomerExpressRepository = companyCustomerExpressRepository;
            _companyFactoryExpressAppService = companyFactoryExpressAppService;
        }

        [AbpMvcAuthorize("CompanyCustomerExpress")]
        public ActionResult CompanyCustomerExpress()
        {
            CompanyCustomerExpressModel model = new CompanyCustomerExpressModel();
            model.CompanyName = _companyCustomerExpressAppService.GetAllCompanyName();
            //List<CompanyDto> dislist = _companyCustomerExpressAppService.GetAllCompanyName();
            //model.CompanyNameId = "{";
            //if (dislist != null && dislist.Count > 0)
            //{
            //    foreach (var item in dislist)
            //    {
            //    model.PartsType += item.ValueData + ":" + item.ValueCode.ToString() + ",";
            //    model.PartsType = model.PartsType.Length == 1 ? "{" : model.PartsType.Substring(0, model.PartsType.Length - 1);
            //}
            //model.PartsType += "}";
            return View(model);
        }

        [AbpMvcAuthorize("CompanyCustomerExpressDetail")]
        public ActionResult CompanyCustomerExpressDetail()
        {
            return View();
        }


        /// <summary>
        /// 设备发货
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("CompanyCustomerExpressDetail.Insert")]
        public JsonResult Insert(CreateOrUpdateCompanyCustomerExpress input)
        {
            if (input.EquipmentDeliveryType == "0")
            {
                //正常发货
                input.CompanyCustomerExpressState = "公司已发货";
                //_companyCustomerExpressAppService.Insert(input);
                if (_companyCustomerExpressAppService.Insert(input) == 0)
                {
                    return Json(new MvcAjaxResponse(true));
                }
                else if (_companyCustomerExpressAppService.Insert(input) == 2)
                {
                    return Json(new MvcAjaxResponse(new ErrorInfo("该批次号不存在！")));
                }
                else if (_companyCustomerExpressAppService.Insert(input) == 1)
                {
                    return Json(new MvcAjaxResponse(new ErrorInfo("该流水号已存在！")));
                }
                else
                {
                    return Json(new MvcAjaxResponse(new ErrorInfo("该批次已发货！")));
                }
            }
            else
            {
                //返修
                input.CompanyCustomerExpressState = "公司已发货";
                //_companyCustomerExpressAppService.Insert(input);
                if (_companyCustomerExpressAppService.ReworkInsert(input) == 0)
                {
                    return Json(new MvcAjaxResponse(true));
                }
                else if (_companyCustomerExpressAppService.ReworkInsert(input) == 2)
                {
                    return Json(new MvcAjaxResponse(new ErrorInfo("公司还未收货，无法发给客户！")));
                }
                else if (_companyCustomerExpressAppService.ReworkInsert(input) == 1)
                {
                    return Json(new MvcAjaxResponse(new ErrorInfo("该流水号已存在！")));
                }
                //else if (_companyCustomerExpressAppService.ReworkInsert(input) == 3)
                //{
                //    return Json(new MvcAjaxResponse(new ErrorInfo("该批次号已存在！")));
                //}
                else
                {
                    return Json(new MvcAjaxResponse(new ErrorInfo("请输入正确的返修批次号！")));
                }
            }
            
        }

        /// <summary>
        /// 返修设备发货
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult DeliveryInsert(CreateOrUpdateCompanyCustomerExpress input)
        {
            input.CompanyId =Convert.ToInt32(AbpSession.CompanyId);
            input.CompanyCustomerExpressState = "客户已发货";
            input.EquipmentDeliveryType = "1";
            //_companyCustomerExpressAppService.Insert(input);
            if (_companyCustomerExpressAppService.DeliveryInsert(input) == 0)
            {
                return Json(new MvcAjaxResponse(true));
            }
            else if (_companyCustomerExpressAppService.DeliveryInsert(input) == 2)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("该批次号不存在！")));
            }
            else if (_companyCustomerExpressAppService.DeliveryInsert(input) == 1)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("该流水号已存在！")));
            }
            else
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("该批次已发货！")));
            }
        }
        /// <summary>
        /// 查询发货详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("CompanyCustomerExpressDetail")]
        public JsonResult GetCompanyCustomerExpressList(SearchCompanyCustomerExpress input)
        {
            //Repository中转换查询
            return this.Json(_companyCustomerExpressRepository.GetCompanyCustomerExpressAll(input));
            //普通框架查询
            //return this.Json(_companyCustomerExpressAppService.GetAllCompanyCustomerExpressList(input));
        }

        /// <summary>
        /// 删除发货详情
        /// </summary>
        /// <param name="input"></param>
        [AbpMvcAuthorize("CompanyCustomerExpressDetail.Delete")]
        public void Delete(CreateOrUpdateCompanyCustomerExpress input)
        {
            _companyCustomerExpressAppService.Delete(input);
        }

        /// <summary>
        /// 修改发货详情
        /// </summary>
        /// <param name="input"></param>
        [AbpMvcAuthorize("CompanyCustomerExpressDetail.Update")]
        public void Update(CreateOrUpdateCompanyCustomerExpress input)
        {
            _companyCustomerExpressAppService.Update(input);
        }

        /// <summary>
        /// Jqgrid调用操作方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("CompanyCustomerExpressDetail")]
        public JsonResult ProcessCompanyCustomerExpress(CreateOrUpdateCompanyCustomerExpress input)
        {
            switch (input.oper)
            {
                case "add":
                    Insert(input);
                    break;
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
        /// 获取分公司名称(jqgrid中用)
        /// </summary>
        /// <returns></returns>
        public string GetCompanyName()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _companyCustomerExpressAppService.GetAllCompanyName())
            {
                strtemp.AppendFormat(option, model.Id, model.CompanyName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        public string GetEquipmentDeliveryType()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            strtemp.AppendFormat(option,"0", "发货");
            strtemp.AppendFormat(option,"1", "返修");
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }
        /// <summary>
        /// 通过id查询companyname
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetCompanyNameList(int Id)
        {
            List<CompanyDto> list = new List<CompanyDto>();
            list.AddRange(_companyCustomerExpressAppService.GetAllCompanyName(Id));
            return this.Json(list);
        }

        /// <summary>
        /// 客户确认收货
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult ConfirmReceiptById(int Id)
        {
            if (_companyCustomerExpressAppService.ConfirmReceiptById(Id) == 1)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("该批次已收货！")));
            }
            else if (_companyCustomerExpressAppService.ConfirmReceiptById(Id) == 2)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("请选择正确批次！")));
            }
            else
            {
                _companyCustomerExpressAppService.ConfirmReceiptById(Id);
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
            return _companyCustomerExpressAppService.GetBatchNumById(id).ToString();
        }

        /// <summary>
        /// 添加公司工厂快递表(返修发货)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult CompanyFactoryInsert(CreateOrUpdateCompanyFactoryExpress input)
        {
            input.CompanyFactoryExpressState = "公司已发货";
            input.EquipmentDeliveryType = "1";
            //_companyCustomerExpressAppService.Insert(input);
            if (_companyFactoryExpressAppService.Insert(input) == 0)
            {
                return Json(new MvcAjaxResponse(true));
            }
            else if (_companyFactoryExpressAppService.Insert(input) == 1)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("该流水号已存在！")));
            }
            else if (_companyFactoryExpressAppService.Insert(input) == 2)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("该批次已发货给工厂！")));
            }
            else if (_companyFactoryExpressAppService.Insert(input) == 4)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("公司还未收到该批次，不能转发！")));
            }
            else
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("该批次不是返修批次！")));
            }
        }


        /// <summary>
        /// 公司确认收货
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult CompanyConfirmReceiptById(int Id)
        {
            if (_companyCustomerExpressAppService.CompanyConfirmReceiptById(Id) == 1)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("该批次已收货！")));
            }
            else if (_companyCustomerExpressAppService.CompanyConfirmReceiptById(Id) == 2)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo("请选择正确批次！")));
            }
            else
            {
                _companyCustomerExpressAppService.CompanyConfirmReceiptById(Id);
                return Json(new MvcAjaxResponse(true));
            }
        }
        
	}
}