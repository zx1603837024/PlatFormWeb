using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Equipments.Dtos
{
    [AutoMapFrom(typeof(EquipmentMaintenance))]
    public class EquipmentMaintenanceDto : EntityDto<long>
    {
        public string PDA { get; set; }

        public string UseStatus { get; set; }

        public virtual DateTime? CreationTime { get; set; }

        public string CreatorUserName { get; set; }

        public string Ramark { get; set; }
    }
}
