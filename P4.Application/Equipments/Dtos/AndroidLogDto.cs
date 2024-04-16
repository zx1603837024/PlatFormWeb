using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Equipments.Dtos
{

    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(AndroidLog))]
    public class AndroidLogDto : EntityDto
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid guid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(4000)]
        public string ExceptionMsg { get; set; }
    }
}
