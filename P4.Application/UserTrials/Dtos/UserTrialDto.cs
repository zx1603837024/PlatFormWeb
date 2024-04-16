using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;

namespace P4.UserTrials.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(UserTrial))]
    public class UserTrialDto : EntityDto
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
