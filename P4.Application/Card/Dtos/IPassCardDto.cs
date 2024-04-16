using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace P4.Card.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(IPassCard))]
    public class IPassCardDto : EntityDto
    {
        /// <summary>
        /// 唯一标示（与abpbusinessdetail中guid对应）
        /// </summary>
        public Guid guid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Package { get; set; }

        /// <summary>
        /// 分公司id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 是否结算
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 商户代码
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 结算状态
        /// 0：未结算
        /// 1：结算成功
        /// 2：结算失败
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 结算结果
        /// </summary>
        public string ReturnResult { get; set; }

        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime? SettlementTime { get; set; }

        /// <summary>
        /// 消费金额
        /// </summary>
        public int Money { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime PayDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PayDateStr {
            get { return PayDate.ToString("yyyy-MM-dd HH:mm:ss"); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SettlementTimeStr
        {
            get { if (SettlementTime.HasValue) {
                    return SettlementTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                } else {
                    return "";
                }
            }
        }
    }
}
