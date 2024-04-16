using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.DecisionAnalysis.Dtos
{
    public class GetAllPeakPeriodOutput : PagedResultOutput<PeakPeriod>, IOutputDto
    {
        public List<PeakPeriodDto> rows { get; set; }
    }
}
