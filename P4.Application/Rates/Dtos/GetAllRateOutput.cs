using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Rates.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllRateOutput : PagedResultOutput<Rate>, IOutputDto
    {
        public List<RateDto> rows { get; set; }
    }
}

