using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;

namespace P4.Tasks.Dto
{
    [AutoMapFrom(typeof(Task))]
    public class TaskDto : EntityDto<int>
    {

        public string TaskInfo { get; set; }

        /// <summary>
        /// 是否查看
        /// </summary>
        public bool IsActive { get; set; }


        public DateTime? ReadTime { get; set; }


        public bool IsRead { get; set; }


        public long CreatorUserId { get; set; }


        public DateTime CreationTime { get; set; }
    }
}
