using Abp.Application.Services;
using Abp.Domain.Repositories;
using System.Collections.Generic;
using Abp.AutoMapper;
using P4.WhiteLists.Dtos;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using System.Linq;

namespace P4.WhiteLists
{
    /// <summary>
    /// 
    /// </summary>
    public class WhiteListsAppService : ApplicationService, IWhiteListsAppService
    {
        #region Var
        private readonly IRepository<WhiteList> _abpWhiteListRepository;
        private readonly IWhiteListsRepository _whiteListRepository;
        private readonly ICacheManager _abpCacheManager;//缓存
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="abpWhiteListRepository"></param>
        /// <param name="whiteListRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="abpCacheManager"></param>
        public WhiteListsAppService(IRepository<WhiteList> abpWhiteListRepository, IWhiteListsRepository whiteListRepository, IUnitOfWorkManager unitOfWorkManager, ICacheManager abpCacheManager)
        {
            _abpWhiteListRepository = abpWhiteListRepository;
            _whiteListRepository = whiteListRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _abpCacheManager = abpCacheManager;
        }

        /// <summary>
        /// 获取白名单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.GetAllWhiteListsOutput GetAllWhiteLists(Dtos.WhiteListsInput input)
        {

            return _whiteListRepository.GetWhiteListAll(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string WhiteListsInsert(Dtos.CreateOrUpdateWhiteListsInput input)
        {
            WhiteList entity = new WhiteList();
            if (input.RelateNumber == null)
            {
                return "车牌不能为空！";
            }
            entity.VehicleType = input.VehicleType;
            entity.RelateNumber = input.RelateNumber;
            entity.IsActive = input.IsActive;
            entity.CompanyId = input.CompanyId;
            entity.Remarks = input.Remarks;
            _abpWhiteListRepository.Insert(entity);
            return null;

          
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string WhiteListsUpdate(Dtos.CreateOrUpdateWhiteListsInput input)
        {
            var entity = _abpWhiteListRepository.Load(input.Id);
            if (_abpWhiteListRepository.FirstOrDefault(dic => dic.RelateNumber == input.RelateNumber && dic.CompanyId == input.CompanyId && dic.Id != input.Id) != default(WhiteList))
            {
                return input.RelateNumber + "车牌已经存在";
            }
            entity.VehicleType = input.VehicleType;
            entity.RelateNumber = input.RelateNumber;
            entity.CompanyId = input.CompanyId;
            entity.IsActive = input.IsActive;
            entity.Remarks = input.Remarks;
            _abpWhiteListRepository.Update(entity);
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void WhiteListsDelete(Dtos.CreateOrUpdateWhiteListsInput input)
        {
            var entity = _abpWhiteListRepository.Load(input.Id);
            _abpWhiteListRepository.Delete(input.Id);
            
        }

        /// <summary>
        /// 收费员获取白名单列表
        /// </summary>
        /// <returns></returns>
        public List<WhiteListsDto> GetAllWhiteCar()
        {
            //var entity = _abpWhiteListRepository.GetAllList(entry => entry.IsActive == true);
            return GetAllWhiteListValue().Where(e => e.CompanyId == AbpSession.CompanyId.Value).ToList();
        }

        /// <summary>
        /// 缓存白名单数据
        /// </summary>
        /// <returns></returns>
        public List<WhiteListsDto> GetAllWhiteListValue()
        {      
            return _abpCacheManager.GetCache(string.Format(Abp.Configuration.SettingManager.CacheWhiteCarValue, AbpSession.CompanyId.Value)).Get(string.Format(Abp.Configuration.SettingManager.CacheWhiteCarValue, AbpSession.CompanyId.Value), () => GetFromDatabase()) as List<WhiteListsDto>;
        }

        /// <summary>
        /// 缓存方法实现
        /// </summary>
        /// <returns></returns>
        public List<WhiteListsDto> GetFromDatabase()
        {
            var a = _abpWhiteListRepository.GetAll().Where(entity => entity.IsActive == true).ToList().MapTo<List<WhiteListsDto>>();
            return _abpWhiteListRepository.GetAll().Where(entity => entity.IsActive == true).ToList().MapTo<List<WhiteListsDto>>();
        }
    }
}
