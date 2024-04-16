using System;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EmployeeBatchNoDynamicReport.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetEmployeeBatchNoDynamicReportInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
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
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }
        /// <summary>
        /// 商户ID
        /// </summary>
        public int TenantId { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public int CompanyId { get; set; }
    }
}
