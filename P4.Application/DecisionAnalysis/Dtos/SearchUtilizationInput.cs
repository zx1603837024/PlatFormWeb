using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;

namespace P4.DecisionAnalysis.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchUtilizationInput : IPagedResultRequest, IFilters
    {
        /// <summary>
        /// 
        /// </summary>
        public int? berthsecId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? begintime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? endtime { get; set; }

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
        public string filters {get; set; }
    }
}
