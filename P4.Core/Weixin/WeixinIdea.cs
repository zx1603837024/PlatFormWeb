using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Weixin
{
    /// <summary>
    /// 微信意见反馈
    /// </summary>
    [Table("Weixinidea")]
    public class WeixinIdea : Entity, IMustHaveTenant
    {

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(255)]
        public string account { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(255)]
        public string contact { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime createTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        public string Reply { get; set; }

        /// <summary>
        /// openId 编号
        /// </summary>
        public string openId { get; set; }

        /// <summary>
        /// 回复时间
        /// </summary>
        public DateTime? ReplyTime { get; set; }
    }
}
