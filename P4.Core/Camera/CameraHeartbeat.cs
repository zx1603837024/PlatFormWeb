using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Camera
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpCameraHeartbeats")]
    public class CameraHeartbeat  : Entity, IHasCreationTime
    {

        /// <summary>
        /// 设备编号
        /// </summary>
        [MaxLength(30)]
        public string devid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public string BaseId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
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
        [MaxLength(30)]
        public string gpslatitude { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
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

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
