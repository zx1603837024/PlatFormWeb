using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Weixin
{
    /// <summary>
    /// 优惠券
    /// </summary>
    [Table("AbpCoupons")]
    public class Coupon : Entity, IHasCreationTime, IMustHaveTenant, IPassivable
    {

        /// <summary>
        /// 优惠券编号
        /// </summary>
        [MaxLength(20)]
        public string CouponNo { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public string BeginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public string EndTime { get; set; }

        /// <summary>
        /// openId
        /// </summary>
        [MaxLength(50)]
        public string OpenId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public string Mark { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(15)]
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
