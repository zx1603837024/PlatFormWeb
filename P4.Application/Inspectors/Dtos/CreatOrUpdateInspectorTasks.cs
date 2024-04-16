using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inspectors.Dtos
{
    [AutoMapTo(typeof(InspectorTask))]
    public class CreatOrUpdateInspectorTasks : EntityDto, IInputDto, IOperDto
    {
        public string oper { get; set; }

        public string CompanyName { get; set; }

        public string ParkName { get; set; }

        public string InspectorName { get; set; }

        public Int16 Status { get; set; }

        public DateTime? EffectiveTime { get; set; }

        public string Remark { get; set; }
    }
}
