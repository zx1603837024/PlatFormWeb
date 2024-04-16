using Abp.Application.Services;
using P4.SpecialLists.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.SpecialLists
{
    public interface ISpecialListAppSevice : IApplicationService
    {
        GetAllSpecialListsOutput GetAllSpecialLists(SpecialListInput input);
        string SpecialListsInsert(CreateOrUpdateSpecialListsInput input);

        string SpecialListsUpdate(CreateOrUpdateSpecialListsInput input);

        void SpecialListsDelete(CreateOrUpdateSpecialListsInput input);
        List<Dtos.SpecialListsDto> GetAllSpecialCar();
    }
}
