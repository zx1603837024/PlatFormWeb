using Abp.Application.Services.Dto;
using P4.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Equipments.Dtos
{
    public class GetAllEquipmentMaintenanceOutput : PagedResultOutput<EquipmentMaintenance>, IOutputDto
    {
        public List<EquipmentMaintenanceDto> rows { get; set; }
    }
}
