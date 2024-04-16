using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Signo.Dtos
{
    /// <summary>
    /// 
    /// </summary>
        public class GetAllSignoOutput : PagedResultOutput<SignoParkInformation>, IOutputDto
        {
            /// <summary>
            /// 
            /// </summary>
            public List<SignoParkInformationDto> rows { get; set; }
        }

}
