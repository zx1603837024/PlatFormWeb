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
    /// 车检器运行状态
    /// </summary>
    [Table("AbpSensorRunStatus")]
    public class SensorRunStatus : Entity<long>
    {
        /// <summary>
        /// 车检器编号
        /// </summary>
        [MaxLength(30)]
        public virtual string SensorNumber { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime? EndTime { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        public virtual int Duration { get; set; }

        /// <summary>
        /// true： 正常
        /// false：异常
        /// </summary>
        public virtual bool Status { get; set; }

    }
}
