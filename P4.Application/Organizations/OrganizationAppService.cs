using Abp.Application.Services;
using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using Abp.Linq.Extensions;
using P4.Organizations.Dtos;
using P4.Messages.Organizations.Dtos;


namespace P4.Organizations
{
    public class OrganizationAppService : ApplicationService, IOrganizationAppService
    {

        #region Var
        private readonly IRepository<Organization> _abpOrganizationRepository;
        #endregion


        public OrganizationAppService(IRepository<Organization> abpOrganizationRepository)
        {
            _abpOrganizationRepository = abpOrganizationRepository;
        }
        /// <summary>
        /// 查询组织架构
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllOrganizationOutput GetAllOrganization(OrganizationInput input)
        {
            int records = _abpOrganizationRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllOrganizationOutput()
            {

                rows = _abpOrganizationRepository.GetAll().OrderByDescending(dic => dic.Id).Filters(input).PageBy(input).ToList().MapTo<List<OrganizationDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
        /// <summary>
        /// 修改组织架构
        /// </summary>
        /// <param name="input"></param>
        public void OrganizationUpdate(CreateOrUpdateOrganization input)
        {
            var entity = _abpOrganizationRepository.Load(input.Id);

            if (entity.OrganizationName != input.OrganizationName || _abpOrganizationRepository.FirstOrDefault(dic => dic.OrganizationName == entity.OrganizationName) != null)
                return;

            entity.OrganizationName = input.OrganizationName;
            entity.FatherCode = input.FatherCode;
            entity.CompanyId = input.CompanyId;
            //entity.TypeValue = input.TypeValue;
            //entity.IsDefault = input.IsDefault;
            //entity.Remark = input.Remark;
            _abpOrganizationRepository.Update(entity);
        }
    }
}
