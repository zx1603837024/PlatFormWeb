using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.BlackLists.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class BlackListsInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
   {
        /// <summary>
        /// 
        /// </summary>
        public int rows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string filters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool _search { get; set; }
    }
}
