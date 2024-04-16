using Abp.Application.Services;
using P4.Inspectors.Dtos;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
namespace P4.Inspectors
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInspectorAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllInspectorOutput GetAllInspectorList(InspectorInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string InspectorInsert(CreatOrUpdateInspector input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string InspectorUpdate(CreatOrUpdateInspector input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        void InspectorDelete(CreatOrUpdateInspector input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetInspectorByTopOutput GetInspectorByTopList(GetInspectorByTopInput input);

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
        /// <returns></returns>
        List<InspectorDto> GetInspectorAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string Insert(CreatOrUpdateInspectorTasks input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string Modify(CreatOrUpdateInspectorTasks input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void Delete(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inspectorId"></param>
        /// <returns></returns>
        InspectorRPB GetParkIdInspectorTask(long inspectorId);

        /// <summary>
        /// 通过巡查员id获取工作组关联权限
        /// </summary>
        /// <param name="inspectorId"></param>
        /// <returns></returns>
        InspectorRPB GetBerthsecListByWorkgroup(long inspectorId);

        /// <summary>
        /// 巡查员签到停车场历史表
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

        /// <summary>
        /// 巡查员签退停车场历史表
        /// </summary>
        void InspectorCheckOutPark();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<InspectorTask> GetAllInspectorTask();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<InspectorDto> GetAllInsperctorMap(int Id);



    }
}
