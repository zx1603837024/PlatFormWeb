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
    /// 微信菜单管理
    /// </summary>
    [Table("AbpWeixinMenu")]
    public class WeixinMenu : Entity, IHasCreationTime, IMayHaveTenant
    {

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(10)]
        public string type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(10)]
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public string url { get; set; }


        /// <summary>
        /// 父类id，如果父类id不为0，的时候
        /// </summary>
        public int fatherId { get; set; }

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
