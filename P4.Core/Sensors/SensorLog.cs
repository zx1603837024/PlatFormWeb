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
    /// 车检器/基站通讯数据
    /// </summary>
    [Table("AbpSensorLogs")]
    public class SensorLog : Entity<long>, IHasCreationTime
    {
        /// <summary>
        /// 接收内容
        /// </summary>
        [MaxLength(2048)]
        public virtual string Content { get; set; }

        /// <summary>
        /// 异常
        /// </summary>
        [MaxLength(500)]
        public virtual string Exception { get; set; }

        /// <summary>
        /// 回执命令
        /// </summary>
        [MaxLength(100)]
        public virtual string ReceiptCmd { get; set; }

        /// <summary>
        /// 接收时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
    }
}
