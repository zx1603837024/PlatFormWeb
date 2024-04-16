using Abp.Application.Services.Dto;
using P4.Weixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.CouponsDetails.Dtos
{
    /// <summary>
    /// 
    /// </summary>
   public  class GetAllCouponDetailOutput : PagedResultOutput<CouponDetail>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<CouponDetailDto> rows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CouponDetailDto userdata { get; set; }
    }
}
