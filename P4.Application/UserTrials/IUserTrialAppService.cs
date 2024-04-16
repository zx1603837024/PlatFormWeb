using Abp.Application.Services;
using P4.UserTrials.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.UserTrials
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserTrialAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> SaveUserTrial(UserTrials.Dtos.UserTrialInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllUseTrialOutput GetAllUserTrials(SearchUseTrialInput input);

    }
}
