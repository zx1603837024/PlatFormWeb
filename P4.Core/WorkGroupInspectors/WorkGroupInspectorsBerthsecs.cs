using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using P4.Berthsecs;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.WorkGroupInspectors
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpWorkGroupInspectorsBerthsecs")]
    public class WorkGroupInspectorsBerthsecs : FullAuditedEntity<int, Users.User>, IMustHaveTenant, IMustHaveCompany, IPassivable
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
        public virtual WorkGroupsInspectors WorkGroup { get; set; }

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
    }
}
