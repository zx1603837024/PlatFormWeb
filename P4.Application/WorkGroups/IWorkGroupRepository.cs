using Abp.Domain.Repositories;
using P4.Employees.Dtos;
using P4.Users;
using P4.Users.Dto;
using P4.WorkGroups.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.WorkGroups
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWorkGroupRepository : IRepository<WorkGroup>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllWorkGroupOutput GetAllWorkGroupList(WorkGroupInput input);

        /// <summary>
        /// 获取所有未分配工作组的收费员和泊位段
        /// </summary>
        /// <returns></returns>
        WorkGroupDto GetWorkGroupList(int workgroupId,int workStatus);
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
    }
}