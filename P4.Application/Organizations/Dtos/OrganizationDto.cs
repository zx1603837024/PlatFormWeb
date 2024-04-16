using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Organizations.Dtos
{
    [AutoMapFrom(typeof(Organization))]
    public class OrganizationDto : EntityDto<int>
    {
        public string OrganizationName { get; set; }

        public string FatherCode { get; set; }

        public int TenantId { get; set; }

        public int CompanyId { get; set; }

     


    }
}
