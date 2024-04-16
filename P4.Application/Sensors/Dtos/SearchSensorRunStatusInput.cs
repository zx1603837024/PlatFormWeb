using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Sensors.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchSensorRunStatusInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        /// <summary>
        /// 车检器编号
        /// </summary>
        public string SensorNumber { get; set; }

        public int page { get; set; }

        public int rows { get; set; }

        public string filters { get; set; }
    }
}
