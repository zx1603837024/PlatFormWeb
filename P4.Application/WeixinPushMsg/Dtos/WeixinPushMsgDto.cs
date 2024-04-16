using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace P4.WeixinPushMsg.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Weixin.WeixinPushMsg))]
    public  class WeixinPushMsgDto : EntityDto
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
        /// 
        /// </summary>
        public string CreationTimeStr
        {
            get {
                return CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
    }
}
