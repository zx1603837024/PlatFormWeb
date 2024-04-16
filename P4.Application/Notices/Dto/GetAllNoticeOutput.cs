using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Notices.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllNoticeOutput : PagedResultOutput<Notice>, IOutputDto
    {
        public List<NoticeDto> rows { get; set; }
    }
}
