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
    /// 设备维护记录
    /// </summary>
    [Table("AbpEquipmentMaintenance")]
    public class EquipmentMaintenance : Entity<long>, ICreationAudited<Users.User>
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public virtual long EquipmentId { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        [MaxLength(50)]
        public virtual string PDA { get; set; }

        /// <summary>
        /// 使用状态
        /// 0.未使用
        /// 1.领用
        /// 2.返修
        /// </summary>
        public virtual Int16 UseStatus { get; set; }

       

        public virtual long? CreatorUserId { get; set; }

        public virtual DateTime CreationTime { get; set; }

        [ForeignKey("CreatorUserId")]
        public virtual Users.User CreatorUser { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(300)]
        public virtual string Ramark { get; set; }
    }
}
