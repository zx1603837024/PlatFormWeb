using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;

namespace P4.OtherPlateNumbers.Dtos
{
    [AutoMapFrom(typeof(OtherPlateNumbers.OtherPlateNumber))]
    public class OtherPlateNumberDto : EntityDto<long>
    {
        public string PlateNumber { get; set; }

        public bool IsActive { get; set; }
    }
}
