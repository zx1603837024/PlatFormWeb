using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace P4.Sensors.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllSensorsListOutput : PagedResultOutput<Sensor>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<SensorDto> rows { get; set; }
    }
}
