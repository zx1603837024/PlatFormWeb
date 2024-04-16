using Abp.Domain.Entities;
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
    /// 车检器时间点故障列表
    /// </summary>
    [Table("AbpSensorFaults")]
    public class SensorFault : Entity<long>
    {
        /// <summary>
        /// 关联AbpSensorBeats中id
        /// </summary>
        public virtual int SensorBeatId { get; set; }

        /// <summary>
        /// 车检器编号
        /// </summary>
        [MaxLength(30)]
        public virtual string SensorNumber { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        public virtual int? RegionId { get; set; }

        [ForeignKey("RegionId")]
        public virtual Regions.Region Region { get; set; }

        public virtual int? ParkId { get; set; }

        [ForeignKey("ParkId")]
        public virtual Parks.Park Park { get; set; }


        public virtual int? BerthsecId { get; set; }

        [ForeignKey("BerthsecId")]
        public virtual Berthsecs.Berthsec Berthsec { get; set; }
        /// <summary>
        /// 泊位号
        /// </summary>
        [MaxLength(20)]
        public virtual string BerthNumber { get; set; }
    }
}
