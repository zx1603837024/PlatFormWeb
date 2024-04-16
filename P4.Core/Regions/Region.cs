using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using P4.Companys;
using P4.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Regions
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpRegions")]
    public class Region : FullAuditedEntity<int, User>, IMustHaveTenant, IMustHaveCompany
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int FatherId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public virtual string RegionName
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
         [MaxLength(100)]
        public virtual string Mark
        { get; set; }

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
        [MaxLength(10)]
        public virtual string WorkRuleTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(10)]
        public virtual string OffRuleTime { get; set; }
    }
}
