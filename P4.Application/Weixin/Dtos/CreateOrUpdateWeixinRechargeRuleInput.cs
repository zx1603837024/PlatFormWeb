using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace P4.Weixin.Dtos
{

    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(WeixinRechargeRule))]
    public class CreateOrUpdateWeixinRechargeRuleInput : EntityRequestInput, IOperDto
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
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }
    }
}
