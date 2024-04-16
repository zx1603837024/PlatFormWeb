using P4.BlackLists.Dtos;
using P4.MonthlyCars.Dtos;
using P4.Rates.Dtos;
using P4.WhiteLists.Dtos;
using System.Collections.Generic;

namespace P4.Web.Models
{
    /// <summary>
    /// 道闸与后台交互模型
    /// </summary>
    public class SignoModel
    {
        /// <summary>
        /// 费率列表
        /// </summary>
        public List<RateDto> RateDtos { get; set; }

        /// <summary>
        /// 黑名单
        /// </summary>
        public List<BlackListsDto> BlackList { get; set; }

        /// <summary>
        /// 白名单
        /// </summary>
        public List<WhiteListsDto> WhiteList { get; set; }

        /// <summary>
        /// 包月车辆
        /// </summary>
        public List<MonthlyCarDto> MonthlyCarList { get; set; }

        /// <summary>
        /// 是否启用微信支付
        /// </summary>
        public bool WeixinPay { get; set; }

        /// <summary>
        /// 是否启用支付宝支付
        /// </summary>
        public bool AliPay { get; set; }
    }
}