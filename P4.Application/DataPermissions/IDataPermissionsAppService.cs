using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using P4.DataPermissions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.DataPermissions
{
    public interface IDataPermissionsAppService: IApplicationService
    {

        ListResultOutput<DataPermissionsDto> GetAll();
        GetAllDataPermissionsOutput GetAllDataPermissionsList(DataPermissionsInput input);
        void UserDataPermissionsInsert(CreateOrUpdateDataPermissionsInput input);

        void UserDataPermissionsUpdate(CreateOrUpdateDataPermissionsInput input);
        void RoleDataPermissionsInsert(CreateOrUpdateDataPermissionsInput input);

        void RoleDataPermissionsUpdate(CreateOrUpdateDataPermissionsInput input);

        void DataPermissionsDelete(CreateOrUpdateDataPermissionsInput input);
        List<UserDataPermissionSetting> GetAllUserDataPermissionByUserid(long userid);
        List<RoleDataPermissionSetting> GetAllRoleDataPermissionByRoleid(long roleid);
    }
}
