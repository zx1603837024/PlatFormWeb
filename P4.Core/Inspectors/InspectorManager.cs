using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inspectors
{
    public class InspectorManager : Abp.Authorization.Inspectors.AbpInspectorsManager<Inspector>
    {
        public InspectorManager(InspectorStore inspectorStore, IUnitOfWorkManager unitOfWorkManager, IIocResolver iocResolver, ISettingStore settingStore)
            : base(inspectorStore, unitOfWorkManager, iocResolver, settingStore) 
        {
        
        }
    }
}
