using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using P4.Berthsecs;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.WorkGroups
{
    /// <summary>
    /// 泊位段工作组表
    /// </summary>
    [Table("AbpWorkGroupBerthsecs")]
    public class WorkGroupBerthsec : FullAuditedEntity<int, Users.User>, IMustHaveTenant, IMustHaveCompany, IPassivable
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int BerthsecId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("BerthsecId")]
        public virtual Berthsec Berthsec { get; set; }

        /// <summary>
        /// 
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
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int CompanyId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 早班(1)，中班(2)，晚班(3)状态
        /// </summary>
        public virtual int Status { get; set; }
    }
}
