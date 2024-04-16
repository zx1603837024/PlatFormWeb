using Abp.Application.Services.Dto;
using P4.Businesses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.InspectorCharges.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllInspectorChargesOutput : PagedResultOutput<BusinessDetail>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<InspectorChargesDto> rows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public InspectorUserData userdata { get; set; }
    }
}
