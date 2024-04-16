using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.ParkingLot.Dto
{
    /// <summary>
    /// 获取包月列表
    /// </summary>
    public class GetParkingMonthlyRentListInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {

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
