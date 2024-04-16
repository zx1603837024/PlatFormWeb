using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Card
{
    /// <summary>
    /// 一卡通上传结算记录
    /// </summary>
    [Table("AbpIPassCards")]
    public class IPassCard : FullAuditedEntity, IMustHaveTenant, IMustHaveCompany, IPassivable
    {
        /// <summary>
        /// 唯一标示（与abpbusinessdetail中guid对应）
        /// </summary>
        public Guid guid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(1000)]
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
        /// 
        /// </summary>
        public int Money { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public DateTime PayDate { get; set; }

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
        [MaxLength(200)]
        public string ReturnResult { get; set; }

        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime? SettlementTime { get; set; }


        /// <summary>
        /// 组包文件名
        /// </summary>
        [MaxLength(50)]
        public string PackageName { get; set; }

        /// <summary>
        /// 行号
        /// </summary>
        public int? LineNumber { get; set; }
    }
}
