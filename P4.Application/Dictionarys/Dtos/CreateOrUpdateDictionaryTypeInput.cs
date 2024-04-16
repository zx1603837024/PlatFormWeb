using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Dictionarys.Dtos
{
    [AutoMapFrom(typeof(DictionaryType))]
    public class CreateOrUpdateDictionaryTypeInput : EntityRequestInput, IOperDto
    {

        public string oper { get; set; }

        [MaxLength(20)]
        public string TypeCode { get; set; }

        [MaxLengthAttribute(30)]
        public string TypeValue { get; set; }
        public bool IsActive { get; set; }


        public bool IsDefault { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLengthAttribute(200)]
        public string Remark { get; set; }
    }
}
