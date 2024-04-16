using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Berthsecs.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Berthsec))]
    public class BerthsecMapDto : EntityDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Y { get; set; }
    }
}
