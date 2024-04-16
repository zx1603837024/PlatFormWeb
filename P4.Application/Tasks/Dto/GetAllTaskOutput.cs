using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Tasks.Dto
{
    public class GetAllTaskOutput : PagedResultOutput<Task>, IOutputDto
    {
        public List<TaskDto> rows { get; set; }
    }
}
