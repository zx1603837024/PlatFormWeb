using Abp.Application.Services;
using P4.DecisionAnalysis.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.DecisionAnalysis
{

    /// <summary>
    /// 
    /// </summary>
    public interface IPeakPeriodAppService : IApplicationService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllPeakPeriodOutput GetAllPeakPeriodList(SearchPeakPriodInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="isactive"></param>
        /// <returns></returns>
        List<PeakPeriodDto> GetTopPeakPeriodList(int count, bool? isactive);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetUtilizationHourListOutput GetUtilizationHourList(SearchUtilizationInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<BerthsecAnalysisResultDto> GetBerthsecAnalysisResultList(SearchBerthsecAnalysisResultInput input);
    }
}
