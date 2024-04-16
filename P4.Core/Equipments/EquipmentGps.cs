using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Equipments
{
    /// <summary>
    /// GPS
    /// </summary>
    [Table("AbpEquipmentGps")]
    public class EquipmentGps : Entity<long>, ICreationAudited
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public virtual string PDA { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public virtual string X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public virtual string Y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 创建用户（收费员Id）
        /// </summary>
        public virtual long? CreatorUserId { get; set; }
    }
}
