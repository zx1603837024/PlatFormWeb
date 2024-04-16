using P4.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Migrations.Data
{
    /// <summary>
    /// 初始化菜单数据
    /// </summary>
    public class InitMenusData
    {
        public void CreateMenus(P4DbContext context)
        {
            //Admin role for tenancy owner
            var defaultTenant = context.Tenants.FirstOrDefault(t => t.TenancyName == "Default");

            if (context.Menus.FirstOrDefault(m => m.RequiredPermissionName == "Control") == null)
            {
                context.Menus.Add(new Menus.Menu() { Name = "Control", RequiredPermissionName = "Control", Icon = "icon-plus", FatherCode = "0", Order = 1, Level = 1, TenantId = defaultTenant.Id });
                context.SaveChanges();
            }

            //if (context.Menus.FirstOrDefault(m => m.RequiredPermissionName == "") == null)
            //{
            //    //context.Menus.Add(new Menus.Menu() { RequiredPermissionName = "Control",  });
            //    context.SaveChanges();
            //}

            //if (context.Menus.FirstOrDefault(m => m.RequiredPermissionName == "") == null)
            //{
            //    //context.Menus.Add(new Menus.Menu() { RequiredPermissionName = "Control",  });
            //    context.SaveChanges();
            //}

            //var adminRoleForTenancyOwner = context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == "Admin");
            //if (adminRoleForTenancyOwner == null)
            //{
            //    adminRoleForTenancyOwner = context.Roles.Add(new Role(null, "Admin", "Admin"));
            //    context.SaveChanges();
            //} 
        }
    }
}
