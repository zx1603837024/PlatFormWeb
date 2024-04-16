using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.PayLevelSetting
{
    [Table("AbpPayLevelSettings")]
    public class PayLevelSettings : Entity, IHasCreationTime
    {
        public virtual int? TenantId { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual Int16? IsDelete { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public virtual int? DeviceType { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        [MaxLength(30)]
        public virtual string DeviceName { get; set; }

        /// <summary>
        /// 设备优先级
        /// </summary>
        public virtual int? DeviceOrder { get; set; }

        /// <summary>
        /// 最后操作时间
        /// </summary>
        [MaxLength(30)]
        public virtual string LastUser { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public virtual DateTime LastUpdateTime { get; set; }
    }
}
