using Abp.Authorization;
using P4.MultiTenancy;
using P4.Users;

namespace P4.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}