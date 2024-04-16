using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.XianPay
{

    /// <summary>
    /// 
    /// </summary>
    [Table("XianPayOrders")]
    public class XianPayOrder : Entity, IMustHaveTenant, IMayHaveCompany
    {
        /// <summary>
        /// 
        /// </summary>
        public int? CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 订单状态
        /// 1预支付
        /// 2支付成功
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid guid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(40)]
        public string ORDER_NO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(15)]
        public string MCHNT_NO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(10)]
        public string CHANNEL_ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal PAY_AMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string CUST_PHONE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(1024)]
        public string INVOICE_CONTENT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string CAR_LICENSE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(528)]
        public string PARK_PLACE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string PARK_START_TIME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string PARK_END_TIME { get; set; }






        /// <summary>
        /// 
        /// </summary>
        [MaxLength(1024)]
        public string SIGNATUR { get; set; }

        [MaxLength(40)]
        public string PAY_LOG_ID { get; set; }

        [MaxLength(2)]
        public string PAY_STATUS { get; set; }

        [MaxLength(10)]
        public string PASS_TYPE { get; set; }

        [MaxLength(20)]
        public string PAY_TIME { get; set; }

        [MaxLength(1024)]
        public string P_DATA { get; set; }

        [MaxLength(50)]
        public string USER_ID { get; set; }

        [MaxLength(10)]
        public string P_FLAG { get; set; }

        [MaxLength(10)]
        public string BANK_TYPE { get; set; }

        [MaxLength(30)]
        public string ACC_NO { get; set; }

    }
}
