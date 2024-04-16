using Abp.Application.Services;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Linq.Extensions;
using P4.Businesses;
using P4.MonthlyCars.Dtos;
using P4.Parks;
using P4.Companys;
using P4.SmsManagements;
using P4.SmsManagements.Dtos;
using System.Net.Http;
using System.Net.Http.Headers;
using P4.SubscribeManager;
using P4.Dictionarys;
using Abp.Domain.Uow;
using System.Web.Script.Serialization;
using Abp.Runtime.Caching;

namespace P4.MonthlyCars
{
    /// <summary>
    /// 
    /// </summary>
    public class MonthlyCarAppService : ApplicationService, IMonthlyCarAppService
    {
        #region Var

        HttpClient client = new HttpClient();

        private readonly IRepository<MonthlyCar> _monthlyCarAppService;
        private readonly IRepository<Park> _abpParkRepository;
        private readonly IRepository<MonthlyCarHistory, long> _monthlyCarHistoryAppService;
        private readonly IMonthlyCarRespository _MonthlyCarRespository;
        private readonly IRepository<MonthlyCarAbnormal, long> _monthlyCarAbnormalAppService;
        private readonly IBusinessAppService _businessAppService;
        private readonly IRepository<BusinessDetail, long> _businessDetailRepository;
        private readonly IRepository<OperatorsCompany> _abpOperatorsCompanyRepository;
        private readonly IRepository<DictionaryValue> _abpDictionaryValueRepository;
        private readonly ISmsManagementAppService _smsManagementAppService;
        private readonly ISmsModelAppService _smsModelAppService;
        private readonly ISubscribeAppService _subscribeAppService;
        private readonly IDictionarysAppService _dictionarysAppService;
        private readonly IMonthlyCarHistoryRespository _MonthlyCarHistoryRespository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ICacheManager _abpCacheManager;//缓存

