using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Equipments
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpAndroidLogs")]
    public class AndroidLog : Entity, IHasCreationTime
    {

        /// <summary>
        /// 
        /// </summary>
        public Guid guid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(4000)]
        public string ExceptionMsg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
