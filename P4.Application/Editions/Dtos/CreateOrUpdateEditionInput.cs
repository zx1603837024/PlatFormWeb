using Abp.Application.Editions;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Editions.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(Edition))]
    public class CreateOrUpdateEditionInput : EntityRequestInput, IOperDto
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string oper { get; set; }
    }
}
