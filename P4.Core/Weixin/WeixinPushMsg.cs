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
    /// 微信推送消息(停车信息推送)
    /// </summary>
    [Table("AbpWeixinPushMsg")]
    public class WeixinPushMsg  : Entity, IHasCreationTime, IMayHaveTenant
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        [MaxLength(50)]
        public string PlateNumber { get; set; }

        /// <summary>
        /// 推送类型
        /// </summary>
        [MaxLength(50)]
        public string PushType { get; set; }

        /// <summary>
        /// 推送内容
        /// </summary>
        [MaxLength(200)]
        public string PushContent { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 商户代码
        /// </summary>
        public int? TenantId { get; set; }
    }
}
