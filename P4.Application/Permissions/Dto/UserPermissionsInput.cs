using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Permissions.Dto
{
    [AutoMapFrom(typeof(UserPermissionSetting))]
    public class UserPermissionsInput : IInputDto, IPagedResultRequest, IFilters
    {
        //[Required]
        public string TenantId { get; set; }

        public int rows { get; set; }

        public int page { get; set; }

        public long userid { get; set; }
        public string filters { get; set; }


        public bool _search { get; set; }
    }
}
