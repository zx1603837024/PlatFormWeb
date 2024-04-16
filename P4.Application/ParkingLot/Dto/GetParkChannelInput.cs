using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.ParkingLot.Dto
{
    /// <summary>
    /// 获取通道列表
    /// </summary>
    public class GetParkChannelInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 分公司ID
        /// </summary>
        public int? CompanyId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 一页多少行
        /// </summary>
        public int rows { get; set; }

        /// <summary>
        /// 筛选条件
        /// </summary>
        public string filters { get; set; }
    }
}
