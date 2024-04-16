using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.SchedulingSystem.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllSchedulingSystemEmployeeCheckOutput : PagedResultOutput<SchedulingSystemEmployeeCheck>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<SchedulingSystemEmployeeCheckDto> rows { get; set; }
    }
}
