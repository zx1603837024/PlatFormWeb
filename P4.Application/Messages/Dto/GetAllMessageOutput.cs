using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Messages.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllMessageOutput : PagedResultOutput<Message>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<MessageDto> rows { get; set; }
    }
}
