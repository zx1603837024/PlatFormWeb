using Abp.Application.Services.Dto;
using Abp.Authorization.DataPermissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.DataPermissions.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllDataPermissionsOutput : PagedResultOutput<DataPermissionSetting>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<DataPermissionsDto> rows { get; set; }
    }
}
