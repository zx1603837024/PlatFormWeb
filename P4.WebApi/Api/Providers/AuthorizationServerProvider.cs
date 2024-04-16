using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Dependency;
using P4.Users;
using Microsoft.Owin.Security.OAuth;
using Abp.Authorization.Users;
using P4.MultiTenancy;
using P4.Authorization;
using Microsoft.Owin.Security;
using P4.Employees;
using Abp.Authorization.Employees;
using P4.Inspectors;
using P4.Equipments;
using P4.Tenants;
using P4.Berthsecs;
using P4.Berthsecs.Dto;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using Abp.Configuration;
using System.Collections.Generic;

namespace P4.WebApi.Api.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider, ITransientDependency
    {
        /// <summary>
        /// The _user manager
        /// </summary>
        private readonly UserManager _userManager;
        private readonly EmployeeManager _abpEmployeesManager;
        private readonly InspectorManager _abpInspectorManager;
        private readonly ITenantAppService _abpTenantAppService;
        private readonly IBerthsecAppService _abpBerthsecAppService;
        private readonly IEmployeeAppService _employeeAppService;
        private readonly IEquipmentAppService _equipmentAppService;
        private readonly ISettingStore _settingStore;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="abpEmployeesManager"></param>
        /// <param name="abpInspectorManager"></param>
        /// <param name="abpTenantAppService"></param>
        /// <param name="abpBerthsecAppService"></param>
        /// <param name="employeeAppService"></param>
        /// <param name="equipmentAppService"></param>
        /// <param name="settingStore"></param>
        public SimpleAuthorizationServerProvider(UserManager userManager, EmployeeManager abpEmployeesManager, InspectorManager abpInspectorManager, ITenantAppService abpTenantAppService, IBerthsecAppService abpBerthsecAppService, IEmployeeAppService employeeAppService, IEquipmentAppService equipmentAppService, ISettingStore settingStore)
        {
            _userManager = userManager;
            _abpEmployeesManager = abpEmployeesManager;
            _abpInspectorManager = abpInspectorManager;
            _abpTenantAppService = abpTenantAppService;
            _abpBerthsecAppService = abpBerthsecAppService;
            _employeeAppService = employeeAppService;
            _equipmentAppService = equipmentAppService;
            _settingStore = settingStore;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }
            var isValidClient = string.CompareOrdinal(clientId, "app") == 0 && string.CompareOrdinal(clientSecret, "app") == 0;
            if (isValidClient)
            {
                context.OwinContext.Set("as:client_id", clientId);
                context.Validated(clientId);
            }
            else
            {
                context.SetError("invalid client");
            }
        }

        /// <summary>
        /// 创建登录
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Abp.Auditing.Audited]
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var tenantId = context.Scope[0];                //商户代码
            var DeviceCode = context.Scope[1];              //设备编号
            //var berthsecid = int.Parse(context.Scope[2].Split(',')[0].Split('.')[0]);   //签到泊位段id, 列表采用 , 号隔开
            List<int> berthsecids = new List<int>();
            foreach (var str in context.Scope[2].Split(','))
            {
                berthsecids.Add(int.Parse(str.Split('.')[0]));
            }
            var Version = context.Scope[3];                 //版本号 
            var flag = "0";
            if (context.Scope.Count > 4)
                flag = context.Scope[4];                        //登录标记    0:代表第一次登录，1:代表第二次登录

            var Longitude = "0";                                    //定位经度
            var Latitude = "0";                                        //定位纬度
            //if (context.Scope.Count > 5)
            //{
            //    Longitude = context.Scope[5];               //定位经度
            //    Latitude = context.Scope[6];                //定位纬度
            //}
            var result = await GetEmployeeLoginResultAsync(context, context.UserName, context.Password, tenantId, DeviceCode, berthsecids, Version, Longitude, Latitude, flag);
            if (result.Result == AbpLoginResultType.Success)
            {
                //var claimsIdentity = result.Identity;                
                var claimsIdentity = new ClaimsIdentity(result.Identity);
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                var ticket = new AuthenticationTicket(claimsIdentity, new AuthenticationProperties());
                context.Validated(ticket);
            }
            else
            {
                context.SetError(result.Result.ToString());
                //throw new UserFriendlyException("ceshi");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        private void ThrowResponseEx(string ex)
        {
            var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(ex),
                ReasonPhrase = "object is not found"
            };
            throw new HttpResponseException(resp);
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.OwinContext.Get<string>("as:client_id");
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.Rejected();
                return;
            }

            var newId = new ClaimsIdentity(context.Ticket.Identity);
            newId.AddClaim(new Claim("newClaim", "refreshToken"));

            var newTicket = new AuthenticationTicket(newId, context.Ticket.Properties);
            context.Validated(newTicket);
        }



        /// <summary>
        /// 收费员登录（支持经纬度）
        /// </summary>
        /// <param name="context"></param>
        /// <param name="usernameOrEmailAddress"></param>
        /// <param name="password"></param>
        /// <param name="tenancyName"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="berthsecId"></param>
        /// <param name="Version"></param>
        /// <param name="Longitude"></param>
        /// <param name="Latitude"></param>
        /// <returns></returns>
        private async Task<AbpEmployeesManager<Employee>.AbpLoginResult> GetEmployeeLoginResultAsync(OAuthGrantResourceOwnerCredentialsContext context, string usernameOrEmailAddress, string password, string tenancyName, string DeviceCode, List<int> berthsecId, string Version, string Latitude, string Longitude, string flag)
        {
            var entity = _abpTenantAppService.FirstOrDefault(tenancyName);
            if (entity == default(Tenants.Dto.TenantDto))
            {
                return new AbpEmployeesManager<Employee>.AbpLoginResult(AbpLoginResultType.InvalidTenancyName);
            }

            var berthsec = _abpBerthsecAppService.GetBerthsecsListByList(berthsecId);
            if(berthsec.Count != berthsecId.Count)
            {
                ThrowResponseEx("泊位段不存在！");
            }

            //泊位段已签到


            List<int> ParkId = new List<int>();
            List<int> RegionId = new List<int>();

            foreach (var model in berthsec)
            {

                if (!ParkId.Contains(model.ParkId))
                    ParkId.Add(model.ParkId);
                if (!RegionId.Contains(model.RegionId))
                    RegionId.Add(model.RegionId);
                if (model.UseStatus == true && model.CheckInDeviceCode != DeviceCode)
                {
                    ThrowResponseEx("泊位段已签到！");
                }
            }

            //判断是否启用电子围栏
            bool ElectronicFence = bool.Parse(_settingStore.GetSettingOrNull(entity.Id, null, "ElectronicFence").Value);

            //电子围栏定位
            if (ElectronicFence && Latitude != "0" && Longitude != "0")
            {
                double ElectronicFenceOffset = double.Parse(_settingStore.GetSettingOrNull(entity.Id, null, "ElectronicFenceOffset").Value);
                if ((double.Parse(berthsec[0].Lng) - ElectronicFenceOffset) < double.Parse(Longitude) && double.Parse(Longitude) < (double.Parse(berthsec[0].Lng) + ElectronicFenceOffset)
                    && (double.Parse(berthsec[0].Lat) - ElectronicFenceOffset) < double.Parse(Latitude) && double.Parse(Latitude) < (double.Parse(berthsec[0].Lat) + ElectronicFenceOffset)
                    )
                {

                }
                else
                {
                    ThrowResponseEx("签到位置偏移，请到正确的上班地点签到");
                }
            }

            //登录成功
            var loginResult = await _abpEmployeesManager.LoginAsync(usernameOrEmailAddress, password, entity.Id, berthsecId, ParkId, RegionId, DeviceCode, flag);
            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    //签到收费员泊位段
                    await UserCheckIn(usernameOrEmailAddress, password, berthsecId, DeviceCode, entity.Id, Version);
                    return loginResult;
                default:
                    CreateExceptionForFailedLoginAttempt(context, loginResult.Result, usernameOrEmailAddress, tenancyName);
                    return loginResult;
            }
        }

        /// <summary>
        /// 用户签到
        /// </summary>
        /// <param name="userNameOrEmailAddress"></param>
        /// <param name="plainPassword"></param>
        /// <param name="berthsecId"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="tenantId"></param>
        /// <param name="Version"></param>
        private async Task UserCheckIn(string userNameOrEmailAddress, string plainPassword, List<int> berthsecId, string DeviceCode, int tenantId, string Version)
        {
            var employee = _employeeAppService.GetEmployeeByNameAndPassword(userNameOrEmailAddress, plainPassword, tenantId);
            int employeeID = Convert.ToInt32(employee.Id);
            //收费员签到修改abpEmployee表
            employee.CheckIn = true;
            employee.CheckOut = false;
            employee.CheckInTime = DateTime.Now;
            //收费员签到历史表
            int ParkID = int.Parse(employee.ParkId.Split(',')[0]);
            DateTime checkInOrOutTime = DateTime.Now;
            _abpBerthsecAppService.EmployeeCheckInPro(berthsecId, DeviceCode, employeeID, tenantId, checkInOrOutTime, employee.CompanyId, ParkID, Version, employee.BatchNo);

            //_employeeAppService.UpdateEmployee(employee);
            //_abpBerthsecAppService.UpdateBerthsecIn(berthsecId, DeviceCode, employeeID, tenantId);//收费员签到泊位段
            //_employeeAppService.UpdateEmployeeCheckIn(employeeID, ParkID, checkIn, checkInOrOutTime, checkOut, DeviceCode, berthsecId, employee.CompanyId, employee.TenantId);
            //收费员登录 设备表添加记录
            //_equipmentAppService.InsertEquipment(DeviceCode, Version, employee.TenantId, employee.CompanyId);
            await _equipmentAppService.UpdateVersion(DeviceCode, Version, tenantId, employee.CompanyId);//更新版本号
        }

        /// <summary>
        /// 平台用户登录
        /// </summary>
        /// <param name="context"></param>
        /// <param name="usernameOrEmailAddress"></param>
        /// <param name="password"></param>
        /// <param name="tenancyName"></param>
        /// <returns></returns>
        private async Task<AbpUserManager<Tenant, Role, User>.AbpLoginResult> GetLoginResultAsync(OAuthGrantResourceOwnerCredentialsContext context, string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _userManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    CreateExceptionForFailedLoginAttempt(context, loginResult.Result, usernameOrEmailAddress, tenancyName);
                    return loginResult;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <param name="usernameOrEmailAddress"></param>
        /// <param name="tenancyName"></param>
        private void CreateExceptionForFailedLoginAttempt(OAuthGrantResourceOwnerCredentialsContext context, AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    throw new ApplicationException("Don't call this method with a success result!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                    //context.HasError = true;
                    ThrowResponseEx("1,用户名验证错误!");
                    break;
                case AbpLoginResultType.InvalidPassword:
                    //context.HasError = true;
                    ThrowResponseEx("1,用户名验证错误!");
                    break;
                case AbpLoginResultType.InvalidTenancyName:
                    ThrowResponseEx("2,未找到此商户!");
                    break;
                case AbpLoginResultType.TenantIsNotActive:
                    ThrowResponseEx("3,企业账号被锁定!");
                    break;
                case AbpLoginResultType.UserIsNotActive:
                    ThrowResponseEx("4,用户被锁定!");
                    break;
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    ThrowResponseEx("5,您的邮件地址未确认!");
                    break;
                case AbpLoginResultType.EmployeeIsCheck:
                    ThrowResponseEx("9,您已经登录过了!");
                    break;
                case AbpLoginResultType.EquipmentIsNotActive:
                    ThrowResponseEx("10,设备未启用!");
                    break;
                default:
                    //context.HasError = true;
                    ThrowResponseEx("6,未知错误的登录!");
                    break;
            }
        }
    }
}
