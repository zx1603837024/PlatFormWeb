using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace P4.Camera
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpCameras")]
    public class Cameras : Entity, IHasCreationTime
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public string sn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public string mac { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public string agrver { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public string hardver { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public string softver { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public string idaddr { get; set; }

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
        public DateTime CreationTime { get; set; }
    }
}
