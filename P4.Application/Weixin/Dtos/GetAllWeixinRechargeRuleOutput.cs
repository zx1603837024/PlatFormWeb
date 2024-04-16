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
    public class GetAllWeixinRechargeRuleOutput : PagedResultOutput<WeixinRechargeRule>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<WeixinRechargeRuleDto> rows { get; set; }
    }
}
