using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace P4.Weixin.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllWeixinIdeaOutput : PagedResultOutput<WeixinIdea>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<WeixinIdeaDto> rows { get; set; }
    }
}
