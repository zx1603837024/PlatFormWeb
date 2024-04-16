using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Employees.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllEmployeeOutput : PagedResultOutput<Employee>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
       public List<EmployeeDto> rows { get; set; }
    }
}
