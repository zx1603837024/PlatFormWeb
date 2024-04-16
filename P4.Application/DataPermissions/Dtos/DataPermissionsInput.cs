using Abp.Application.Services.Dto;
using Abp.Authorization.DataPermissions;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.DataPermissions.Dtos
{


    [AutoMapFrom(typeof(DataPermissionSetting))]

    public class DataPermissionsInput : IInputDto, IPagedResultRequest, IFilters
    {
        //[Required]
        public string TenantId { get; set; }

        public int rows { get; set; }

        public int page { get; set; }


        public string filters { get; set; }
    }
}
