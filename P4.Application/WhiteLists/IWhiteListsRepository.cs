using Abp.Domain.Repositories;
using P4.WhiteLists.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.WhiteLists
{
    public interface IWhiteListsRepository : IRepository<WhiteList>
    {
      GetAllWhiteListsOutput GetWhiteListAll(WhiteListsInput input);
    }
}
