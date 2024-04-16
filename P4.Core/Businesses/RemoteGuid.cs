using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Businesses
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpRemoteGuids")]
    public class RemoteGuid : CreationAuditedEntity, IPassivable
    {
        /// <summary>
        /// 主键guid
        /// </summary>
        public Guid BusinessDetailGuid { get; set; }

        /// <summary>
        /// 同步状态
        /// true已同步
        /// false未同步
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 泊位段id
        /// </summary>
        public int BerthsecId { get; set; }
    }
}
