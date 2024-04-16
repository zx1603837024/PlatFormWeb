using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Companys.Dtos
{
    public class GetAllCompanyOutput : PagedResultOutput<Company>, IOutputDto
    {
        public List<CompanyDto> rows { get; set; }
    }
}
