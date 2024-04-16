using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.AliPay.Dto
{
    [AutoMapFrom(typeof(AliPayOrder))]
    public class AliPayOrderUserData
    {
        /// <summary>
        /// 支付宝交易号
        /// </summary>
        public string trade_no { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        public double total_amount { get; set; }
    }
}
