using Abp.Application.Services.Dto;
using P4.Sensors;
using System.Collections.Generic;

namespace P4.SensorBusinessDetails.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllSensorBusinessDetailOutput : PagedResultOutput<SensorBusinessDetail>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<SensorBusinessDetailDto> rows { get; set; }
    }
}
