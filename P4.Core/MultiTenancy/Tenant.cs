using Abp.MultiTenancy;
using P4.Users;

namespace P4.MultiTenancy
{
    public class Tenant : AbpTenant<Tenant, User>
    {
        protected Tenant()
        {

        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {

        }
    }
}