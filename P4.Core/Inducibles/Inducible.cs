using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inducibles
{
    /// <summary>
    /// 诱导设备表
    /// </summary>
    [Table("AbpInducibles")]
    public class Inducible : FullAuditedEntity, IMustHaveTenant, IMustHaveCompany, IPassivable
    {
        /// <summary>
        /// 诱导名称
        /// </summary>
        [MaxLength(50)]
        public virtual string InducibleName { get; set; }

        /// <summary>
        /// 诱导类型
        /// 1：一级诱导
        /// 2：二级诱导
        /// 3：三级诱导
        /// 4：四级诱导
        /// </summary>
        public virtual Int16 InducibleType { get; set; }

        [MaxLength(80)]
        public virtual string X { get; set; }

        [MaxLength(80)]
        public virtual string Y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]        
        public virtual string Address { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("InducibleId")]
        public virtual ICollection<InducibleToPark> parklist { get; set; }

        [ForeignKey("InducibleId")]
        public virtual ICollection<InducibleToAD> advert { get; set; }

        public virtual int TenantId { get; set; }

        public virtual int CompanyId { get; set; }

     
        /// <summary>
        /// 诱导设备编号
        /// </summary>
        [MaxLength(40)]
        public virtual string EquipmentId { get; set; }
    }
}
