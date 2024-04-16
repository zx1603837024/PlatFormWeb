using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Sensors.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Sensor))]
    public class SensorInfoDto : EntityDto
    {
        /// <summary>
        /// 时间差判断车检器状态
        /// </summary>
        public int TimeDiff { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ParkId { get; set; }
        /// <summary>
        /// 车检器的状态
        /// </summary>
        public string SensorStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime BeatDatetime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string BeatDatetimeString
        {
            get { return BeatDatetime.ToString("yyyy-MM-dd HH:mm:ss"); }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string SensorNumber { get; set; }
       // public int TenantId { get; set; }
        /// <summary>
        /// 时间段内车检器的掉线次数
        /// </summary>
        public int SensorARPCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BerthsecName { get; set; }
        //public long RowNumber { get; set; }
    }
}
