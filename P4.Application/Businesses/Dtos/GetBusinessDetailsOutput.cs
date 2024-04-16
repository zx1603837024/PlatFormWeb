using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Businesses.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetBusinessDetailsOutput : PagedResultOutput<BusinessDetail>, IOutputDto
    {

        /// <summary>
        /// 
        /// </summary>
        public List<BusinessDetailsDto> rows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BusinessDetailUserData userdata { get; set; }
    }
}
