using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.OtherAccounts.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllOtherAccountOutput : PagedResultOutput<DeductionRecord>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<OtherAccountDto> rows { get; set; }
    }
}
