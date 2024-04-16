using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.SpecialLists.Dtos
{
    public class SpecialListInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        public int rows { get; set; }

        public int page { get; set; }

        public string filters { get; set; }

        public bool _search { get; set; }
    }
}
