using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.OtherAccounts
{
    public class OtherAccountStore : Abp.Authorization.AppAccounts.AbpOtherAccountStore<OtherAccount>
    {
        public OtherAccountStore(IRepository<OtherAccount, long> otherAccountRepository, IAbpSession session, IUnitOfWorkManager unitOfWorkManager, ICacheManager cacheManager)
            : base(otherAccountRepository, session, unitOfWorkManager, cacheManager)
        {

        }
    }
}
