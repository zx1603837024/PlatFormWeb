using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Weixin.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(MonthRule))]
    public class WeixinMonthRuleDto : EntityDto
    {
        /// <summary>
        /// 商户代码
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ParkId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public string ParkName { get; set; }

        /// <summary>
        /// 包月金额
        /// </summary>
        public decimal MonthMoney { get; set; }

        /// <summary>
        /// 包年金额
        /// </summary>
        public decimal YearMoney { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
    }
}
