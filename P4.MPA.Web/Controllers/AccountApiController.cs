using Abp.Authorization.Users;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using P4.OtherAccounts;
using P4.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{

    /// <summary>
    /// app用户接口控制器
    /// </summary>
    public class AccountApiController : P4ControllerBase
    {
        #region Var
        private readonly ITenantAppService _tenantApplicationService;
        private readonly OtherAccountManager _abpOtherAccountManager;
        #endregion
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountApiController(ITenantAppService tenantApplicationService, OtherAccountManager abpOtherAccountManager)
        {
            _tenantApplicationService = tenantApplicationService;
            _abpOtherAccountManager = abpOtherAccountManager;
        }


        /// <summary>
        /// 收费员登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<JsonResult> Login(string userNameOrEmailAddress, string plainPassword, string tenancyName, string DeviceCode)
        {
            var tenant = _tenantApplicationService.FirstOrDefault(tenancyName);
            if (tenant == null)
            {
                return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 7, Message = "未找到此商户: " + tenancyName }), JsonRequestBehavior.AllowGet);
            }

            if (!tenant.IsActive)
            {
                return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 8, Message = "商户组织被锁定: " + tenant.Name }), JsonRequestBehavior.AllowGet);
            }

            var loginResult = await _abpOtherAccountManager.LoginAsync(userNameOrEmailAddress, plainPassword, tenant.Id, 1, 1, 1, DeviceCode);
            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:

                    break;
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 1, Message = "用户名验证错误!" }), JsonRequestBehavior.AllowGet);
                case AbpLoginResultType.InvalidTenancyName:
                    return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 2, Message = "未找到此商户!" }), JsonRequestBehavior.AllowGet);
                case AbpLoginResultType.TenantIsNotActive:
                    return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 3, Message = "企业账号被锁定!" }), JsonRequestBehavior.AllowGet);
                case AbpLoginResultType.UserIsNotActive:
                    return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 4, Message = "用户被锁定!" }), JsonRequestBehavior.AllowGet);
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 5, Message = "您的邮件地址为确认!" }), JsonRequestBehavior.AllowGet);
                case AbpLoginResultType.EmployeeIsCheck:
                    return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 9, Message = "您已经登录过了！" }), JsonRequestBehavior.AllowGet);
                default:
                    return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 6, Message = "未知错误的登录!" }), JsonRequestBehavior.AllowGet);
            }
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);//退出外部cookie
            AbpSession.UserMenus = null;
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, loginResult.Identity);
            return this.Json(new MvcAjaxResponse(DateTime.Now.ToString()), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 巡查员退出系统
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public JsonResult Logout()
        {
            if (!AbpSession.UserId.HasValue)
                return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 12, Message = "退出失败，在线用户信息已经丢失！ " }), JsonRequestBehavior.AllowGet);
            return this.Json("");
        }

        public string AppLogin(string username, string mobileNum)
        {
            return "{\"result\":\"1\",\"hxname\":\"hx21776\",\"hxpass\":\"hxzldpass\",\"wximgurl\":\"http://wx.qlogo.cn/mmopen/SeOhdv6hwgCltjOANVibJ30BE6G73UMibPU4XCVyhu5VacibNerlm4ktoCHKnyYMicbMjHzUdF7O7QqvKFVnGSeqKiaULpNarT2lo/0\"}";
        }

    }
}