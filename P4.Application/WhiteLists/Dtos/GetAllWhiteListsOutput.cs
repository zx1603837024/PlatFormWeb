using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace P4.WhiteLists.Dtos
{
    public class GetAllWhiteListsOutput : PagedResultOutput<WhiteList>, IOutputDto
    {
        public List<WhiteListsDto> rows { get; set; }
    }
}
