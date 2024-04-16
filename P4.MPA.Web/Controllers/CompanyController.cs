using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.Companys;
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
    
    /// <summary>
    /// P3系统用户管理
    /// </summary>
    [AbpMvcAuthorize]
    public class CompanyController : P4ControllerBase
    {

        #region Var
        private readonly ICompanyAppService _companyApplicationService;
        #endregion

        public CompanyController(ICompanyAppService companyApplicationService)
        {
            _companyApplicationService = companyApplicationService;
        }

        [AbpMvcAuthorize("CompanyManagement")]
        public ActionResult Companys()
        {
            return View();
        }

        [AbpMvcAuthorize("CompanySetting")]
        public ActionResult CompanySetting()
        {


            var model = new Models.CompanySetting();
            CompanyDto company  =_companyApplicationService.FirstOrDefault(AbpSession.CompanyId ?? 0);
            if(company==null)
                return View(model);
            model.CompanyName = company.CompanyName;
            model.CompanyId = AbpSession.CompanyId;
            model.Contacts = company.Contacts;
            model.Address = company.Address;
            model.X = company.X;
            model.Y = company.Y;
            model.TelePhone = company.TelePhone;
            
            return View(model);
          
          
        }
        public JsonResult SaveCompanySetting(Models.CompanySetting entity)
        {
            CompanyDto company = new CompanyDto();
            company.CompanyName = entity.CompanyName;
            company.Id = AbpSession.CompanyId ?? 0;
            company.Contacts = entity.Contacts;
            company.TelePhone = entity.TelePhone;
            company.X = entity.X;
            company.Y = entity.Y;
            company.Address = entity.Address;
            _companyApplicationService.SaveCompany(company);
            return this.Json(new MvcAjaxResponse());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult CompanyList(OperatorCompanyInput input)
        {
            if (!string.IsNullOrWhiteSpace(input.filters))
                input.filters = input.filters.Replace("CompanyName", "CompanyName.temp");
            var list = _companyApplicationService.GetAll(input);
            return this.Json(list);
        }
       

        public JsonResult ProcessCompany(CreateCompanyInput input)
        {
            JsonResult returnJson = new JsonResult();
            ErrorInfo info = new ErrorInfo();
            switch (input.oper)
            {
                case P4Consts.JqGridInsert:
                    info.Message = Insert(input);
                    break;
                case P4Consts.JqGridDelete:
                    returnJson = Delete(input);
                    break;
                case P4Consts.JqGridUpdate:
                    info.Message = Edit(input);
                    break;
                default:
                    break;
            }
            return this.Json(info.Message == null ? new MvcAjaxResponse() : new MvcAjaxResponse(info));
        }

        [AbpMvcAuthorize("CompanyManagement.Insert")]
        public string Insert(CreateCompanyInput input)
        {
           return  _companyApplicationService.Insert(input);
       
        }
       
        [AbpMvcAuthorize("CompanyManagement.Delete")]
        public JsonResult Delete(CreateCompanyInput input)
        {
            _companyApplicationService.Delete(input);
            return this.Json("123123");
        }
        [AbpMvcAuthorize("CompanyManagement.Update")]
        public string Edit(CreateCompanyInput input)
        {
           return  _companyApplicationService.Update(input);
           
        }
   
        public string GetCompanysSelect()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            foreach (var model in _companyApplicationService.GetAll().Items)
            {
                strtemp.AppendFormat(option, model.Id, model.CompanyName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }
    }
}