using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.TicketCss
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpTicketStyle")]
    public class TicketStyle: FullAuditedEntity, IMustHaveTenant, IMustHaveCompany
    {
        /// <summary>
        /// 类型
        /// </summary>
        [MaxLength(20)]
        public virtual string Status { get; set; }

        /// <summary>
        /// 二维码
        /// </summary>
        [MaxLength(50)]
        public virtual string TwoBarCode { get; set; }

        /// <summary>
        /// 文本
        /// </summary>
        [MaxLength(1000)]
        public virtual string Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int CompanyId { get; set; }
    }
}
