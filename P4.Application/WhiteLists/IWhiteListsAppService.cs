using Abp.Application.Services;
using P4.WhiteLists.Dtos;
using System.Collections.Generic;

namespace P4.WhiteLists
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWhiteListsAppService : IApplicationService
   {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllWhiteListsOutput GetAllWhiteLists(WhiteListsInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string WhiteListsInsert(CreateOrUpdateWhiteListsInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string WhiteListsUpdate(CreateOrUpdateWhiteListsInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        void WhiteListsDelete(CreateOrUpdateWhiteListsInput input);


        /// <summary>
        /// 收费员获取白名单列表
        /// </summary>
        /// <returns></returns>
        List<WhiteListsDto> GetAllWhiteCar();

   }
}
