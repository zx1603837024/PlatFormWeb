using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.RechargeRule.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllRechargeRuleOutput : PagedResultOutput<P4.OtherAccounts.RechargeRule>, Abp.Application.Services.Dto.IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<RechargeRuleDto> rows { get; set; }
    }

}
