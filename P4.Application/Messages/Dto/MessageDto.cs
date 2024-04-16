using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;

namespace P4.Messages.Dto
{

    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Message))]
    public class MessageDto : EntityDto<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long CreatorUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReadTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRead { get; set; }
    }
}
