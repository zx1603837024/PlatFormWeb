using Abp.Domain.Repositories;
using P4.WorkGroupInspectors.Dto;
using System.Collections.Generic;

namespace P4.WorkGroupInspectors
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWorkGroupInspectorsRepository : IRepository<WorkGroupsInspectors>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllWorkGroupInspectorsOutput GetAllWorkGroupList(WorkGroupInspectorsInput input);

        /// <summary>
        /// 获取所有未分配工作组的收费员和泊位段
        /// </summary>
        /// <returns></returns>
        WorkGroupsInspectorsDto GetWorkGroupList(int workgroupId);


        /// <summary>
        /// 工作组Echars数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<WorkGroupInspectorsEcharsDto> GetWorkGroupReportToList(WorkGroupInspectorsInput input);


        /// <summary>
        /// 工作组JqGrid数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllWorkGroupInspectorsReport GetWorkGroupReportJqGrid(WorkGroupInspectorsInput input);
    }
}