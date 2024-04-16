using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Companys
{
    /// <summary>
    /// 分公司数据
    /// </summary>
    [Table("AbpOperatorsCompany")]
    public class OperatorsCompany : FullAuditedEntity<int>, IMustHaveTenant, IPassivable
    {
        [MaxLength(200)]
        public virtual string CompanyName { get; set; }

        /// <summary>
        /// 法人
        /// </summary>
        [MaxLength(20)]
        public virtual string Corporation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public virtual string Contacts { get; set; }

        /// <summary>
        /// 联系号码
        /// </summary>
        [MaxLength(100)]
        public virtual string TelePhone { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(100)]
        public virtual string Address { get; set; }

        /// <summary>
        /// 商户Id
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
         [MaxLength(30)]
        public virtual string X
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public virtual string Y
        { get; set; }
        /// <summary>
        ///激活状态 
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 密码1
        /// </summary>
        [MaxLength(20)]
        public virtual string Password1 { get; set; }

        /// <summary>
        /// 密码2
        /// </summary>
        [MaxLength(20)]
        public virtual string Password2 { get; set; }

        /// <summary>
        /// 密码3
        /// </summary>
        [MaxLength(20)]
        public virtual string Password3 { get; set; }

        /// <summary>
        /// 密码4
        /// </summary>
        [MaxLength(20)]
        public virtual string Password4 { get; set; }

        /// <summary>
        /// 密码5
        /// </summary>
        [MaxLength(20)]
        public virtual string Password5 { get; set; }

    }
}
