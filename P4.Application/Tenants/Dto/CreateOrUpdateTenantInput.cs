using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Tenants.Dto
{
    [AutoMapTo(typeof(Tenant))]
    public class CreateOrUpdateTenantInput : EntityRequestInput, IOperDto
    {

        public string oper { get; set; }

        public bool IsActive { get; set; }
        public string DomainName { get; set; }

        public string TenancyName { get; set; }

        public string Name { get; set; }
        public string HQ { get; set; }
        public string Address { get; set; }
        public string Contacts { get; set; }

        /// <summary>
        /// 版本名称
        /// </summary>
        public string EditionName { get; set; }


    }
}
