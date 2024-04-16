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
    /// PDA心跳包
    /// </summary>
    [Table("AbpEquipmentBeats")]
    public class EquipmentBeat : Entity<long>, ICreationAudited
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        [MaxLength(50)]
        public virtual string PDA { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 创建用户（收费员Id）
        /// </summary>
        public virtual long? CreatorUserId { get; set; }
    }
}