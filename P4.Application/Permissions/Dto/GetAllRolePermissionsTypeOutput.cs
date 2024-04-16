using Abp.Application.Services.Dto;
using Abp.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Permissions.Dto
{
    public class GetAllRolePermissionsTypeOutput : PagedResultOutput<PermissionSetting>, IOutputDto
    {
        public List<PermissionDto> rows { get; set; }
    }
}
