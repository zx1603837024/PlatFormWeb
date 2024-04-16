using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.Dictionarys;
using P4.Parks.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using P4.Regions;
using P4.SubscribeManager;
using P4.ParkingLot.Dto;
using P4.ParkingLot.Dtos;
using P4.ParkingLot;

namespace P4.Parks
{
    /// <summary>
    /// 
    /// </summary>
    public class ParkAppService : P4AppServiceBase, IParkAppService
    {    
        #region Var
        private readonly IRepository<Park> _abpParkRepository;
        private readonly IRepository<Companys.OperatorsCompany> _abpCompanyRepository;
        private readonly IParkRepository _parkRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<Berths.Berth, long> _abpBerthRepository;
        private readonly IRegionAppService _regionAppService;
        private readonly ISubscribeAppService _subscribeAppService;
        private readonly IRepository<ParkChannel> _abpParkChannelRepository;
        private readonly IRepository<ParkingMonthlyRent> _abpParkingMonthlyRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpparkTypeRepository"></param>
        /// <param name="parkRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="abpBerthRepository"></param>
        /// <param name="regionAppService"></param>
        /// <param name="abpCompanyRepository"></param>
        /// <param name="subscribeAppService"></param>
        /// <param name="abpParkChannelRepository"></param>
        public ParkAppService(IRepository<Park> abpparkTypeRepository, 
                              IParkRepository parkRepository,
                              IUnitOfWorkManager unitOfWorkManager,
                              IRepository<Berths.Berth, long> abpBerthRepository,
                              IRegionAppService regionAppService, 
                              IRepository<Companys.OperatorsCompany> abpCompanyRepository, 
                              ISubscribeAppService subscribeAppService,
                              IRepository<ParkChannel> abpParkChannelRepository,
                              IRepository<ParkingMonthlyRent> abpParkingMonthlyRepository)
        {
            _abpParkRepository = abpparkTypeRepository;
            _parkRepository = parkRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _abpBerthRepository = abpBerthRepository;
            _regionAppService = regionAppService;
            _abpCompanyRepository = abpCompanyRepository;
            _subscribeAppService = subscribeAppService;
            _abpParkChannelRepository = abpParkChannelRepository;
            _abpParkingMonthlyRepository = abpParkingMonthlyRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllParkOutput GetParkAll(ParkInput input)
        {
            return _parkRepository.GetParkAll(input);
        }

        /// <summary>
        /// 获取所有的封闭停车场
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllParkOutput GetCurbPark(ParkInput input)
        {
            return _parkRepository.GetCurbPark(input);
        }


        /// <summary>
        /// 获取所有通道
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetParkChannelOutput GetAllChannel(GetParkChannelInput input)
        {
            return _parkRepository.GetAllChannel(input);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ParkInfoMap> GetParkInfoMapList()
        {

            var CompanyIds = AbpSession.CompanyId.HasValue == false ? AbpSession.CompanyIds ?? new List<int>() : new List<int> { AbpSession.CompanyId.Value };
            return _parkRepository.GetParkInfoMapList(AbpSession.TenantId, CompanyIds);
        }




        /// <summary>
        /// 添加停车场
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string InsertChannel(CreateOrUpdateParkChannelInput input)
        {
            if (input.ChannelName == null)
            {
                return "通道名称不能为空！";
            }
            if (input.ChannelCode == null)
            {
                return "通道编码不能为空！";
            }
            if (_abpParkChannelRepository.FirstOrDefault(dic => dic.ChannelCode == input.ChannelCode) != null)
            {
                return input.ChannelCode + "已经存在!";
            }
            var park = _abpParkRepository.FirstOrDefault(u => u.Id == input.ParkId);
            if (park == null)
            {
                return "停车场不存在！";
            }

            _abpParkChannelRepository.Insert(new ParkChannel
            {
                ChannelCode = input.ChannelCode,
                ChannelDirection = input.ChannelDirection,
                ChannelName = input.ChannelName,
                ZhiBoChannelId = input.ZhiBoChannelId,
                TenantId = input.TenantId,
                CompanyId = input.CompanyId,
                ParkId = input.ParkId,
                CreatorUserId = AbpSession.UserId,
                CreationTime = DateTime.Now
            });
            return null;
        }

        /// <summary>
        /// 删除通道
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteChannel(int Id)
        {
            var entity = _abpParkChannelRepository.FirstOrDefault(u => u.Id == Id);
            if (entity != null)
            {
                _abpParkChannelRepository.Delete(Id);
            }
        }

        /// <summary>
        /// 编辑通道
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string ModifyChannerl(CreateOrUpdateParkChannelInput input)
        {
            if (input.ChannelName == null)
            {
                return "通道名称不能为空！";
            }
            if (input.ChannelCode == null)
            {
                return "通道编码不能为空！";
            }
            if (_abpParkChannelRepository.FirstOrDefault(dic => dic.ChannelCode == input.ChannelCode && dic.Id!=input.Id) != null)
            {
                return input.ChannelCode + "已经存在!";
            }
            var park = _abpParkRepository.FirstOrDefault(u => u.Id == input.ParkId);
            if (park == null)
            {
                return "停车场不存在！";
            }
            var entity = _abpParkChannelRepository.FirstOrDefault(u => u.Id == input.Id);
            if (entity == null)
            {
                return "ID有误";
            }
            entity.ParkId = input.ParkId;
            entity.ZhiBoChannelId = input.ZhiBoChannelId;
            entity.ChannelCode = input.ChannelCode;
            entity.ChannelDirection = input.ChannelDirection;
            entity.ChannelName = input.ChannelName;
            _abpParkChannelRepository.Update(entity);
            return null;
        }


        /// <summary>
        /// 添加停车场
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Insert(CreateOrUpdateParkInput input)
        {
            if (_abpParkRepository.FirstOrDefault(dic => dic.ParkName == input.ParkName) != default(Park))
            {
                return input.ParkName + "已经存在!";
            }

            if (input.ParkName == null)
            {
                return "停车场不能为空！";
            }

            if (!string.IsNullOrWhiteSpace(input.BeginNumber) && !string.IsNullOrWhiteSpace(input.EndNumber) && int.Parse(input.BeginNumber) > int.Parse(input.EndNumber))
            {
                return "开始编号必须小于等于结束编号";
            }

            List<Berths.Berth> newberths = new List<Berths.Berth>();
            if (!string.IsNullOrWhiteSpace(input.BeginNumber) && !string.IsNullOrWhiteSpace(input.EndNumber))
            {
                var BeginNumber = int.Parse(input.BeginNumber);
                var EndNumber = int.Parse(input.EndNumber);
                for (int i = BeginNumber; i <= EndNumber; i++)
                {
                    newberths.Add(new Berths.Berth() { ParkId = input.Id, guid = Guid.NewGuid(), BerthNumber = i.ToString(), RegionId = input.RegionId, BerthStatus = "2", BerthsecId = -1 });
                }
            }

            if (!string.IsNullOrWhiteSpace(input.OtherNumber))
            {
                foreach (var v in input.OtherNumber.Split(','))
                {
                    if (!string.IsNullOrWhiteSpace(v) && !newberths.Exists(p => p.BerthNumber == v))
                    {
                        newberths.Add(new Berths.Berth() { ParkId = input.Id, guid = Guid.NewGuid(), BerthNumber = v, RegionId = input.RegionId, BerthStatus = "2", BerthsecId = -1 });
                    }
                }
            }

            input.BerthCount = newberths.Count;
            var companyInfo = _regionAppService.Load(input.RegionId);
            input.CompanyId = companyInfo.CompanyId;
            input.TenantId = companyInfo.TenantId;
            var parkinfo = input.MapTo<Park>();

            var company = _abpCompanyRepository.Load(input.CompanyId);
            parkinfo.X = company.X;
            parkinfo.Y = company.Y;
            var parkid = _parkRepository.InsertAndGetId(parkinfo);//获取停车场id
            StringBuilder str = new StringBuilder();
            foreach (var model in newberths)
            {
                str.AppendLine(" insert into AbpBerths(BerthNumber, BerthStatus, RegionId, ParkId, TenantId, CreatorUserId, CreationTime, BerthsecId, IsActive, CompanyId, guid, Prepaid, CarType) " +
                    "values('" + model.BerthNumber + "', '2', " + model.RegionId + ", " + parkid + ", " + companyInfo.TenantId + ", " + AbpSession.UserId + ", getdate(), -1, 1, " + input.CompanyId + ", '" + model.guid + "', 0, 0)");
            }
            //更新停车场权限
            str.AppendLine(" update AbpDataPermissions set ParkIds = ParkIds + '," + parkid + "' where IsDeleted = 0 and Discriminator = 'UserDataPermissionSetting' and UserId = " + AbpSession.UserId.Value);
            Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString, System.Data.CommandType.Text, str.ToString());

            _subscribeAppService.SendMessage("BaseBusinessManagement", "  添加了" + input.ParkName + "停车场,  泊位：" + input.BeginNumber + "——" + input.EndNumber, "");
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(int Id)
        {
            Park park=_parkRepository.Load(Id);
            _parkRepository.Delete(Id);
            _subscribeAppService.SendMessage("BaseBusinessManagement", "  删除了" + park.ParkName + "停车场,  泊位：" + park.BeginNumber + "——" + park.EndNumber, "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(isTransactional: false)]
        public string Modify(CreateOrUpdateParkInput input)
        {
            var entity = _parkRepository.Load(input.Id);

            if (!string.IsNullOrWhiteSpace(input.BeginNumber) && !string.IsNullOrWhiteSpace(input.EndNumber) && int.Parse(input.BeginNumber) > int.Parse(input.EndNumber))
            {
                return "开始编号必须小于等于结束编号";
            }

            if (_parkRepository.FirstOrDefault(dic => dic.ParkName == input.ParkName && dic.Id != input.Id) != default(Park))
            {
                return input.ParkName + "已经存在!";
            }

            List<Berths.Berth> berths = new List<Berths.Berth>();
            List<Berths.Berth> newberths = new List<Berths.Berth>();
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveBerthsec))
            {
                berths = _abpBerthRepository.GetAllList(p => p.ParkId == input.Id);
            }

            int count = 0;

            if (!string.IsNullOrWhiteSpace(input.BeginNumber) && !string.IsNullOrWhiteSpace(input.EndNumber))
            {
                var BeginNumber = int.Parse(input.BeginNumber);
                var EndNumber = int.Parse(input.EndNumber);
                for (int i = BeginNumber; i <= EndNumber; i++)
                {
                    newberths.Add(new Berths.Berth() { ParkId = input.Id, BerthNumber = i.ToString(), guid = Guid.NewGuid(), RegionId = input.RegionId, BerthStatus = "2", BerthsecId = -1 });
                }
            }

            if (!string.IsNullOrWhiteSpace(input.OtherNumber))
            {
                foreach (var v in input.OtherNumber.Split(','))
                {
                    if (!string.IsNullOrWhiteSpace(v) && !newberths.Exists(p => p.BerthNumber == v))
                    {
                        newberths.Add(new Berths.Berth() { ParkId = input.Id, BerthNumber = v, RegionId = input.RegionId, BerthStatus = "2", BerthsecId = -1 });
                    }
                }
            }

            StringBuilder str = new StringBuilder();

            foreach (var model in berths)
            {
                if (!newberths.Exists(p => p.BerthNumber == model.BerthNumber))
                {
                    str.AppendLine("delete from AbpBerths where id = " + model.Id);
                    //Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString, System.Data.CommandType.Text, sql);
                }
            }

            foreach (var model in newberths)
            {
                if (!berths.Exists(p => p.BerthNumber == model.BerthNumber))
                {
                    str.AppendLine("insert into AbpBerths(BerthNumber, BerthStatus, RegionId, ParkId, TenantId, CreatorUserId, CreationTime, BerthsecId, IsActive, CompanyId, guid, Prepaid, CarType) values('" + model.BerthNumber + "', '2', " + model.RegionId + ", " + model.ParkId + ", " + entity.TenantId + ", " + AbpSession.UserId + ", getdate(), -1, 1, " + entity.CompanyId + ", '" + model.guid + "',0.00,0)");
                }
            }
            if (!string.IsNullOrWhiteSpace(str.ToString()))
                Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString, System.Data.CommandType.Text, str.ToString());

            entity.ParkName = input.ParkName;
            entity.RegionId = input.RegionId;
            entity.BeginNumber = input.BeginNumber;
            entity.EndNumber = input.EndNumber;
            entity.ZhiBoParkId = input.ZhiBoParkId;
            if (input.OtherNumber != null)
            {
                entity.OtherNumber = input.OtherNumber;
            }
            entity.ParkType = input.ParkType;
            entity.BerthCount = newberths.Count;
            _parkRepository.Update(entity);
            _subscribeAppService.SendMessage("BaseBusinessManagement", "  修改了" + input.ParkName + "停车场数据,  泊位：" + input.BeginNumber + "——" + input.EndNumber, "");
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ParkDto Load(int Id)
        {
            return _parkRepository.FirstOrDefault(Id).MapTo<ParkDto>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ParkDto> GetParkAll()
        {
            return _parkRepository.GetAllList().MapTo<List<ParkDto>>();
        }

        /// <summary>
        /// 获取已绑定停车场平台的数据
        /// </summary>
        /// <returns></returns>
        public List<ParkDto> GetBindParkingParks()
        {
            return _abpParkRepository.GetAll().Where(u=>!string.IsNullOrEmpty(u.ZhiBoParkId)).MapTo<List<ParkDto>>();
        }

        /// <summary>
        /// 巡查员获得所有停车场
        /// </summary>
        /// <returns></returns>
        public List<ParkDto> InspectorGetParkAll()
        {
            using (_unitOfWorkManager.Current.EnableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany, AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark))
            {
                return _parkRepository.GetAllList().MapTo<List<ParkDto>>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public List<ParkDto> GetParkByRegionID(int regionId)
        {

            var a = AbpSession.ParkIds;
            List<ParkDto> chen = _parkRepository.GetAllList().Where(p => p.RegionId == regionId && AbpSession.ParkIds.Contains(p.Id)).MapTo<List<ParkDto>>();
            return _parkRepository.GetAllList().Where(p => p.RegionId == regionId && AbpSession.ParkIds.Contains(p.Id)).MapTo<List<ParkDto>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ParkAppDto> GetParkAppAll()
        {
            return _parkRepository.GetParkAppAll();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ParkDto> GetParkNumber()
        {
            int? tenantID = AbpSession.TenantId;
            return _parkRepository.GetParkNumber(tenantID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ParkDto> GetUseDataPermissions()
        {
            return _parkRepository.GetAllList(entity => AbpSession.ParkIds.Contains(entity.Id)).ToList().MapTo<List<ParkDto>>();
        }

        /// <summary>
        /// 获取停车场坐标
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ParkInfoMap GetParkInfoById(int Id)
        {
            var park = _parkRepository.FirstOrDefault(Id);

            if (string.IsNullOrWhiteSpace(park.X) || string.IsNullOrWhiteSpace(park.Y))
            {
                var company = _abpCompanyRepository.Load(park.CompanyId);
                park.X = company.X;
                park.Y = company.Y;
                park.Address = company.Address;
            }
            return new ParkInfoMap() { id = park.Id, X = park.X, Y = park.Y, ParkName = park.ParkName, ParkType = park.ParkType, Address = park.Address };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public int SaveParkAddress(ParkInfoMap map)
        {
            var model = _parkRepository.Load(map.id);
            model.X = map.X;
            model.Y = map.Y;
            model.Address = map.Address;
            _parkRepository.Update(model);
            return 1;
        }
    }
}
