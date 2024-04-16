using P4.EquipmentVersion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using P4.Dictionarys;
using Abp.Linq.Extensions;
using P4.MultiTenancy;

namespace P4.EntityFramework.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class EquipmentVersionRepository : P4RepositoryBase<P4.Equipments.EquipmentVersion>, IEquipmentVersionRepository
    {
        public EquipmentVersionRepository(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        { 

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public EquipmentVersion.Dtos.GetAllEquipmentVersionOutput GetAll(EquipmentVersion.Dtos.SearchEquipmentVersionInput input)
        {
            int records = Table.Filters(input).Count();
            var query = Table.Select(c => new P4.EquipmentVersion.Dtos.EquipmentVersionDto
            {
                Id = c.Id,
                TenantName = Context.Set<Tenant>().FirstOrDefault(entity => entity.Id == c.TenantId).Name,
                EqupmentType = Context.Set<DictionaryValue>().FirstOrDefault(dictype => dictype.ValueCode == c.EqupmentType && dictype.TypeCode == P4Consts.EquipmentVersion).ValueData,
                Version = c.Version,
                IsActive = c.IsActive,
                IsUpgrade = c.IsUpgrade
            }).Orders(input).Filters(input).PageBy(input);
            return new P4.EquipmentVersion.Dtos.GetAllEquipmentVersionOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
    }
}
