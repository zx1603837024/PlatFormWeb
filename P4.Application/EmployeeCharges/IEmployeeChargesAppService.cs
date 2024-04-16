using Abp.Application.Services;
using P4.EmployeeCharges.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EmployeeCharges
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEmployeeChargesAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<EmployeeChargesDto> GetEmployeeChargesDayReport(EmployeeChargeInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllEmployeeChargesOutput GetEmployeeChargesDayReport1(EmployeeChargeInput input);

        List<EmployeeChargesDto> GetChargesRateDayEcharReport(EmployeeChargeInput input);

        GetAllEmployeeChargesOutput GetChargeRateDayReport(EmployeeChargeInput input);

        /// <summary>
        /// 获取收费员日报按日显示
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllEmployeeChargesOutput GetEmployeeChargesDayReportDetail(EmployeeChargeInput input);
    }
}
