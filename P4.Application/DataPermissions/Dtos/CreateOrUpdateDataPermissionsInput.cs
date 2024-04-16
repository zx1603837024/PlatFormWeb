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
    public class CreateOrUpdateDataPermissionsInput : EntityRequestInput, IOperDto
    {

        public string oper { get; set; }

        public int RoleId { get; set; }

        public long UserId { get; set; }
        public string CompanyIds { get; set; }
        public string RegionIds { get; set; }
        public string ParkIds { get; set; }
        public string BerthsecIds { get; set; }
        public int TenantId { get; set; }

        public DataPermission datapermission { get; set; }

        public Int64 LastModifierUserId { get; set; }
    }
}
