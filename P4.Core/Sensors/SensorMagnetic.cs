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
    /// 
    /// </summary>
    [Table("AbpSensorsMagnetic")]
    public class SensorMagnetic : Entity<long>, IHasCreationTime
    {
        /// <summary>
        /// 车检器编号
        /// </summary>
        [MaxLength(30)]
        public virtual string SensorNumber { get; set; }

        /// <summary>
        /// 流水号
        /// </summary>
        public virtual int Number { get; set; }

        /// <summary>
        /// 电压
        /// </summary>
        public virtual int Voltage { get; set; }

        /// <summary>
        /// 信号
        /// </summary>
        public virtual int Signal { get; set; }

        /// <summary>
        /// X磁场
        /// </summary>
        public virtual int X { get; set; }

        /// <summary>
        /// Y磁场
        /// </summary>
        public virtual int Y { get; set; }

        /// <summary>
        /// Z磁场
        /// </summary>
        public virtual int Z { get; set; }

        /// <summary>
        /// X磁场
        /// </summary>
        public virtual int? X0 { get; set; }

        /// <summary>
        /// Y磁场
        /// </summary>
        public virtual int? Y0 { get; set; }

        /// <summary>
        /// Z磁场
        /// </summary>
        public virtual int? Z0 { get; set; }

        /// <summary>
        /// 平方差
        /// </summary>
        public virtual double Variance { get; set; }

        /// <summary>
        /// 状态
        /// 0无车
        /// 1有车
        /// </summary>
        public virtual Int16 Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
    }
}
