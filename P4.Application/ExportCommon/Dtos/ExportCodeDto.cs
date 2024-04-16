using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.ExportCommon.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(P4.ExportCommon.ExportCode))]
    public class ExportCodeDto : EntityDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }
    }
}
