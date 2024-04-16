using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.Regions.Dtos;
using System.Collections.Generic;
using System.Linq;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using P4.Companys;
using P4.SubscribeManager;

namespace P4.Regions
{

    /// <summary>
    /// 区域管理
    /// </summary>
    public class RegionAppService : ApplicationService, IRegionAppService
    {
        #region Var
        private readonly IRepository<Region> _regionRepository;
        private readonly IRepository<OperatorsCompany> _operatorsCompanyRepository;
        private readonly ISubscribeAppService _subscribeAppService;
        #endregion

        /// <summary>
        /// 区域管理构造函数
        /// </summary>
        /// <param name="regionRepository"></param>
        /// <param name="operatorsCompanyRepository"></param>
        public RegionAppService(IRepository<Region> regionRepository, IRepository<OperatorsCompany> operatorsCompanyRepository, ISubscribeAppService subscribeAppService)
        {
            _regionRepository = regionRepository;
            _operatorsCompanyRepository = operatorsCompanyRepository;
            _subscribeAppService = subscribeAppService;
        }

        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllRegionsOutput GetAllRegionsList(GetAllRegionsInput input)
        {
            var records = _regionRepository.GetAll().Filters(input).Count();
            List<RegionDto> regions = new List<RegionDto>();
            RegionDto regionDto = null;
            foreach (var entity in _regionRepository.GetAll().Filters(input).Orders(input).PageBy(input).ToList().MapTo<List<RegionDto>>())
            {
                regionDto = entity;
                regionDto.CompanyName = _operatorsCompanyRepository.Load(entity.CompanyId).CompanyName;
                regions.Add(regionDto);
            }
            return new GetAllRegionsOutput()
            {
                rows = regions,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Insert(CreateOrUpdateDtoInput input)
        {
      
            if (_regionRepository.FirstOrDefault(dic => dic.RegionName == input.RegionName) != default(Region))
            {
                return input.RegionName + "已经存在!";
            }
            if(string.IsNullOrWhiteSpace(input.RegionName))
            {
                return "区域不能为空！";
            }

            if(string.IsNullOrWhiteSpace(input.CompanyName))
            {
                return "分公司不能为空！";
            }

            Region entity = new Region();
            entity.RegionName = input.RegionName;
            entity.Mark = input.Mark;
            entity.OffRuleTime = input.OffRuleTime;
            entity.WorkRuleTime = input.WorkRuleTime;
            entity.CompanyId = int.Parse(input.CompanyName);
            var id = _regionRepository.InsertAndGetId(entity);
            string sql = " update AbpDataPermissions set RegionIds = RegionIds + '," + id + "' where IsDeleted = 0 and Discriminator = 'UserDataPermissionSetting' and UserId = " + AbpSession.UserId.Value;
            Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString, System.Data.CommandType.Text, sql);
            _subscribeAppService.SendMessage("BaseBusinessManagement", "  新增了 " + input.RegionName + "区域", "");
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(int Id)
        {
            Region entity = _regionRepository.Load(Id);
            _regionRepository.Delete(Id);
            _subscribeAppService.SendMessage("BaseBusinessManagement", "  删除了 " + entity.RegionName + "区域", "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Modify(CreateOrUpdateDtoInput input)
        {
            var entity = _regionRepository.Load(input.Id);
            if (_regionRepository.FirstOrDefault(dic => dic.RegionName == input.RegionName && dic.Id!=input.Id) != default(Region))
            {
                return input.RegionName + "已经存在!";
            }
            if (input.RegionName == null)
            {
                return "区域不能为空！";
            }
           
            entity.FatherId = input.FatherId;
            entity.RegionName = input.RegionName;
            entity.OffRuleTime = input.OffRuleTime;
            entity.WorkRuleTime = input.WorkRuleTime;
            entity.Mark = input.Mark;
            _regionRepository.Update(entity);
            _subscribeAppService.SendMessage("BaseBusinessManagement", "  修改了 " + input.RegionName + "区域", "");
            return null;
        }

        /// <summary>
        /// 获取区域数据
        /// 可提供下拉菜单使用
        /// </summary>
        /// <returns></returns>
        public List<RegionDto> GetAllRegion()
        {
            return _regionRepository.GetAllList().MapTo<List<RegionDto>>();
          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public RegionDto Load(int Id)
        {
            return _regionRepository.FirstOrDefault(Id).MapTo<RegionDto>();
        }

        /// <summary>
        /// 获取数据权限转换Name
        /// </summary>
        /// <returns></returns>
        public List<RegionDto> GetUseDataPermissions()
        {
            return _regionRepository.GetAllList(entity => AbpSession.RegionIds.Contains(entity.Id)).ToList().MapTo<List<RegionDto>>();
        }
    }
}
