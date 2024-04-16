using System;
using Abp.Application.Services.Dto;
using P4.Weixin;
using Abp.AutoMapper;

namespace P4.Coupons.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(Coupon))]
    public class CreateOrUpdateCouponInput : EntityRequestInput, IOperDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string tel { get; set; }
    }
}
