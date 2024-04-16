using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Messages.Organizations.Dtos
{
    [AutoMapFrom(typeof(Organization))] //组织架构
    public class CreateOrUpdateOrganization : EntityRequestInput, IOperDto
    {
      public  string oper { get; set; }
        /// <summary>
        /// 组织机构代码
        /// </summary>
        [MaxLength(10)]
        public  string OrganizationCode { get; set; }

        /// <summary>
        /// 组织机构名称
        /// </summary>
        public  string OrganizationName { get; set; }

        /// <summary>
        /// 父类代码
        /// "0"为父类
        /// </summary>
        [MaxLength(10)]
        public  string FatherCode { get; set; }

       // public  int TenantId { get; set; }  //商户id

        public  int Order { get; set; }
        public  int? CompanyId { get; set; }
    }
}
