using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.SchedulingSystem
{
    /// <summary>
    /// 节假日管理
    /// </summary>
    [Table("AbpSchedulingSystemHolidaysManagers")]
    public class SchedulingSystemHolidaysManager : Entity, IMustHaveTenant, IPassivable
    {
        /// <summary>
        /// 假期名称
        /// </summary>
        [MaxLength(50)]
        public virtual string HolidaysName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [MaxLength(20)]
        public virtual string BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [MaxLength(20)]
        public virtual string EndTime { get; set; }

        /// <summary>
        /// 商户代码
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsActive { get; set; }
    }
}

