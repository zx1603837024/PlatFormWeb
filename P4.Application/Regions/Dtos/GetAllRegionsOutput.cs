using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Regions.Dtos
{
    public class GetAllRegionsOutput : PagedResultOutput<Region>, IOutputDto
    {
        public List<RegionDto> rows { get; set; }
    }
}
