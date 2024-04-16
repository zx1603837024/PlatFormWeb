using Abp.Application.Services.Dto;
using System;

namespace P4.WorkGroupInspectors.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class WorkGroupInspectorsInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        /// <summary>
        /// 
        /// </summary>
        public string TenantId { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public string operateDateBegin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string operateDateEnd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime begindt
        {
            get
            {
                return string.IsNullOrWhiteSpace(operateDateBegin) == true ? DateTime.Now.Date : DateTime.Parse(operateDateBegin + " 00:00:00");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime enddt
        {
            get
            {
                return string.IsNullOrWhiteSpace(operateDateEnd) == true ? DateTime.Now.AddDays(1).Date : DateTime.Parse(operateDateEnd + " 23:59:59");

            }

        }
    }
}
