using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.OtherAccounts
{
    /// <summary>
    /// 短信记录
    /// </summary>
    [Table("SmsSendRecords")]
    public class SmsSendRecord : Entity, IMustHaveTenant, IHasCreationTime
    {
        /// <summary>
        /// 商户代码
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [MaxLength(50)]
        public string PlateNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(255)]
        public string TelePhone { get; set; }

        /// <summary>
        /// 欠费金额
        /// </summary>
        public decimal SUMArrearage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 已发送短信的次数
        /// </summary>
        public int SendSmsNumber { get; set; }
    }
}
