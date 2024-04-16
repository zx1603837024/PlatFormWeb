using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace P4.Weixin.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(WeixinOrder))]
    public class WeixinOrdersDto : EntityDto
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(32)]
        public string appid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(32)]
        public string out_trade_no { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(128)]
        public string openId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [MaxLength(32)]
        public string mch_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int cash_fee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double total_fee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(8)]
        public string fee_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(16)]
        public string result_code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(32)]
        public string err_code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(1)]
        public string is_subscribe { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(8)]
        public string trade_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string bank_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(40)]
        public string transaction_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(255)]
        public string attach { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public string time_end { get; set; }

        /// <summary>
        /// 支付类型
        /// </summary>
        public int couresId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TenantId { get; set; }
    }

    /// <summary>
    /// 带总数
    /// </summary>
    public class WeixinOrdersTotalCountDto: WeixinOrdersDto
    {

        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
