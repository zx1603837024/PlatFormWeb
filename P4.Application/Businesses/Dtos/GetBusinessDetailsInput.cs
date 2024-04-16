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
    public class GetBusinessDetailsInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
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
        /// 
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ParkId { get; set; }

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
        /// 进场收费员
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int16 StopType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int16 PayStatus { get; set; }
    }
}
