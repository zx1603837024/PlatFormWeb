using Abp.Domain.Repositories;
using P4.Employees.Dtos;
using P4.Inspectors.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inspectors
{
    /// <summary>
    /// 
    /// </summary>
   public interface IInspectorRepository: IRepository<Inspector, long>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllInspectorOutput GetInspectorAll(InspectorInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllInspectorTasksOutput GetAllInspectorTasksList(InspectorTasksInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllInspectorTaskFeedbacksOutput GetAllInspectorTaskFeedbacksList(InspectorTaskFeedbacksInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InspectorId"></param>
        /// <param name="parkId"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="CompanyId"></param>
        /// <param name="TenantId"></param>
        void InspectorCheckInPark(long InspectorId, List<string> parkId, string DeviceCode, int CompanyId, int TenantId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InspectorId"></param>
        /// <param name="inspectorRPB"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="CompanyId"></param>
        /// <param name="TenantId"></param>
        void InspectorCheckInPark(long InspectorId, InspectorRPB inspectorRPB, string DeviceCode, int CompanyId, int TenantId);
    }
}
