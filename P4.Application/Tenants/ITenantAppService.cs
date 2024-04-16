using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P4.Tenants.Dto;
using Abp.Application.Services.Dto;

namespace P4.Tenants
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITenantAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllTenantOutput GetAll(TenantInput input);
         ListResultOutput<TenantDto> GetAll();
         List<TenantDto> GetAllTenantName();

        TenantDto FirstOrDefault(string DomainName);

        TenantDto FirstOrDefault(int telantId);

        void SaveTenant(TenantDto entity);

        void TenantInsert(CreateOrUpdateTenantInput input);

        void TenantUpdate(CreateOrUpdateTenantInput input);

        void TenantDelete(CreateOrUpdateTenantInput input);
    }
}
