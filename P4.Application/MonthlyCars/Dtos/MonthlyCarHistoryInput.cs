using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.MonthlyCars.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class MonthlyCarHistoryInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {

        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string filters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime operateDateBegin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime operateDateEnd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int rows { get; set; }
    }
}
