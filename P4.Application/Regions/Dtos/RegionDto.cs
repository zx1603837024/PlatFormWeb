using Abp.AutoMapper;
using Abp.Domain.Entities;
using P4.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Regions.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Region))]
    public class RegionDto : Entity<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public int FatherId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public string RegionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorUserName { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public string Mark { get; set; }
    }
}
