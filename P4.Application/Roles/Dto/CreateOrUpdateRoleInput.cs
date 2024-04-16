using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Roles.Dto
{
    [AutoMapFrom(typeof(Role))]
    public class CreateOrUpdateRoleInput : EntityRequestInput, IOperDto
    {
    
 
        public string oper { get; set; }

   
        public int TenantId { get; set; }

        [MaxLength(32)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string DisplayName { get; set; }

        /// <summary>
        /// 分公司
        /// </summary>
        public string CompanyName { get; set; }

        public bool IsStatic { get; set; }

        public bool IsDefault { get; set; }

        public Int64 LastModifierUserId { get; set; }

        public string TenantName { get; set; }

    }
}
