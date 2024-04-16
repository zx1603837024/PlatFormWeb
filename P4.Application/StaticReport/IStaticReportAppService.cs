using Abp.Application.Services;
using P4.Collectors.Dtos;
using P4.StaticReport.Dtos;
using System.Collections.Generic;

namespace P4.StaticReport
{

    /// <summary>
    /// 
    /// </summary>
    public interface IStaticReportAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<GetEmployeeReportTotalOutput> GetEmployeeReportEchar(StaticReportInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllEmployeeReportOutput GetEmployeeReportGrid(StaticReportInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<GetParkTotalOutput> GetParkReportEchar(StaticReportInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllParkReportOutput GetParkReportGrid(StaticReportInput input);

        /// <summary>
        /// 时间段停车场报表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllParkReportOutput GetTimeBucketParkReport(StaticReportInput input);

        /// <summary>
        /// 时间段停车场报表 Echar
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<GetParkTotalOutput> GetTimeBucketParkReportEchar(StaticReportInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<GetBerthsecReportTotalOutput> GetBerthsecReportEchar(StaticReportInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllBerthsecReportOutput GetBerthsecReportGrid(StaticReportInput input);
        /// <summary>
        /// 时间段泊位段报表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllBerthsecReportOutput GetTBucketBerthsecGrid(StaticReportInput input);
        /// <summary>
        /// 获取时间段泊位段Echar数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<GetBerthsecReportTotalOutput> GetTBucketBerthsecEchar(StaticReportInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<GetOperatorsCompanyTotalOutput> GetOperatorsCompanyReportEchar(StaticReportInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllOperatorsCompanyReportOutput GetOperatorsCompanyReportGrid(StaticReportInput input);
        //GetAllEmployeeChargesOutput GetEmployeeChargesDayReport1(DateTime begindt, DateTime enddt, string employeeIdInput, string employeeNameInput);
    }
}
