using Abp.Application.Services.Dto;
using P4.App;
using System.Collections.Generic;

namespace P4.AppMessages.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllAppMessagesOutput : PagedResultOutput<AppMessage>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<AppMessageDto> rows { get; set; }
    }
}
