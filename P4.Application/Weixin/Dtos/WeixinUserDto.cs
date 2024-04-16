using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace P4.Weixin.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(WeixinUser))]
    public class WeixinUserDto : EntityDto
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(255)]
        public string openId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? subscribeTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(10)]
        public string province { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sex { get; set; }

        /// <summary>
        /// 
        /// </summary>
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
