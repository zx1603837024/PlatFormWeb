using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using P4.Common;
using P4.ParkingLot.Dto;
using P4.Parks;
using P4.Regions;
using P4.SubscribeManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using P4.ParkingLot.Dtos;

namespace P4.ParkingLot
{
    /// <summary>
    /// 停车场服务类
    /// </summary>
    public class ParkingAppService:IParkingAppService
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
        public ParkingAppService(IRepository<Park> abpparkTypeRepository,
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
        /// 修改包月
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string ModifyMonthlyRent(ModifyMonthlyRentInput input)
        {
            var find = _abpParkingMonthlyRepository.FirstOrDefault(u => u.Id == input.Id);
            find.BeginTime = input.BeginTime;
            find.EndTime = input.EndTime;
            _abpParkingMonthlyRepository.Update(find);
            var res = WebApiHelper.PostWebApi(ConfigurationManager.AppSettings["PlatePictureSaveHost"] + "/api/InterfaceParking/ModifyMonthlyRent",
                                    new
                                    {
                                        openId = find.OpenId,
                                        plateNumber = find.PlateNumber,
                                        tenantId = find.TenantId,
                                        parkId = find.ParkId,
                                        beginDate = input.BeginTime.ToString("yyyy-MM-dd"),
                                        endDate = input.EndTime.ToString("yyyy-MM-dd"),
                                        carType = (int)input.carType
                                    }.ToJson());
            var obj = JObject.Parse(res);
            if ((int)obj["status"] == 0)
            {
                return obj["message"].ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 删除包月
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string DleteMonthlyRent(ModifyMonthlyRentInput input)
        {
            var find = _abpParkingMonthlyRepository.FirstOrDefault(u => u.Id == input.Id);
            var res = WebApiHelper.PostWebApi(ConfigurationManager.AppSettings["PlatePictureSaveHost"] + "/api/InterfaceParking/DelMonthlyRent",
                                new
                                {
                                    openId = find.OpenId,
                                    plateNumber = find.PlateNumber,
                                    parkId = find.ParkId,
                                }.ToJson());
            var obj = JObject.Parse(res);
            if ((int)obj["status"] == 0)
            {
                return obj["message"].ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 新增包月
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string CreateMonthlyRent(ModifyMonthlyRentInput input)
        {
            var park = _abpParkRepository.FirstOrDefault(u => u.Id == input.ParkId);
            var res = WebApiHelper.PostWebApi(ConfigurationManager.AppSettings["PlatePictureSaveHost"] + "/api/InterfaceParking/ModifyMonthlyRent",
                               new
                               {
                                   openId = input.OpenId,
                                   plateNumber = input.PlateNumber,
                                   tenantId = park.TenantId,
                                   parkId = park.Id,
                                   beginDate = input.BeginTime.ToString("yyyy-MM-dd"),
                                   endDate = input.EndTime.ToString("yyyy-MM-dd"),
                                   carType= (int)input.carType
                               }.ToJson());
            var obj = JObject.Parse(res);
            if ((int)obj["status"] == 0)
            {
                return obj["message"].ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取包月列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetParkingMonthlyRentListOutput GetParkingMonthlyRentList(GetParkingMonthlyRentListInput input)
        {
            var filter = _abpParkingMonthlyRepository.GetAll().Filters(input);
            var records = filter.Count();
            var dblist = filter.Orders(input).PageBy(input);
            return new GetParkingMonthlyRentListOutput()
            {
                rows = dblist.MapTo<List<AbpParkingMonthlyRentDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 获取停车场对应的通道列表
        /// </summary>
        /// <param name="parkId"></param>
        /// <returns></returns>
        public GetParkChannelOutput GetParkChannel(int parkId)
        {
            var channelList= _abpParkChannelRepository.GetAll().Where(u => u.ParkId == parkId);
            return new GetParkChannelOutput()
            {
                rows = channelList.MapTo<List<ParkChannelDto>>(),
            };
        }

        /// <summary>
        /// 编辑通道
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string ModifyParkChannel(ModifyParkChannelInput input)
        {
            if(input.Channels==null || input.Channels.Count == 0)
            {
                return "";
            }
            var channelList = _abpParkChannelRepository.GetAll().Where(u => u.ParkId == input.parkId).ToList();

            foreach(var channel in input.Channels)
            {
                ParkChannel find = null;
                if (channel.Id > 0)
                {
                    find = channelList.FirstOrDefault(u => u.Id == channel.Id);
                }
                if (find == null)
                {
                    _abpParkChannelRepository.Insert(new ParkChannel
                    {
                        ChannelCode = channel.ChannelCode,
                        ParkId = input.parkId,
                        ChannelDirection = channel.ChannelDirection.ToString(),
                        ChannelName = channel.ChannelName,
                        ZhiBoChannelId = channel.ZhiBoChannelId
                    });
                }
                else
                {
                    channelList.Remove(find);
                    find.ChannelCode = channel.ChannelCode;
                    find.ChannelDirection = channel.ChannelDirection.ToString();
                    find.ChannelName = channel.ChannelName;
                    find.ZhiBoChannelId = channel.ZhiBoChannelId;
                    _abpParkChannelRepository.Update(find);
                }
            }
            if (channelList.Any())
            {
                channelList.ForEach(e =>
                {
                    _abpParkChannelRepository.Delete(e);
                });
            }
            return "";
        }

    }
}
