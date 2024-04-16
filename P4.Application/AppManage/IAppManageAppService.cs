using Abp.Application.Services;
using P4.AppManage.Dto;
using System.Collections.Generic;

namespace P4.AppManage
{

    /// <summary>
    /// 
    /// </summary>
    public interface IAppManageAppService : IApplicationService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllAppOutput GetAll(SearchAppInput input);

        /// <summary>
        /// 手机登录
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        string CarLogin(string mobile);
    }
}
