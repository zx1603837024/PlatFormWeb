using Abp.Application.Services;
using Abp.Authorization.DataPermissions;
using Abp.EntityFramework;
using P4.DataPermissions.Dtos;
using P4.EntityFramework;
using P4.EntityFramework.Repositories;
using P4.MultiTenancy;
using P4.Users;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using P4.DataPermissions;
using Abp.Linq.Extensions;

namespace P4.EntityFramework.Repositories
{
    public class DataPermissinsRepository : P4RepositoryBase<DataPermissionSetting, long>, IDataPermissinsRepository
    {
        public DataPermissinsRepository(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }


        public GetAllDataPermissionsOutput GetUserAll(DataPermissionsInput input)
        {
            {
                int records = Table.Filters(input).Count();
            
                return new GetAllDataPermissionsOutput()
                {
                    rows = Table.Select(c => new DataPermissionsDto
                        {
                            Id = c.Id,
                            //RoleId = c.RoleId,
                            CompanyIds = c.CompanyIds,
                            RegionIds = c.RegionIds,
                            ParkIds = c.ParkIds,
                            //TenantId = c.TenantId,
                            BerthsecIds = c.BerthsecIds,
                            TenantId = c.TenantId,
                            TenantName = Context.Set<Tenant>().FirstOrDefault(t => t.Id == c.TenantId).Name,
                            LastModificationTime = c.LastModificationTime,
                            LastModifierUserId = c.LastModifierUserId,
                            LastModifierUserName = Context.Set<User>().FirstOrDefault(t => t.Id == c.LastModifierUserId).UserName,
                            CreationTime = c.CreationTime,
                            CreatorUserId = c.CreatorUserId,
                            CreatorUserName = Context.Set<User>().FirstOrDefault(t => t.Id == c.CreatorUserId).UserName
                        }).OrderByDescending(datapermissions => datapermissions.Id).PageBy(input).Filters(input).ToList(),
                    records = records,
                    total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
                };

            }
        }

    }
}
