using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Smss
{
    [Table("AbpSms")]
    public class Sms : Entity<long>, IHasCreationTime, IMayHaveTenant
    {
        /// <summary>
        /// 短信内容
        /// </summary>
        [MaxLength(2000)]
        public virtual string SmsMsg { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(200)]
        public virtual string TelePhones { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(500)]
        public virtual string SmsResult { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime CreationTime { get; set; }


        /// <summary>
        /// 短信条数
        /// </summary>
        public virtual int SmsCount { get; set; }

        /// <summary>
        /// 商户id
        /// </summary>
        public virtual int? TenantId { get; set; }
    }
}
