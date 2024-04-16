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
    public class GetAllWeixinMonthRuleOutput : PagedResultOutput<MonthRule>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<WeixinMonthRuleDto> rows { get; set; }
    }
}
