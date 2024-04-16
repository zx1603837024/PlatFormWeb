using Abp.Application.Services;
using Abp.Application.Services.Dto;
using P4.Tasks.Dto;
using System.Collections.Generic;

namespace P4.Tasks
{

    /// <summary>
    /// 
    /// </summary>
    public interface ITaskAppService : IApplicationService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ListResultOutput<TaskDto> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllTaskOutput GetAll(TaskInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int GetAllCount();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        bool ReadTask(List<int> Ids);
    }
}
