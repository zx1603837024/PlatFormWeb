using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Tenants
{
    public interface ITenantRepository : IRepository<Dictionarys.DictionaryType>
    {
        void InsertDictionaryTemplate(int? TenantId);
    }
}
