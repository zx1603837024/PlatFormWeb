using System;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;

namespace P4.DecisionAnalysis.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(PeakPeriod))]
    public class PeakPeriodDto : EntityDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// true 已过期
        /// false 未过期
        /// </summary>
        public bool IsActive { get; set; }
    }
}
