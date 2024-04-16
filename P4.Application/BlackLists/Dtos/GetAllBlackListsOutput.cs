using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace P4.BlackLists.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllBlackListsOutput : PagedResultOutput<BlackList>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
       public List<BlackListsDto> rows { get; set; }
    }
}
