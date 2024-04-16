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
    /// 微信自动回复消息模板
    /// </summary>
    [Table("AbpWeixinSendMsgModels")]
    public class WeixinSendMsgModel : Entity, IHasCreationTime, IMustHaveTenant, IPassivable
    {
        /// <summary>
        /// 标签，使用;隔开
        /// </summary>
        [MaxLength(300)]
        public string Crux { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        [MaxLength(300)]
        public string Msg { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 商户代码
        /// </summary>
        public int TenantId { get; set; }
    }
}
