using Abp.Application.Services;
using System.Collections.Generic;
using P4.WorkGroupInspectors.Dto;

namespace P4.WorkGroupInspectors
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWorkGroupInspectorsAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllWorkGroupInspectorsOutput GetAllWorkGroupList(WorkGroupInspectorsInput input);
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workgroupId"></param>
        /// <returns></returns>
        WorkGroupsInspectorsDto GetWorkGroupList(int workgroupId);

        /// <summary>
        /// 通过收费员获取工作组信息
        /// </summary>
        /// <param name="InspectorId"></param>
        /// <returns></returns>
        WorkGroupsInspectorsDto GetWorkGroupListByInspectorId(long InspectorId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int Insert(CreateOrUpdateWorkGroupInspectorsInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int Update(CreateOrUpdateWorkGroupInspectorsInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int Delete(CreateOrUpdateWorkGroupInspectorsInput input);
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
