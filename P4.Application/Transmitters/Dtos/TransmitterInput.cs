using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Transmitters.Dtos
{
     [AutoMapFrom(typeof(Transmitter))]
    public class TransmitterInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        //[Required]
        public string TenantId { get; set; }

        public int rows { get; set; }

        public int page { get; set; }


        public string filters { get; set; }


        public bool _search { get; set; }
    }
 }