using Abp.Application.Services;
using Abp.Application.Services.Dto;
using P4.Authorization;
using P4.Roles.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Roles
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRoleAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ListResultOutput<RoleDto> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllRoleOutput GetAllRoleList(RoleInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int RoleInsert(CreateOrUpdateRoleInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int RoleUpdate(CreateOrUpdateRoleInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int RoleDelete(CreateOrUpdateRoleInput input);

        Role GetRoleByName(String name);
        Role GetRoleById(int id);
         
    }
}
