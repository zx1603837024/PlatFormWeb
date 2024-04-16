using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Users.Dto
{
        [AutoMapFrom(typeof(User))]
    public  class UserInput: OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        //[Required]
        public string TenantId { get; set; }

        public int rows { get; set; }

        public int page { get; set; }


        public string filters { get; set; }


        public bool _search { get; set; }
    }
}
