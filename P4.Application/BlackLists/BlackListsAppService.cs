using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Net.Mail;
using Abp.Runtime.Caching;
using P4.BlackLists.Dtos;
using P4.SubscribeManager;
using P4.WhiteLists;
using System.Collections.Generic;
using System.Linq;

namespace P4.BlackLists
{

    /// <summary>
    /// 黑名单管理
    /// </summary>
    public class BlackListsAppService : P4AppServiceBase, IBlackListsAppService
    {
        #region Var
        private readonly IRepository<BlackList> _abpBlackListRepository;
        private readonly ICacheManager _abpCacheManager;//缓存
        private readonly IBlackListRepository _blackListRepository;
        private readonly IEmailSender _emailSender;
        private readonly ISubscribeAppService _subscribeAppService;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="abpBlackListRepository"></param>
        /// <param name="blackListRepository"></param>
        /// <param name="subscribeAppService"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="abpCacheManager"></param>
        /// <param name="emailSender"></param>
        public BlackListsAppService(IRepository<BlackList> abpBlackListRepository, IBlackListRepository blackListRepository, ISubscribeAppService subscribeAppService, IUnitOfWorkManager unitOfWorkManager, ICacheManager abpCacheManager, IEmailSender emailSender)
        {
            _abpBlackListRepository = abpBlackListRepository;
            _blackListRepository = blackListRepository;
            _subscribeAppService = subscribeAppService;
            _unitOfWorkManager = unitOfWorkManager;
            _abpCacheManager = abpCacheManager;
            _emailSender = emailSender;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.GetAllBlackListsOutput GetAllBlackLists(Dtos.BlackListsInput input)
        {
            return _blackListRepository.GetBlackListAll(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string BlackListsInsert(Dtos.CreateOrUpdateBlackListsInput input)
        {
            if (string.IsNullOrWhiteSpace(input.RelateNumber))
            {
                return "车牌不能为空！";
            }
            BlackList entity = new BlackList();
            entity.CarOwner = input.CarOwner;
            entity.CompanyId = int.Parse(input.CompanyName);
            entity.VehicleType = input.VehicleType;
            entity.Remarks = input.Remarks;
            entity.IdNumber = input.IdNumber;
            entity.RelateNumber = input.RelateNumber;
            entity.IsActive = input.IsActive;
            _abpBlackListRepository.Insert(entity);
            _subscribeAppService.SendMessage("BlackManagement", "增加了黑名单数据，车牌号" + entity.RelateNumber, "");
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string BlackListsUpdate(Dtos.CreateOrUpdateBlackListsInput input)
        {
            var entity = _abpBlackListRepository.Load(input.Id);
            if (_blackListRepository.FirstOrDefault(dic => dic.RelateNumber == input.RelateNumber && dic.CompanyId == input.CompanyId && dic.Id != input.Id) != default(WhiteList))
            {
                return input.RelateNumber + "车牌已经存在";
            }
            entity.CarOwner = input.CarOwner;
            entity.VehicleType = input.VehicleType;
            entity.CompanyId = input.CompanyId;
            entity.Remarks = input.Remarks;
            entity.IdNumber = input.IdNumber;
            entity.RelateNumber = input.RelateNumber;
            entity.IsActive = input.IsActive;
            _abpBlackListRepository.Update(entity);
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void BlackListsDelete(Dtos.CreateOrUpdateBlackListsInput input)
        {
            _abpBlackListRepository.Delete(input.Id);
        }

        /// <summary>
        /// 收费员黑名单列表
        /// </summary>
        /// <returns></returns>
        public List<BlackListsDto> GetAllBlackList()
        {
            //return GetAllBlackListValue().Where(e => e.CompanyId == AbpSession.CompanyId.Value).ToList();
            var entity = _abpBlackListRepository.GetAllList(entry => entry.IsActive == true  && entry.CompanyId== AbpSession.CompanyId.Value);
            return entity.MapTo<List<BlackListsDto>>();
        }

        /// <summary>
        /// 检测黑名单是否存在
        /// </summary>
        /// <param name="PlateNumber"></param>
        /// <returns></returns>
        public bool CheckBlackExisis(string PlateNumber)
        {
            var model = _blackListRepository.FirstOrDefault(entity => entity.RelateNumber == PlateNumber);
            if (model != default(BlackList))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 缓存黑名单数据
        /// </summary>
        /// <returns></returns>
        public List<BlackListsDto> GetAllBlackListValue()
        {          
            return _abpCacheManager.GetCache(string.Format(Abp.Configuration.SettingManager.CacheBlackCarValue, AbpSession.CompanyId.Value)).Get(string.Format(Abp.Configuration.SettingManager.CacheBlackCarValue, AbpSession.CompanyId.Value), () => GetFromDatabase()) as List<BlackListsDto>;
        }

        /// <summary>
        /// 缓存方法实现
        /// </summary>
        /// <returns></returns>
        public List<BlackListsDto> GetFromDatabase()
        {
            return _abpBlackListRepository.GetAll().Where(entity => entity.IsActive == true).ToList().MapTo<List<BlackListsDto>>();
        }
    }
}
