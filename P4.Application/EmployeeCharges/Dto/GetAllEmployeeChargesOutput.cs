using Abp.Application.Services.Dto;
using P4.Businesses;
using System.Collections.Generic;

namespace P4.EmployeeCharges.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllEmployeeChargesOutput : PagedResultOutput<BusinessDetail>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<EmployeeChargesDto> rows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EmployeeUserData userdata { get; set; }
    }
}
