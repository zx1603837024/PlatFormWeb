using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace P4.App
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpAppMessages")]
    public class AppMessage : Entity<long>, IHasCreationTime
    {

        /// <summary>
        /// 接收手机号码
        /// </summary>
        [MaxLength(50)]
        public string RevicePhone { get; set; }

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
        /// 消息类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public int HasRead { get; set; }
    }
}
