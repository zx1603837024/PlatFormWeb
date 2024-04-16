using Abp.Web.Mvc.Authorization;
using P4.AuditLogs;
using P4.Tenants;
using P4.Users;
using P4.Users.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Abp.Configuration;
using P4.Dictionarys;
using Abp;
using Abp.Timing;
using P4.Permissions.Dto;
using Abp.Authorization;
using P4.DataPermissions;
using P4.Tree;
using P4.DataPermissions.Dtos;
using P4.Web.Models;
using P4.Regions;
using P4.Parks;
using P4.Berthsecs;
using P4.DataLogs;
using Abp.Web.Mvc.Models;
using System.Text;
using P4.Companys;
using P4.Messages;
using P4.Notices;
using P4.Tasks;
using Abp.Runtime.Caching;
using System;
using P4.UserTrials;
using Abp.Auditing;
using P4.WebApi.Api.Providers;
using P4.Businesses;
using P4.Businesses.Dtos;

namespace P4.Web.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [AbpMvcAuthorize]
    public class UsersController : P4ControllerBase
    {
        #region Var
        private readonly IUserAppService _userApplicationService;
        private readonly IAuditLogAppService _auditLogApplicationService;
        private readonly ITenantAppService _tenantApplicationService;
        private readonly ISettingStore _settingStore;
        private readonly IDictionarysAppService _dictionarysApplicationService;
        private readonly ITreeAppService _treeApplicationService;
        private readonly IDataPermissionsAppService _datapermissionApplicationService;
        private readonly UserManager _userManager;
        private readonly IRegionAppService _regionAppService;
        private readonly IParkAppService _parkAppService;
        private readonly IBerthsecAppService _berthsecAppService;
        private readonly IDataLogsAppService _dataLogsAppService;
        private readonly ICompanyAppService _companyAppService;
        private readonly IMessageAppService _messageAppService;
        private readonly INoticeAppService _noticeAppService;
        private readonly ITaskAppService _taskAppService;
        private readonly ICacheManager _cacheManager;
        private readonly IUserTrialAppService _userTrialAppService;
        private readonly IPictureStoreAppService _pictureStoreAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userApplicationService"></param>
        /// <param name="auditLogApplicationService"></param>
        /// <param name="tenantApplicationService"></param>
        /// <param name="settingStore"></param>
        /// <param name="dictionarysApplicationService"></param>
        /// <param name="treeApplicationService"></param>
        /// <param name="userManager"></param>
        /// <param name="datapermissionApplicationService"></param>
        /// <param name="regionAppService"></param>
        /// <param name="parkAppService"></param>
        /// <param name="berthsecAppService"></param>
        /// <param name="dataLogsAppService"></param>
        /// <param name="companyAppService"></param>
        /// <param name="messageAppService"></param>
        /// <param name="noticeAppService"></param>
        /// <param name="taskAppService"></param>
        /// <param name="cacheManager"></param>
        /// <param name="userTrialAppService"></param>
        public UsersController(IUserAppService userApplicationService, IAuditLogAppService auditLogApplicationService,
            ITenantAppService tenantApplicationService, ISettingStore settingStore,
            IDictionarysAppService dictionarysApplicationService, ITreeAppService treeApplicationService,
            UserManager userManager, IDataPermissionsAppService datapermissionApplicationService,
            IRegionAppService regionAppService, IParkAppService parkAppService, IBerthsecAppService berthsecAppService, IDataLogsAppService dataLogsAppService, ICompanyAppService companyAppService, IMessageAppService messageAppService, INoticeAppService noticeAppService, ITaskAppService taskAppService, ICacheManager cacheManager, IUserTrialAppService userTrialAppService, IPictureStoreAppService pictureStoreAppService)
        {
            _userApplicationService = userApplicationService;
            _auditLogApplicationService = auditLogApplicationService;
            _tenantApplicationService = tenantApplicationService;
            _settingStore = settingStore;
            _dictionarysApplicationService = dictionarysApplicationService;
            _treeApplicationService = treeApplicationService;
            _userManager = userManager;;
            _datapermissionApplicationService = datapermissionApplicationService;
            _regionAppService = regionAppService;
            _parkAppService = parkAppService;
            _berthsecAppService = berthsecAppService;
            _dataLogsAppService = dataLogsAppService;
            _companyAppService = companyAppService;
            _messageAppService = messageAppService;
            _noticeAppService = noticeAppService;
            _taskAppService = taskAppService;
            _cacheManager = cacheManager;
            _userTrialAppService = userTrialAppService;
            _pictureStoreAppService = pictureStoreAppService;
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
        /// <returns></returns>
        [AbpMvcAuthorize("UserTrial")]
        public ActionResult UserTrial()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetAllUserTrials(P4.UserTrials.Dtos.SearchUseTrialInput input)
        {
            return Json(_userTrialAppService.GetAllUserTrials(input));
        }

        /// <summary>
        /// 在线用户（Token登录）
        /// </summary>
        /// <returns></returns>
        //[AbpMvcAuthorize("UserOnline")]
        public ActionResult UseOnlineToken()
        {
            SimpleRefreshTokenProvider._refreshTokens.Count();
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult UserOnlineList()
        {
            return Json("");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetAllCompanyName()
        {
            StringBuilder strtemp = new StringBuilder("<select>");
            if (!AbpSession.CompanyId.HasValue)
            { 
                strtemp.AppendLine(emptyoption);
            }
            foreach (var model in _companyAppService.GetAllCompanyName())
            {
                if (AbpSession.CompanyId.HasValue && AbpSession.CompanyId.Value != 0)
                {
                    if (model.Id == AbpSession.CompanyId)
                    {
                        strtemp.AppendFormat(option, model.Id, model.CompanyName);
                        break;
                    }
                    continue;
                }
                strtemp.AppendFormat(option, model.Id, model.CompanyName);
            }
            strtemp.AppendLine("</select>");
            return strtemp.ToString();
        }

        /// <summary>
        /// 用户管理列表
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize("UsersManagement")]
        public ActionResult Detail()
        {
            return View();
        }

        /// <summary>
        /// 获取用户主页信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("UsersManagement.Field4")]
        public JsonResult GetUserShortcuts(Int32 Id)
        {
            var User = _userApplicationService.GetUserInfo(Id);
            var settingInfo = _settingStore.GetSettingOrNull(User.TenantId, Id, "Shortcuts");
            return Json(settingInfo.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Shortcuts"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("UsersManagement.Field4")]
        public async Task<JsonResult> SaveUserShortcuts(Int32 UserId, string Shortcuts)
        {
            var User = _userApplicationService.GetUserInfo(UserId);
            var settingInfo = _settingStore.GetSettingOrNull(User.TenantId, UserId, "Shortcuts");
            settingInfo.Value = Shortcuts;
            await _settingStore.UpdateAsync(settingInfo);
            return null;
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUsers()
        {
            var list = _userApplicationService.GetUsers();
            return Json(list.Items);
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("UsersManagement")]
        public JsonResult GetAllUsers(UserInput entity)
        {
            return Json(_userApplicationService.GetAllUsers(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<JsonResult> ProcessUser(UserDto model, string id)
        {
            switch (model.oper)//操作类型
            {
                case "del":
                    return Delete(model);
                case "edit":
                    return await Modify(model);
                case "add":
                    return await Insert(model);
                default:
                    return Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo("异常")));
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("UsersManagement.Delete")]
        private JsonResult Delete(UserDto model)
        {
            _userApplicationService.Delete(model);
            return Json(new MvcAjaxResponse());
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("UsersManagement.Insert")]
        private async Task<JsonResult> Insert(UserDto model)
        {
            if (model.TenantName == "0")
                model.TenantId = null;
            else
                model.TenantId = int.Parse(model.TenantName);
            var temp = await _userApplicationService.Add(model);
            if (temp == 1)
            { 
                
            }
            return Json(new MvcAjaxResponse());
        }

        /// <summary>
        /// 用户修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("UsersManagement.Update")]
        private async Task<JsonResult> Modify(UserDto model)
        {
            if (model.TenantName == "0")
                model.TenantId = null;
            else
                model.TenantId = int.Parse(model.TenantName);
            var temp = await _userApplicationService.Update(model);
            return Json(new MvcAjaxResponse());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public JsonResult SaveUserPicture(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String.Replace("data:image/png;base64,", ""));
            _userApplicationService.UpdateImage(imageBytes);
            return Json("");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FileResult ShowImage()
        {
            return File(_userApplicationService.GetUserInfo(AbpSession.UserId.Value).Image, "image/jpg");
        }

        /// <summary>
        /// 个人资料
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ProfileInfo()
        {
            Models.ProfileInfoModel model = new Models.ProfileInfoModel();
            model.user = _userApplicationService.GetUserInfo(AbpSession.UserId.Value);
            if (model.user.TenantId.HasValue)
                model.TenantName = _tenantApplicationService.FirstOrDefault(model.user.TenantId.Value).Name;
            else
                model.TenantName = null;
            if (model.user.CompanyId.HasValue)
                model.CompanyName = _companyAppService.FirstOrDefault(model.user.CompanyId.Value).CompanyName;
            else
                model.CompanyName = null;
            model.Permissions = AbpSession.Permissions.OrderBy(entity => entity.Name).ToList();
            model.UserRoles = await _userManager.GetRolesAsync(AbpSession.UserId.Value);
            model.DataLogs = _dataLogsAppService.GetTopDataLog(10);
            List<string> list = new List<string>();
            if (AbpSession.RegionIds != null)
            {
                foreach (var entity in _regionAppService.GetUseDataPermissions())
                {
                    if (entity != null)
                        list.Add(entity.RegionName);
                }
            }
            model.RegionList = list;
            list = new List<string>();
            if (AbpSession.ParkIds != null)
            {
                foreach (var entity in _parkAppService.GetUseDataPermissions())
                {
                    if (entity != null)
                        list.Add(entity.ParkName);
                }
            }
            model.ParkList = list;
            list = new List<string>();
            if (AbpSession.BerthsecIds != null)
            {
                foreach (var entity in _berthsecAppService.GetUseDataPermissions())
                {
                    if (entity != null)
                        list.Add(entity.BerthsecName);
                }
            }
            model.BerthsecList = list;
            return View("ProfileInfo", model);
        }

        /// <summary>
        /// 保存个性化设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<JsonResult> SaveProfileInfo(UpdateProfileInfoModel model)
        {
            var entity = _userManager.GetUserById(AbpSession.UserId.Value);
            entity.Birthday = model.Birthday;
            entity.EmailAddress = model.EmailAddress;
            entity.Gender = model.Gender;
            entity.Surname = model.Surname;
            entity.UserName = model.UserName;
            entity.Telephone = model.Telephone;
            var identityResult = await _userManager.UpdateAsync(entity);
            if (!string.IsNullOrWhiteSpace(model.NewPassord))
            {
                if (model.NewPassord == model.ConfirmPassword)
                {
                    await _userApplicationService.ChangePasswordAsync(entity, model.NewPassord);
                }
            }
            return Json("");
        }

        /// <summary>
        /// 删除审计日志
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult DeteleAuditLog(long Id)
        {
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> UserSetting()
        {
            var settings = await _settingStore.GetAllListAsync(AbpSession.TenantId, AbpSession.UserId.Value);
            var model = new P4.Web.Models.SystemSettingModel();
            model.LoginPush = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "LoginPush").Value);
            model.Multiuser = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "Multiuser").Value);
            model.OperationLog = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "OperationLog").Value);
            model.OperationPush = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "OperationPush").Value);
            model.SystemPromptBox = settings.FirstOrDefault(entity => entity.Name == "SystemPromptBox").Value;
            model.Language = settings.FirstOrDefault(entity => entity.Name == "Language").Value;

            model.ComboxValue = _dictionarysApplicationService.GetAllValueData(P4Consts.SystemPromptBox);
            model.Languages = _dictionarysApplicationService.GetAllValueData(P4Consts.SystemLanguage);
            
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSignalrMessage()
        {
            var temp = _messageAppService.GetSignalRMessageTypeList(new P4.Messages.Dto.SignalRMessageTypeInput());
            return Json(temp);
        }

        /// <summary>
        /// 用户设置
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> SetSystemSetting(P4.Web.Models.SystemSettingModel entity)
        {
            SettingInfo model = new SettingInfo();
            model.TenantId = AbpSession.TenantId.Value;
            model.UserId = AbpSession.UserId.Value;
            model.Name = "OperationLog";
            model.Value = entity.OperationLog.ToString();
            await _settingStore.UpdateAsync(model);
            model.Name = "LoginPush";
            model.Value = entity.LoginPush.ToString();
            await _settingStore.UpdateAsync(model);
            model.Name = "Multiuser";
            model.Value = entity.Multiuser.ToString();
            await _settingStore.UpdateAsync(model);
            model.Name = "OperationPush";
            model.Value = entity.OperationPush.ToString();
            await _settingStore.UpdateAsync(model);
            model.Name = "SystemPromptBox";
            model.Value = entity.SystemPromptBox;
            await _settingStore.UpdateAsync(model);

            if (!Abp.Localization.GlobalizationHelper.IsValidCultureCode(entity.Language))
            {
                throw new AbpException("未知语言: " + entity.Language + ". 必须是一个有效的语言！");
            }

            Response.Cookies.Add(new HttpCookie("Abp.Localization.CultureName", entity.Language) { Expires = Clock.Now.AddYears(2) });

            model.Name = "Language";
            model.Value = entity.Language;
            await _settingStore.UpdateAsync(model);

            _messageAppService.SaveUserSubscribeMessageSetting(entity.MessageString);

            return Json(new MvcAjaxResponse());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult UserInbox()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Messages()
        {
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetMessagesList(Messages.Dto.MessageInput input)
        {
            var list = _messageAppService.GetAll(input);
            return Json(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetNoticesList(Notices.Dto.NoticeInput input)
        {
            var list = _noticeAppService.GetAll(input);
            return Json(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public JsonResult GetTasksList(Tasks.Dto.TaskInput input)
        {
            var list = _taskAppService.GetAll(input);
            return Json(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Notices()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Tasks()
        {
            return View();
        }

        public JsonResult ReadTask(List<int> Ids)
        {
            bool flag = _taskAppService.ReadTask(Ids);
            if (flag)
            {
                return Json(new { a = "OK" });
            }
            else
            {
                return Json(new { a = "NO" });
            }
        }


        public JsonResult ReadNotice(List<int> Ids)
        {
            bool flag = _noticeAppService.ReadNotice(Ids);
            if (flag)
            {
                return Json(new { a = "OK" });
            }
            else
            {
                return Json(new { a = "NO" });
            }
        }

        public JsonResult ReadMessage(List<int> Ids)
        {
            bool flag = _messageAppService.ReadMessage(Ids);
            if (flag)
            {
                return Json(new { a = "OK" });
            }
            else
            {
                return Json(new { a = "NO" });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string UserOperPermission(string userid)
        {
            return _treeApplicationService.OperPermission(userid, "user");

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string UserDataPermission(string userid)
        {
            return _treeApplicationService.DataPermission(userid, "user");
        }

        /// <summary>
        /// 初始化密码
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [AbpMvcAuthorize("UsersManagement.Field3")]
        public async Task<JsonResult> InitPassword(long id)
        {
            var user = _userApplicationService.GetUserInfo(id);
            if (!user.TenantId.HasValue)
            {
                return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo("用户无所属商户不能初始化密码")));
            }
            var tenant = _tenantApplicationService.FirstOrDefault(user.TenantId.Value);
            await _userApplicationService.ChangePasswordAsync(user, tenant.Password ?? "123456");
            return Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存用户权限
        /// </summary>
        /// <param name="savetype"></param>
        /// <param name="chooseuserid"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public async Task<JsonResult> SavePermission(string savetype, long chooseuserid, string nodes)
        {
            
            nodes = nodes.Replace("checked", "check");
            List<ZTree> zlist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ZTree>>(nodes);
            List<Permission> permissionlist = new List<Permission>();
            //long userid = long.Parse();
            P4.Users.User user = new Users.User() { Id = chooseuserid };
            if (savetype == "data")
            {
                CreateOrUpdateDataPermissionsInput input = new CreateOrUpdateDataPermissionsInput();
                input.UserId = chooseuserid;
                input.datapermission =_treeApplicationService.AnalyzeDataPermission(zlist);
                _datapermissionApplicationService.UserDataPermissionsInsert(input);
               // DataPermission dp= anasyDataPermission(zlist);
            }
            else if (savetype == "oper")
            {
                foreach (ZTree zt in zlist)
                {
                    permissionlist.Add(PermissionManager.GetPermissionOrNull(zt.id));
                }
                await _userApplicationService.SavePermission(user, permissionlist);
            }

            return Json(new MvcAjaxResponse());
        }

        /// <summary>
        /// 获取用户配置信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [DisableAuditing]
        public JsonResult GetUserSetting(string name)
        {
            SettingInfo info = new SettingInfo();
            _cacheManager.GetUserSettingsCache().GetOrDefault(AbpSession.UserId.Value).TryGetValue(name, out info);
            return this.Json(info.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [DisableAuditing]
        public async Task<JsonResult> SaveUserSetting(string name, string value)
        {
            Dictionary<string, SettingInfo> dic = new Dictionary<string, SettingInfo>();
            dic = await _cacheManager.GetUserSettingsCache().GetOrDefaultAsync(AbpSession.UserId.Value);
            dic.Remove(name);
            dic.Add(name, new SettingInfo(AbpSession.TenantId, AbpSession.UserId, name, value));
            await _cacheManager.GetUserSettingsCache().SetAsync(AbpSession.UserId.Value, dic, new TimeSpan(2, 0, 0, 0));
            return this.Json(new MvcAjaxResponse());
        }
    }
}