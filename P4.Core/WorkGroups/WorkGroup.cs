using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.WorkGroups
{
    [Table("AbpWorkGroups")]
    public class WorkGroup : FullAuditedEntity<int, Users.User>, IMustHaveTenant, IMustHaveCompany, IPassivable
    {
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

        /// <summary>
        /// 早班(1)，中班(2)，晚班(3)状态
        /// </summary>
        public virtual int Status { get; set; }
    }
}
