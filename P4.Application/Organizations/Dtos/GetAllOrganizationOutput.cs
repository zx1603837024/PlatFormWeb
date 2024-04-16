using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Organizations.Dtos
{
    public class GetAllOrganizationOutput: PagedResultOutput<Organization>, IOutputDto
    {
        public List<OrganizationDto> rows { get; set; }
    }
}
