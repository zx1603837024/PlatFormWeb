using Abp.Application.Services.Dto;
using P4.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EmployeeLoggings.Dtos
{
   public class GetAllEmployeeCheckOutput : PagedResultOutput<EmployeeCheck>, IOutputDto
    {
        public List<EmployeeCheckDto> rows { get; set; }
    }
}
