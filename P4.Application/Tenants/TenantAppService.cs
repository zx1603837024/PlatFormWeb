using Abp.Domain.Repositories;
using P4.MultiTenancy;
using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using P4.Tenants.Dto;
using Abp.Application.Services;
using Abp.Linq.Extensions;
using P4.Dictionarys;
using Abp.Configuration;

namespace P4.Tenants
{
    /// <summary>
    /// 商户管理（企业信息管理）
    /// </summary>
    public class TenantAppService : ApplicationService, ITenantAppService
    {
        #region Var
        private readonly IRepository<Tenant> _abpTenantRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly IRepository<SettingTemplate> _abpSettingTemplateRepository;
        private readonly ISettingStore _settingManager;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpTenantRepository"></param>
        /// <param name="tenantRepository"></param>
        /// <param name="abpSettingTemplateRepository"></param>
        /// <param name="settingManager"></param>
        public TenantAppService(IRepository<Tenant> abpTenantRepository, ITenantRepository tenantRepository, IRepository<SettingTemplate> abpSettingTemplateRepository, ISettingStore settingManager)
        {
            _abpTenantRepository = abpTenantRepository;
            _tenantRepository = tenantRepository;
            _abpSettingTemplateRepository = abpSettingTemplateRepository;
            _settingManager = settingManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  GetAllTenantOutput GetAll(TenantInput input)
        {
            int records = _abpTenantRepository.Count();
            return new GetAllTenantOutput()
            {
                rows = _abpTenantRepository.GetAll().Orders(input).PageBy(input).ToList().MapTo<List<TenantDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 读取单条记录
        /// </summary>
        /// <param name="DomainName"></param>
        /// <returns></returns>
        public TenantDto FirstOrDefault(string DomainName)
        {
            return _abpTenantRepository.FirstOrDefault(tenant => tenant.DomainName == DomainName).MapTo<TenantDto>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="telantId"></param>
        /// <returns></returns>
        public TenantDto FirstOrDefault(int telantId)
        {
            return _abpTenantRepository.FirstOrDefault(telantId).MapTo<TenantDto>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void SaveTenant(TenantDto entity)
        {
            var tenant = _abpTenantRepository.Load(entity.Id);
            tenant.DomainName = entity.DomainName;
            tenant.Id = AbpSession.TenantId ?? 0;
            tenant.HQ = entity.HQ;
            tenant.Address = entity.Address;
            tenant.Contacts = entity.Contacts;
            tenant.Telephone = entity.Telephone;
            tenant.Password = entity.Password;
            tenant.X = entity.X;
            tenant.Y = entity.Y;
            _abpTenantRepository.Update(tenant);
        }

        public void TenantInsert(CreateOrUpdateTenantInput input)
        {
            Tenant entity = new Tenant(input.TenancyName, input.Name);

            if (!string.IsNullOrEmpty(input.EditionName) && input.EditionName != "0")
            {
                entity.EditionId = int.Parse(input.EditionName);
            }

            entity.HQ = input.HQ;
            entity.DomainName = input.DomainName;
            entity.Contacts = input.Contacts;
            entity.Address = input.Address;
            entity.IsActive = input.IsActive;
            entity.Password = "123456";//商户默认密码
            var id = _abpTenantRepository.InsertAndGetId(entity);
            _tenantRepository.InsertDictionaryTemplate(id);
            //插入个性化设置
            List<SettingTemplate> settingList = new List<SettingTemplate>();
            settingList = _abpSettingTemplateRepository.GetAll().Where(entry => entry.TenantId == 1 && entry.UserId == null).ToList();
            foreach (var entry in settingList)
            {
                _settingManager.CreateAsync(new SettingInfo(id, null, entry.Name, entry.Value) { Mark = entry.Mark });
            }
        }

        /// <summary>
        /// 更新商户信息
        /// </summary>
        /// <param name="input"></param>
        public void TenantUpdate(CreateOrUpdateTenantInput input)
        {
            Tenant entity = _abpTenantRepository.Load(input.Id);
            if (!string.IsNullOrEmpty(input.EditionName) && input.EditionName != "0")
            {
                entity.EditionId = int.Parse(input.EditionName);
            }
            entity.HQ = input.HQ;
            entity.DomainName = input.DomainName;
            entity.Contacts = input.Contacts;
            entity.Address = input.Address;
            entity.IsActive = input.IsActive;
            _abpTenantRepository.Update(entity);
        }

        public void TenantDelete(CreateOrUpdateTenantInput input)
        {
            _abpTenantRepository.Delete(input.Id);
        }
        
        
        public Abp.Application.Services.Dto.ListResultOutput<TenantDto> GetAll()
        {
            return new Abp.Application.Services.Dto.ListResultOutput<TenantDto>()
            {
                Items = _abpTenantRepository.GetAllList().MapTo<List<TenantDto>>().ToList()
            };
        }
      

        public List<TenantDto> GetAllTenantName()
        {
            return _abpTenantRepository.GetAllList().MapTo<List<TenantDto>>();
        }
    }
}
