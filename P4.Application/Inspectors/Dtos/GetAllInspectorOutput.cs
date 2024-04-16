using Abp.Application.Services.Dto;
using P4.Inspectors;
using P4.Inspectors.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inspectors.Dtos
{
    public class GetAllInspectorOutput : PagedResultOutput<Inspector>, IOutputDto
    {
       public List<InspectorDto> rows { get; set; }
    }
}
