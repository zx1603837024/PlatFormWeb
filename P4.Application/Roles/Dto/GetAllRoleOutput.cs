using Abp.Application.Services.Dto;
using P4.Authorization;
using P4.DataPermissions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Roles.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllRoleOutput : PagedResultOutput<Role>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<RoleDto> rows { get; set; }
    }
}
