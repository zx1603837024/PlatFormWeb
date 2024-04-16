using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EquipmentMaintain
{
    /// <summary>
    /// 返修流程表
    /// </summary>
    [Table("AbpReworkFlow")]
    public class ReworkFlow : Entity
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        [MaxLength(50)]
        public virtual string EqId { get; set; }

        /// <summary>
        /// 配件ID
        /// </summary>
        [MaxLength(30)]
        public virtual string PartsId { get; set; }

        /// <summary>
        /// 故障关键字
        /// </summary>
        [MaxLength(10)]
        public virtual string FaultKeyword { get; set; }

        /// <summary>
        /// 故障描述
        /// </summary>
        [MaxLength(500)]
        public virtual string FaultDescription { get; set; }

        /// <summary>
        /// 维修描述
        /// </summary>
        [MaxLength(500)]
        public virtual string MaintainDescription { get; set; }

        /// <summary>
        /// 返修批次号
        /// </summary>
        public virtual int BatchNum { get; set; }

        /// <summary>
        /// 返修状态
        /// </summary>
        [MaxLength(20)]
        public virtual string MaintainState { get; set; }


    }
}
