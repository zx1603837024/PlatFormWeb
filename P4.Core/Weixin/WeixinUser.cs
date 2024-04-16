using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Weixin
{
    /// <summary>
    /// 微信关注用户管理
    /// </summary>
    [Table("Weixinusers")]
    public class WeixinUser : Entity, IMayHaveTenant
    {
        
        [MaxLength(255)]
        public string openId { get; set; }

        public DateTime? subscribeTime { get; set; }

        public DateTime? unsubscribeTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(255)]
        public string nickName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(255)]
        public string unionid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(255)]
        public string privilege { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(255)]
        public string headimgurl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(10)]
        public string country { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(10)]
        public string city { get; set; }

        [MaxLength(10)]
        public string province { get; set; }

        public int sex { get; set; }

        public DateTime? updateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 商户代码
        /// </summary>
        public int? TenantId { get; set; }
    }
}
