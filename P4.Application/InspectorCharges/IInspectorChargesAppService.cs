using Abp.Application.Services;
using P4.InspectorCharges.Dtos;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
namespace P4.InspectorCharges
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInspectorChargesAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllInspectorChargesOutput GetInspectorChargesDayReport(InspectorChargeInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<InspectorChargesDto> GetInspectorChargesDayReportToEchart(InspectorChargeInput input);
    }
}
