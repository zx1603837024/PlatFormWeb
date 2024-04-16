using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Organizations.Dtos
{
     [AutoMapFrom(typeof(Organization))]
    public class OrganizationInput : EntityRequestInput, IOperDto, IPagedResultRequest, IFilters
    {

      
        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }

        public int page { get; set; }

        public int rows { get; set; }

        /// <summary>
        /// 过滤条件
        /// </summary>
        public string filters { get; set; }


        public bool _search { get; set; }
    }
}
