using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inspectors.Dtos
{
    public class GetAllInspectorTasksOutput : PagedResultOutput<InspectorTask>, IOutputDto
    {
        public List<InspectorTasksDto> rows { get; set; }
    }
}
