﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Weixin
{
    /// <summary>
    /// 微信公众号相关配置表
    /// </summary>
    [Table("AbpWeixinConfig")]
    public class WeixinConfig : Entity, IHasCreationTime, IMayHaveTenant
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string AppId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public string webAppId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool encryptMessage { get; set; }

        /// <summary>
        /// 微信支付，商户id
        /// 
        /// </summary>
        [MaxLength(50)]
        public string mch_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(200)]
        public string paternerKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(500)]
        public string subscribe_rul { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        [MaxLength(500)]
        public string domain { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public string AppSecret { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(10)]
        public string Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public string EncodingAESKey { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 商户代码
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 微信支付
        /// </summary>
        [MaxLength(20)]
        public string PayUrl { get; set; }

        /// <summary>
        /// 微信公众号名称
        /// </summary>
        [MaxLength(20)]
        public string AppName { get; set; }

        /// <summary>
        /// 微信退款路径
        /// </summary>
        [MaxLength(20)]
        public string BackPayUrl { get; set; }

        /// <summary>
        /// 地磁选择泊位状态
        /// 1代表 取泊位状态
        /// 其他代表 取地磁状态
        /// </summary>
        public int BerthStatus { get; set; }

        /// <summary>
        /// 车位详情
        /// </summary>
        public bool BerthDetail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string ZappId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(2000)]
        public string privateKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(2000)]
        public string alipayPulicKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(200)]
        public string serverUrl { get; set; }

        /// <summary>
        /// 包月
        /// </summary>
        public Int64 MonthlyPayment { get; set; }
        /// <summary>
        /// 储蓄卡
        /// </summary>
        public Int64 DepositCard { get; set; }
        /// <summary>
        /// 追缴
        /// </summary>
        public Int64 RecoverMoneny { get; set; }

        /// <summary>
        /// 在线停车
        /// </summary>
        public Int64 OnlineOrdering { get; set; }
    }
}
