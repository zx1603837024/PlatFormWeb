using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Collectors.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetWeixinOutput : IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public int AllRegisterCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int NewRegisterCount { get; set; }
    }
}
