using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using P4.Authorization;
using P4.MultiTenancy;

namespace P4.Users
{
    public class UserStore : AbpUserStore<Tenant, Role, User>
    {
        public UserStore(
            IRepository<User, long> userRepository,
            IRepository<UserLogin, long> userLoginRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<Role> roleRepository,
            IRepository<UserPermissionSetting, long> userPermissionSettingRepository,
            IAbpSession session,
            IUnitOfWorkManager unitOfWorkManager,
            ICacheManager cacheManager)
            : base(
                userRepository,
                userLoginRepository,
                userRoleRepository,
                roleRepository,
                userPermissionSettingRepository,
                session,
                unitOfWorkManager,
                cacheManager)
        {
        }
    }
}