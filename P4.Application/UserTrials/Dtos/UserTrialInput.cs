using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace P4.UserTrials.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(UserTrial))]
    public class UserTrialInput : IInputDto
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
    }
}
