using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Sensors
{
    /// <summary>
    /// 车检器电压历史表
    /// </summary>
    [Table("AbpSensorCaution")]
    public class SensorCaution : Entity<long>, IHasCreationTime
    {
        public virtual int SensorId { get; set; }

        [ForeignKey("SensorId")]
        public virtual Sensor Sensor { get; set; }

        /// <summary>
        /// 电容电量
        /// </summary>
        public virtual decimal Magnetism { get; set; }

        /// <summary>
        /// 电池电量
        /// </summary>
        public virtual decimal Battery { get; set; }


        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
