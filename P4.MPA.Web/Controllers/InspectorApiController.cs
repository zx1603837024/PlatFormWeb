using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using P4.Berths;
using P4.Berthsecs;
using P4.Berthsecs.Dto;
using P4.Companys;
using P4.Inspectors;
using P4.Inspectors.Dtos;
using P4.Parks;
using P4.Tenants;
using P4.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 巡查员Api
    /// </summary>
    public class InspectorApiController : P4ControllerBase
    {
        #region Var
        private readonly ITenantAppService _tenantApplicationService;
        private readonly InspectorManager _abpInspectorManager;
        private readonly IRepository<Inspector, long> _abpInspectorRepository;
        private readonly IInspectorAppService _inspectorApplicationService;
        private readonly IBerthsecAppService _berthsecAppService;  //泊位段
        private readonly IBerthAppService _berthAppService;  //泊位
        private readonly ICompanyAppService _companyAppService;
        private readonly IParkAppService _parkAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tenantApplicationService"></param>
        public InspectorApiController(ITenantAppService tenantApplicationService, InspectorManager abpInspectorManager, IRepository<Inspector, long> abpInspectorRepository, IInspectorAppService inspectorApplicationService, IBerthsecAppService berthsecAppService, IBerthAppService berthAppService, ICompanyAppService companyAppService,IParkAppService parkAppService)
        {
            _tenantApplicationService = tenantApplicationService;
            _abpInspectorManager = abpInspectorManager;
            _abpInspectorRepository = abpInspectorRepository;
            _inspectorApplicationService = inspectorApplicationService;
            _berthsecAppService = berthsecAppService;
            _berthAppService = berthAppService;
            _companyAppService = companyAppService;
            _parkAppService = parkAppService;
        }

        /// <summary>
        /// 巡查员登录（获取所有任务停车场权限）
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> InspectorLogin(string userNameOrEmailAddress, string plainPassword, string tenancyName, string DeviceCode)
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
            var insp = _abpInspectorRepository.FirstOrDefault(entity => entity.UserName == userNameOrEmailAddress && entity.Password == plainPassword && entity.TenantId == tenant.Id);
            List<int> regionId = new List<int>();//区域
            List<string> parkId = new List<string>();//停车场
            List<int> berthsecId = new List<int>();//泊位段
            InspectorRPB IRPB = new InspectorRPB();
            if (insp != null)
            {
                IRPB = _inspectorApplicationService.GetBerthsecListByWorkgroup(insp.Id);
                regionId = IRPB.regionId;
                parkId = IRPB.parkId;
                berthsecId = IRPB.berthsecId;
            }
            //登录结果并且处理权限
            var loginResult = await _abpInspectorManager.LoginAsync(userNameOrEmailAddress, plainPassword, tenant.Id, berthsecId, parkId, regionId, DeviceCode);
            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    //巡查员签到修改AbpInspectors  修改签到状态与签到时间
                    var inspector = _abpInspectorRepository.FirstOrDefault(entity => entity.UserName == userNameOrEmailAddress && entity.Password == plainPassword);
                    inspector.CheckIn = true;
                    inspector.CheckOut = false;
                    inspector.CheckInTime = DateTime.Now;
                    _abpInspectorRepository.Update(inspector);
                    //巡查员签到历史表
                    _inspectorApplicationService.InspectorCheckInPark(insp.Id, parkId, DeviceCode, insp.CompanyId, insp.TenantId);
                    //更新版本号
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
            return this.Json(new MvcAjaxResponse(new GetAllInspectorApiOutput() { rows = IRPB.berthsec, Inspector = insp }), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 巡查员退出系统
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        public JsonResult InspectorLogout()
        {
            if (!AbpSession.UserId.HasValue)
                return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 12, Message = "退出失败，在线用户信息已经丢失！ " }), JsonRequestBehavior.AllowGet);

            //巡查员签退修改AbpInspectors  修改签退状态与签退时间
            int InspectorID = Convert.ToInt32(AbpSession.UserId);
            var inspector = _abpInspectorRepository.FirstOrDefault(entity => entity.Id == InspectorID);
            inspector.CheckOut = true;
            inspector.CheckIn = false;
            inspector.CheckOutTime = DateTime.Now;
            _abpInspectorRepository.Update(inspector);
            //巡查员签退历史记录表  AbpInspectorCheck
            _inspectorApplicationService.InspectorCheckOutPark();
            AuthenticationManager.SignOut();//应该是清除缓存
            return this.Json(new MvcAjaxResponse("退出成功"), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 停车场泊位数据下载
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        public JsonResult DownParameter()
        {
            PDAModel model = new PDAModel();
            if (AbpSession.ParkIds!=null)
            {
                model.BerthsecList = _berthsecAppService.LoadListBerhtsec(AbpSession.ParkIds);
            }
         
            //model.WhiteList = _whiteListsAppService.GetAllWhiteCar();
           // model.Rate = _ratesAppService.GetRateById(berthsec.RateId);
            //model.MonthlyCarList = _monthlyCarAppService.GetAllMonthlyCar();
            //model.BlackList = _blackListsAppService.GetAllBlackList();
            model.Berths = _berthAppService.GetAllBerths();
            model.ParkList = _parkAppService.InspectorGetParkAll();
            P4.Companys.Dtos.CompanyDto companydto = _companyAppService.FirstOrDefault(AbpSession.CompanyId.Value);
            model.CompanyName = companydto.CompanyName;
            model.SysInfoPassword = companydto.Password1;
            model.CardMenuPassword = companydto.Password2;
            model.HrTotalPassword = companydto.Password3;
            model.LogoutPassword = companydto.Password4;
            model.Password5 = companydto.Password5;
            //model.CompanyName = _companyAppService.FirstOrDefault(AbpSession.CompanyId.Value).CompanyName;
            //  model.SysInfoPassword = _companyAppService.FirstOrDefault(AbpSession.CompanyId.Value).CompanyName;
            return this.Json(new MvcAjaxResponse(model), JsonRequestBehavior.AllowGet);
        }


    }
}