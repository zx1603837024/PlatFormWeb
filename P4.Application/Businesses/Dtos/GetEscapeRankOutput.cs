using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace P4.Businesses.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetEscapeRankOutput : PagedResultOutput<BusinessDetail>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<EscapeRankDto> rows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EscapeRankDto userdata { get; set; }
    }
}
