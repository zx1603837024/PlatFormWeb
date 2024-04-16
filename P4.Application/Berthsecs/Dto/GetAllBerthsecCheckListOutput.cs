using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Berthsecs.Dto
{
    public class GetAllBerthsecCheckListOutput : IOutputDto
    {
        public List<BerthsecCheckDto> rows { get; set; }
    }
}
