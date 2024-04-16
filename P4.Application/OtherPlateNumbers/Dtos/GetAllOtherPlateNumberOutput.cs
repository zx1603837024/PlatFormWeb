using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.OtherPlateNumbers.Dtos
{
    public class GetAllOtherPlateNumberOutput : IOutputDto
    {
        public List<OtherPlateNumberDto> Items { get; set; }
    }
}
