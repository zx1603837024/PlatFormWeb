using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Equipments.Dtos
{
    [AutoMapTo(typeof(EquipmentMaintenance))]
    public class CreateOrUpdateEquipmentMaintenanceInput : EntityDto, IInputDto, IOperDto
    {
        public string oper { get; set; }
    }
}
