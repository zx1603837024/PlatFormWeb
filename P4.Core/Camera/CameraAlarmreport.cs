using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Camera
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpCameraAlarmreports")]
    public class CameraAlarmreport : Entity, IHasCreationTime
    {
        /// <summary>
        /// 
        /// </summary>
        public int alarmtype { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(10)]
        public string devtype { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public string sn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public string cmd { get; set; }

        /// <summary>
        /// 
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
        public string imageFile { get; set; }

        

        /// <summary>
        /// 
        /// </summary>
        public int imageFileLen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime PhotoTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

    }
}
