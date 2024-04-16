using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace P4.WorkGroupInspectors.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllWorkGroupInspectorsReport : PagedResultOutput<WorkGroupsInspectors>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<WorkGroupInspectorsEcharsDto> rows { get; set; }
    }
}
