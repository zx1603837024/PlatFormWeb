using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
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
    /// 设备详情表
    /// </summary>
    [Table("AbpEquipmentDetail")]
    public class EquipmentDetail :Entity, IHasCreationTime
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        [MaxLength(50)]
        public virtual string EqId { get; set; }

        /// <summary>
        /// 配件类型
        /// </summary>
        [MaxLength(20)]
        public virtual string PartsType { get; set; }

        /// <summary>
        /// 配件编号
        /// </summary>
        [MaxLength(30)]
        public virtual string PartsId { get; set; }

        ///// <summary>
        ///// 电池编号
        ///// </summary>
        //[MaxLength(30)]
        //public virtual string BatteryId { get; set; }

        ///// <summary>
        ///// SIM卡id
        ///// </summary>
        //[MaxLength(30)]
        //public virtual string SimId { get; set; }

        ///// <summary>
        ///// TF卡id
        ///// </summary>
        //[MaxLength(30)]
        //public virtual string TfId { get; set; }

        ///// <summary>
        ///// SAM卡id
        ///// </summary>
        //[MaxLength(30)]
        //public virtual string SamId { get; set; }


        ///// <summary>
        ///// 打印机id
        ///// </summary>
        //[MaxLength(30)]
        //public virtual string PrinterId { get; set; }

        ///// <summary>
        ///// 打印机电池id
        ///// </summary>
        //[MaxLength(30)]
        //public virtual string PrinterBatteryId { get; set; }
        

        ///// <summary>
        ///// 卡带数
        ///// </summary>
        //[MaxLength(10)]
        //public virtual string Cassette { get; set; }

        public virtual DateTime CreationTime { get; set; }
    }
}
