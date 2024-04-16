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
    public class GetAllSensorsBeatOutput : PagedResultOutput<SensorBeat>, IOutputDto
    {
         public List<SensorBeatDto> rows { get; set; }
    }
}
