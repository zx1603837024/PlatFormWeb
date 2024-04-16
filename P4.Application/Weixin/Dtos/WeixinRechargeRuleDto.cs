using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace P4.Weixin.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(WeixinRechargeRule))]
    public class WeixinRechargeRuleDto : EntityDto
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
