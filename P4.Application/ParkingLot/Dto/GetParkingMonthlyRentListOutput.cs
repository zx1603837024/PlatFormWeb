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
    public class GetParkingMonthlyRentListOutput : PagedResultOutput<ParkingMonthlyRent>, IOutputDto
    {
        /// <summary>
        /// 数据
        /// </summary>
        public List<AbpParkingMonthlyRentDto> rows { get; set; }
    }
}
