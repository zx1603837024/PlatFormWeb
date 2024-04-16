using P4.Web.Models;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using P4.Users;
using Abp.Authorization.Users;
using Abp.Web.Mvc.Authorization;
using Abp.Application.Navigation;
using Abp.Configuration;
using P4.Authorization;
using P4.Tenants;
using P4.Tenants.Dto;
using P4.Businesses;
using P4.Businesses.Dtos;
using System;
using P4.Collectors;
using P4.OperationLog;
using P4.Inspectors;
using Abp.Web.Mvc.Models;
using P4.UserTrials;
using P4.Berthsecs;
using P4.Sensors;
using System.Reflection;
using Abp.Domain.Uow;
using System.Collections.Generic;
using Abp.Runtime.Caching;
using P4.DecisionAnalysis;
using P4.Weixin;
using Abp.Runtime.Security;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 平台控制器
    /// </summary>
    public class PlatformController : P4ControllerBase
    {
        #region Var
        private readonly IUserAppService _userApplicationService; 
        private readonly UserManager _userManager;
        private readonly INavigationManager _navigationManager;
        private readonly ISettingStore _settingStore;
        private readonly RoleManager _roleManager;
        private readonly RoleStore _roleStore;
        private readonly ITenantAppService _tenantApplicationService;
        private readonly IBusinessAppService _businessAppService;
        private readonly ICollectorAppService _collectorAppService;
        private readonly IOperationLogAppService _operationLogAppService;
        private readonly IInspectorAppService _inspectorAppService;
        private readonly IUserTrialAppService _userTrialAppService;
        private readonly IBerthsecAppService _berthsecAppService;
        private readonly ISensorAppService _sernsorAppService;
        private readonly ICacheManager _cacheManager;
        private readonly IPeakPeriodAppService _peakPeriodAppService;
        private readonly IWeixinAppService _weixinAppService;
        System.Web.Script.Serialization.JavaScriptSerializer jsSer = new System.Web.Script.Serialization.JavaScriptSerializer();
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
        /// 
        /// </summary>
        /// <param name="userApplicationService"></param>
        /// <param name="userManager"></param>
        /// <param name="navigationManager"></param>
        /// <param name="settingStore"></param>
        /// <param name="roleManager"></param>
        /// <param name="roleStore"></param>
        /// <param name="tenantApplicationService"></param>
        /// <param name="businessAppService"></param>
        /// <param name="collectorAppService"></param>
        /// <param name="operationLogAppService"></param>
        /// <param name="inspectorAppService"></param>
        /// <param name="userTrialAppService"></param>
        /// <param name="berthsecAppService"></param>
        /// <param name="sernsorAppService"></param>
        /// <param name="cacheManager"></param>
        /// <param name="peakPeriodAppService"></param>
        /// <param name="weixinAppService"></param>
        public PlatformController(IUserAppService userApplicationService, UserManager userManager, 
            INavigationManager navigationManager, ISettingStore settingStore, 
            RoleManager roleManager, RoleStore roleStore, ITenantAppService tenantApplicationService, 
            IBusinessAppService businessAppService, ICollectorAppService collectorAppService, 
            IOperationLogAppService operationLogAppService, IInspectorAppService inspectorAppService,
            IUserTrialAppService userTrialAppService, IBerthsecAppService berthsecAppService,
            ISensorAppService sernsorAppService, ICacheManager cacheManager, IPeakPeriodAppService peakPeriodAppService, IWeixinAppService weixinAppService)
        {
            _userApplicationService = userApplicationService;
            _userManager = userManager;
            _navigationManager = navigationManager;
            _settingStore = settingStore;
            _roleManager = roleManager;
            _roleStore = roleStore;
            _tenantApplicationService = tenantApplicationService;
            _businessAppService = businessAppService;
            _collectorAppService = collectorAppService;
            _operationLogAppService = operationLogAppService;
            _inspectorAppService = inspectorAppService;
            _userTrialAppService = userTrialAppService;
            _berthsecAppService = berthsecAppService;
            _sernsorAppService = sernsorAppService;
            _cacheManager = cacheManager;
            _peakPeriodAppService = peakPeriodAppService;
            _weixinAppService = weixinAppService;
        }

        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("Control")]
        [UnitOfWork(false)]
        public async Task<ActionResult> Index()
        {
            IndexModel model = new IndexModel();
            //收费金额
            var moneyentity = _businessAppService.IndexMoneyTotal(new GetMoneyInput() { BeginTime = DateTime.Now.AddDays(-1), EndTime = DateTime.Now });
            model.ArrearageTotal = moneyentity.ArrearageTotal;
            model.FactReceiveTotal = moneyentity.FactReceiveTotal;
            model.PrepaidTotal = moneyentity.PrepaidTotal;
            model.ReceivableTotal = moneyentity.ReceivableTotal;
            model.RepaymentTotal = moneyentity.RepaymentTotal;
            //收费员签到
            model.CheckTotal = _collectorAppService.GetCollectorCheckCount();
            // 操作日志
            //model.OperationLogs = _operationLogAppService.GetOperationLogToTopList(5);
            // 获取指定条数巡查员
            model.Inspectors = _inspectorAppService.GetInspectorByTopList(new Inspectors.Dtos.GetInspectorByTopInput() { Count = 13 });
            //车辆在停表
            model.ParkCount = _collectorAppService.GetBerthCheckTotalCount();
            //车检器在线率
            model.SensorCount = _collectorAppService.GetSensorCheckTotalCount();
            //model.SensorCount.SensorOnLineCount=0;
            //泊位段收费统计
            model.BerthsecCount = _collectorAppService.GetBerthsecBussinessCount();
            //收费员收费统计

            model.EmployeeCountList = _collectorAppService.GetEmployeeBussinessCount();
            //停车场收费统计
            model.ParkTotalListJson = jsSer.Serialize(_collectorAppService.GetParkBussinessCount());
            //当月收费统计
            model.MonthTotalListJson = jsSer.Serialize(_collectorAppService.GetMonthBussinessCount());
            //当年收费统计
            model.YearTotal = _collectorAppService.GetYearBussinessCount();
            //获取巡查员任务列表
            model.inspectorTaskList = _inspectorAppService.GetAllInspectorTask();

            model.PeakPeriodList = _peakPeriodAppService.GetTopPeakPeriodList(5, false);

            model.WeixinCount = _collectorAppService.GetWeixinCount();

            model.WeixinIdeas = _weixinAppService.GetWeixinIdeaToTop(10);

            model.TargetAmount = decimal.Parse(_settingStore.GetSettingOrNull(AbpSession.TenantId, null, "TargetAmount").Value);

            ViewBag.Company = "智能科技系统";
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult TestPage() 
        {
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="EmailAddress"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public JsonResult ResetPassword(string EmailAddress, string Code)
        {
            _userApplicationService.ResetPasswordAsync(EmailAddress, Code);
            return Json(new MvcAjaxResponse("密码修改成功"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetVersion()
        {
            Assembly ab = Assembly.GetExecutingAssembly();
            string[] tmp = ab.FullName.Split(new char[] { ',' });
            string version = tmp[1];
            version = version.Replace("=", " ");
            return version;
        }

        /// <summary>
        /// 登录界面
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="id">登录域名</param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> Login(string returnUrl = "", string id = null)
        {

            if (AbpSession.UserId.HasValue)
            {
                return await ExtraLock(null);
            }


            LoginViewModel model = new LoginViewModel();
            TenantDto tenantDto = null;
            if (!string.IsNullOrWhiteSpace(id))
                tenantDto = _tenantApplicationService.FirstOrDefault(id);
            if (tenantDto != default(TenantDto))
            {
                model.HQ = tenantDto.HQ;
                model.TenancyName = tenantDto.TenancyName;
            }
            else
            {
                model.TenancyName = "综合停车管理与交通诱导系统云平台";
                model.HQ = "综合停车管理与交通诱导系统云平台";
            }
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }
            model.id = id;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }


        /// <summary>
        /// 用户登录
        /// 加载用户配置数据到缓存
        /// </summary>
        /// <param name="loginModel"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> UserLogin(LoginViewModel loginModel, string returnUrl = "")
        {
            if (loginModel.TenancyName == "智能停车管理云平台")
                loginModel.TenancyName = null;
            var loginResult = await _userManager.LoginAsync(
                loginModel.UsernameOrEmailAddress,
                loginModel.Password,
                loginModel.id
                );

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    break;
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return this.Json("1_0");
                //throw new UserFriendlyException("用户名验证错误!");
                case AbpLoginResultType.InvalidTenancyName:
                    //throw new UserFriendlyException("未找到此商户: " + loginModel.TenancyName);
                    return this.Json("2_" + loginModel.TenancyName);
                case AbpLoginResultType.TenantIsNotActive:
                    //throw new UserFriendlyException("商户组织被锁定: " + loginModel.TenancyName);
                    return this.Json("3_" + loginModel.TenancyName);
                case AbpLoginResultType.UserIsNotActive:
                    //throw new UserFriendlyException("用户被锁定: " + loginModel.UsernameOrEmailAddress);
                    return this.Json("4_" + loginModel.UsernameOrEmailAddress);
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    //throw new UserFriendlyException("您的邮件地址未确认!");
                    return this.Json("5_0");
                default:
                    //throw new UserFriendlyException("未知错误的登录: " + loginResult.Result);
                    return this.Json("6_0");

            }

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AbpSession.UserMenus = null;
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = loginModel.RememberMe == "on" ? true : false }, loginResult.Identity);

            returnUrl = Url.Action("MetroIndex", "Metro");
            if (loginModel.RequestType != "Post")
                return this.Json(returnUrl);
            if (!string.IsNullOrWhiteSpace(returnUrl) && returnUrl.Length > 2)
            {
                return RedirectToAction(returnUrl);
            }
            return RedirectToAction("MetroIndex", "Metro");
        }

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Platform");
        }

        /// <summary>
        /// 退出系统
        /// 缓存数据持久化
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<ActionResult> Logout()
        {
            if (AbpSession.UserId.HasValue)
            {
                Dictionary<string, SettingInfo> dic = _cacheManager.GetUserSettingsCache().GetOrDefault(AbpSession.UserId.Value);
                foreach (var v in dic.Values)
                    await _settingStore.UpdateAsync(v);
            }

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
            return RedirectToAction("Login", "Platform");
        }

        #region 锁定屏幕

        /// <summary>
        /// 锁定屏幕
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<ActionResult> ExtraLock(string returnUrl = "")
        {
            string TenancyName = null;
            if (AbpSession.TenantId.HasValue) 
                TenancyName = _tenantApplicationService.FirstOrDefault(AbpSession.TenantId.Value).DomainName;
            long userid = AbpSession.UserId ?? 0;
            var findResult = await _userManager.FindByIdAsync(userid);
            if (findResult == null)
            {
                Logout();
                return null;
            }

            ViewBag.Sumname = findResult.Surname;
            ViewBag.id = TenancyName;
            ViewBag.TenancyName = TenancyName;
            ViewBag.EmailAddress = findResult.EmailAddress;
            ViewBag.UserName = findResult.UserName;
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Version = GetVersion();
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie,
                DefaultAuthenticationTypes.ApplicationCookie,
                DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
            //return findResult.Gender == false ? View("ExtraLockWoman") : View("ExtraLockMan");

            return View("ExtraLockMan");

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<JsonResult> SaveUserTrials(Models.UserTrials entity)
        {
            UserTrials.Dtos.UserTrialInput input = new UserTrials.Dtos.UserTrialInput
            {
                Address = entity.Address,
                CompanyName = entity.CompanyName,
                Email = entity.Email,
                Telephone = entity.Telephone,
                TrueName = entity.TrueName
            };
            var id = await _userTrialAppService.SaveUserTrial(input);
            return this.Json(new MvcAjaxResponse(id));
        }

        /// <summary>
        /// 权限异常界面
        /// </summary>
        /// <returns></returns>
        public ActionResult PermissionError()
        {
            return View();
        }

        #endregion

        /// <summary>
        /// 中转页面，继承了以前母版页的左侧导航栏和头部
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("Control")]
        public ActionResult BridgeIndex()
        {
            return View();
        }
        [AbpMvcAuthorize("ParamConfig")]
        public ActionResult ParamConfig()
        {
            return View();
        }
    }
}