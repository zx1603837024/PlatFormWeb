using Abp.EntityFramework;
using P4.EquipmentMaintain;
using P4.EquipmentMaintain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using P4.Dictionarys;

namespace P4.EntityFramework.Repositories
{
    public class EquipmentsMRepository: P4RepositoryBase<EquipmentsM>, IEquipmentsMRepository
    {
        public EquipmentsMRepository(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        { 

        }
        /// <summary>
        /// 两表查询（设备表/DictionaryValue表）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllEquipmentsMOutput GetAllEquipmentsMList(SearchEquipmentsMInput input)
        {
            int records = Table.Filters(input).Count();
            var query = Table.Select(c => new EquipmentsMDto
            {
                Id = c.Id,
                EqId=c.EqId,
                EqName=c.EqName,
                EqFactory=c.EqFactory,
                BatchNum=c.BatchNum,
                CreationTime=c.CreationTime,
                EqVersion=c.EqVersion,
                EqVersionName = Context.Set<DictionaryValue>().FirstOrDefault(dictype => dictype.ValueCode == c.EqVersion).ValueData,
            }).Orders(input).Filters(input).PageBy(input);
            return new GetAllEquipmentsMOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
    }
}
