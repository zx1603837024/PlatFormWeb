using Abp.Application.Services.Dto;
using P4.EquipmentMaintain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.ReworkFlowManagement.Dto
{
    public class GetAllReworkFlowOutput : PagedResultOutput<ReworkFlow>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ReworkFlowDto> rows { get; set; }
    }
}
