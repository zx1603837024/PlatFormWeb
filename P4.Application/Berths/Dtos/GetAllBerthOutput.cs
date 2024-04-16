using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Berths.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllBerthOutput : PagedResultOutput<Berth>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<BerthDto> rows { get; set; }
    }
}
