using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using P4.Permissions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultOutput<PermissionDto> GetAll();

        GetAllRolePermissionsTypeOutput GetAllRolePermissionsList(RolePermissionsInput input);
        void RolePermissionsInsert(CreateOrUpdateRolePermissionsInput input);

        void RolePermissionsUpdate(CreateOrUpdateRolePermissionsInput input);

        void RolePermissionsDelete(CreateOrUpdateRolePermissionsInput input);

        List<UserPermissionSetting> GetAllUserPermissionByUserid(long userid);
        List<RolePermissionSetting> GetAllRolePermissionByRoleid(long roleid);

    }
}
