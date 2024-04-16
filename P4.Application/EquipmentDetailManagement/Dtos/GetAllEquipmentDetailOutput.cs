using Abp.Application.Services.Dto;
using P4.EquipmentMaintain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EquipmentDetailManagement.Dtos
{
    public class GetAllEquipmentDetailOutput : PagedResultOutput<EquipmentDetail>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<EquipmentDetailDto> rows { get; set; }
    }
}
