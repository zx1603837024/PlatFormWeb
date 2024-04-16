using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using P4.Employees;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.WorkGroups
{
    /// <summary>
    /// 收费员工作组表
    /// </summary>
    [Table("AbpWorkGroupEmployees")]
    public class WorkGroupEmployee : FullAuditedEntity<int, Users.User>, IPassivable
    {
        /// <summary>
        /// 工作组Id
        /// </summary>
        public virtual int WorkGroupId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("WorkGroupId")]
        public virtual WorkGroup WorkGroup { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual long EmployeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 早班(1)，中班(2)，晚班(3)状态
        /// </summary>
        public virtual int Status { get; set; }
    }
}