        JavaScriptSerializer js = new JavaScriptSerializer();
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="monthlyCarAppService"></param>
        /// <param name="monthlyCarHistoryAppService"></param>
        /// <param name="monthlyCarAbnormalAppService"></param>
        /// <param name="businessAppService"></param>
        /// <param name="MonthlyCarRespository"></param>
        /// <param name="abpParkRepository"></param>
        /// <param name="businessDetailRepository"></param>
        /// <param name="abpOperatorsCompanyRepository"></param>
        /// <param name="smsManagementAppService"></param>
        /// <param name="smsModelAppService"></param>
        /// <param name="subscribeAppService"></param>
        /// <param name="abpDictionaryValueRepository"></param>
        /// <param name="dictionarysAppService"></param>
        /// <param name="MonthlyCarHistoryRespository"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="abpCacheManager"></param>
        public MonthlyCarAppService(IRepository<MonthlyCar> monthlyCarAppService,IRepository<MonthlyCarHistory, long> monthlyCarHistoryAppService,IRepository<MonthlyCarAbnormal, long> monthlyCarAbnormalAppService,IBusinessAppService businessAppService, IMonthlyCarRespository MonthlyCarRespository, IRepository<Park> abpParkRepository,IRepository<BusinessDetail, long> businessDetailRepository, IRepository<OperatorsCompany> abpOperatorsCompanyRepository, ISmsManagementAppService smsManagementAppService, ISmsModelAppService smsModelAppService, ISubscribeAppService subscribeAppService,IRepository<DictionaryValue> abpDictionaryValueRepository, IDictionarysAppService dictionarysAppService, IMonthlyCarHistoryRespository MonthlyCarHistoryRespository, IUnitOfWorkManager unitOfWorkManager, ICacheManager abpCacheManager)
        {

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            _abpCacheManager = abpCacheManager;
            _monthlyCarAppService = monthlyCarAppService;
            _monthlyCarHistoryAppService = monthlyCarHistoryAppService;
            _monthlyCarAbnormalAppService = monthlyCarAbnormalAppService;
            _businessAppService = businessAppService;
            _MonthlyCarRespository = MonthlyCarRespository;
            _abpParkRepository = abpParkRepository;
            _abpOperatorsCompanyRepository = abpOperatorsCompanyRepository;
            _businessDetailRepository = businessDetailRepository;
            _smsManagementAppService = smsManagementAppService;
            _smsModelAppService = smsModelAppService;
            _subscribeAppService = subscribeAppService;
            _abpDictionaryValueRepository = abpDictionaryValueRepository;
            _dictionarysAppService = dictionarysAppService;
            _MonthlyCarHistoryRespository = MonthlyCarHistoryRespository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> Insert(Dtos.CreateOrUpdateMonthlyCarInput input)
        {
            if (_MonthlyCarRespository.FirstOrDefault(e => e.PlateNumber == input.PlateNumber) != default(MonthlyCar))
            {
                return input.PlateNumber + "已经存在！";
            }
            MonthlyCar entity = new MonthlyCar();

            if (input.ParkName.Contains("0,"))
                entity.ParkIds = "0";
            else
                entity.ParkIds = input.ParkName;
            entity.CompanyId = int.Parse(input.CompanyName);
            entity.PlateNumber = input.PlateNumber;
            entity.VehicleOwner = input.VehicleOwner;
            entity.Telphone = input.Telphone;
            entity.MonthyType = input.MonthyType;
            entity.EndTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            entity.BeginTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            entity.Money = 0;
            var MonthlyCarId = _monthlyCarAppService.InsertAndGetId(entity);

            //if (MonthlyCarId > 0)
            //{
            //    string ParkName = "";
            //    string[] praklist = input.ParkName.Split(',');
            //    foreach (var parkid in praklist)
            //    {
            //        int parkId = 0;
            //        int.TryParse(parkid, out parkId);
            //        ParkName += _abpParkRepository.FirstOrDefault(p => p.Id == parkId) == null ? null : _abpParkRepository.FirstOrDefault(p => p.Id == parkId).ParkName + ",";
            //    }
            //    if (!string.IsNullOrEmpty(ParkName))
            //        ParkName = ParkName.Substring(0, ParkName.Length - 1);


            //    //消费金额 以及账户余额（短信发送）
            //    _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto()
            //    {
            //        Destnumbers = input.Telphone,
            //        Msg = string.Format(_smsModelAppService.GetSmsModelByType("ConsumptionMonthModel").SmsContext, input.PlateNumber, DateTime.Now.ToString("yyyy年MM月dd日hh时mm分"), ParkName, input.BeginTime.ToString("yyyy年MM月dd日"), input.EndTime.ToString("yyyy年MM月dd日"))
            //    });

            //    _subscribeAppService.SendMessage("MonthCarsManagement", "增加包月车辆数据，车牌号：" + input.PlateNumber + ",收费金额：" + input.Money, "");

            //    InsertMonthlyCarHistory(new MonthlyCarHistory() { BeginTime = input.BeginTime, EndTime = input.EndTime, Money = input.Money, MonthlyCarId = MonthlyCarId, Status = true, ParkIds = input.ParkName });
            //}
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        private void InsertMonthlyCarHistory(MonthlyCarHistory entity)
        {
            _monthlyCarHistoryAppService.Insert(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> Update(Dtos.CreateOrUpdateMonthlyCarInput input)
        {
            if (_MonthlyCarRespository.FirstOrDefault(e => e.PlateNumber == input.PlateNumber && e.Id != input.Id) != default(MonthlyCar))
            {
                return input.PlateNumber + "已经存在！";
            }


            MonthlyCar entity = _monthlyCarAppService.FirstOrDefault(input.Id);
            //if (entity.Money != input.Money ||
            //    entity.BeginTime != input.BeginTime ||
            //    entity.EndTime != input.EndTime)
            //{
            //    InsertMonthlyCarHistory(new MonthlyCarHistory() { BeginTime = input.BeginTime, EndTime = input.EndTime, Money = input.Money, MonthlyCarId = entity.Id, Status = false, ParkIds = input.ParkName });


            //    string ParkName = "";
            //    string[] praklist = input.ParkName.Split(',');
            //    foreach (var parkid in praklist)
            //    {
            //        int parkId = 0;
            //        int.TryParse(parkid, out parkId);
            //        ParkName += _abpParkRepository.FirstOrDefault(p => p.Id == parkId) == null ? null : _abpParkRepository.FirstOrDefault(p => p.Id == parkId).ParkName + ",";
            //    }
            //    if (!string.IsNullOrEmpty(ParkName))
            //        ParkName = ParkName.Substring(0, ParkName.Length - 1);

            //    _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto()
            //    {
            //        Destnumbers = input.Telphone,
            //        Msg = string.Format(_smsModelAppService.GetSmsModelByType("ConsumptionMonthRenewModel").SmsContext, input.PlateNumber, DateTime.Now.ToString("yyyy年MM月dd日hh时mm分"), ParkName, input.BeginTime.ToString("yyyy年MM月dd日"), input.EndTime.ToString("yyyy年MM月dd日"))
            //    });
            //}
            //entity.Money = input.Money + entity.Money;
            //entity.BeginTime = input.BeginTime;
            //entity.EndTime = input.EndTime;

            if (input.ParkName.Contains("0,"))
                entity.ParkIds = "0";
            else
                entity.ParkIds = input.ParkName;
            entity.PlateNumber = input.PlateNumber;
            entity.Telphone = input.Telphone;
            entity.VehicleOwner = input.VehicleOwner;
            entity.MonthyType = input.MonthyType;
             _monthlyCarAppService.Update(entity);
            _subscribeAppService.SendMessage("MonthCarsManagement", "修改包月车辆数据，车牌号：" + input.PlateNumber + ",收费金额：" + input.Money, "");
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(int Id)
        {
            var entity = _monthlyCarAppService.Load(Id);
            _subscribeAppService.SendMessage("MonthCarsManagement", "删除包月车辆数据，车牌号：" + entity.PlateNumber, "");
            _monthlyCarAppService.Delete(Id);
            _monthlyCarHistoryAppService.Delete(entry => entry.MonthlyCarId == Id);
        }

        /// <summary>
        /// 包月车辆列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllMonthlyCarsOutput GetMonthlyCarList(MonthlyCarInput input)
        {
            int records = _MonthlyCarRespository.GetAll().Filters(input).ToList().Count;
            if (records == 0)
            {
                return new GetAllMonthlyCarsOutput()
                {
                    rows = new List<MonthlyCarDto>(),
                    records = 0,
                    total = 0
                };
            }
            List<MonthlyCarDto> monthlycarDtoList = new List<MonthlyCarDto>();
            List<MonthlyCar> monthlycarlist = _MonthlyCarRespository.GetAll().Orders(input).Filters(input).PageBy(input).ToList();

            foreach (MonthlyCar c in monthlycarlist)
            {
                MonthlyCarDto monthDto = new MonthlyCarDto();
                monthDto.Money = c.Money;
                monthDto.Id = c.Id;
                monthDto.Telphone = c.Telphone;
                monthDto.PlateNumber = c.PlateNumber;
                monthDto.VehicleOwner = c.VehicleOwner;
                monthDto.BeginTime = c.BeginTime;
                monthDto.MonthyType = _abpDictionaryValueRepository.FirstOrDefault(entity => entity.ValueCode == c.MonthyType && entity.TypeCode ==P4Consts.StopType).ValueData;
                monthDto.EndTime = c.EndTime;
                monthDto.CompanyId = c.CompanyId;
                var company = _abpOperatorsCompanyRepository.FirstOrDefault(e => e.Id == c.CompanyId);
                if (company != default(OperatorsCompany))
                    monthDto.CompanyName = company.CompanyName;
                if (c.ParkIds != null)
                {
                    string[] praklist = c.ParkIds.Split(',');
                    foreach (var parkid in praklist)
                    {
                        int parkId = 0;
                        int.TryParse(parkid, out parkId);
                        if (parkId == 0)
                        {
                            monthDto.ParkName += "不限停车场,";
                            break;
                        }
                        else
                            monthDto.ParkName += _abpParkRepository.FirstOrDefault(p => p.Id == parkId) == null ? null : _abpParkRepository.FirstOrDefault(p => p.Id == parkId).ParkName + ",";
                    }
                    if (!string.IsNullOrEmpty(monthDto.ParkName))
                        monthDto.ParkName = monthDto.ParkName.Substring(0, monthDto.ParkName.Length - 1);
                }
                monthlycarDtoList.Add(monthDto);
            }
            return new GetAllMonthlyCarsOutput()
            {
                rows = monthlycarDtoList,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CarNumber"></param>
        /// <returns></returns>
        public string GetMonthyCarByCarNumber(string CarNumber)
        {
            MonthlyCarDto monthDto = new MonthlyCarDto();
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany, AbpDataFilters.MayHaveTenant);
            var model = _MonthlyCarRespository.FirstOrDefault(entity => entity.IsDeleted == false && entity.PlateNumber == CarNumber);
            if (model != default(MonthlyCar))
            {
                monthDto.Telphone = model.Telphone;
                monthDto.PlateNumber = model.PlateNumber;
                monthDto.VehicleOwner = model.VehicleOwner;
                monthDto.BeginTime = model.BeginTime;
                monthDto.MonthyType = _abpDictionaryValueRepository.FirstOrDefault(entity => entity.ValueCode == model.MonthyType && entity.TypeCode == "A10022").ValueData;
                monthDto.EndTime = model.EndTime;
                monthDto.CompanyId = model.CompanyId;
                var company = _abpOperatorsCompanyRepository.FirstOrDefault(e => e.Id == model.CompanyId);
                if (company != default(OperatorsCompany))
                    monthDto.CompanyName = company.CompanyName;
                if (model.ParkIds != null)
                {
                    string[] praklist = model.ParkIds.Split(',');
                    foreach (var parkid in praklist)
                    {
                        int parkId = 0;
                        int.TryParse(parkid, out parkId);
                        if (parkId == 0)
                        {
                            monthDto.ParkName += "不限停车场,";
                            break;
                        }
                        else
                            monthDto.ParkName += _abpParkRepository.FirstOrDefault(p => p.Id == parkId) == null ? null : _abpParkRepository.FirstOrDefault(p => p.Id == parkId).ParkName + ",";
                    }
                    if (!string.IsNullOrEmpty(monthDto.ParkName))
                        monthDto.ParkName = monthDto.ParkName.Substring(0, monthDto.ParkName.Length - 1);
                }
            }

            return js.Serialize(monthDto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.GetAllMonthlyCarHistoryOutput GetMonthlyCarHistoryList(Dtos.MonthlyCarHistoryInput input)
        {
            int records = _monthlyCarHistoryAppService.GetAll().Where(entity => entity.MonthlyCarId == input.id).Filters(input).Count();
            return new Dtos.GetAllMonthlyCarHistoryOutput()
            {
                rows = _monthlyCarHistoryAppService.GetAll().Where(entity => entity.MonthlyCarId == input.id).Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<Dtos.MonthlyCarHistoryDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllMonthlyCarHistoryOutput GetMonthlyCarHistoryAll(MonthlyCarHistoryInput input)
        {
            var query = _MonthlyCarHistoryRespository.GetAllList(entity => entity.CreationTime > input.operateDateBegin && entity.CreationTime < input.operateDateEnd);
            int records = query.Count;
            if (records == 0)
                return null;
            List<MonthlyCarHistoryDto> newlist = new List<MonthlyCarHistoryDto>();
            List<MonthlyCarHistoryDto> list = _MonthlyCarHistoryRespository.GetMonthlyCarHistoryAll(input);
            foreach (MonthlyCarHistoryDto c in list)
            {
                MonthlyCarHistoryDto monthDto = new MonthlyCarHistoryDto();
                monthDto.Money = c.Money;
                monthDto.Id = c.Id;
                monthDto.Telphone = c.Telphone;
                monthDto.PlateNumber = c.PlateNumber;
                monthDto.VehicleOwner = c.VehicleOwner;
                monthDto.BeginTime = c.BeginTime;
                monthDto.EndTime = c.EndTime;
                monthDto.CreationTime = c.CreationTime;
                if (c.MonthyType != null)
                {
                    monthDto.MonthyType = _abpDictionaryValueRepository.FirstOrDefault(entity => entity.ValueCode == c.MonthyType && entity.TypeCode == "A10022").ValueData;
                }
                
                if (c.ParkIds != null)
                {
                    string[] praklist = c.ParkIds.Split(',');
                    foreach (var parkid in praklist)
                    {
                        int parkId = 0;
                        int.TryParse(parkid, out parkId);
                        if (parkId == 0)
                        {
                            monthDto.ParkName += "不限停车场,";
                            break;
                        }
                        else
                            monthDto.ParkName += _abpParkRepository.FirstOrDefault(p => p.Id == parkId) == null ? null : _abpParkRepository.FirstOrDefault(p => p.Id == parkId).ParkName + ",";
                    }
                    if (!string.IsNullOrEmpty(monthDto.ParkName))
                        monthDto.ParkName = monthDto.ParkName.Substring(0, monthDto.ParkName.Length - 1);
                }
                newlist.Add(monthDto);
            }
            return new GetAllMonthlyCarHistoryOutput()
            {
                rows = newlist,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
            //return _MonthlyCarHistoryRespository.GetMonthlyCarHistoryAll(input);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.GetAllMonthlyCarAbnormalOutput GetMonthlyCarAbnormalList(Dtos.MonthlyCarAbnormalInput input)
        {
            int records = _monthlyCarAbnormalAppService.GetAll().Filters(input).Count();
            return new Dtos.GetAllMonthlyCarAbnormalOutput()
            {
                rows = _monthlyCarAbnormalAppService.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<Dtos.MonthlyCarAbnormalDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void Update(Dtos.CreateOrUpdateMonthlyCarAbnormalInput input)
        {
            var entity = _monthlyCarAbnormalAppService.Load(input.Id);
            if (entity.Status != input.Status)
            {
                if (entity.Status == true)//如果为异常数据，将收费表中记录删除
                {
                    _businessDetailRepository.Delete(entity.BusinessDetailId);
                }
                else//启用收费表中数据
                {
                    _businessAppService.RestoreDelete(entity.BusinessDetailId);
                }
            }
            entity.Status = input.Status;
            entity.Remark = input.Remark;
            _monthlyCarAbnormalAppService.Update(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(long Id)
        {
            var entity = _monthlyCarAbnormalAppService.Load(Id);
            if (entity.Status == true)
                _businessAppService.RestoreDelete(entity.BusinessDetailId);
            _monthlyCarAbnormalAppService.Delete(Id);
        }

        /// <summary>
        /// 收费员获取包月卡数据
        /// </summary>
        /// <returns></returns>
        public List<MonthlyCarDto> GetAllMonthlyCar()
        {
            List<MonthlyCarDto> list = new List<MonthlyCarDto>();
            var parkid = string.Join(",", AbpSession.ParkIds);
            string[] strs = parkid.Split(',');
            var entity = GetAllWhiteListValue().Where(e => e.BeginTime <= DateTime.Now && e.EndTime >= DateTime.Now && e.CompanyId == AbpSession.CompanyId.Value).ToList();
            foreach (var str in strs)
            {
                var index = "," + str + ",";
                for (int i = 0; i < entity.Count; i++)
                {
                    if (("," + entity[i].ParkIds + ",").Contains(index) || entity[i].ParkIds == "0")
                    {
                        list.Add(new MonthlyCarDto() { ParkIds = str, BeginTime = entity[i].BeginTime, EndTime = entity[i].EndTime, PlateNumber = entity[i].PlateNumber,  VehicleOwner = entity[i].VehicleOwner ,MonthyType =entity[i].MonthyType});
                    }
                }
            }
            return list;
            //return _MonthlyCarRespository.GetAllMonthlyCarBySql(parkid, AbpSession.TenantId.Value, AbpSession.CompanyId.Value);
        }

        /// <summary>
        /// 包月卡续费
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int MonthRenew(CreateOrUpdateMonthlyCarInput input)
        {
            var entity = _MonthlyCarRespository.Load(input.Id);
            entity.Money = entity.Money + input.Money;

            if (entity.BeginTime == input.BeginTime)
            {
                if (entity.EndTime > DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00")))
                {
                    entity.EndTime = input.EndTime;
                }
                else
                {
                    entity.BeginTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
                    entity.EndTime = input.EndTime;
                }
            }
            else {
                entity.BeginTime = input.BeginTime;
                entity.EndTime = input.EndTime;
            }
            entity.IsSms = 0;

            InsertMonthlyCarHistory(new MonthlyCarHistory() { BeginTime = entity.BeginTime, EndTime = entity.EndTime, Money = input.Money, MonthlyCarId = entity.Id, Status = false, ParkIds = entity.ParkIds });


            string ParkName = "";
            string[] praklist = entity.ParkIds.Split(',');
            foreach (var parkid in praklist)
            {
                int parkId = 0;
                int.TryParse(parkid, out parkId);
                ParkName += _abpParkRepository.FirstOrDefault(p => p.Id == parkId) == null ? null : _abpParkRepository.FirstOrDefault(p => p.Id == parkId).ParkName + ",";
            }
            if (!string.IsNullOrEmpty(ParkName))
                ParkName = ParkName.Substring(0, ParkName.Length - 1);

            var monthyvalue = _dictionarysAppService.GetDictionaryValue("A10022", entity.MonthyType);
            _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto()
            {
                Destnumbers = entity.Telphone,
                Msg = string.Format(_smsModelAppService.GetSmsModelByType("ConsumptionMonthRenewModel").SmsContext, entity.PlateNumber, DateTime.Now.ToString("yyyy年MM月dd日hh时mm分"), ParkName, monthyvalue, entity.BeginTime.ToString("yyyy年MM月dd日"), entity.EndTime.ToString("yyyy年MM月dd日"))
            });


            //entity.Money = input.Money + entity.Money;
            //entity.BeginTime = input.BeginTime;
            //entity.EndTime = input.EndTime;
            
            _MonthlyCarRespository.Update(entity);
            _subscribeAppService.SendMessage("MonthCarsManagement","  "+ entity.PlateNumber + " 已续费" + input.Monthy + "月，收费金额：" + entity.Money + ",生效日期：" + entity.BeginTime, "");
            return 1;
        }

        /// <summary>
        ///  获取包月卡数据（单条）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public MonthlyCarDto GetModel(int Id)
        {
            var model = _MonthlyCarRespository.Load(Id).MapTo<MonthlyCarDto>();
            model.MonthyType = _abpDictionaryValueRepository.FirstOrDefault(entity => entity.ValueCode == model.MonthyType && entity.TypeCode == "A10022").ValueData;
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public DecisionDto GetDecisionModel(DateTime BeginTime, DateTime EndTime)
        {
            DecisionDto entity = new DecisionDto();
            entity.AllMonthyCount = _monthlyCarAppService.Count();
            entity.AllMonthyMoney = _monthlyCarAppService.GetAll().Sum(entry => entry.Money).ToString();
            entity.NewMonthyCount = _monthlyCarHistoryAppService.Count(entry=> entry.CreationTime > BeginTime && entry.CreationTime < EndTime);
            entity.NewMonthyMoney = _monthlyCarHistoryAppService.GetAllList(entry => entry.CreationTime > BeginTime && entry.CreationTime < EndTime).Sum(entry => entry.Money).ToString();
            return entity;
        }

        /// <summary>
        /// 缓存包月车辆数据
        /// </summary>
        /// <returns></returns>
        public List<MonthlyCarDto> GetAllWhiteListValue()
        {         
            return _abpCacheManager.GetCache(string.Format(Abp.Configuration.SettingManager.CacheMonthyValue,AbpSession.CompanyId.Value)).Get(string.Format(Abp.Configuration.SettingManager.CacheMonthyValue, AbpSession.CompanyId.Value), () => GetFromDatabase()) as List<MonthlyCarDto>;
        }

        /// <summary>
        /// 缓存方法实现
        /// </summary>
        /// <returns></returns>
        public List<MonthlyCarDto> GetFromDatabase()
        {          
            return _monthlyCarAppService.GetAll().ToList().MapTo<List<MonthlyCarDto>>();
        }
    }
}
