using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Locked
{
    /// <summary>
    /// 地锁设备详细信息
    /// </summary>
    [Table("AbpLockDevice")]
    public  class LockDevices : Entity, IHasCreationTime
    {
        /// <summary>
        /// 地锁ID
        /// </summary>
        [MaxLength(10)]
        public string LockID { get; set; }

        /// <summary>
        /// 电池电量
        /// </summary>
        public virtual decimal? Battery { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int? CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int? RegionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int? ParkId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int? BerthsecId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 泊位号
        /// </summary>
        [MaxLength(20)]
        public virtual string BerthNumber { get; set; }

        /// <summary>
        /// 心跳时间
        /// </summary>
        public virtual DateTime? BeatDatetime { get; set; }
    }
}
