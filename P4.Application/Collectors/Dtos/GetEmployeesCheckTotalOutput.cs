using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Collectors.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetCollectorCheckTotalOutput : IOutputDto
    {
        /// <summary>
        /// 签到
        /// </summary>
        public int CheckInCout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CheckOutCount { get; set; }


        /// <summary>
        /// 总员工数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<CollectorDto> Items { get; set; }
    } 
}
