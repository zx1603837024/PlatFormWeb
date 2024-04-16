using Abp.Authorization.DataPermissions;

using Abp.Domain.Repositories;
using P4.DataPermissions.Dtos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.DataPermissions
{
    public interface IDataPermissinsRepository : IRepository<DataPermissionSetting, long>
    {
      
        GetAllDataPermissionsOutput GetUserAll(DataPermissionsInput input);
       

    }
}
