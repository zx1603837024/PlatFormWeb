using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.DecisionAnalysis.Dtos
{

    /// <summary>
    /// 
    /// </summary>
    public class GetUtilizationHourListOutput : PagedResultOutput<UtilizationHourDto>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<UtilizationHourDto> rows { get; set; }
    }
}
