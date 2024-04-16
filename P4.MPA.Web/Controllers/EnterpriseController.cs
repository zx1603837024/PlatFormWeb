using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using P4.AuditLogs;
using P4.Tenants;
using P4.Tenants.Dto;
using P4.Users;
using P4.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{

    [AbpMvcAuthorize]
    public class EnterpriseController : P4ControllerBase
    {

        #region Var
        private readonly IUserAppService _userApplicationService;
        private readonly IAuditLogAppService _auditLogApplicationService;
        private readonly ITenantAppService _tenantApplicationService;
        #endregion

        public EnterpriseController(IUserAppService userApplicationService, IAuditLogAppService auditLogApplicationService, ITenantAppService tenantApplicationService)
        {
            _userApplicationService = userApplicationService;
            _auditLogApplicationService = auditLogApplicationService;
            _tenantApplicationService = tenantApplicationService;
        }


        // GET: Enterprise
        public ActionResult EnterpriseSetting()
        {
            var model = new Models.EnterpriseSetting();
            TenantDto tenant = _tenantApplicationService.FirstOrDefault(AbpSession.TenantId ?? 0);
            model.DomainName = tenant.DomainName;
            model.HQ = tenant.HQ;
            tenant.Address = tenant.Address;
            model.Contacts = tenant.Contacts;
            model.Password = tenant.Password;
            model.Telephone = tenant.Telephone;
            model.X = tenant.X;
            model.Y = tenant.Y;
            return View(model);
          
        }


        public JsonResult SaveEnterpriseSetting(Models.EnterpriseSetting entity)
        {  
            TenantDto tenant = new TenantDto();
            tenant.DomainName = entity.DomainName;
            tenant.Id = AbpSession.TenantId ?? 0;
            tenant.HQ = entity.HQ;
            tenant.Address = entity.Address;
            tenant.Contacts = entity.Contacts;
            tenant.Telephone = entity.Telephone;
            tenant.Password = entity.Password;
            tenant.X = entity.X;
            tenant.Y = entity.Y;
            _tenantApplicationService.SaveTenant(tenant);
            return this.Json(new MvcAjaxResponse());
        }

     
    }
}