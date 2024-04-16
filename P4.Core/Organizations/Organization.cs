using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Organizations
{
    /// <summary>
    /// 组织架构
    /// </summary>
    [Table("AbpOrganizations")]
    public class Organization : AuditedEntity, IMustHaveTenant, IMayHaveCompany
    {
        /// <summary>
        /// 组织机构代码
        /// </summary>
        [MaxLength(10)]
        public virtual string OrganizationCode { get; set; }

        /// <summary>
        /// 组织机构名称
        /// </summary>
        public virtual string OrganizationName { get; set; }

        /// <summary>
        /// 父类代码
        /// "0"为父类
        /// </summary>
        [MaxLength(10)]
        public virtual string FatherCode { get; set; }

        public virtual int TenantId { get; set; }

        public virtual int Order { get; set; }
        public virtual int? CompanyId { get; set; }
    }
}
