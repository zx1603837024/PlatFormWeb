using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.SpecialLists.Dtos
{
    public class GetAllSpecialListsOutput : PagedResultOutput<SpecialList>, IOutputDto
    {
        public List<SpecialListsDto> rows { get; set; }
    }
}
