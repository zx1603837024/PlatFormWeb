using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Sensors.Dtos
{
        [AutoMapFrom(typeof(Sensor))]
    public class SensorParkRatioDto : EntityDto
    {
            /// <summary>
            /// 停车场名字
            /// </summary>
            public string ParkName { get; set; }
            /// <summary>
            /// 停车场在线率
            /// </summary>
            public string Ratio { get; set; }
    }
}
