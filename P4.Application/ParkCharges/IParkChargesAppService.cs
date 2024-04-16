using Abp.Application.Services;
using P4.ParkCharges.Dto;
using System.Collections.Generic;

namespace P4.ParkCharges
{
    /// <summary>
    /// 
    /// </summary>
    public interface IParkChargesAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ParkChargesDto> GetParkChargesDayReport(ParkChargeInput input);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ParkChargesDto> GetBerthsecChargesDayReportEchar(ParkChargeInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllParkChargesOutput GetParkChargesDayReport1(ParkChargeInput input);

        /// <summary>
        /// 泊位段报表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllParkChargesOutput GetBerthsecChargesDayReport(ParkChargeInput input);

        /// <summary>
        /// 欠费报表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllParkChargesOutput GetBerthescOwnOutput(ParkChargeInput input);

        /// <summary>
        /// 欠费报表详细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ParkChargesDto> OwnReportDetail(ParkChargeInput input);
    }
}
