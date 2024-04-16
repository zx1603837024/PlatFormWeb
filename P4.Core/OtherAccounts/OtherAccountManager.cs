using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.OtherAccounts
{
    /// <summary>
    /// 
    /// </summary>
    public class OtherAccountManager : Abp.Authorization.AppAccounts.AbpOtherAccountManager<OtherAccount>
    {
        public OtherAccountManager(OtherAccountStore otherAccountStore, IUnitOfWorkManager unitOfWorkManager, IIocResolver iocResolver, ISettingStore settingStore)
            : base(otherAccountStore, unitOfWorkManager, iocResolver, settingStore)
        {

        }
    }
}
