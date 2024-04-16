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
    public class GetAllWeixinTUserOutput : PagedResultOutput<TUser>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<WeixinTUserDto> rows { get; set; }
    }
}
