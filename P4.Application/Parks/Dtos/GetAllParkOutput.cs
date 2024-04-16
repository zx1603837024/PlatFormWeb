using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Parks.Dtos
{
    public  class GetAllParkOutput: PagedResultOutput<Park>, IOutputDto
    {
      public List<ParkDto> rows { get; set; }
    }
}
