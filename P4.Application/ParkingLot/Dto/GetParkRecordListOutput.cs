using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.ParkingLot.Dto
{
    /// <summary>
    /// 获取停车记录列表
    /// </summary>
    public class GetParkRecordListOutput:PagedResultOutput<ParkingRecord>, IOutputDto
    {
        /// <summary>
        /// 停车数据
        /// </summary>
        public List<ParkingRecordEntity> rows { get; set; }


    }
}
