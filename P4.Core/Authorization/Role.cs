using Abp.Authorization.Roles;
using P4.MultiTenancy;
using P4.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public class Role : AbpRole<Tenant, User>
    {
        public Role()
        {

        }

        public Role(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {

        }

     
    }
}