using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Parks.Dtos
{

    [AutoMapFrom(typeof(Park))]
    public class ParkInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        public string Id { get; set; }

        public int rows { get; set; }

        public int page { get; set; }

        public string filters { get; set; }

        public bool _search { get; set; }
    }
}
