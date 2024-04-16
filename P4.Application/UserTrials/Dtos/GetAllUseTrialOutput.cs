using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.UserTrials.Dtos
{

    /// <summary>
    /// 
    /// </summary>
    public class GetAllUseTrialOutput : PagedResultOutput<UserTrial>, IOutputDto
    {
        public List<UserTrialDto> rows { get; set; }
    }
}
