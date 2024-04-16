using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Dictionarys.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllDictionaryTypeOutput : PagedResultOutput<DictionaryType>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<DictionaryTypeDto> rows { get; set; }
    }
}
