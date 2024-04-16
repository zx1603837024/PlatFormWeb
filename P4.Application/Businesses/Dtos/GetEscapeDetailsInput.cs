using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Businesses.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetEscapeDetailsInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        /// <summary>
        /// 
        /// </summary>
        public string filters { get; set; }

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
        public int RegionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ParkId { get; set; }

        /// <summary>
        /// 是否追缴  1追缴 2未追缴
        /// </summary>
        public int RepaymentYorN { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BerthsecId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BerthNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PlateNumber { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<int> CompanyIds { get; set; }
        /// <summary>
        /// 补缴收费员
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Int16 RepaymentPayStatus { get; set; }

    }
}
