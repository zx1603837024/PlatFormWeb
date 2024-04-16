using Abp.Application.Services.Dto;
using P4.Dictionarys.Dtos;
using P4.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Tenants.Dto
{
    public class GetAllTenantOutput : PagedResultOutput<Tenant>, IOutputDto
    {
        public List<TenantDto> rows { get; set; }
    }
}
