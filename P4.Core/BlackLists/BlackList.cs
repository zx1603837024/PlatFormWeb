using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.BlackLists
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpBlackList")]
    public class BlackList : FullAuditedEntity<int>, IMustHaveTenant, IMustHaveCompany, IPassivable, IMustVersion
    {

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public virtual string VehicleType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public virtual string RelateNumber { get; set; }


        /// <summary>
        /// 告知手机号码
        /// </summary>
        [MaxLength(50)]
        public virtual string IdNumber { get; set; }


        [MaxLength(50)]
        public virtual string CarOwner { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(500)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int Version { get; set; }
    }
}
