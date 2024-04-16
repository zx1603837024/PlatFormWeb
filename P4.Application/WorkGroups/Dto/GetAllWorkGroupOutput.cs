using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace P4.WorkGroups.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllWorkGroupOutput : PagedResultOutput<WorkGroup>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<WorkGroupDto> rows { get; set; }
    }
}
