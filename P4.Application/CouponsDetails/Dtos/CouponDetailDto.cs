using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Weixin;
using System;
using System.ComponentModel.DataAnnotations;

namespace P4.CouponsDetails.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(CouponDetail))]
    public class CouponDetailDto : EntityDto
    {
        /// <summary>
        /// 优惠券编号
        /// </summary>
        [MaxLength(20)]
        public string CouponNo { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [MaxLength(10)]
        public string Platenumber { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal OldMoney { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal NewMoney { get; set; }

        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal ConsumptionMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(15)]
        public string tel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public string openId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreationTimeStr { get { return CreationTime.ToString("yyyy-MM-dd HH:mm:ss"); } }

        /// <summary>
        /// 消费类型
        /// 1充值
        /// 2消费
        /// </summary>
        public int CouponType { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        public string Source { get; set; }
    }
}
