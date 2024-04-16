using Abp.Application.Services;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using P4.Companys.Dtos;
using Abp.Linq.Extensions;
using P4.EquipmentMaintain;

namespace P4.Companys
{
    /// <summary>
    /// 停车公司系统管理
    /// </summary>
    public class CompanyAppService : ApplicationService, ICompanyAppService
    {
        #region  Var
        private readonly IRepository<OperatorsCompany, int> _abpCompanyRepository;
        private readonly IRepository<CompanyCustomerExpress> _abpCompanyCustomerExpressRepository;
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpCompanyRepository"></param>
        /// <param name="abpCompanyCustomerExpressRepository"></param>
        public CompanyAppService(IRepository<OperatorsCompany, int> abpCompanyRepository, IRepository<CompanyCustomerExpress> abpCompanyCustomerExpressRepository)
        {
            _abpCompanyRepository = abpCompanyRepository;
            _abpCompanyCustomerExpressRepository = abpCompanyCustomerExpressRepository;
        }

        public GetAllCompanyOutput GetAll(OperatorCompanyInput input)
        {
            int records = _abpCompanyRepository.Count();
            return new GetAllCompanyOutput()
            {

                rows = _abpCompanyRepository.GetAll().OrderByDescending(dic => dic.Id).Filters(input).PageBy(input).ToList().MapTo<List<CompanyDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Abp.Application.Services.Dto.ListResultOutput<CompanyDto> GetAll()
        {
            return new Abp.Application.Services.Dto.ListResultOutput<CompanyDto>()
            {
                Items = _abpCompanyRepository.GetAllList().MapTo<List<CompanyDto>>().ToList()
            };
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Insert(CreateCompanyInput input)
        {
            if (input.CompanyName == null)
            {
                return "分公司不能为空！";
            }
            if (_abpCompanyRepository.FirstOrDefault(dic => dic.CompanyName == input.CompanyName) != default(OperatorsCompany))
            {
                return input.CompanyName + "已经存在！";
            }
            OperatorsCompany entity = new OperatorsCompany();
            entity.CompanyName = input.CompanyName;
            entity.TelePhone = input.TelePhone;
            entity.IsActive = input.IsActive;
            entity.Contacts = input.Contacts;
            entity.Address = input.Address;
            entity.Password1 = input.Password1;
            entity.Password2 = input.Password2;
            entity.Password3 = input.Password3;
            entity.Password4 = input.Password4;
            entity.Password5 = input.Password5;
            _abpCompanyRepository.Insert(entity);
            return null;

        }

        /// <summary>
        /// 更新公司信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Update(CreateCompanyInput input)
        {
            if (_abpCompanyRepository.FirstOrDefault(dic => dic.CompanyName == input.CompanyName&&dic.Id!=input.Id) != default(OperatorsCompany))
            {
                return input.CompanyName + "已经存在！";
            }
            OperatorsCompany entity = new OperatorsCompany();
            entity.Id = input.Id;
            entity.CompanyName = input.CompanyName;
            entity.TelePhone = input.TelePhone;
            entity.IsActive = input.IsActive;
            entity.Contacts = input.Contacts;
            entity.Address = input.Address;
            entity.Password1 = input.Password1;
            entity.Password2 = input.Password2;
            entity.Password3 = input.Password3;
            entity.Password4 = input.Password4;
            entity.Password5 = input.Password5;

            _abpCompanyRepository.Update(entity);
            return null;

          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void Delete(CreateCompanyInput input)
        {
            this._abpCompanyRepository.Delete(input.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public CompanyDto FirstOrDefault(int CompanyId)
        {
            return _abpCompanyRepository.FirstOrDefault(AbpSession.CompanyId ?? 0).MapTo<CompanyDto>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void SaveCompany(CompanyDto entity)
        {
            var company = _abpCompanyRepository.Load(entity.Id);
             company .CompanyName = entity.CompanyName;
             company.Id = AbpSession.CompanyId ?? 0;
             company.Contacts = entity.Contacts;
             company.TelePhone = entity.TelePhone;
             company.Address = entity.Address;
             company.X = entity.X;
             company.Y = entity.Y;
             _abpCompanyRepository.Update(company);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CompanyDto> GetAllCompanyName()
        {
            return _abpCompanyRepository.GetAllList().MapTo<List<CompanyDto>>();
        }

        /// <summary>
        /// 通过id查询分公司名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CompanyDto> GetAllCompanyName(int id)
        {
            var company = _abpCompanyCustomerExpressRepository.Load(id);
            return _abpCompanyRepository.GetAllList().Where(entity=>entity.Id==company.CompanyId).MapTo<List<CompanyDto>>();
        }
    }
}
