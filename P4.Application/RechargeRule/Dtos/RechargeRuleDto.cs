using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.RechargeRule.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(P4.OtherAccounts.RechargeRule))]
    public class RechargeRuleDto : EntityDto
    {
        /// <summary>
        /// 优惠名称
        /// </summary>
        [MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 赠送金额
        /// </summary>
        public decimal Preferential { get; set; }
    }
}
