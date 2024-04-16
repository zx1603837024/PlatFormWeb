using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.MonthlyCars.Dtos
{
    public class GetAllMonthlyCarHistoryOutput : PagedResultOutput<MonthlyCar>, IOutputDto
    {
        public List<MonthlyCarHistoryDto> rows { get; set; }
    }
}
