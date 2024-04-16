using Abp.EntityFramework;
using P4.Dictionarys;
using System.Linq;
using P4.Dictionarys.Dtos;
using Abp.Linq.Extensions;

namespace P4.EntityFramework.Repositories
{
    public class DictionaryRepository : P4RepositoryBase<DictionaryValue>, IDictionaryRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextProvider"></param>
         public DictionaryRepository(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        { 
        }
        public GetAllDictionaryValueOutput GetDictionaryAll(DictionaryValueInput input)
        {
            int records = Table.Filters(input).Count();
            var query = Table.Select(c => new DictionaryValueDto { Id = c.Id, TypeCode = c.TypeCode, ValueCode = c.ValueCode, ValueData = c.ValueData, IsActive = c.IsActive, IsDefault = c.IsDefault, Order = c.sort, Remark = c.Remark, TypeValue = Context.Set<DictionaryType>().FirstOrDefault(dictype => dictype.TypeCode == c.TypeCode).TypeValue }).Orders(input).Filters(input).PageBy(input);
            return new GetAllDictionaryValueOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
    }
}
