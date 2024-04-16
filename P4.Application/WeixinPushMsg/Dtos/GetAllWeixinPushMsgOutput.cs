using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace P4.WeixinPushMsg.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllWeixinPushMsgOutput : PagedResultOutput<Weixin.WeixinPushMsg>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<WeixinPushMsgDto> rows { get; set; }
    }
}
