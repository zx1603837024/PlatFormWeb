using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Dictionarys.Dtos
{
    [AutoMapTo(typeof(DictionaryValue))]
    public class CreateOrUpdateDictionaryValueInput : EntityRequestInput, IOperDto, ICustomValidate 
    {

        public string oper { get; set; }

        [MaxLength(20)]
        public string TypeCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLengthAttribute(30)]
        public string TypeValue { get; set; }
        
        public string ValueCode { get; set; }
        public string ValueData { get; set; }

       
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public int Order { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLengthAttribute(200)]
        public string Remark { get; set; }

        public void AddValidationErrors(List<ValidationResult> results)
        {
            if (Order == 0 && oper != "del")
            {
                results.Add(new ValidationResult(string.Format("{0}-Order:非法字符!", this.GetType().Name)));
            }
            //throw new NotImplementedException();
        }
    }
}
