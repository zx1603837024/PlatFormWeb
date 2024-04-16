using Abp.EntityFramework;
using P4.WhiteLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using P4.WhiteLists.Dtos;
using P4.Dictionarys;

namespace P4.EntityFramework.Repositories
{
    public class WhiteListsRepository : P4RepositoryBase<WhiteList>, IWhiteListsRepository
    {
        public WhiteListsRepository(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public WhiteLists.Dtos.GetAllWhiteListsOutput GetWhiteListAll(WhiteLists.Dtos.WhiteListsInput input)
        {

            int records = Table.Filters(input).Count();
            var query = Table.Select(c => new WhiteListsDto
            {
                Id = c.Id,
                RelateNumber = c.RelateNumber,
                CompanyId = c.CompanyId,
                CompanyName = Context.Set<Companys.OperatorsCompany>().FirstOrDefault(entity => entity.Id == c.CompanyId).CompanyName,
                IsActive = c.IsActive,
                Remarks = c.Remarks,
                VehicleType = Context.Set<DictionaryValue>().FirstOrDefault(dictype => dictype.ValueCode == c.VehicleType && dictype.TypeCode == P4Consts.VehicleType).ValueData

            }).Orders(input).Filters(input).PageBy(input);
            return new GetAllWhiteListsOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
    }
}
