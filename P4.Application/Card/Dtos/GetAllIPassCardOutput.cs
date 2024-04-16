using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace P4.Card.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllIPassCardOutput : PagedResultOutput<IPassCard>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<IPassCardDto> rows { get; set; }
    }
}
