using Abp.Application.Services.Dto;
using P4.Weixin;
using System.Collections.Generic;

namespace P4.Coupons.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllCouponOutput : PagedResultOutput<Coupon>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<CouponDto> rows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CouponDto userdata { get; set; }
    }
}
