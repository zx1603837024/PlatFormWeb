using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Weixin
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpCouponDetails")]
    public class CouponDetail : Entity, IHasCreationTime, IMustHaveTenant
    {
        /// <summary>
        /// 优惠券编号
        /// </summary>
        [MaxLength(20)]
        public string CouponNo { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [MaxLength(10)]
        public string Platenumber { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal OldMoney { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal NewMoney { get; set; }

        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal ConsumptionMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(15)]
        public string tel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public string openId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 消费类型
        /// 1充值
        /// 2消费
        /// </summary>
        public int CouponType { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        [MaxLength(20)]
        public string Source { get; set; }
    }
}
