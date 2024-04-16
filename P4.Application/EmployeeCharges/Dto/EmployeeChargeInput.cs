using Abp.Application.Services.Dto;
using System;

namespace P4.EmployeeCharges.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeChargeInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {

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
                return string.IsNullOrWhiteSpace(operateDateBegin) == true ? DateTime.Now.Date : DateTime.Parse(operateDateBegin);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime enddt
        {
            get
            {
                return string.IsNullOrWhiteSpace(operateDateEnd) == true ? DateTime.Now.AddDays(1).Date : DateTime.Parse(operateDateEnd);

            }

        }

        /// <summary>
        /// //gcj  add 逃逸时间检查text
        /// </summary>
        public string EscapeTimeDateBegin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EscapeTimeDateEnd { get; set; }

        /// <summary>
        /// //gcj  add 逃逸时间检查text
        /// </summary>
        public DateTime EscapeTimebegindt
        {
            get
            {
                return string.IsNullOrWhiteSpace(EscapeTimeDateBegin) == true ? DateTime.Now.Date : DateTime.Parse(EscapeTimeDateBegin);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime EscapeTimeenddt
        {
            get
            {
                return string.IsNullOrWhiteSpace(EscapeTimeDateEnd) == true ? DateTime.Now.AddDays(1).Date : DateTime.Parse(EscapeTimeDateEnd);

            }

        }

        /// <summary>
        /// 
        /// </summary>
        public string employeeIdInput { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string employeeNameInput { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CompanyId { get; set; }

        /// <summary>
        /// 泊位段Ids
        /// </summary>
        public string BerthsecIds { get; set; }
    }
}
