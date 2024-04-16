using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.PayLevelSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.PayLevelSettingsNs.Dtos
{
    [AutoMapTo(typeof(PayLevelSettings))]
    public class CreateOrUpdatePayLevel : EntityDto, IInputDto, IOperDto
    {
        public string oper { get; set; }
        public int? DeviceOrder { get; set; }
        public string DeviceName { get; set; }
    }
}
