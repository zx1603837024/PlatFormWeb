using Abp.Application.Services.Dto;
using P4.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.AppManage.Dto
{

    /// <summary>
    /// 
    /// </summary>
    public class GetAllAppOutput : PagedResultOutput<AppAccount>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<AppManageDto> rows { get; set; }
    }
}
