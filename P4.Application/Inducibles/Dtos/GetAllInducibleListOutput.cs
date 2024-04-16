using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inducibles.Dtos
{
    public class GetAllInducibleListOutput : IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<InducibleDto> rows { get; set; }
    }
}
