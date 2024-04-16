using Abp.Application.Services.Dto;
using P4.Smss;
using System.Collections.Generic;

namespace P4.SmsManagements.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllSmsModelOutput : PagedResultOutput<SmsModel>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<SmsModelDto> rows { get; set; }
    }
}
