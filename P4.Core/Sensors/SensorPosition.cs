using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Sensors
{
    /// <summary>
    /// 车检器地理位置
    /// </summary>
    public class SensorPosition : Entity<long>
    {
        /// <summary>
        /// 地磁设备id
        /// </summary>
        [MaxLength(30)]
        public virtual string SensorNumber { get; set; }

        /// <summary>
        /// 右边车检器
        /// </summary>
        [MaxLength(30)]
        public virtual string RightSensorNumber { get; set; }

        /// <summary>
        /// 1.首尾相连
        /// 2.左右想接
        /// </summary>
        public virtual Int16 ConnectStatus { get; set; }

        /// <summary>
        /// 左边车检器
        /// </summary>
        [MaxLength(30)]
        public virtual string LeftSensorNumber { get; set; }


    }
}
