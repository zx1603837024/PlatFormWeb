using Abp.Application.Services;
using Abp.Application.Services.Dto;
using P4.Users.Dto;
using Abp.AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Authorization.Users;
using Microsoft.AspNet.Identity;
using Abp.Linq.Extensions;
using P4.Authorization;
using P4.MultiTenancy;
using Abp.Net.Mail;
using Abp.Domain.Uow;
using Abp.Configuration;
using P4.Companys;




namespace P4.Users
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserAppService : ApplicationService, IUserAppService
    {
        #region Var
        private readonly UserManager _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<UserLogin, long> _abpUserLoginRepository;
        private readonly IRepository<UserRole, long> _abpUserRoleRepository;
        private readonly IRepository<Role, int> _abpRoleRepository;
        private readonly IRepository<Tenant> _abpTenantRepository;
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<SettingTemplate> _abpSettingTemplateRepository;
        private readonly ISettingStore _settingManager;
        private readonly IRepository<OperatorsCompany> _abpOperatorsCompanyRepository;
        private readonly IRepository<UserDataPermissionSetting, long> _abpUserDataPermissionRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="userRepository"></param>
        /// <param name="abpUserLoginRepository"></param>
        /// <param name="abpUserRoleRepository"></param>
        /// <param name="abpRoleRepository"></param>
        /// <param name="abpTenantRepository"></param>
        /// <param name="emailSender"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="abpSettingTemplateRepository"></param>
        /// <param name="settingManager"></param>
        /// <param name="abpOperatorsCompanyRepository"></param>
        /// <param name="abpUserDataPermissionRepository"></param>
        public UserAppService(UserManager userManager, IUserRepository userRepository,
            IRepository<UserLogin, long> abpUserLoginRepository, IRepository<UserRole, long> abpUserRoleRepository,
            IRepository<Role, int> abpRoleRepository, IRepository<Tenant> abpTenantRepository,
            IEmailSender emailSender, IUnitOfWorkManager unitOfWorkManager, IRepository<SettingTemplate> abpSettingTemplateRepository, ISettingStore settingManager, IRepository<OperatorsCompany> abpOperatorsCompanyRepository, IRepository<UserDataPermissionSetting, long> abpUserDataPermissionRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _abpUserLoginRepository = abpUserLoginRepository;
            _abpUserRoleRepository = abpUserRoleRepository;
            _abpRoleRepository = abpRoleRepository;
            _abpTenantRepository = abpTenantRepository;
            _emailSender = emailSender;
            _unitOfWorkManager = unitOfWorkManager;
            _abpSettingTemplateRepository = abpSettingTemplateRepository;
            _settingManager = settingManager;
            _abpOperatorsCompanyRepository = abpOperatorsCompanyRepository;
            _abpUserDataPermissionRepository = abpUserDataPermissionRepository;
            //EventBus.Default.Register<EntityChangedEventData<User>>(new ActivityWriter());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ListResultOutput<UserDto> GetUsers()
        {
            if (AbpSession.TenantId.HasValue)//普通用户
                return new ListResultOutput<UserDto>
                {
                    Items = _userManager.Users.ToList().MapTo<List<UserDto>>()
                };
            else
            {
                using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MayHaveCompany))
                {
                    return new ListResultOutput<UserDto>
                    {
                        Items = _userManager.Users.ToList().MapTo<List<UserDto>>()
                    };
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllUserOutput GetAllUsers(UserInput input)
        {
            if (!AbpSession.TenantId.HasValue)
                _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MayHaveCompany);
            int records = _userRepository.GetAll().Filters(input).Count();
            if (records == 0)
                return null;
            List<UserDto> userDtoList = new List<UserDto>();
            List<User> roles = _userRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList();

            foreach (User c in roles)
            {
                UserDto userDto = new UserDto();
                
                userDto.Id = c.Id;
                if (c.CompanyId != 0 && c.CompanyId != null)
                {
                    var company = _abpOperatorsCompanyRepository.FirstOrDefault(e => e.Id == c.CompanyId);
                    if (company != default(Company))
                        userDto.CompanyName = company.CompanyName;
                }

                else
                {
                    userDto.CompanyName = "";
                }
                
                userDto.UserName = c.UserName;
                userDto.Name = c.Name;
                userDto.Surname = c.Surname;
                userDto.EmailAddress = c.EmailAddress;
                //TenantId = c.TenantId,
                userDto.TenantName = c.Tenant == null ? null : c.Tenant.Name;
                userDto.IsEmailConfirmed = c.IsEmailConfirmed;
                userDto.TelePhone = c.Telephone;
                userDto.Birthday = c.Birthday;
                userDto.IsActive = c.IsActive;
                userDto.Gender = c.Gender;
                userDto.LastModificationTime = c.LastModificationTime;
                userDto.LastLoginTime = c.LastLoginTime;
                userDto.LastModifierUserName = c.LastModifierUser == null ? null : c.LastModifierUser.Name;
                if (c.Roles != null && c.Roles.Count > 0)
                {     
                    foreach (var r in c.Roles)
                    {
                        userDto.RoleDisplayName += _abpRoleRepository.FirstOrDefault(r.RoleId) == null ? null : _abpRoleRepository.FirstOrDefault(r.RoleId).Name + ",";
                    }
                    if (!string.IsNullOrEmpty(userDto.RoleDisplayName))
                        userDto.RoleDisplayName = userDto.RoleDisplayName.Substring(0, userDto.RoleDisplayName.Length - 1);
                }
                userDtoList.Add(userDto);
            }
            return new GetAllUserOutput()
            {
                rows = userDtoList,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            return _userRepository.ExecuteSqlCommand("update AbpUsers set TenantId = TenantId", new SqlParameter[] { });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public int UpdateImage(byte[] image)
        {
            return _userRepository.ExecuteSqlCommand("update AbpUsers set image = @image where id = " + AbpSession.UserId.Value, new SqlParameter[] { new SqlParameter("@image", image) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object SqlQuery()
        {
            return _userRepository.SqlQuery();
        }
        /// <summary>
        /// 更新用户信息，需一同更新用户角色表UserRole
        /// 
        /// ？？？？？如果一个用户对应多个角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize("UsersManagement.Update")]
        public async Task<int> Update(UserDto model)
        {
            if (!AbpSession.TenantId.HasValue)
                _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MayHaveCompany);
            User entity = _userManager.GetUserById(model.Id);
            entity.IsActive = model.IsActive;
            entity.Gender = model.Gender;
            entity.EmailAddress = model.EmailAddress;
            entity.IsEmailConfirmed = model.IsEmailConfirmed;
            entity.Name = model.Name;
            entity.UserName = model.UserName;
            if (model.CompanyName == "0")
            {
                entity.CompanyId = null;
            }
            else
            {
                entity.CompanyId = int.Parse(model.CompanyName);
            }
                
            entity.Surname = model.Surname;
            entity.TenantId = model.TenantId;
            entity.Telephone = model.TelePhone;
            //如果model.RoleDisplayName不为空 更改UserRole表数据
            if (!string.IsNullOrWhiteSpace(model.RoleDisplayName))
            {
                //获取更改的角色名
                string[] strarr = model.RoleDisplayName.Split(',');
                //如果用户角色表里存在该用户角色信息，先清空再插入
                await _userManager.SetRoles(entity, strarr);
            }
            return _userManager.Update(entity) == null ? 1 : 0;
            //var roleEntity= _roleApplicationService.
        }
        /// <summary>
        /// 添加用户信息，需要向用户角色关系表UserRole表插入相应数据
        /// ？？？？？添加多个角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize("UsersManagement.Insert")]
        public async Task<int> Add(UserDto model)
        {
            var entity = new User();
            if (!AbpSession.TenantId.HasValue)
            {
                _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MayHaveCompany);
                entity.AuthenticationSource = AbpSession.UserName;
            }
            if (_userRepository.FirstOrDefault(dic => dic.UserName == model.UserName) != default(User))
            {
                return 1;
            }

            if (model.CompanyName == "0")
                entity.CompanyId = null;
            else
                entity.CompanyId = int.Parse(model.CompanyName);
            entity.IsActive = model.IsActive;
            entity.Gender = model.Gender;
            entity.EmailAddress = model.EmailAddress;
            entity.Name = model.Name;
            entity.UserName = model.UserName;
            entity.Surname = model.Surname;
            entity.Birthday = model.Birthday;
            entity.Telephone = model.TelePhone;
            entity.TenantId = model.TenantId;
            if (model.TenantId != null)
            {
                entity.Password = _abpTenantRepository.Load(int.Parse(model.TenantName)).Password ?? P4Consts.InitPassword;//初始化默认密码
            }
            else
                entity.Password = P4Consts.InitPassword;//初始化默认密码


            entity.Password = new PasswordHasher().HashPassword(entity.Password);
            var userid = _userRepository.InsertAndGetId(entity);
            entity.Id = userid;
               
            _unitOfWorkManager.Current.SaveChanges();
            if (!string.IsNullOrWhiteSpace(model.RoleDisplayName))
            {
                string[] strarr = model.RoleDisplayName.Split(',');
                entity.Roles = new List<UserRole>();
                await _userManager.SetRoles(entity, strarr);
            }
            //处理用户个性化设置参数
            GetAll(entity.TenantId, entity.Id);
            _abpUserDataPermissionRepository.Insert(new UserDataPermissionSetting() { UserId = entity.Id, TenantId = (entity.TenantId.HasValue == true ? entity.TenantId.Value : 0), RegionIds = "", ParkIds = "", BerthsecIds = "" });
            return entity.Id == 0 ? 1 : 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantid"></param>
        /// <param name="userid"></param>
        private void GetAll(int? tenantid, long? userid)
        {
            List<SettingTemplate> settingList = new List<SettingTemplate>();
            if (tenantid != null)
                settingList = _abpSettingTemplateRepository.GetAll().Where(entity => entity.TenantId == 1 && entity.UserId == 1).ToList();
            else
                settingList = _abpSettingTemplateRepository.GetAll().Where(entity => entity.TenantId == null && entity.UserId == 1).ToList();
            foreach (var entity in settingList)
            {
                _settingManager.CreateAsync(new SettingInfo(tenantid, userid, entity.Name, entity.Value) { Mark = entity.Mark });
            }
        }

        /// <summary>
        /// 删除用户之前，先判断是用户是否分配了角色
        /// 如果分配了角色，则将用户角色关系表UserRole中此用户的角色数据一并删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AbpAuthorize("UsersManagement.Delete")]
        public int Delete(UserDto entity)
        {
            if (!AbpSession.TenantId.HasValue)
                _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MayHaveCompany);
            _userManager.RemoveFromRoles(entity.Id, _userManager.GetRoles(entity.Id).ToArray<string>());
            _userManager.Delete(new User() { Id = entity.Id });
            return 0;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public User GetUserInfo(long Id)
        {
            if (!AbpSession.TenantId.HasValue)
                _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MayHaveCompany);
            return _userRepository.FirstOrDefault(Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ICollection<Abp.Authorization.Users.UserLogin> Logins(long Id)
        {
            return _abpUserLoginRepository.GetAllList();
        }

        /// <summary>
        /// 初始化密码
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task<IdentityResult> ChangePasswordAsync(User user, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, newPassword);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="Code"></param>
        public void ResetPasswordAsync(string emailAddress, string Code)
        {
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MayHaveCompany);

            User entity;
            if (string.IsNullOrWhiteSpace(Code))
            {
                entity = _userRepository.FirstOrDefault(user => user.EmailAddress == emailAddress);
                entity.SetNewPasswordResetCode();
                _emailSender.Send(emailAddress, "重置密码", entity.PasswordResetCode, false);
            }
            else
            {
                entity = _userRepository.FirstOrDefault(user => user.EmailAddress == emailAddress && user.PasswordResetCode == Code);
                entity.Password = new PasswordHasher().HashPassword(_abpTenantRepository.Load(entity.TenantId.Value).Password);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="permissionlist"></param>
        /// <returns></returns>
        public async Task SavePermission(User user, List<Permission> permissionlist)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MayHaveCompany))
            {
                await _userManager.SetGrantedPermissions(user, permissionlist);
            }
            return;
        }
    }
}
