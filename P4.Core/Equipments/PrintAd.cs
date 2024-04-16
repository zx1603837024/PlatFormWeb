using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Equipments
{
    /// <summary>
    /// 打印小票广告
    /// </summary>
    [Table("AbpPrintAds")]
    public class PrintAd : Entity, IMustHaveTenant, IPassivable
    {

        /// <summary>
        /// 广告名称
        /// </summary>
        [MaxLength(40)]
        public virtual string AdName { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [MaxLength(500)]
        public virtual string Context { get; set; }

        /// <summary>
        /// 二维码
        /// </summary>
        [MaxLength(30)]
        public virtual string QrCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime BeginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime EndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsActive { get; set; }

    }
}
