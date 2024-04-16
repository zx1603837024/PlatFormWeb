using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Sensors.Dtos
{
    public class GetAllSensorRunStatusOutput : PagedResultOutput<SensorRunStatus>, IOutputDto
    {
        public List<SensorRunStatusDto> rows { get; set; }
    }
}
