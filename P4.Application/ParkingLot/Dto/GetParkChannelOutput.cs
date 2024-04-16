using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.ParkingLot.Dtos
{
    /// <summary>
    /// 获取停车场通道
    /// </summary>
    public class GetParkChannelOutput:PagedResultOutput<ParkChannel>, IOutputDto
    {
        /// <summary>
        /// 数据
        /// </summary>
        public List<ParkChannelDto> rows { get; set; }
    }
}
