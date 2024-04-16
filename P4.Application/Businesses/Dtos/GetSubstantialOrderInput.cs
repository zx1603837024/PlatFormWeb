using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Businesses.Dtos
{
    /// <summary>
    /// 大额订单输入条件
    /// </summary>
    public class GetSubstantialOrderInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
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

        public string operateDateBegin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string operateDateEnd { get; set; }

        public DateTime begindt
        {
            get
            {
                return string.IsNullOrWhiteSpace(operateDateBegin) == true ? DateTime.Now.Date : DateTime.Parse(operateDateBegin + " 00:00:00");
            }

        }
        public DateTime enddt
        {
            get
            {
                return string.IsNullOrWhiteSpace(operateDateEnd) == true ? DateTime.Now.AddDays(1).Date : DateTime.Parse(operateDateEnd + " 23:59:59");

            }

        }
        public int RegionId { get; set; }

        public int ParkId { get; set; }

        /// <summary>
        /// 是否追缴  1追缴 2未追缴
        /// </summary>
        public int RepaymentYorN { get; set; }

        public int BerthsecId { get; set; }

        public string BerthNumber { get; set; }

        public string PlateNumber { get; set; }

        public int? TenantId { get; set; }

        public int? CompanyId { get; set; }

        public List<int> CompanyIds { get; set; }
        /// <summary>
        /// 补缴收费员
        /// </summary>
        public int EmployeeId { get; set; }
    }
}
