using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EquipmentMaintain.Dtos
{
    public class GetAllEquipmentsMOutput : PagedResultOutput<EquipmentsM>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<EquipmentsMDto> rows { get; set; }
    }
}
