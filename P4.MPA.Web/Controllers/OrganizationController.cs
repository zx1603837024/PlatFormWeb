using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.Messages.Organizations.Dtos;
using P4.Organizations;
using P4.Organizations.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{

   [AbpMvcAuthorize]
    public class OrganizationController :P4ControllerBase
    {
           
        #region Var
        IOrganizationAppService _OrganizationAppService;
        #endregion

        public OrganizationController(IOrganizationAppService organizationAppService)
        {
            _OrganizationAppService = organizationAppService;
        }
        // GET: Organization
         [AbpMvcAuthorize("OrganizationManagement")]
        public ActionResult OrganizationManagement()
        {
            return View();
        }
         public JsonResult GetAllOrganization(OrganizationInput entity)
         {
             return this.Json(_OrganizationAppService.GetAllOrganization(entity));
         }
       /// <summary>
       /// 组织架构管理增删改
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>
         public JsonResult ProcessOrganization(CreateOrUpdateOrganization input)
         {
             JsonResult returnJson = new JsonResult();
             switch (input.oper)
             {
                 case P4Consts.JqGridInsert:
                     //returnJson = OrganizationInsert(input);
                     break;
                 case P4Consts.JqGridDelete:
                     //returnJson = OrganizationDelete(input);
                     break;
                 case P4Consts.JqGridUpdate:
                     returnJson = OrganizationEdit(input);
                     break;
                 default:
                     break;
             }
             return this.Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
         }

         //[AbpMvcAuthorize("Organization.Insert")]
         //public JsonResult OrganizationInsert(CreateOrUpdateOrganization input)
         //{
         //    _OrganizationAppService.DictionaryTypeInsert(input);
         //    return this.Json("12121");
         //}

         //[AbpMvcAuthorize("Organization.Delete")]
         //public JsonResult OrganizationDelete(CreateOrUpdateOrganization input)
         //{
         //    _OrganizationAppService.DictionaryTypeDelete(input);
         //    return this.Json("123123");
         //}
         [AbpMvcAuthorize("Organization.Update")]
         public JsonResult OrganizationEdit(CreateOrUpdateOrganization input)
         {
             _OrganizationAppService.OrganizationUpdate(input);

             return this.Json("123123");
         }
    }
}