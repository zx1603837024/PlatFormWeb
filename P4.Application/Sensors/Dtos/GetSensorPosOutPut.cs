using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace P4.Sensors.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetSensorPosOutPut : PagedResultOutput<SearchSensorPosDto>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<SearchSensorPosDto> rows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SensorPosUserData userdata { get; set; }
    }
}
