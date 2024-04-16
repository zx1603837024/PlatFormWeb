using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.OtherAccounts.Dtos
{
    /// <summary>
    /// 钱包消费记录
    /// </summary>
    public class GetAllDeductionRecordOutput : PagedResultOutput<DeductionRecord>, IOutputDto
    {
        public List<DeductionRecordDto> rows { get; set; }
    }
}
