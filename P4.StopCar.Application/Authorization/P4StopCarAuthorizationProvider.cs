using Abp.Authorization;
using Abp.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.StopCar.Application.Authorization
{
    public class P4StopCarAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission("Control", new FixedLocalizableString(""), isGrantedByDefault: true, multiTenancySides: Abp.MultiTenancy.MultiTenancySides.Tenant);
        }
    }
}
