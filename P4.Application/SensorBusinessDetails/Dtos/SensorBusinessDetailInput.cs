using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.SensorBusinessDetails.Dtos
{
    public class SensorBusinessDetailInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        public string filters { get; set; }

        public int page { get; set; }

        public int rows { get; set; }
    }
}
