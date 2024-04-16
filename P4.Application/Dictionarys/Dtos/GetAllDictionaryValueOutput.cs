using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace P4.Dictionarys.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllDictionaryValueOutput : PagedResultOutput<DictionaryValue>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<DictionaryValueDto> rows { get; set; }
    }
}
