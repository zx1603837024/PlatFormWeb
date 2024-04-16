using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Application.Editions;

namespace P4.Editions.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Edition))]
    public class EditionDto : EntityDto
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; set; }

        public virtual string DisplayName { get; set; }
    }
}
