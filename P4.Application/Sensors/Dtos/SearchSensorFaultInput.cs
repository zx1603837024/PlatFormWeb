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
    public class SearchSensorFaultInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }
        public int SensorBeatId { get; set; }

        public int page { get; set; }

        public int rows { get; set; }

        public string filters { get; set; }
    }
}
