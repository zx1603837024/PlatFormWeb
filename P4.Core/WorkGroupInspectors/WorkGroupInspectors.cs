using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using P4.Inspectors;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.WorkGroupInspectors
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpWorkGroupInspectors")]
    public class WorkGroupInspectors : FullAuditedEntity<int, Users.User>, IPassivable
    {
        /// <summary>
        /// 工作组Id
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
        public virtual long InspectorsId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("InspectorsId")]
        public virtual Inspector Inspector { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsActive { get; set; }
    }
}
