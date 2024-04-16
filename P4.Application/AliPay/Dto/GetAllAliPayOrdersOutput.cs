using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.AliPay.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllAliPayOrdersOutput : PagedResultOutput<AliPayOrder>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<AliPayOrdersDto> rows { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public AliPayOrderUserData userdata { get; set; }
    }
}
