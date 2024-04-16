using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.AppManage.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchAppInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        public int page { get; set; }

        public int rows { get; set; }

        public string filters { get; set; }
    }
}
