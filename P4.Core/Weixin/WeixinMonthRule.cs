using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Weixin
{
    /// <summary>
    /// 包月车辆规则
    /// </summary>
    [Table("MonthRule")]
    public class MonthRule : Entity, IPassivable, IMustHaveTenant
    {
        /// <summary>
        /// 商户代码
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ParkId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public string ParkName { get; set; }

        /// <summary>
        /// 包月金额
        /// </summary>
        public decimal MonthMoney { get; set; }

        /// <summary>
        /// 包年金额
        /// </summary>
        public decimal YearMoney { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
    }
}
