using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Smss
{

    /// <summary>
    /// 短信通道模板
    /// </summary>
    [Table("AbpSmsModel")]
    public class SmsModel : Entity, IHasCreationTime, IMustHaveTenant
    {

        /// <summary>
        /// 短信类型
        /// </summary>
        [MaxLength(30)]
        public virtual string ModelType { get; set; }

        /// <summary>
        /// 短信内容
        /// </summary>
        [MaxLength(200)]
        public virtual string SmsContext { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public virtual string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(80)]
        public virtual string Password { get; set; }

        /// <summary>
        /// 发送短信路径
        /// </summary>
        [MaxLength(100)]
        public virtual string Url { get; set; }

        /// <summary>
        /// 商户id
        /// </summary>
        public virtual int TenantId { get; set; }
    }
}
