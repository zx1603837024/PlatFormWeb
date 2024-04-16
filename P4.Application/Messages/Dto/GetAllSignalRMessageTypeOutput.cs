using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace P4.Messages.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllSignalRMessageTypeOutput : PagedResultOutput<SignalRMessageType>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<SignalRMessageTypeDto> rows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<SignalRMessageTypeDto> selectrows { get; set; }
    }
}
