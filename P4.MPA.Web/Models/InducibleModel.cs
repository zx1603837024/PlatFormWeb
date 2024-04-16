using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using P4.Inducibles.Dtos;
using P4.Parks.Dtos;
using P4.Sensors.Dtos;

namespace P4.Web.Models
{
    public class InducibleModel
    {

        public List<InducibleDto> inducibleList { get; set; }
        public List<ParkDto> parkList { get; set; }

        public List<SensorDto> sensorList { get; set; }

        public List<SensorToParkDto> sensorParkList { get; set; }
    }
}