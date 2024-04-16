using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Camera.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(CameraHeartbeat))]
    public class HeartbeatRequestDto : BaseRequestDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string sn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int second { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int lockstatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ledstatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int radarstatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int gpsstatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gpslatitude { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gpslongitude { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int gyroxstatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int gyroX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int gyroY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int gyroZ { get; set; }
    }
}
