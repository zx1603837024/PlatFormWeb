using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Web.Mvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using P4.Berths;
using P4.Berthsecs;
using P4.Berthsecs.Dto;
using P4.BlackLists;
using P4.Businesses;
using P4.Businesses.Dtos;
using P4.Card;
using P4.Companys;
using P4.Dictionarys;
using P4.Employees;
using P4.Equipments;
using P4.Equipments.Dtos;
using P4.EquipmentVersion;
using P4.MonthlyCars;
using P4.OtherAccounts;
using P4.Parks;
using P4.Parks.Dtos;
using P4.PrintAdvertisement;
using P4.Rates;
using P4.SpecialLists;
using P4.SystemManager;
using P4.Tenants;
using P4.Tenants.Dto;
using P4.TicketManagement;
using P4.Web.Models;
using P4.WhiteLists;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace P4.Web.Controllers
{
    /// <summary>
    /// PDA外部接口
    /// </summary>
    public class InterfaceController : P4ControllerBase
    {
        #region Var
        private readonly ITickeAppService _abpTicketStyleRepository;
        private readonly IBerthsecAppService _berthsecAppService;
        private readonly ITenantAppService _tenantApplicationService;
        private readonly IWhiteListsAppService _whiteListsAppService;
        private readonly IBerthAppService _berthAppService;
        private readonly IBlackListsAppService _blackListsAppService;
        private readonly IRatesAppService _ratesAppService;
        private readonly IMonthlyCarAppService _monthlyCarAppService;
        private readonly EmployeeManager _abpEmployeesManager;
        private readonly IRepository<BusinessDetailPicture> _businessDetailPictureRepository;
        //private readonly IRepository<Employee, long> _abpEmployeeRepository;
        private readonly ICompanyAppService _companyAppService;
        private readonly IEmployeeAppService _employeeAppService;
        private readonly IEquipmentAppService _equipmentAppService;
        private readonly IRepository<OtherAccount, long> _otherAccountRepository;
        private readonly IOtherAccountAppService _otherAccountAppService;
        //private readonly IParkAppService _parkAppService;
        //private readonly IRepository<BusinessDetail, long> _businessDetailRepository;
        private readonly IBusinessAppService _businessAppService;
        private readonly ISpecialListAppSevice _specialListAppService;
        private readonly IDictionarysAppService _dictionarysAppService;
        private readonly ISystemAppService _systemAppService;
        private readonly ISettingStore _settingStore;
        private readonly IEquipmentVersionAppService _equipmentVersionAppService;
        private readonly IPrintAdAppService _printAdAppService;
        private readonly IPictureStoreAppService _pictureStoreAppService;
        private readonly IParkAppService _parkAppService;
        private readonly IIPassBlackCardAppService _iPassBlackCardAppService;
        private readonly string PictureStore = ConfigurationManager.ConnectionStrings["PictureStore"].ToString();
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
        /// <param name="berthsecAppService"></param>
        /// <param name="tenantApplicationService"></param>
        /// <param name="abpEmployeesManager"></param>
        /// <param name="whiteListsAppService"></param>
        /// <param name="berthAppService"></param>
        /// <param name="blackListsAppService"></param>
        /// <param name="ratesAppService"></param>
        /// <param name="monthlyCarAppService"></param>
        /// <param name="businessDetailPictureRepository"></param>
        /// <param name="employeeAppService"></param>
        /// <param name="companyAppService"></param>
        /// <param name="equipmentAppService"></param>
        /// <param name="otherAccountAppService"></param>
        /// <param name="otherAccountRepository"></param>
        /// <param name="businessAppService"></param>
        /// <param name="specialListAppService"></param>
        /// <param name="dictionarysAppService"></param>
        /// <param name="systemAppService"></param>
        /// <param name="settingStore"></param>
        /// <param name="equipmentVersionAppService"></param>
        /// <param name="printAdAppService"></param>
        public InterfaceController(IBerthsecAppService berthsecAppService, ITenantAppService tenantApplicationService, EmployeeManager abpEmployeesManager, IWhiteListsAppService whiteListsAppService, IBerthAppService berthAppService, IBlackListsAppService blackListsAppService,
            IRatesAppService ratesAppService, IMonthlyCarAppService monthlyCarAppService, IRepository<BusinessDetailPicture> businessDetailPictureRepository, IEmployeeAppService employeeAppService, ICompanyAppService companyAppService, IEquipmentAppService equipmentAppService,
            IOtherAccountAppService otherAccountAppService, IRepository<OtherAccount, long> otherAccountRepository, IBusinessAppService businessAppService,
            ISpecialListAppSevice specialListAppService, IDictionarysAppService dictionarysAppService, ISystemAppService systemAppService, ISettingStore settingStore, IEquipmentVersionAppService equipmentVersionAppService, IPrintAdAppService printAdAppService, IPictureStoreAppService pictureStoreAppService, IParkAppService parkAppService, IIPassBlackCardAppService iPassBlackCardAppService, ITickeAppService abpTicketStyleRepository)
        {
            _abpTicketStyleRepository = abpTicketStyleRepository;
            _berthsecAppService = berthsecAppService;
            _tenantApplicationService = tenantApplicationService;
            _abpEmployeesManager = abpEmployeesManager;
            _whiteListsAppService = whiteListsAppService;
            _berthAppService = berthAppService;
            _blackListsAppService = blackListsAppService;
            _ratesAppService = ratesAppService;
            _monthlyCarAppService = monthlyCarAppService;
            _businessDetailPictureRepository = businessDetailPictureRepository;
            //_abpEmployeeRepository = abpEmployeeRepository;
            _employeeAppService = employeeAppService;
            _companyAppService = companyAppService;
            _equipmentAppService = equipmentAppService;
            _otherAccountRepository = otherAccountRepository;
            _otherAccountAppService = otherAccountAppService;
            //_businessDetailRepository = businessDetailRepository;
            _businessAppService = businessAppService;
            _specialListAppService = specialListAppService;
            _dictionarysAppService = dictionarysAppService;
            _systemAppService = systemAppService;
            _settingStore = settingStore;
            _equipmentVersionAppService = equipmentVersionAppService;
            _printAdAppService = printAdAppService;
            _pictureStoreAppService = pictureStoreAppService;
            _parkAppService = parkAppService;
            _iPassBlackCardAppService = iPassBlackCardAppService;
        }

        /// <summary>
        /// 验证收费员账号密码
        /// </summary>
        /// <param name="userNameOrEmailAddress"></param>
        /// <param name="plainPassword"></param>
        /// <param name="tenancyName"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<JsonResult> CheckEmployeeLogin(string userNameOrEmailAddress, string plainPassword, string tenancyName)
        {
            return await CheckEmployeeLoginByDevice(userNameOrEmailAddress, plainPassword, tenancyName, "");
        }

        /// <summary>
        /// 设备型号
        /// </summary>
        /// <param name="userNameOrEmailAddress"></param>
        /// <param name="plainPassword"></param>
        /// <param name="tenancyName"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="DeviceType"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<JsonResult> CheckEmployeeLoginByDeviceType(string userNameOrEmailAddress, string plainPassword, string tenancyName, string DeviceCode, int DeviceType)
        {
            var tenant = _tenantApplicationService.FirstOrDefault(tenancyName);
            if (tenant == default(TenantDto))
            {
                return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 7, Message = "未找到此商户: " + tenancyName }), JsonRequestBehavior.AllowGet);
            }

            if (!tenant.IsActive)
            {
                return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 8, Message = "商户组织被锁定: " + tenant.Name }), JsonRequestBehavior.AllowGet);
            }

            var loginResult = await _abpEmployeesManager.LoginAsync(userNameOrEmailAddress, plainPassword, tenant.Id, DeviceCode, DeviceType);

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
                case AbpLoginResultType.EquipmentIsNotActive:
                    return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 10, Message = DeviceCode + "设备未启用！" }), JsonRequestBehavior.AllowGet);
                case AbpLoginResultType.PDAbindUser:
                    return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 10, Message = DeviceCode + "设备与用户未绑定！" }), JsonRequestBehavior.AllowGet);
                default:
                    return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 6, Message = "未知错误的登录!" }), JsonRequestBehavior.AllowGet);
            }
            var berthsecs = _berthsecAppService.GetAllBerthsecCheckList(loginResult.User.Id);
            return this.Json(new MvcAjaxResponse(berthsecs), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 验证收费员账号密码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<JsonResult> CheckEmployeeLoginByDevice(string userNameOrEmailAddress, string plainPassword, string tenancyName, string DeviceCode)
        {
            return await CheckEmployeeLoginByDeviceType(userNameOrEmailAddress, plainPassword, tenancyName, DeviceCode, 0);
        }

        /// <summary>
        /// 收费员签退
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <param name="BerthsecId"></param>
        /// <param name="DeviceCode"></param>
        /// <returns></returns>
        public JsonResult EmployeeLogoutById(Int32 EmployeeId,  int BerthsecId, string DeviceCode)
        {
            DateTime checkInOrOutTime = DateTime.Now;
            _berthsecAppService.EmployeeCheckOutPro(BerthsecId,  DeviceCode,  EmployeeId,  checkInOrOutTime);
            AuthenticationManager.SignOut();
            return this.Json(new MvcAjaxResponse("退出成功"), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 收费员签退
        /// </summary>
        /// <param name="berthsecid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public JsonResult EmployeeOutLineLogout(string berthsecid, int userid, string DeviceCode, string Longitude, string Latitude)
        {
            DateTime checkInOrOutTime = DateTime.Now;
            #region 判断是否启用电子围栏
            //bool ElectronicFence = bool.Parse(_settingStore.GetSettingOrNull(AbpSession.TenantId.Value, null, "ElectronicFence").Value);
            //double ElectronicFenceOffset = double.Parse(_settingStore.GetSettingOrNull(AbpSession.TenantId.Value, null, "ElectronicFenceOffset").Value);

            //if (ElectronicFence && !string.IsNullOrWhiteSpace(Longitude) && Longitude != "0")
            //{
            //    var berthsec = _berthsecAppService.Load(berthsecid);

            //    if ((double.Parse(berthsec.Lng) - ElectronicFenceOffset) < double.Parse(Longitude) && double.Parse(Longitude) < (double.Parse(berthsec.Lng) + ElectronicFenceOffset)
            //        && (double.Parse(berthsec.Lat) - ElectronicFenceOffset) < double.Parse(Latitude) && double.Parse(Latitude) < (double.Parse(berthsec.Lat) + ElectronicFenceOffset)
            //        )
            //    {
            //        _berthsecAppService.EmployeeCheckOutOulinePro(berthsecid, DeviceCode, userid, checkInOrOutTime);
            //        AuthenticationManager.SignOut();
            //        return this.Json(new MvcAjaxResponse("退出成功"), JsonRequestBehavior.AllowGet);
            //    }
            //    else
            //    {
            //        return this.Json(new MvcAjaxResponse(new ErrorInfo() { Message = "签退位置偏移，请到正确的下班地点签退！" }), JsonRequestBehavior.AllowGet);
            //    }
            //}
            #endregion
            _berthsecAppService.EmployeeCheckOutOulinePro(berthsecid, DeviceCode, userid, checkInOrOutTime);

            AuthenticationManager.SignOut();
            return this.Json(new MvcAjaxResponse("退出成功"), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 收费员签退
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        public JsonResult EmployeeLogout()
        {
            if (!AbpSession.UserId.HasValue)
                return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 12, Message = "退出失败，在线用户信息已经丢失！ " }), JsonRequestBehavior.AllowGet);
            //修改泊位段的数据
            int berthsecID = AbpSession.BerthsecIds[0];
            string DeviceCode = AbpSession.DeviceCode;
            int employeeID = Convert.ToInt32(AbpSession.UserId);

            #region 修改收费员数据AbpEmployees
            //_berthsecAppService.UpdateBerthsecOut(berthsecID, DeviceCode, employeeID);  //泊位段签退
            //修改收费员数据AbpEmployees
            //var employee = _abpEmployeeRepository.FirstOrDefault(entity => entity.Id == employeeID);
            //_employeeAppService.GetEmployeeById(employeeID);
            //employee.CheckOut = true;
            //employee.CheckIn = false;
            //employee.CheckOutTime = DateTime.Now;
            //employee.CheckOuttype = "1";  //PDA签退
            ////_abpEmployeeRepository.Update(employee);
            //_employeeAppService.UpdateEmployee(employee);
            #endregion

            //收费员签退历史表
            int ParkID = AbpSession.ParkIds[0];
            DateTime checkInOrOutTime = DateTime.Now;
            //string DeviceCode=AbpSession.DeviceCode;
            //int BerthsecId
            _berthsecAppService.EmployeeCheckOutPro(berthsecID, DeviceCode, employeeID, checkInOrOutTime);

            #region 收费员解锁欠费记录
            //_employeeAppService.InAndOutEmployeeCheck(employeeID, ParkID, checkIn, checkInOrOutTime, checkOut, DeviceCode, berthsecID);
            //收费员解锁欠费记录
            //_businessAppService.CheckOutDeblockByEmployee();
            //var EscapeDList = _businessAppService.GetAllLockBusiness();
            //if (EscapeDList != null)
            //{
            //    BusinessDetail BD = null;
            //    for (int i = 0; i < EscapeDList.Count(); i++) //解锁欠费记录
            //    {
            //        BD = EscapeDList[i];
            //        BD.IsLock = false;
            //        BD.EmployeeId = AbpSession.UserId;
            //        //_businessDetailRepository.Update(BD);
            //        _businessAppService.UpdateBusinessLock(BD);
            //    }
            //}
            #endregion

            AuthenticationManager.SignOut();
            return this.Json(new MvcAjaxResponse("退出成功"), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 收费员登录
        /// </summary>
        /// <param name="userNameOrEmailAddress"></param>
        /// <param name="plainPassword"></param>
        /// <param name="tenancyName"></param>
        /// <param name="berthsecId"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="VersionNum"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<JsonResult> EmployeeLogin(string userNameOrEmailAddress, string plainPassword, string tenancyName, string berthsecId, string DeviceCode, string VersionNum)
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

            List<int> list = new List<int>();
            foreach (var str in berthsecId.Split(','))
            {
                list.Add(int.Parse(str));
            }
            var berthsec = _berthsecAppService.GetBerthsecsListByList(list);


            if (list.Count != berthsec.Count)
            {
                return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 11, Message = "不存在此泊位段!" }), JsonRequestBehavior.AllowGet);
            }
            //泊位段已签到

            List<int> ParkId = new List<int>();
            List<int> RegionId = new List<int>();
            foreach (var model in berthsec)
            {
                ParkId.Add(model.ParkId);
                RegionId.Add(model.RegionId);
                if (model.UseStatus == true && model.CheckInDeviceCode != DeviceCode)
                {
                    return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 10, Message = model.BerthsecName + "已签到！" }), JsonRequestBehavior.AllowGet);
                }
            }

            var loginResult = await _abpEmployeesManager.LoginAsync(userNameOrEmailAddress, plainPassword, tenant.Id, list, ParkId, RegionId, DeviceCode, "0");
            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    //var employee = _abpEmployeeRepository.FirstOrDefault(entity => entity.UserName == userNameOrEmailAddress && entity.Password == plainPassword);
                    var employee = _employeeAppService.GetEmployeeByNameAndPassword(userNameOrEmailAddress, plainPassword, tenant.Id);
                    int employeeID = Convert.ToInt32(employee.Id);

                    _berthsecAppService.EmployeeCheckInPro(list, DeviceCode, employeeID, tenant.Id, DateTime.Now, employee.CompanyId, ParkId[0], VersionNum, employee.BatchNo);

                    //_berthsecAppService.UpdateBerthsecIn(berthsecId, DeviceCode, employeeID, tenant.Id);//收费员签到泊位段
                    ////收费员签到修改abpEmployee表
                    //employee.CheckIn = true;
                    //employee.CheckOut = false;
                    //employee.CheckInTime = DateTime.Now;
                    //_employeeAppService.UpdateEmployee(employee);
                    ////收费员签到历史表
                    //int ParkID = employee.ParkId;
                    //bool checkIn = true;
                    //DateTime checkInOrOutTime = DateTime.Now;
                    //bool checkOut = false;
                    ////_employeeAppService.InAndOutEmployeeCheck(employeeID, ParkID, checkIn, checkInOrOutTime, checkOut, DeviceCode, berthsecId);
                    //_employeeAppService.UpdateEmployeeCheckIn(employeeID, ParkID, checkIn, checkInOrOutTime, checkOut, DeviceCode, berthsecId, employee.CompanyId, employee.TenantId);

                    //收费员登录 设备表添加记录
                    await _equipmentAppService.InsertEquipment(DeviceCode, VersionNum, employee.TenantId, employee.CompanyId);

                    await _equipmentAppService.UpdateVersion(DeviceCode, VersionNum, tenant.Id, employee.CompanyId);//更新版本号
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
                    return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 5, Message = "您的邮件地址未确认!" }), JsonRequestBehavior.AllowGet);
                case AbpLoginResultType.EmployeeIsCheck:
                    return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 9, Message = "您已经登录过了！" }), JsonRequestBehavior.AllowGet);
                default:
                    return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 6, Message = "未知错误的登录!" }), JsonRequestBehavior.AllowGet);
            }
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);               //退出外部cookie
            AbpSession.UserMenus = null;
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, loginResult.Identity);
            return this.Json(new MvcAjaxResponse(DateTime.Now.ToString()), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 收费员离线之后的登录（不处理泊位段签到，收费员签到）
        /// </summary>
        /// <param name="userNameOrEmailAddress"></param>
        /// <param name="plainPassword"></param>
        /// <param name="tenancyName"></param>
        /// <param name="berthsecId"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="VersionNum"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<JsonResult> EmployeeLoginOffLine(string userNameOrEmailAddress, string plainPassword, string tenancyName, string berthsecId, string DeviceCode, string VersionNum)
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


            List<int> list = new List<int>();
            foreach (var str in berthsecId.Split(','))
            {
                list.Add(int.Parse(str));
            }
            var berthsec = _berthsecAppService.GetBerthsecsListByList(list);


            if (list.Count != berthsec.Count)
            {
                return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 11, Message = "不存在此泊位段!" }), JsonRequestBehavior.AllowGet);
            }
            //泊位段已签到

            List<int> ParkId = new List<int>();
            List<int> RegionId = new List<int>();
            foreach (var model in berthsec)
            {
                ParkId.Add(model.ParkId);
                RegionId.Add(model.RegionId);
                if (model.UseStatus == true && model.CheckInDeviceCode != DeviceCode)
                {
                    return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 10, Message = model.BerthsecName + "已签到！" }), JsonRequestBehavior.AllowGet);
                }
            }

            var loginResult = await _abpEmployeesManager.LoginAsync(userNameOrEmailAddress, plainPassword, tenant.Id, list, ParkId, RegionId, DeviceCode, "0");
            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    //_equipmentAppService.UpdateVersion(DeviceCode, VersionNum, tenant.Id, berthsec);//更新版本号
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
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);               //退出外部cookie
            AbpSession.UserMenus = null;
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, loginResult.Identity);
            return this.Json(new MvcAjaxResponse(DateTime.Now.ToString()), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 下载启动数据
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task<JsonResult> DownParameter()
        {
            var settings = await _settingStore.GetAllListAsync(AbpSession.TenantId.Value, null);
            PDAModel model = new PDAModel();
            List<BerthsecDto> list = _berthsecAppService.LoadToSql(AbpSession.BerthsecIds);
            model.WhiteList = _whiteListsAppService.GetAllWhiteCar();               //白名单
            model.MonthlyCarList = _monthlyCarAppService.GetAllMonthlyCar();        //包月卡
            model.SpecialCarList = _specialListAppService.GetAllSpecialCar();
            model.BlackList = _blackListsAppService.GetAllBlackList();
            model.Berths = _berthAppService.GetAllBerths();
            model.Employee = _employeeAppService.LoadPDAEmployee(AbpSession.UserId.Value);
            Companys.Dtos.CompanyDto companydto = _companyAppService.FirstOrDefault(AbpSession.CompanyId.Value);
            model.BerthsecList = list;
            model.CompanyName = companydto.CompanyName;
            model.CompanyID = companydto.Id;
            model.CarInTicketCss = _abpTicketStyleRepository.GetCarInTicketStyle();
            model.CarOutTicketCss = _abpTicketStyleRepository.GetCarOutTicketStyle();
            model.OweTicketCSS = _abpTicketStyleRepository.GetOweTicketStyle();
            model.RepayTicketCss = _abpTicketStyleRepository.GetRepayTicketStyle();
            model.DayChargeTicketCss = _abpTicketStyleRepository.GetDayChargeTicketStyle();
            //model.Css = _abpTicketStyleRepository.GetDayChargeTicketStyle();


            ParkInfoMap parkinfo = _parkAppService.GetParkInfoById(AbpSession.ParkIds[0]);
            model.ParkType = parkinfo.ParkType;

            #region PDA配置参数
            model.SysInfoPassword = settings.FirstOrDefault(entity => entity.Name == "PDAPassword1").Value;
            model.CardMenuPassword = settings.FirstOrDefault(entity => entity.Name == "PDAPassword2").Value;
            model.HrTotalPassword = settings.FirstOrDefault(entity => entity.Name == "PDAPassword3").Value;
            model.LogoutPassword = settings.FirstOrDefault(entity => entity.Name == "PDAPassword4").Value;
            model.Password5 = settings.FirstOrDefault(entity => entity.Name == "PDAPassword5").Value;
            model.PDAChar = settings.FirstOrDefault(entity => entity.Name == "PDAChar").Value;
            model.PDAInCarEscape = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "PDAInCarEscape").Value);
            model.PDAInCarPhotoFlag = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "PDAInCarPhotoFlag").Value);
            model.PDAInCarPhotoNum = settings.FirstOrDefault(entity => entity.Name == "PDAInCarPhotoNum").Value;
            model.PDAOutCarEscape = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "PDAOutCarEscape").Value);
            model.PDAOutCarPhotoFlag = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "PDAOutCarPhotoFlag").Value);
            model.PDAOutCarPhotoNum = settings.FirstOrDefault(entity => entity.Name == "PDAOutCarPhotoNum").Value;
            model.PDARegion = settings.FirstOrDefault(entity => entity.Name == "PDARegion").Value;
            model.PDAPrepaidFlag = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "PDAPrepaidFlag").Value);
            model.PDAPrepaid = settings.FirstOrDefault(entity => entity.Name == "PDAPrepaid").Value;//预缴金额
            model.SensorTimer = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "SensorTimer").Value);//车检器计时
            model.EscapeBlack = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "EscapeBlack").Value);
            model.EscapeBlackMoney = int.Parse(settings.FirstOrDefault(entity => entity.Name == "EscapeBlackMoney").Value);
            model.EscapePhoto = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "EscapePhoto").Value);
            model.EscapePhotoMoney = int.Parse(settings.FirstOrDefault(entity => entity.Name == "EscapePhotoMoney").Value);
            model.PeriodPaid = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "PeriodPaid").Value);
            model.PeriodTime = settings.FirstOrDefault(entity => entity.Name == "PeriodTime").Value;
            model.PeriodTime1 = settings.FirstOrDefault(entity => entity.Name == "PeriodTime1").Value;
            model.WeixinPay = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "WeixinPay").Value);
            model.AliPay = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "AliPay").Value);
            model.VideoRecognition = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "VideoRecognition").Value);
            model.SensorMorningBegin = int.Parse(settings.FirstOrDefault(entity => entity.Name == "SensorMorningBegin").Value);
            model.SensorMorningDelay = int.Parse(settings.FirstOrDefault(entity => entity.Name == "SensorMorningDelay").Value);
            model.SensorMorningEnd = int.Parse(settings.FirstOrDefault(entity => entity.Name == "SensorMorningEnd").Value);
            model.SensorNightBegin = int.Parse(settings.FirstOrDefault(entity => entity.Name == "SensorNightBegin").Value);
            model.SensorNightDelay = int.Parse(settings.FirstOrDefault(entity => entity.Name == "SensorNightDelay").Value);
            model.SensorNightEnd = int.Parse(settings.FirstOrDefault(entity => entity.Name == "SensorNightEnd").Value);
            model.PDASyncData = bool.Parse(settings.FirstOrDefault(entitiy => entitiy.Name == "PDASyncData").Value);
            model.PrivilegeCarReceipt = bool.Parse(settings.FirstOrDefault(entitiy => entitiy.Name == "PrivilegeCarReceipt").Value);
            model.UploadSqlite = bool.Parse(settings.FirstOrDefault(entitiy => entitiy.Name == "UploadSqlite").Value);
            model.OnlinePayUrl = settings.FirstOrDefault(entitiy => entitiy.Name == "OnlinePayUrl").Value;
            #endregion

            //获得刷卡折扣
            model.WeixinPay = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "WeixinPay").Value);
            model.WeixinDiscount = settings.FirstOrDefault(entity => entity.Name == "WeixinDiscount").Value;
            model.EscapePrint = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "EscapePrint").Value);
            model.EscapeXingCode = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "EscapeXingCode").Value);
            model.IPassCardPay = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "IPassCardPay").Value);
            model.IPassCardDiscount = settings.FirstOrDefault(entity => entity.Name == "IPassCardDiscount").Value;

            model.CashPay = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "CashPay").Value); 
            model.CashPayDiscount = settings.FirstOrDefault(entity => entity.Name == "CashPayDiscount").Value;

            model.OutCarRecognition = bool.Parse(settings.FirstOrDefault(entity => entity.Name == "OutCarRecognition").Value);
            model.DictionaryValueList = _dictionarysAppService.GetAllValueData(P4Consts.CardDiscount);
            model.PrintList = _printAdAppService.GetAllPrintAdsList();
            model.IPassBlackCardList = _iPassBlackCardAppService.GetIPassBlackCardList();
            return Json(new MvcAjaxResponse(model), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 照片上传接口（服务端）
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="businessID"></param>
        /// <returns></returns>
        [AbpAuthorize]
        [HttpPost]
        public JsonResult PhotoUpLoad(Guid guid, int businessID)
        {
            try
            {
                Stream str = Request.InputStream;
                byte[] bytes = new byte[str.Length];
                str.Read(bytes, 0, bytes.Length);
                str.Seek(0, SeekOrigin.Begin);
                BusinessDetailPicture entity = new BusinessDetailPicture();
                entity.BusinessDetailGuid = guid;
                entity.BusinessDetailId = businessID;
                //entity.BusinessDetailPictureUrl = guid + "/" + dt + ".jpg";
                entity.CreatorUserId = AbpSession.UserId;
                BusinessDetailPicture insertResult = _businessDetailPictureRepository.Insert(entity);
                _businessAppService.CreatePicture(new PicMongoDto() { BusinessDetailGuid = guid, BusinessDetailId = insertResult.Id, Image = bytes });
                return Json(new { ReturnStr = insertResult == null ? "false" : "true" });
            }
            catch (Exception ex)
            {

                return Json(new { ReturnStr = ex.Message });
            }
        }

        /// <summary>
        /// 上传数据库文件
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadSqliteDB(string EmployeeId)
        {
            try
            {
                Stream str = Request.Files["sqlitedb"].InputStream;
                byte[] bytes = new byte[str.Length];
                str.Read(bytes, 0, bytes.Length);
                str.Seek(0, SeekOrigin.Begin);

                if (Directory.Exists(Server.MapPath("~\\sqlitedb\\" + DateTime.Now.ToString("yyyy-MM-dd"))) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\sqlitedb\\" + DateTime.Now.ToString("yyyy-MM-dd")));
                }
                System.IO.File.WriteAllBytes(Request.PhysicalApplicationPath + "sqlitedb\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\" + EmployeeId + "&" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".db", bytes);
                return Json(new { ReturnStr = "true" });
            }
            catch (Exception ex)
            {
                return Json(new { ReturnStr = ex.Message });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExceptionMsg"></param>
        /// <returns></returns>
        public JsonResult UploadAndroidExceptionLog(Guid guid, string ExceptionMsg)
        {
            AndroidLogDto input = new AndroidLogDto() { guid = guid, ExceptionMsg = ExceptionMsg };
            _equipmentAppService.InsertAndroidLog(input);
            return Json(new { ReturnStr = "true" });
        }

        /// <summary>
        /// 图片上传，android
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        [HttpPost]
        public JsonResult PhotoUpLoadToAndroid(Guid guid, int businessid)
        {
            try
            {
                if (_businessAppService.GetBusinessDetailPicture(guid, businessid))
                {
                    return Json("true");//记录已经存在，避免重复上传图片
                }

                Stream str = Request.Files["pic"].InputStream;
                byte[] bytes = new byte[str.Length];
                str.Read(bytes, 0, bytes.Length);
                str.Seek(0, SeekOrigin.Begin);
                BusinessDetailPicture entity = new BusinessDetailPicture();
                entity.BusinessDetailGuid = guid;
                entity.BusinessDetailId = businessid;



                //Image image = Image.FromStream(str);
                //string dt = DateTime.Now.ToString("yyyyMMddHHmmssff");
                //string path = "../BusinessImage/" + guid + "/" + dt + ".jpg";
                //DirectoryHelper.CreateIfNotExists(Server.MapPath("../BusinessImage/" + guid));
                //image.Save(Server.MapPath(path), System.Drawing.Imaging.ImageFormat.Jpeg);

                entity.CreatorUserId = AbpSession.UserId;
                BusinessDetailPicture insertResult = _businessDetailPictureRepository.Insert(entity);

                if (PictureStore == "SqlServer")
                {
                    _pictureStoreAppService.InsertPicture(new PicMongoDto() { BusinessDetailGuid = guid, BusinessDetailId = insertResult.Id, Image = bytes });
                }
                else
                {
                    _businessAppService.CreatePicture(new PicMongoDto() { BusinessDetailGuid = guid, BusinessDetailId = insertResult.Id, Image = bytes });
                }

                return Json(new { ReturnStr = insertResult == null ? "false" : "true" });
            }
            catch (Exception ex)
            {
                return Json(new { ReturnStr = ex.Message });
            }
        }


        /// <summary>
        /// 图片上传，android
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        [HttpPost]
        public JsonResult PhotoUpLoadToAndroid2(Guid guid, int businessid, int pictype)
        {
            Stream str = Request.Files["pic"].InputStream;
            return PhotoUpLoadToAndroid(guid, str, businessid, pictype, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="str"></param>
        /// <param name="businessid"></param>
        /// <param name="pictype"></param>
        /// <param name="createTime"></param>
        /// <returns></returns>
        [AbpAuthorize]
        private JsonResult PhotoUpLoadToAndroid(Guid guid, Stream str, int businessid, int pictype, string createTime)
        {
            try
            {
                if (_businessAppService.GetBusinessDetailPicture(guid, businessid))
                {
                    return Json("true");//记录已经存在，避免重复上传图片
                }
                //Stream str = Request.Files["pic"].InputStream;
                byte[] bytes = new byte[str.Length];
                str.Read(bytes, 0, bytes.Length);
                str.Seek(0, SeekOrigin.Begin);
                BusinessDetailPicture entity = new BusinessDetailPicture();
                entity.BusinessDetailGuid = guid;
                entity.BusinessDetailId = businessid;
                entity.PicType = pictype;
                entity.CreateTime = DateTime.Parse(createTime);

                entity.CreatorUserId = AbpSession.UserId;
                BusinessDetailPicture insertResult = _businessDetailPictureRepository.Insert(entity);

                if (PictureStore == "SqlServer")
                {
                    _pictureStoreAppService.InsertPicture(new PicMongoDto() { BusinessDetailGuid = guid, BusinessDetailId = insertResult.Id, Image = bytes, PicType = pictype });
                }
                else
                {
                    _businessAppService.CreatePicture(new PicMongoDto() { BusinessDetailGuid = guid, BusinessDetailId = insertResult.Id, Image = bytes, PicType = pictype });
                }

                return Json(new { ReturnStr = insertResult == null ? "false" : "true" });
            }
            catch (Exception ex)
            {
                return Json(new { ReturnStr = ex.Message });
            }
        }

        ///<summary>
        ///图片上传，android
        ///</summary>
        ///<returns></returns>
        [AbpAuthorize]
        [HttpPost]
        public JsonResult PhotoUpLoadToAndroid3(Guid guid,int businessid, int pictype,string createTime)
        {
            Stream str = Request.Files["pic"].InputStream;
            return PhotoUpLoadToAndroid(guid, str, businessid, pictype, createTime);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="CarInStr"></param>
        /// <returns></returns>
        public BInsertCarInDto GetStringA(string CarInStr)
        {
            string[] strlist = CarInStr.Split(new string[] { "=", "&" }, StringSplitOptions.RemoveEmptyEntries);
            BInsertCarInDto bicid = new BInsertCarInDto();
            //for (int i = 1; i < strlist.Length; i+=2)
            //{
            //}
            return bicid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userNameOrEmailAddress"></param>
        /// <param name="plainPassword"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public JsonResult CarOwnerLogin(string userNameOrEmailAddress, string plainPassword)
        {
            if (string.IsNullOrEmpty(userNameOrEmailAddress))
            {
                throw new ArgumentNullException("userNameOrEmailAddress");
            }

            if (string.IsNullOrEmpty(plainPassword))
            {
                throw new ArgumentNullException("plainPassword");
            }

            var carOwner = _otherAccountRepository.FirstOrDefault(entity => entity.UserName == userNameOrEmailAddress && entity.Password == plainPassword && entity.IsLock == false);
            if (carOwner == default(OtherAccount))//检测是否存在用户
            {
                return this.Json(new MvcAjaxResponse(new Abp.Web.Models.ErrorInfo() { Code = 1, Message = "用户名验证错误!" }), JsonRequestBehavior.AllowGet);
            }
            return this.Json(new MvcAjaxResponse(carOwner), JsonRequestBehavior.AllowGet);
            //return new AbpLoginResult(carOwner, null);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userPhone"></param>
        /// <returns></returns>
        public JsonResult CarOwnerRegister(string userPhone)
        {
            //对手机号码进行校验
            //????
            return this.Json(_otherAccountAppService.CarOwnerRegister(userPhone));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userPhone"></param>
        /// <returns></returns>
        public string GetIdentifyingCode(string userPhone)
        {
            return _otherAccountAppService.GetIdentifyingCode(userPhone);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userPhone"></param>
        /// <returns></returns>
        public string GetIdentifyingCode1(string userPhone)
        {
            return _otherAccountAppService.GetIdentifyingCode1(userPhone);
        }

        /// <summary>
        /// 下载Apk文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public FileResult DownApk(string id)
        {
            string fileName = id + ".zip";
            string absoluFilePath = Server.MapPath("../../apk/" + fileName);
            return File(new FileStream(absoluFilePath, FileMode.Open), "application/octet-stream", Server.UrlEncode(fileName));
        }

        /// <summary>
        /// 检测版本号
        /// </summary>
        /// <param name="Version">老版本号</param>
        /// <param name="PDA">设备编号</param>
        /// <param name="Type">设备型号</param>
        /// <returns></returns>
        public JsonResult CheckVersion(string OldVersion, string PDA, string Type)
        {
            return this.Json(_equipmentVersionAppService.GetEquipmentVersion(OldVersion, PDA, Type), JsonRequestBehavior.AllowGet);
        }
    }
}