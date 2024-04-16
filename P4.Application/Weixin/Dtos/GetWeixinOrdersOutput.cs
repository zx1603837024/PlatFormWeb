using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace P4.Weixin.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetWeixinOrdersOutput : PagedResultOutput<WeixinOrder>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<WeixinOrdersDto> rows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public WeixinOrdersDto userdata { get; set; }
    }
}
