using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace P4.Weixin.Dtos
{
    /// <summary>
    /// 系统意见反馈dto
    /// </summary>
    [AutoMapFrom(typeof(WeixinIdea))]
    public class WeixinIdeaDto : EntityDto
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
        public string createTimeStr
        {
            get { return createTime.ToString("yyyy-MM-dd HH:mm:ss"); }
        }

        /// <summary>
        /// 回复时间
        /// </summary>
        public DateTime? ReplyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ReplyTimeStr
        {
            get
            {
                if (ReplyTime.HasValue)
                    return ReplyTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                else
                    return "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        public string Reply { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string openId { get; set; }
    }
}
