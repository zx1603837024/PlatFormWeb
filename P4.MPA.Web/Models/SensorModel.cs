using P4.Berthsecs;
using P4.Berthsecs.Dto;
using P4.Sensors.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    public class SensorModel
    {
        public List<SensorDto> sensorList { get; set; }
        public List<BerthsecDto> berthsecList { get; set; }
    }
}