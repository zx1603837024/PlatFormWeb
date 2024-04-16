using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Weixin
{
    /// <summary>
    /// 微信记录
    /// </summary>
    [Table("WeixinSendRecords")]
    public class WeixinSendRecord : Entity, IMustHaveTenant, IHasCreationTime
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
        public string openId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(255)]
        public string nickName { get; set; }

        /// <summary>
        /// 欠费金额
        /// </summary>
        public decimal SUMArrearage { get; set; }

        /// <summary>
        /// 已发送微信的次数
        /// </summary>
        public int SendWeixinNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

    }
}
