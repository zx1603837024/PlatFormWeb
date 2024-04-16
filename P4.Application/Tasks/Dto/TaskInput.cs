using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Tasks.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Task))]
    public class TaskInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        public int rows { get; set; }

        public int page { get; set; }

        public string filters { get; set; }
    }
}
