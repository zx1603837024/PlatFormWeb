using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EmployeeLoggings.Dtos
{
    public class EmployeeCheckInput :OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        public string Id { get; set; }
        public string filters { get; set; }

        public int page { get; set; }

        public int rows { get; set; }
    }
}
