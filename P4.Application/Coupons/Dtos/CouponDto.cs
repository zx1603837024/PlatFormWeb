using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Weixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Coupons.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Coupon))]
    public class CouponDto : EntityDto
    {
        /// <summary>
        /// 优惠券编号
        /// </summary>
        public string CouponNo { get; set; }

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
        /// openId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string tel { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 优惠券类型
        /// 1注册送券
        /// 2首充送券
        /// 3活动券
        /// 4后台券
        /// </summary>
        public int CouponType { get; set; }
    }
}
