using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Weixin.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllWeixinConfigOutput: PagedResultOutput<WeixinConfig>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<WeixinConfigDto> rows { get; set; }
    }
}
