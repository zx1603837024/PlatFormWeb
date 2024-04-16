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
    public class GetEscapeDetailsOutput : PagedResultOutput<BusinessDetail>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<EscapeDetailsDto> rows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EscapeDetailsUserData userdata { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GetEscapeDetailsOutputTow : PagedResultOutput<BusinessDetail>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public new List<EscapeDetailsDto> Items { get; set; }
    }
}
