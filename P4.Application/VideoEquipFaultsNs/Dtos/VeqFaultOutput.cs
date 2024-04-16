using Abp.Application.Services.Dto;
using P4.VideoEquips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.VideoEquipFaultsNs.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class VeqFaultOutput : PagedResultOutput<VideoEquipFaults>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<VideoEqFaultDto> rows { get; set; }
    }
}
