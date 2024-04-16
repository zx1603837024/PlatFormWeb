using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Equipments
{
    /// <summary>
    /// PDA软件版本管理
    /// </summary>
    [Table("AbpEquipmentVersions")]
    public class EquipmentVersion : FullAuditedEntity, IMustHaveTenant, IPassivable
    {

        /// <summary>
        /// 商户代码
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        [MaxLength(5)]
        public virtual string EqupmentType { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        [MaxLength(30)]
        public virtual string Version { get; set; }

        /// <summary>
        /// 是否整个型号自动升级（针对商户）
        /// </summary>
        public virtual bool IsUpgrade { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool IsActive { get; set; }
    }
}
