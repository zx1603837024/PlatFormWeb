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
    /// 基站设备电压
    /// </summary>
    [Table("AbpTransmitterCaution")]
    public class TransmitterCaution : Entity<long>, IHasCreationTime
    {
        public virtual int TransmitterId { get; set; }

        [ForeignKey("TransmitterId")]
        public virtual Transmitter Transmitter { get; set; }

        /// <summary>
        /// 电池电量
        /// </summary>
        public virtual decimal VoltageCaution { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
