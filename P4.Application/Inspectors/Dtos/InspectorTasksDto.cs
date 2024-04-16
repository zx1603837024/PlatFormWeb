using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inspectors.Dtos
{
    [AutoMapFrom(typeof(InspectorTask))]
    public class InspectorTasksDto : EntityDto<long>
    {
        public string CompanyName { get; set; }

        public string ParkName { get; set; }

        public string InspectorName { get; set; }

        public string Status { get; set; }

        public DateTime? EffectiveTime { get; set; }

        public string Remark { get; set; }


    }
}
