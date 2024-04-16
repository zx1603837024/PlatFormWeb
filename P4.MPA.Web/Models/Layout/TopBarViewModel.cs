using P4.Messages.Dto;
using P4.Notices.Dto;
using P4.Tasks.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models.Layout
{
    public class TopBarViewModel
    {
        public string Name { get; set; }

        /// <summary>
        /// 系统提示框
        /// </summary>
        public string SystemPromptBox { get; set; }

        /// <summary>
        /// 系统消息
        /// </summary>
        public IReadOnlyList<MessageDto> Messages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MessagesCount { get; set; }

        /// <summary>
        /// 系统任务
        /// </summary>
        public IReadOnlyList<TaskDto> Tasks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TasksCount { get; set; }


        /// <summary>
        /// 系统通知
        /// </summary>
        public IReadOnlyList<NoticeDto> Notices { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int NoticesCount { get; set; }

    }
}