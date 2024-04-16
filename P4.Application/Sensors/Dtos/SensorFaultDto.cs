using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
namespace P4.Sensors.Dtos
{
    [AutoMapFrom(typeof(SensorFault))]
    public class SensorFaultDto : EntityDto<long>
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string CreationTimeString
        {
            get { return CreationTime.ToString("yyyy-MM-dd HH:mm:ss"); }
        }


        /// <summary>
        /// 
        /// </summary>
        public virtual string SensorNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string RegionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ParkName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string BerthsecName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string BerthNumber { get; set; }
    }
}
