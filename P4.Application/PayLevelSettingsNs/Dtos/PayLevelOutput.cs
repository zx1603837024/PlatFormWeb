using Abp.Application.Services.Dto;
using P4.PayLevelSetting;
using P4.VideoEquips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.PayLevelSettingsNs.Dtos
{
    public class PayLevelOutput : PagedResultOutput<PayLevelSettings>, IOutputDto
    {
        public List<PayLevelSettingsDto> rows { get; set; }
    }
}
