using Abp.Application.Services;
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
    public interface IBlackListsAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllBlackListsOutput GetAllBlackLists(BlackListsInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string BlackListsInsert(CreateOrUpdateBlackListsInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string BlackListsUpdate(CreateOrUpdateBlackListsInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        void BlackListsDelete(CreateOrUpdateBlackListsInput input);


        /// <summary>
        /// 收费员获取黑名单
        /// </summary>
        /// <returns></returns>
        List<BlackListsDto> GetAllBlackList();

        /// <summary>
        /// 检测黑名单是否存在
        /// </summary>
        /// <param name="PlateNumber"></param>
        /// <returns></returns>
        bool CheckBlackExisis(string PlateNumber);
    }
}
