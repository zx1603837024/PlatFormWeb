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
    public class UserPermissionDto: EntityDto<long>
    {
         public  long UserId { get; set; }
         public bool IsUserPermission
         { get; set; }
    }
}
