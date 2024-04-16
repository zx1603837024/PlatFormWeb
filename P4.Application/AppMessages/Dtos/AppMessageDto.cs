using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.App;
using System;
using System.ComponentModel.DataAnnotations;

namespace P4.AppMessages.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(AppMessage))]
    public  class AppMessageDto: EntityDto
    {

        /// <summary>
        /// 消息主题
        /// </summary>
        [MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 消息体
        /// </summary>
        [MaxLength(500)]
        public string Content { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string CreationTimeStr
        {
            get { return CreationTime.ToString("yyyy-MM-dd HH:mm:ss"); }
        }

        /// <summary>
        /// 消息类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public int HasRead { get; set; }
    }
}
