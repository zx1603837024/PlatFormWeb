using Abp.Application.Services;
using P4.Berthsecs.Dto;
using P4.EmployeeCharges.Dto;
using P4.Employees.Dtos;
using P4.WorkGroups.Dto;
using System.Collections.Generic;
using System.Web.Http;

namespace P4.WorkGroups
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWorkGroupAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllWorkGroupOutput GetAllWorkGroupList(WorkGroupInput input);
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workgroupId"></param>
        /// <param name="workStatus"></param>
        /// <returns></returns>
        WorkGroupDto GetWorkGroupList(int workgroupId,int workStatus);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int Insert(CreateOrUpdateWorkGroupInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int Update(CreateOrUpdateWorkGroupInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int Delete(CreateOrUpdateWorkGroupInput input);
        /// <summary>
        /// 工作组Echars数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<WorkGroupEcharsDto> GetWorkGroupReportToList(WorkGroupInput input);
        /// <summary>
        /// 工作组JqGrid数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllWorkGroupReport GetWorkGroupReportJqGrid(WorkGroupInput input);

        /// <summary>
        /// 通过巡查账号获取收费员定位
        /// </summary>
        /// <param name="InspectorId"></param>
        /// <returns></returns>
        List<EmployeeDto> GetEmployeeGps(long InspectorId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InspectorId"></param>
        /// <returns></returns>
        List<BerthsecDto> GetBerthsecGps(long InspectorId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BerthsecId"></param>
        /// <returns></returns>
        [HttpGet]
        List<Berths.Dtos.BerthDto> GetBerthList(int BerthsecId);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeesId"></param>
        /// <returns></returns>
        EmployeeChargesDto GetEmployeeChargesDto(long EmployeesId);
    }
}
