using Abp.Authorization.Users;
using P4.MultiTenancy;
using System;

namespace P4.Users
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class User : AbpUser<Tenant, User>
    {

    }
}