using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace P4.Berthsecs.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllBerthsecsListOutput : PagedResultOutput<Berthsec>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<BerthsecDto> rows { get; set; }
    }
}
