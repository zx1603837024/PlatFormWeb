using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using P4.Sensors;
using Abp.Application.Services.Dto;

namespace P4.SensorBusinessDetails.Dtos
{
    [AutoMapTo(typeof(SensorBusinessDetail))]
    public class CreatOrUpdateSensorBusinessDetailInput : EntityDto, IInputDto, IOperDto
    {
        public string oper { get; set; }
    }
}
