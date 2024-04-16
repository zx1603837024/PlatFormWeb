using Abp.Domain.Repositories;
using P4.BlackLists.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.BlackLists
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBlackListRepository : IRepository<BlackList>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllBlackListsOutput GetBlackListAll( BlackListsInput input);
    }
}
