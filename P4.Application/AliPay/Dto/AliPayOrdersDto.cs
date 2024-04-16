using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.AliPay.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(AliPayOrder))]
    public class AliPayOrdersDto : EntityDto
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int? CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        public double total_amount { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [MaxLength(50)]
        public string buyer_logon_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(500)]
        public string subject { get; set; }

        /// <summary>
        /// 交易主题
        /// </summary>
        [MaxLength(500)]
        public string body { get; set; }

        /// <summary>
        /// 交易流水号
        /// </summary>
        [MaxLength(100)]
        public string trade_no { get; set; }

        /// <summary>
        /// 商户网站唯一订单号
        /// </summary>
        [MaxLength(100)]
        public string out_trade_no { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [MaxLength(200)]
        public string gmt_create { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        [MaxLength(200)]
        public string gmt_payment { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        [MaxLength(50)]
        public string trade_status { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public int PayType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 支付宝总条数
        /// </summary>
        public int AliPayOrdersCount { get; set; }
    }
}
