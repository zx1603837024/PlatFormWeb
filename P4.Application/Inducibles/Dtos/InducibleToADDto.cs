using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;

namespace P4.Inducibles.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(InducibleToAD))]
    public class InducibleToADDto : EntityDto
    {
        public string AD { get; set; }

        public int InducibleId { get; set; }
    }
}
