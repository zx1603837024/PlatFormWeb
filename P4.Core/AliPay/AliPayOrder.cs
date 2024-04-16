using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace P4.AliPay
{

    /// <summary>
    /// 支付宝支付流水
    /// </summary>
    [Table("AliPayOrders")]
    public class AliPayOrder : Entity, IMustHaveTenant, IMayHaveCompany
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
        /// 交易主题
        /// </summary>
        [MaxLength(500)]
        public string subject { get; set; }

        /// <summary>
        /// 交易内容
        /// </summary>
        [MaxLength(500)]
        public string body { get; set; }

        /// <summary>
        /// 支付宝交易号
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
        /// 
        /// </summary>
        public string guid { get; set; }
    }
}
