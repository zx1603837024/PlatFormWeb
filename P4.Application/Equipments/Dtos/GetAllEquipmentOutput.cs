using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Equipments.Dtos
{
    public class GetAllEquipmentOutput : PagedResultOutput<Equipment>, IOutputDto
    {
        public List<EquipmentDto> rows { get; set; }
    }
}
