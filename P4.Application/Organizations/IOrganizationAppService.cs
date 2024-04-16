using Abp.Application.Services;
using P4.Messages.Organizations.Dtos;
using P4.Organizations.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Organizations
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrganizationAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllOrganizationOutput GetAllOrganization(OrganizationInput input);

        /// <summary>
        /// 修改组织架构管理
        /// </summary>
        /// <param name="input"></param>
        void OrganizationUpdate(CreateOrUpdateOrganization input);

    }
}
