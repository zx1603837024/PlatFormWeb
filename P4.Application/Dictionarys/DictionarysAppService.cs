using Abp.Application.Services;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using P4.Dictionarys.Dtos;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Events.Bus.Entities;
using P4.Events;
using Abp.Linq.Extensions;
using Abp.Runtime.Caching;
using Abp.Net.Mail;
using Abp.SignalR.SignalrService;


namespace P4.Dictionarys
{
    /// <summary>
    /// 字典管理
    /// </summary>
    public class DictionarysAppService : ApplicationService, IDictionarysAppService
    {
        #region Var
        private readonly IRepository<DictionaryValue> _abpDictionaryValueRepository;
        private readonly IRepository<DictionaryType> _abpDictionaryTypeRepository;
        private readonly ICacheManager _abpCacheManager;//缓存
        private readonly IDictionaryRepository _dictionaryRepository;
        private readonly IEmailSender _emailSender;
        private readonly IP4ChatService _p4ChatService;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="abpDictionaryTypeRepository"></param>
        /// <param name="abpDictionaryValueRepository"></param>
        /// <param name="dictionaryRepository"></param>
        /// <param name="abpCacheManager"></param>
        /// <param name="emailSender"></param>
        /// <param name="p4ChatService"></param>
        /// <param name="unitOfWorkManager"></param>
        public DictionarysAppService(IRepository<DictionaryType> abpDictionaryTypeRepository, IRepository<DictionaryValue> abpDictionaryValueRepository, IDictionaryRepository dictionaryRepository, ICacheManager abpCacheManager, IEmailSender emailSender, IP4ChatService p4ChatService, IUnitOfWorkManager unitOfWorkManager)
        {
            _abpDictionaryTypeRepository = abpDictionaryTypeRepository;
            _abpDictionaryValueRepository = abpDictionaryValueRepository;
            _dictionaryRepository = dictionaryRepository;
            _abpCacheManager = abpCacheManager;
            _emailSender = emailSender;
            _p4ChatService = p4ChatService;
            _unitOfWorkManager = unitOfWorkManager;
            //EventBus.Default.Register<EntityChangedEventData<DictionaryValue>>(new DictionaryActivityWriter(abpCacheManager));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllDictionaryTypeOutput GetAllDictionaryTypeList(DictionaryTypeInput input)
        {
            
            int records = _abpDictionaryTypeRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllDictionaryTypeOutput()
            {
                rows = _abpDictionaryTypeRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<DictionaryTypeDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllDictionaryValueOutput GetAllDictionaryValueList(DictionaryValueInput input)
        {
            return _dictionaryRepository.GetDictionaryAll(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string DictionaryTypeInsert(CreateOrUpdateDictionaryTypeInput input)
        {
            if (_abpDictionaryTypeRepository.FirstOrDefault(dic => dic.TypeCode == input.TypeCode) !=default(DictionaryType))
            {
                //已存在typeCode
                return input.TypeCode +"已经存在！" ;
            }
            
            DictionaryType entity = new DictionaryType();
            entity.IsActive = input.IsActive;
            entity.TypeCode = input.TypeCode;
            entity.TypeValue = input.TypeValue;
            entity.Remark = input.Remark;
            _abpDictionaryTypeRepository.Insert(entity);
            return null;
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string DictionaryTypeUpdate(CreateOrUpdateDictionaryTypeInput input)
        {
            var entity = _abpDictionaryTypeRepository.Load(input.Id);
            if (_abpDictionaryTypeRepository.FirstOrDefault(dic => dic.TypeCode == entity.TypeCode && dic.Id != entity.Id) != default(DictionaryValue))
            {
                return input.TypeCode + "已经存在！";
            }
            if (entity.IsDefault == true)
            {
                return "默认字典类型不能修改！";
            }
          
            entity.TypeCode = input.TypeCode;
            entity.IsActive = input.IsActive;
            entity.TypeCode = input.TypeCode;
            entity.TypeValue = input.TypeValue;
            entity.IsDefault = input.IsDefault;
            entity.Remark = input.Remark;
            _abpDictionaryTypeRepository.Update(entity);
            return null;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string DictionaryTypeDelete(CreateOrUpdateDictionaryTypeInput input)
        {
            var entity = _abpDictionaryTypeRepository.Load(input.Id);
            if (entity.IsDefault == true)
                return entity.TypeCode + "字典数据未默认类型，不允许删除！";
            _abpDictionaryTypeRepository.Delete(input.Id);
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void DictionaryValueInsert(CreateOrUpdateDictionaryValueInput input)
        {
            //DictionaryValue entity = input.MapTo<DictionaryValue>();
            DictionaryValue entity = new DictionaryValue();
            entity.IsActive = input.IsActive;
            entity.TypeCode = input.TypeValue;
            entity.ValueCode = input.ValueCode;
            entity.sort = input.Order;
            entity.ValueData = input.ValueData;
            entity.Remark = input.Remark;
            _abpDictionaryValueRepository.Insert(entity);
        }

    

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void DictionaryValueUpdate(CreateOrUpdateDictionaryValueInput input)
        {
            var entity = _abpDictionaryValueRepository.Load(input.Id);
     
            entity.IsActive = input.IsActive;
            entity.TypeCode = input.TypeValue;
            entity.ValueCode = input.ValueCode;
            entity.sort = input.Order;
            entity.ValueData = input.ValueData;
            entity.Remark = input.Remark;
            //_abpDictionaryValueRepository.Update(entity);
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void DictionaryValueDelete(CreateOrUpdateDictionaryValueInput input)
        {
            var entity = _abpDictionaryValueRepository.Load(input.Id);
            if (!entity.IsDefault)
                _abpDictionaryValueRepository.Delete(input.Id);
        }


        /// <summary>
        /// 下拉菜单列表方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<DictionaryTypeDto> GetAllDictionaryType(DictionaryTypeInput input)
        {
            return _abpDictionaryTypeRepository.GetAll().OrderBy(dic=>dic.TypeCode).ToList().MapTo<List<DictionaryTypeDto>>();
        }
       
        /// <summary>
        /// 缓存字典数据
        /// </summary>
        /// <param name="TypeCode"></param>
        /// <returns></returns>
        public List<GetAllValueDataDto> GetAllValueData(string TypeCode)
        {
            return _abpCacheManager.GetCache(string.Format(Abp.Configuration.SettingManager.CacheDictionaryValue, AbpSession.TenantId ?? 0)).Get(TypeCode, () => GetFromDatabase(TypeCode)) as List<GetAllValueDataDto>;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeCode"></param>
        /// <returns></returns>
        public List<GetAllValueDataDto> GetFromDatabase(string TypeCode)
        {
            _unitOfWorkManager.Current.EnableFilter(AbpDataFilters.MayHaveTenant);
            return _abpDictionaryValueRepository.GetAll().OrderBy(dic => dic.sort).Where(dic => dic.TypeCode == TypeCode).ToList().MapTo<List<GetAllValueDataDto>>();
        }

        /// <summary>
        /// 数据日志  操作值  下拉菜单
        /// </summary>
        /// <returns></returns>
        public List<GetAllValueDataDto> GetDictionaryValue()
        {
            _unitOfWorkManager.Current.EnableFilter(AbpDataFilters.MayHaveTenant);
            return _abpDictionaryValueRepository.GetAll().OrderBy(dic => dic.sort).Where(dic => dic.TypeCode == P4Consts.OperationType).ToList().MapTo<List<GetAllValueDataDto>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeCode"></param>
        /// <param name="ValueCode"></param>
        /// <returns></returns>
        public string GetDictionaryValue(string TypeCode, string ValueCode)
        {
            var entity = _abpDictionaryValueRepository.FirstOrDefault(entry => entry.TypeCode == TypeCode && entry.ValueCode == ValueCode);
            if (entity == default(DictionaryValue))
                return null;
            else
                return entity.ValueData;
        }
    }
}
