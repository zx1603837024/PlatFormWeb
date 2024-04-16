using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace P4.Weixin.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(MonthRule))]
    public class CreateOrUpdateWeixinMonthRuleInput : EntityRequestInput, IOperDto
    {

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

        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }
    }
}
