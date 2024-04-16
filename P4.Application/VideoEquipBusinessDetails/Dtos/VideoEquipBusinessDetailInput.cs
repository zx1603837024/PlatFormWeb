using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace P4.VideoEquipBusinessDetails.Dtos
{
    public class VideoEquipBusinessDetailInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        public string filters { get; set; }

        public int page { get; set; }

        public int rows { get; set; }

    }
}
