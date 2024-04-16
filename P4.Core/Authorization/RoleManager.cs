using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Abp.Zero.Configuration;
using P4.MultiTenancy;
using P4.Users;

namespace P4.Authorization
{
    public class RoleManager : AbpRoleManager<Tenant, Role, User>
    {
        public RoleManager(
            RoleStore store, 
            IPermissionManager permissionManager, 
            IRoleManagementConfig roleManagementConfig, 
            IUnitOfWorkManager unitOfWorkManager,
            ICacheManager cacheManager)
            : base(
                store,
                permissionManager,
                roleManagementConfig,
                unitOfWorkManager, cacheManager)
        {
        }
    }
}