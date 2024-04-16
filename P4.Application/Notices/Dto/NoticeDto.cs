using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
namespace P4.Notices.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Notice))]
    public class NoticeDto : EntityDto<int>
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string NoticeInfo { get; set; }

        /// <summary>
        ///  消息链接
        /// </summary>
        public string NoticeUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long CreatorUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReadTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

    }
}
