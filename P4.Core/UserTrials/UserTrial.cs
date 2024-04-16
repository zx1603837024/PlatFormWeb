using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.UserTrials
{
    /// <summary>
    /// 申请试用
    /// </summary>
    [Table("AbpUserTrials")]
    public class UserTrial : Entity, IHasCreationTime, IPassivable
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public virtual string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(10)]
        public virtual string TrueName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public virtual string CompanyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(18)]
        public virtual string Telephone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public virtual string Address { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
        /// <summary>
        /// 是否查看
        /// </summary>
        public virtual bool IsActive { get; set; }
    }
}
