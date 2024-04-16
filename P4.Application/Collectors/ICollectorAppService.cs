using Abp.Application.Services;
using P4.Collectors.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace P4.Collectors
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICollectorAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MonthCarChainDto GetMonthCarCountChainDto();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        GetCollectorCheckTotalOutput GetCollectorCheckCount();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        GetSensorCheckTotalOutput GetSensorCheckTotalCount();


        /// <summary>
        /// 道闸在线率
        /// </summary>
        /// <returns></returns>
        GetsignoOnlineOutput GetsignoOnlineOutput();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        GetBerthCheckTotalOutput GetBerthCheckTotalCount();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        GetBerthsecReportTotalOutput GetBerthsecBussinessCount();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<GetEmployeeReportTotalOutput> GetEmployeeBussinessCount();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<GetParkTotalOutput> GetParkBussinessCount();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<GetMonthTotalOutput> GetMonthBussinessCount();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        GetYearTotalOutput GetYearBussinessCount();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        GetWeixinOutput GetWeixinCount();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        TitleModelDto TitleModelDto();

    }
}
