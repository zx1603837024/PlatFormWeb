using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;
using P4.Employees;

namespace P4.Collectors.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Employee))]
    public class CollectorDto : EntityDto<long>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(16)]
        public string UserName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [MaxLength(20)]
        public string TrueName { get; set; } 
    }
}
