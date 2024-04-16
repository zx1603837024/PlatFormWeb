using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.WorkGroupInspectors
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpWorkGroupsInspectors")]
    public class WorkGroupsInspectors : FullAuditedEntity<int, Users.User>, IMustHaveTenant, IMustHaveCompany, IPassivable
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public virtual string WorkGroupName { get; set; }

        /// <summary>
        /// 商户
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 分公司
        /// </summary>
        public virtual int CompanyId { get; set; }
    }
}
