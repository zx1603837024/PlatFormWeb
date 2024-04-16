using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Tenants.Dto
{
      [AutoMapFrom(typeof(Tenant))]
    public class TenantInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
   {
         public int rows { get; set; }

        public int page { get; set; }



        public string filters { get; set; }
   }
}
