using System.Linq;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using EntityFramework.DynamicFilters;
using P4.Authorization;
using P4.EntityFramework;
using P4.MultiTenancy;
using P4.Users;

namespace P4.Migrations.Data
{
    public class InitialDataBuilder
    {
        public void Build(P4DbContext context)
        {
            context.DisableAllFilters();
            new InitUsersData().CreateUserAndRoles(context);
            //new InitMenusData().CreateMenus(context);
        }
    }
}