using Abp.Domain.Repositories;
using P4.SpecialLists.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.SpecialLists
{
    public interface ISpecialRepository : IRepository<SpecialList>
    {
        GetAllSpecialListsOutput GetAllSpecialList(SpecialListInput input);
        List<SpecialLists.Dtos.SpecialListsDto> GetAllSpecialCar(int? TenantId, int? parkid);
    }
}
