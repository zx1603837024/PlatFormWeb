using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace P4.Sensors.Dtos
{
    [AutoMapFrom(typeof(Transmitter))]
    public class TransmitterDto : EntityDto
    {
        /// <summary>
        /// 基站名称
        /// </summary>
        [MaxLength(50)]
        public virtual string TransmitterName { get; set; }

        /// <summary>
        /// 基站编号
        /// </summary>
        [Required]
        [MaxLength(12)]
        public virtual string TransmitterNumber { get; set; }

        /// <summary>
        /// 电池电量
        /// </summary>
        public virtual decimal? VoltageCaution { get; set; }

        /// <summary>
        /// 电压上传时间
        /// </summary>
        public virtual DateTime? Updatetime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public virtual string X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public virtual string Y { get; set; }

        /// <summary>
        /// 设备地址
        /// </summary>
        [MaxLength(30)]
        public virtual string Address { get; set; }

        /// <summary>
        /// 录入时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 心跳时间
        /// </summary>
        public virtual DateTime? BeatDatetime { get; set; }
    }
}
