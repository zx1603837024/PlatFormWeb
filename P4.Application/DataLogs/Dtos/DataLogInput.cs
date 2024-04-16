using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.DataLogs.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class DataLogInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        /// <summary>
        /// 
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int rows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string filters { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
    }
}
