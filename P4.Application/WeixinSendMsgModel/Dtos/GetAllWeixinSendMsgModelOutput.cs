using Abp.Application.Services.Dto;
using P4.Weixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.WeixinSendMsgModel.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllWeixinSendMsgModelOutput : PagedResultOutput<P4.Weixin.WeixinSendMsgModel>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<WeixinSendMsgModelDto> rows { get; set; }
    }
}
