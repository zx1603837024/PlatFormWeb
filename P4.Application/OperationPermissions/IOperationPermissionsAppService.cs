using Abp.Application.Services;
using P4.OperationPermissions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.OperationPermissions
{
    public interface IOperationPermissionsAppService : IApplicationService
    {
        GetOperationPermissionsList GetAllPermissions();
    }
}
