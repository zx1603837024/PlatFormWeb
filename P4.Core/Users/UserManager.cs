using Abp.Authorization;
using Abp.Authorization.DataPermissions;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Abp.Zero.Configuration;
using P4.Authorization;
using P4.MultiTenancy;

namespace P4.Users
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserManager : AbpUserManager<Tenant, Role, User>
    {
        public UserManager(
            UserStore store,
            RoleManager roleManager,
            IRepository<Tenant> tenantRepository,
            IMultiTenancyConfig multiTenancyConfig,
            IPermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager,
            IUserManagementConfig userManagementConfig,
            IRepository<UserDataPermissionSetting, long> userDataPermissionRepository,
            IRepository<RoleDataPermissionSetting, long> roleDataPermissionRepository,
            IRepository<UserPermissionSetting, long> userPermissionPermissionRepository,
            IRepository<RolePermissionSetting, long> rolePermissionPermissionRepository,
             IIocResolver iocResolver,
            ISettingStore settingStore, 
            ICacheManager cacheManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IOrganizationUnitSettings organizationUnitSettings
           )
            : base(
                store,
                roleManager,
                tenantRepository,
                multiTenancyConfig,
                permissionManager,
                unitOfWorkManager,
                settingManager,
                userManagementConfig,
                userDataPermissionRepository,
                roleDataPermissionRepository,
                userPermissionPermissionRepository,
                rolePermissionPermissionRepository,
                iocResolver,
                settingStore,
                cacheManager,
                organizationUnitRepository,
                userOrganizationUnitRepository,
                organizationUnitSettings
                )
        {

        }
    }
}