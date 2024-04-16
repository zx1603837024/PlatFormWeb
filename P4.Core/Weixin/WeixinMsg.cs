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
    /// 微信消息管理
    /// </summary>
    [Table("AbpWeixinMsg")]
    public class WeixinMsg : Entity, IHasCreationTime, IMayHaveTenant
    {

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(300)]
        public string Msg { get; set; }

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
