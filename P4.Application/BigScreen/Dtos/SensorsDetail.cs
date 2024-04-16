using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.BigScreen.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class SensorsDetail : IOutputDto
    {
        /// <summary>
        /// 地磁总数
        /// </summary>
        public int SensorsTotal { get; set; }
        /// <summary>
        /// 地磁在线数
        /// </summary>
        public int OnLine { get; set; }
        /// <summary>
        /// 地磁掉线数
        /// </summary>
        public int OffLine { get; set; }

    }
    
}
