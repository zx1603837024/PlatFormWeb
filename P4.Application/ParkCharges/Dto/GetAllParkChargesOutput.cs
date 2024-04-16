using Abp.Application.Services.Dto;
using P4.Businesses;
using System.Collections.Generic;

namespace P4.ParkCharges.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllParkChargesOutput : PagedResultOutput<BusinessDetail>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ParkChargesDto> rows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ParkChargesData userdata { get; set; }
    }
    
}
