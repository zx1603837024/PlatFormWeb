using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Dictionarys.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(DictionaryType))]
    public class DictionaryTypeDto : EntityDto<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TypeValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }


    }
}
