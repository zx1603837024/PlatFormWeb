using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Collectors.Dtos
{
    public class GetBerthCheckTotalOutput : IOutputDto
    {
        /// <summary>
        /// 泊位有车辆在停
        /// </summary>
        public int BerthOnUseCount { get; set; }
        /// <summary>
        /// 泊位没有车辆在停
        /// </summary>
        public int BerthOffUseCount { get; set; }

        /// <summary>
        /// 泊位总数
        /// </summary>
        public int BerthCount { get; set; }


    }
}
