using Abp.Application.Services.Dto;
using P4.Businesses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Signo.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetIllegalOpenOutput : PagedResultOutput<IllegalOpenSignoPicture>, IOutputDto
    {
        public List<IllegalOpenSignoDto> rows { get; set; }
    }
}
