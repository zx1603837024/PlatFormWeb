using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.MonthlyCars.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllMonthlyCarAbnormalOutput : PagedResultOutput<MonthlyCarAbnormal>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<MonthlyCarAbnormalDto> rows { get; set; }
    }
}
