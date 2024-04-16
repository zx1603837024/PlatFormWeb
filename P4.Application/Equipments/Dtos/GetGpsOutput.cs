using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Equipments.Dtos
{
    public class GetGpsOutput : IOutputDto
    {
        public List<GpsDto> Items;
    }
}
