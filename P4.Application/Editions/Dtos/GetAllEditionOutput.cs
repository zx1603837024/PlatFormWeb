using Abp.Application.Editions;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Editions.Dtos
{
    public class GetAllEditionOutput : PagedResultOutput<Edition>, IOutputDto
    {
        public List<EditionDto> rows { get; set; }
    }
}
