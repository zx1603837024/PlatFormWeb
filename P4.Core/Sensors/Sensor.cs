using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Sensors
{
    /// <summary>
    /// 车检器
    /// </summary>
    [Table("AbpSensors")]
    public class Sensor : Entity, IHasCreationTime
    {
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
        [ForeignKey("BerthsecId")]
        public virtual Berthsecs.Berthsec BerthSec { get; set; }
        /// <summary>
        /// 泊位号
        /// </summary>
        [MaxLength(20)]
        public virtual string BerthNumber { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [MaxLength(10)]
        public virtual string RelateNumber { get; set; }
        /// <summary>
        /// 进场时间
        /// </summary>

        public virtual DateTime? InCarTime { get; set; }
        /// <summary> 
        /// 
        /// 出场时间
        /// </summary>
        public virtual DateTime? OutCarTime { get; set; }

        /// <summary>
        /// 应收
        /// </summary>
        public virtual decimal Receivable { get; set; }

        /// <summary>
        /// 停车状态
        /// 1：再停
        /// 0：未停
        /// </summary>
        public virtual Int16 ParkStatus { get; set; }

        /// <summary>
        /// 电容电量
        /// </summary>
        public virtual decimal? Magnetism { get; set; }
        /// <summary>
        /// 电池电量
        /// </summary>
        public virtual decimal? Battery { get; set; }

        /// <summary>
        /// 电压上传时间
        /// </summary>
        public virtual DateTime? Updatetime { get; set; }
        /// <summary>
        /// 基站编号
        /// </summary>
        [MaxLength(12)]
        public virtual string TransmitterNumber { get; set; }

        /// <summary>
        /// 车检器编号
        /// </summary>
        [MaxLength(30)]
        public virtual string SensorNumber { get; set; }



        /// <summary>
        /// X磁场场强
        /// </summary>
        public virtual int? X { get; set; }

        /// <summary>
        /// Y磁场场强
        /// </summary>
        public virtual int? Y { get; set; }

        /// <summary>
        /// Z磁场场强
        /// </summary>
        public virtual int? Z { get; set; }

        /// <summary>
        /// 场强变化区间
        /// </summary>
        [MaxLength(20)]
        public string Range { get; set; }

        /// <summary>
        /// 录入时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 心跳时间
        /// </summary>
        public virtual DateTime? BeatDatetime { get; set; } 

        /// <summary>
        /// guid
        /// </summary>
        public virtual Guid? Guid { get; set; }

    }
}
