using System.Collections.Generic;
using Abp.Application.Services.Dto;
using P4.Card;

namespace P4.CardCode.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAlllPassCardCodeOutput : PagedResultOutput<IpassCardCode>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<IPassCardCodeDto> rows { get; set;}
    }
}
