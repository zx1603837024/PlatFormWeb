using Abp.Application.Services.Dto;
using P4.Smss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.SmsManagements.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllSmsOutput: PagedResultOutput<Sms>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<SmsDto> rows { get; set; }
    }
}
