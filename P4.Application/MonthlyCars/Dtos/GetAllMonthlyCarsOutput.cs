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
    public class GetAllMonthlyCarsOutput : PagedResultOutput<MonthlyCar>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<MonthlyCarDto> rows { get; set; }
    }
}
