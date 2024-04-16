using System;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.SchedulingSystem
{
    /// <summary>
    /// 收费员请假管理
    /// </summary>
    [Table("AbpSchedulingSystemLeaveAdministrations")]
    public class SchedulingSystemLeaveAdministration : Entity, IMustHaveTenant, IMustHaveCompany, IPassivable
    { 
        /// <summary>
        /// 
        /// </summary>
        public virtual int EmployeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string TrueName { get; set; }
        /// <summary>
        /// 请假开始时间
        /// </summary>
        [MaxLength(30)]
        public virtual string BeginTime { get; set; }

        /// <summary>
        /// 请假结束时间
        /// </summary>
        [MaxLength(30)]
        public virtual string EndTime { get; set; }

        /// <summary>
        /// 请假原因备注
        /// </summary>
        [MaxLength(100)]
        public virtual string LeaveRemark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int CompanyId { get; set; }

        /// <summary>
        /// 审批字段
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int TenantId { get; set; }
    }
}
