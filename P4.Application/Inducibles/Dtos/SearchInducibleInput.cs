using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inducibles.Dtos
{
    public class SearchInducibleInput : EntityDto, IInputDto
    {
        /// <summary>
        /// 诱导类型
        /// </summary>
        public Int16? InducibleType { get; set; }

        public bool IsActive { get; set; }
    }
}
