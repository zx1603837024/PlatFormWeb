using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.WhiteLists
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpWhiteList")]
    public class WhiteList : FullAuditedEntity<int>, IMustHaveTenant, IMustHaveCompany, IPassivable, IMustVersion
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
        /// 卡号
        /// </summary>
        [MaxLength(50)]
        public virtual string IdNumber { get; set; }


        /// <summary>
        /// 车主
        /// </summary>
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
        public int Version { get; set; }
    }
}
