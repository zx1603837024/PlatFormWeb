using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Roles.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Role))]
    public class RoleInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        /// <summary>
        /// 
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int rows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string filters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool _search { get; set; }
    }
}
