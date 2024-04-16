using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inspectors
{
    public class InspectorStore : Abp.Authorization.Inspectors.AbpInspectorStore<Inspector>
    {
        public InspectorStore(IRepository<Inspector, long> inspectorRepository, IAbpSession session, IUnitOfWorkManager unitOfWorkManager, ICacheManager cacheManager)
            : base(inspectorRepository, session, unitOfWorkManager, cacheManager)
        {

        }
    }
}
