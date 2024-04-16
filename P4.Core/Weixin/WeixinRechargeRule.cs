using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace P4.Weixin
{
    /// <summary>
    /// 充值规则
    /// </summary>
    [Table("WeixinRechargeRule")]
    public class WeixinRechargeRule : Entity, IPassivable, IMustHaveTenant
    {
        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 赠送金额
        /// </summary>
        public decimal Donation { get; set; }

        /// <summary>
        /// 商户代码
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
    }
}
