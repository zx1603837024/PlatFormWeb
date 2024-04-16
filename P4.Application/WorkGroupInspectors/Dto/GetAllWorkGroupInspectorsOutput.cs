using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.WorkGroupInspectors.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllWorkGroupInspectorsOutput : PagedResultOutput<WorkGroupsInspectors>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<WorkGroupsInspectorsDto> rows { get; set; }
    }
}
