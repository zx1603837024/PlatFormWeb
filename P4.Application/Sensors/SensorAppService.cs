using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Helper;
using Abp.Linq.Extensions;
using Abp.Runtime.Caching;
using Abp.SignalR.SignalrService;
using Newtonsoft.Json;
using P4.Berths;
using P4.Berthsecs;
using P4.BlackLists;
using P4.Businesses;
using P4.Employees;
using P4.MonthlyCars;
using P4.MonthlyCars.Dtos;
using P4.OtherAccounts.Dtos;
using P4.Rates;
using P4.Sensors.Dtos;
using P4.SmsManagements;
using P4.SmsManagements.Dtos;
using P4.SubscribeManager;
using P4.Weixin;
using P4.WhiteLists.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace P4.Sensors
{
    /// <summary>
    /// 车检器管理类
    /// </summary>
    [Audited]
    public class SensorAppService : P4AppServiceBase, ISensorAppService
    {
        #region Var
        private readonly IRepository<Sensor> _sensorRepository;//车检器设备
        private readonly ISensorsRepository _abpsensorRepository;
        private readonly IRepository<SensorCaution, long> _sensorCautionAppService;//车检器电压历史表
        private readonly IRepository<SensorLog, long> _sensorLogAppService;//车检器通讯记录表
        private readonly IRepository<SensorBusinessDetail, long> _sensorBusinessDetailAppService;//车检器进出场业务表
        private readonly IRepository<Transmitter> _transmitterAppService;//基站管理
        private readonly IRepository<TransmitterCaution, long> _transmitterCautionAppService;//基站历史电压表
        private readonly IRepository<Berth, long> _berthAppService;//泊位管理
        private readonly IRatesAppService _ratesAppService;//停车费用计算
        private readonly IP4ChatService _p4ChatService;
        private readonly IRepository<SensorBeat> _sensorBeatAppService;                 //车检器在线率数据
        private readonly IRepository<SensorFault, long> _sensorFaultAppService;         //故障车检器
        private readonly IRepository<SensorRunStatus, long> _sensorRunStatusAppService; //车检器运行状态
        private readonly IRepository<SensorMagnetic, long> _sensorMagneticAppService;
        private readonly IRepository<Employees.Employee, long> _employeeAppService;
        private readonly IRepository<RemoteGuid> _remoteGuidRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ICacheManager _abpCacheManager;//缓存
        private readonly IBusinessRepository _businessRepository;
        private readonly ISettingStore _settingStore;
        private readonly ISmsManagementAppService _smsManagementAppService;  //短信接口
        private readonly ISmsModelAppService _smsModelAppService;
        private readonly ISubscribeAppService _subscribeAppService;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sensorRepository"></param>
        /// <param name="sensorCautionAppService"></param>
        /// <param name="sensorLogAppService"></param>
        /// <param name="sensorBusinessDetailAppService"></param>
        /// <param name="transmitterAppService"></param>
        /// <param name="transmitterCautionAppService"></param>
        /// <param name="berthAppService"></param>
        /// <param name="p4ChatService"></param>
        /// <param name="abpsensorRepository"></param>
        /// <param name="ratesAppService"></param>
        /// <param name="sensorBeatAppService"></param>
        /// <param name="sensorFaultAppService"></param>
        /// <param name="sensorRunStatusAppService"></param>
        /// <param name="sensorMagneticAppService"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="remoteGuidRepository"></param>
        /// <param name="abpCacheManager"></param>
        /// <param name="smsModelAppService"></param>
        /// <param name="employeeAppService"></param>
        /// <param name="settingStore"></param>
        /// <param name="businessRepository"></param>
        /// <param name="smsManagementAppService"></param>
        /// <param name="subscribeAppService"></param>
        public SensorAppService(IRepository<Sensor> sensorRepository,
            IRepository<SensorCaution, long> sensorCautionAppService, IRepository<SensorLog, long> sensorLogAppService, IRepository<SensorBusinessDetail, long> sensorBusinessDetailAppService, IRepository<Transmitter> transmitterAppService, IRepository<TransmitterCaution, long> transmitterCautionAppService, IRepository<Berths.Berth, long> berthAppService, IP4ChatService p4ChatService, ISensorsRepository abpsensorRepository, IRatesAppService ratesAppService, IRepository<SensorBeat> sensorBeatAppService, IRepository<SensorFault, long> sensorFaultAppService, IRepository<SensorRunStatus, long> sensorRunStatusAppService, IRepository<SensorMagnetic, long> sensorMagneticAppService, IUnitOfWorkManager unitOfWorkManager, IRepository<RemoteGuid> remoteGuidRepository, ICacheManager abpCacheManager, ISmsModelAppService smsModelAppService, IRepository<Employees.Employee, long> employeeAppService, ISettingStore settingStore, IBusinessRepository businessRepository, ISmsManagementAppService smsManagementAppService, ISubscribeAppService subscribeAppService)
        {
            _smsModelAppService = smsModelAppService;
            _smsManagementAppService = smsManagementAppService;
            _settingStore = settingStore;
            _sensorRepository = sensorRepository;
            _sensorCautionAppService = sensorCautionAppService;
            _sensorLogAppService = sensorLogAppService;
            _sensorBusinessDetailAppService = sensorBusinessDetailAppService;
            _transmitterAppService = transmitterAppService;
            _transmitterCautionAppService = transmitterCautionAppService;
            _berthAppService = berthAppService;
            _abpsensorRepository = abpsensorRepository;
            _businessRepository = businessRepository;
            _p4ChatService = p4ChatService;
            _ratesAppService = ratesAppService;
            _sensorBeatAppService = sensorBeatAppService;
            _sensorFaultAppService = sensorFaultAppService;
            _sensorRunStatusAppService = sensorRunStatusAppService;
            _sensorMagneticAppService = sensorMagneticAppService;
            _unitOfWorkManager = unitOfWorkManager;
            _employeeAppService = employeeAppService;
            _remoteGuidRepository = remoteGuidRepository;
            _abpCacheManager = abpCacheManager;
            _subscribeAppService = subscribeAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Magnetism"></param>
        /// <param name="Battery"></param>
        /// <param name="TransmitterNumber"></param>
        /// <param name="SensorNumber"></param>
        /// <param name="ParkStatus"></param>
        /// <returns></returns>
        public Sensor InsertSensor(decimal? Magnetism, decimal? Battery, string TransmitterNumber, string SensorNumber, short ParkStatus)
        {
            return InsertSensor(new Sensor()
            {
                Battery = (Battery == 0 ? null : Battery),
                Magnetism = (Magnetism == 0 ? null : Magnetism),
                TransmitterNumber = TransmitterNumber,
                SensorNumber = SensorNumber,
                ParkStatus = ParkStatus
            });
        }

        /// <summary>
        /// 车检器设备心跳/电压、电容
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Sensor InsertSensor(Sensor entity)
        {
            List<Sensor> list = new List<Sensor>();
            list = Abp.DataProcessHelper.GetEntityFromTable<Sensor>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select * from AbpSensors with(nolock) where SensorNumber = '" + entity.SensorNumber + "'").Tables[0]);
            Sensor sensor = new Sensor();
            //var sensor = _sensorRepository.FirstOrDefault(entry => entry.SensorNumber == entity.SensorNumber);
            if (list.Count == 0)
            {
                sensor = new Sensor();
                sensor.SensorNumber = entity.SensorNumber;
                sensor.TransmitterNumber = entity.TransmitterNumber;
            }
            else
            {
                sensor = list[0];
            }
            sensor.BeatDatetime = DateTime.Now;
            int Id = 0;
            //是否通过标记位来计算是否启用心跳来判断有车无车
            if (entity.Battery != null)
            {
                sensor.Battery = entity.Battery;
                sensor.Magnetism = 5;
                sensor.Updatetime = DateTime.Now;

                SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text,
                "if(" + sensor.Id + " <> 0) begin update AbpSensors set Battery = " + entity.Battery + ", Magnetism = 5, Updatetime = getdate(), BeatDatetime = getdate(), ParkStatus = " + entity.ParkStatus + " where id = " + sensor.Id + " end");
                Id = sensor.Id;
            }
            Id = _sensorRepository.InsertOrUpdateAndGetId(sensor);

            Id = int.Parse(SqlHelper.ExecuteScalar(SqlHelper.connectionString, CommandType.Text,
               " if(" + sensor.Id + " = 0) begin insert into AbpSensors(SensorNumber, TransmitterNumber, CreationTime, BeatDatetime, ParkStatus, Receivable) values('" + entity.SensorNumber + "', '" + entity.TransmitterNumber + "', getdate(), getdate(), " + entity.ParkStatus + ", 0) SELECT @@IDENTITY AS ID end else begin update AbpSensors set BeatDatetime = getdate(), ParkStatus = " + entity.ParkStatus + " where Id =  " + sensor.Id + "  select " + sensor.Id + " end").ToString());


            //using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany, AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHaveCompany, AbpDataFilters.MustHaveBerthsec))
            //{
            //    //更新泊位表车检器信息
            //    var berth = _berthAppService.FirstOrDefault(entry => entry.SensorNumber == entity.SensorNumber);
            //    if (berth != default(Berth))
            //    {
            //        berth.SensorBeatTime = sensor.BeatDatetime;                    
            //        _berthAppService.Update(berth);
            //    }
            //}

            //_abpsensorRepository.UpdateBerthStatus(entity.SensorNumber, sensor.BeatDatetime);

            if (entity.Battery != null)
            {
                _sensorCautionAppService.Insert(new SensorCaution() { SensorId = Id, Battery = entity.Battery.Value, Magnetism = 5 });
            }

            if (sensor != null && sensor.TenantId != null)
                _p4ChatService.CreateChatService().Clients.Group(P4Consts.SensorGroup + sensor.TenantId).SensorMessage(5, entity.Battery, entity.SensorNumber, entity.ParkStatus);
            return sensor;
        }

        /// <summary>
        /// 读取车检器信息
        /// </summary>
        /// <param name="SensorNumber">车检器编号</param>
        /// <returns></returns>
        public SensorDto LoadSensor(string SensorNumber)
        {
            return _sensorRepository.FirstOrDefault(entry => entry.SensorNumber == SensorNumber).MapTo<SensorDto>();
        }

        /// <summary>
        /// 获取设备故障率
        /// 总数、故障数
        /// </summary>
        /// <returns></returns>
        public GetAllSensorsBeatOutput GetSensorBeatList(SearchSensorBeatInput input)
        {
            return new GetAllSensorsBeatOutput()
            {
                rows = _sensorBeatAppService.GetAll().Where(entity => entity.CreationTime >= input.OperateDateBegin && entity.CreationTime <= input.OperateDateEnd).OrderBy(entity => entity.CreationTime).MapTo<List<SensorBeatDto>>()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllSensorRunStatusOutput GetSensorRunStatusList(SearchSensorRunStatusInput input)
        {
            return new GetAllSensorRunStatusOutput()
            {
                rows = _sensorRunStatusAppService.GetAll().Where(entity => entity.SensorNumber == input.SensorNumber).OrderByDescending(entity => entity.Id).PageBy(input).ToList().MapTo<List<SensorRunStatusDto>>()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetSensorParkRation GetSensorPRatio(SearchSensorsInput input)
        {
            var list = new List<int>();
            list.Add(-1);
            input.CompanyIds = input.CompanyId.HasValue == false ? AbpSession.CompanyIds ?? list : new List<int> { input.CompanyId.Value };
            return _abpsensorRepository.GetSensorPRatio(input);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllSensorsFaultOutput GetFaultSensorList(SearchSensorFaultInput input)
        {
            var entry = _sensorBeatAppService.FirstOrDefault(entity => entity.CreationTime == input.CreationTime);
            if (entry == default(SensorBeat))
                return default(GetAllSensorsFaultOutput);
            input.SensorBeatId = entry.Id;
            var records = _sensorFaultAppService.Count(entity => entity.SensorBeatId == input.SensorBeatId);
            return new GetAllSensorsFaultOutput()
            {
                rows = _sensorFaultAppService.GetAll().Where(entity => entity.SensorBeatId == input.SensorBeatId).Orders(input).PageBy(input).ToList().MapTo<List<SensorFaultDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 基站心跳/电压
        /// </summary>
        /// <param name="TransmitterNumber"></param>
        /// <param name="VoltageCaution"></param>
        /// <returns></returns>
        public Transmitter InsertTransmitter(string TransmitterNumber, decimal? VoltageCaution)
        {
            return InsertTransmitter(new Transmitter() { TransmitterNumber = TransmitterNumber, VoltageCaution = (VoltageCaution == 0 ? null : VoltageCaution) });
        }

        /// <summary>
        /// 基站心跳/电压
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Transmitter InsertTransmitter(Transmitter entity)
        {
            List<Transmitter> list = Abp.DataProcessHelper.GetEntityFromTable<Transmitter>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select * from [AbpTransmitters] with(nolock) where TransmitterNumber = '" + entity.TransmitterNumber + "'").Tables[0]);


            if (list.Count == 0)
            {
                Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "insert into AbpTransmitters(TransmitterNumber,  CreationTime, BeatDatetime) values('" + entity.TransmitterNumber + "', getdate(), getdate())");
            }
            else
            {
                if (entity.VoltageCaution != null)//如果电量不为空
                {
                    Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "update AbpTransmitters set VoltageCaution = " + entity.VoltageCaution + ", BeatDatetime = getdate(), Updatetime = getdate() where Id = " + list[0].Id);
                    _transmitterCautionAppService.Insert(new TransmitterCaution() { VoltageCaution = entity.VoltageCaution.Value, TransmitterId = list[0].Id });
                }
                else
                {
                    Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "update AbpTransmitters set BeatDatetime = getdate() where Id = " + list[0].Id);
                }
            }
            return entity;



            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant);//禁用商户过滤器
            var Id = 0;
            var transmitter = _transmitterAppService.FirstOrDefault(entry => entry.TransmitterNumber == entity.TransmitterNumber);
            if (transmitter == default(Transmitter))
            {
                transmitter = new Transmitter();
                transmitter.TransmitterNumber = entity.TransmitterNumber;
            }
            if (entity.VoltageCaution != null)//如果电量不为空
            {
                transmitter.VoltageCaution = entity.VoltageCaution;
                transmitter.Updatetime = DateTime.Now;
            }
            transmitter.BeatDatetime = DateTime.Now;
            Id = _transmitterAppService.InsertOrUpdateAndGetId(transmitter);
            if (entity.VoltageCaution != null)//如果电量不为空
            {
                _transmitterCautionAppService.Insert(new TransmitterCaution() { VoltageCaution = entity.VoltageCaution.Value, TransmitterId = Id });
            }
            return transmitter;
        }

        /// <summary>
        /// 车辆入场
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public SensorBusinessDetail InsertCarAdmission(SensorBusinessDetail entity)
        {
            Guid guid = Guid.NewGuid();
            DataTable dt = Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select CarOutTime, BerthsecId, PlateNumber, ParkId, CompanyId from AbpSensorBusinessDetail with(nolock) where SensorNumber = '" + entity.SensorNumber + "' and Indicate = '" + entity.Indicate + "' and status = 0").Tables[0];
            int? StopTime = null;
            decimal Receivable = 0;
            if (dt.Rows.Count > 0 && string.IsNullOrWhiteSpace(dt.Rows[0]["CarOutTime"].ToString()) == false && string.IsNullOrWhiteSpace(dt.Rows[0]["BerthsecId"].ToString()) == false)
            {
                var model = _ratesAppService.RateCalculate(int.Parse(dt.Rows[0]["BerthsecId"].ToString()), entity.CarInTime.Value, DateTime.Parse(dt.Rows[0]["CarOutTime"].ToString()), 2, 0, int.Parse(dt.Rows[0]["ParkId"].ToString()), dt.Rows[0]["PlateNumber"].ToString(), int.Parse(dt.Rows[0]["CompanyId"].ToString()));
                StopTime = (int?)model.ParkTime;
                Receivable = model.CalculateMoney;
            }

            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@SensorNumber", entity.SensorNumber),
                 new SqlParameter("@Indicate", entity.Indicate),
                 new SqlParameter("@guid", guid),
                 new SqlParameter("@CarInTime", entity.CarInTime),
                 new SqlParameter("@StopTime", StopTime),
                new SqlParameter("@Receivable", Receivable)
            };
            DataTable result = SqlHelper.ExecuteDataset(SqlHelper.connectionString, "Pro_Sensor_SensorInsertCarAdmission", param).Tables[0];
            SensorBusinessDetail tempmodel = new SensorBusinessDetail();
            tempmodel.BerthNumber = result.Rows[0]["BerthNumber"].ToString();
            tempmodel.TenantId = string.IsNullOrWhiteSpace(result.Rows[0]["TenantId"].ToString()) == true ? 0 : int.Parse(result.Rows[0]["TenantId"].ToString());
            tempmodel.CompanyId = string.IsNullOrWhiteSpace(result.Rows[0]["CompanyId"].ToString()) == true ? 0 : int.Parse(result.Rows[0]["CompanyId"].ToString());
            tempmodel.RegionId = string.IsNullOrWhiteSpace(result.Rows[0]["RegionId"].ToString()) == true ? 0 : int.Parse(result.Rows[0]["RegionId"].ToString());
            tempmodel.ParkId = string.IsNullOrWhiteSpace(result.Rows[0]["ParkId"].ToString()) == true ? 0 : int.Parse(result.Rows[0]["ParkId"].ToString());
            tempmodel.BerthsecId = string.IsNullOrWhiteSpace(result.Rows[0]["BerthsecId"].ToString()) == true ? 0 : int.Parse(result.Rows[0]["BerthsecId"].ToString());


            _p4ChatService.CreateChatService().Clients.Group(P4Consts.SensorGroup + tempmodel.TenantId).SensorStatusMessage(entity.SensorNumber, "1");


            return tempmodel;
        }

        /// <summary>
        /// 获取地磁信息
        /// </summary>
        /// <returns></returns>
        //public List<SensorsDetail> GetSensorsDetail()
        //{
        //    return Abp.DataProcessHelper.GetEntityFromTable<SensorsDetail>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select count(*) as SensorsTotal,sum(case when DATEDIFF(MINUTE, convert(varchar(20), BeatDatetime, 120), convert(varchar(20), getdate(), 120)) < " + P4Consts.SensorDayOnline + " then 1 else 0 end) as OnLine,sum(case when DATEDIFF(MINUTE, convert(varchar(20), BeatDatetime, 120), convert(varchar(20), getdate(), 120)) > " + P4Consts.SensorDayOnline + " then 1 else 0 end) as OffLine from AbpSensors where TenantId = " + AbpSession.TenantId.Value + " and CompanyId = " + AbpSession.CompanyId.Value + "").Tables[0]);
        //}


        /// <summary>
        /// 车辆出场
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public SensorBusinessDetail InsertCarEntrance(SensorBusinessDetail entity)
        {
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select CarInTime, BerthsecId, PlateNumber, ParkId, CompanyId from AbpSensorBusinessDetail with(nolock) where SensorNumber = '" + entity.SensorNumber + "' and Indicate = '" + entity.Indicate + "' and status = 0").Tables[0];
            int? StopTime = null;
            decimal Receivable = 0;
            if (dt.Rows.Count > 0 && string.IsNullOrWhiteSpace(dt.Rows[0]["CarInTime"].ToString()) == false && string.IsNullOrWhiteSpace(dt.Rows[0]["BerthsecId"].ToString()) == false)
            {
                var model = _ratesAppService.RateCalculate(int.Parse(dt.Rows[0]["BerthsecId"].ToString()), DateTime.Parse(dt.Rows[0]["CarInTime"].ToString()), entity.CarOutTime.Value, 2, 0, int.Parse(dt.Rows[0]["ParkId"].ToString()), dt.Rows[0]["PlateNumber"].ToString(), int.Parse(dt.Rows[0]["CompanyId"].ToString()));
                StopTime = (int?)model.ParkTime;
                Receivable = model.CalculateMoney;
            }

            Guid guid = Guid.NewGuid();
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@SensorNumber", entity.SensorNumber),
                new SqlParameter("@Indicate", entity.Indicate),
                new SqlParameter("@guid", guid),
                new SqlParameter("@CarOutTime", entity.CarOutTime),
                new SqlParameter("@StopTime", StopTime),
                new SqlParameter("@Receivable", Receivable)
            };
            DataTable result = SqlHelper.ExecuteDataset(SqlHelper.connectionString, "Pro_Sensor_SensorInsertCarEntrance", param).Tables[0];
            SensorBusinessDetail tempmodel = new SensorBusinessDetail
            {
                BerthNumber = result.Rows[0]["BerthNumber"].ToString(),
                TenantId = string.IsNullOrWhiteSpace(result.Rows[0]["TenantId"].ToString()) == true ? 0 : int.Parse(result.Rows[0]["TenantId"].ToString()),
                CompanyId = string.IsNullOrWhiteSpace(result.Rows[0]["CompanyId"].ToString()) == true ? 0 : int.Parse(result.Rows[0]["CompanyId"].ToString()),
                RegionId = string.IsNullOrWhiteSpace(result.Rows[0]["RegionId"].ToString()) == true ? 0 : int.Parse(result.Rows[0]["RegionId"].ToString()),
                ParkId = string.IsNullOrWhiteSpace(result.Rows[0]["ParkId"].ToString()) == true ? 0 : int.Parse(result.Rows[0]["ParkId"].ToString()),
                BerthsecId = string.IsNullOrWhiteSpace(result.Rows[0]["BerthsecId"].ToString()) == true ? 0 : int.Parse(result.Rows[0]["BerthsecId"].ToString())
            };
            

            //判断是否需要远程出场
           /*bool RemotePresence = bool.Parse(SqlHelper.ExecuteScalar(SqlHelper.connectionString, CommandType.Text,
                "select Value from AbpSettings where TenantId = " + tempmodel.TenantId + " and Name = 'RemotePresence'").ToString());
            if (RemotePresence && tempmodel.BerthsecId.HasValue)
            {
                var tempguid = SqlHelper.ExecuteScalar(SqlHelper.connectionString, CommandType.Text,
                "select guid from AbpBerths where BerthsecId = " + tempmodel.BerthsecId + " and BerthStatus = 1 and BerthNumber = '" + tempmodel.BerthNumber + "'");
                if (tempguid == null)
                    return tempmodel;
                 _remoteGuidRepository.Insert(new RemoteGuid() { BusinessDetailGuid = new Guid(tempguid.ToString()), BerthsecId = tempmodel.BerthsecId.Value });
                //远程自主结单
                var businessGuid = new Guid(tempguid.ToString());
                var businessList = Abp.DataProcessHelper.GetEntityFromTable<BusinessDetail>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpBusinessDetail with(nolock) where guid = '" + businessGuid + "' and Status = 1 and IsDeleted = 0").Tables[0]);
                if(businessList.Count > 0)
                {
                    var businessModel = businessList[0];
                    string berthNumber = businessModel.BerthNumber;
                    int berthsecId = businessModel.BerthsecId;
                    int tenantId = businessModel.TenantId;
                    DateTime carOutTime =entity.CarOutTime.Value;

                   

                    var rateModel = _ratesAppService.RateCalculate(businessModel.BerthsecId, businessModel.CarInTime.Value, carOutTime, businessModel.CarType, 0, businessModel.ParkId, businessModel.PlateNumber, businessModel.CompanyId);

                    string isPay = "false";
                    string cardNo = "";
                    string factReceive = "0";
                    string payStatus = "0";
                    if(rateModel.CalculateMoney == 0)
                    {
                        isPay = "true";
                    }
                    else
                    {
                        var otherAccountList = Abp.DataProcessHelper.GetEntityFromTable<OtherAccountDto>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select CardNo, TelePhone, PlateNumber, Wallet, IsEnabled, AutoDeduction from ExtOtherAccount with(nolock) left join ExtOtherPlateNumber on ExtOtherAccount.Id = ExtOtherPlateNumber.AssignedOtherAccountId where (ExtOtherAccount.CardNo = '" + businessModel.PlateNumber + "' or TelePhone = '" + businessModel.PlateNumber + "' or (ExtOtherPlateNumber.PlateNumber = '" + businessModel.PlateNumber + "' and ExtOtherPlateNumber.IsDeleted = 0)) and ExtOtherAccount.IsActive = 1 and ExtOtherAccount.IsDeleted = 0 and ExtOtherAccount.IsEnabled = 1 and TenantId = " + tenantId).Tables[0]);
                        if (otherAccountList.Count > 0)
                        {
                            var otherAccountModel = otherAccountList[0];
                                //自动扣款，判断金额是否满足
                            if (otherAccountModel.AutoDeduction)
                            {
                                //判断是否存在优惠券，确定优惠券金额
                                decimal tempMoney = rateModel.CalculateMoney;
                                var couponList = Abp.DataProcessHelper.GetEntityFromTable<Coupon>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpCoupons with(nolock) where IsActive = 0 and money > 0 and EndTime > getdate() and tel = '" + otherAccountModel.TelePhone + "' order by Money desc").Tables[0]);
                                if (couponList.Count > 0)
                                {
                                    if(couponList[0].Money <= rateModel.CalculateMoney)
                                    {
                                        tempMoney = tempMoney - couponList[0].Money;
                                    }
                                    else
                                    {
                                        tempMoney = 0;
                                    }
                                }
                                if(otherAccountModel.Wallet >= tempMoney)
                                {
                                    cardNo = otherAccountModel.CardNo;
                                    factReceive = rateModel.CalculateMoney.ToString();
                                    payStatus = "4";
                                    isPay = "true";
                                }
                            }
                        }
                    }
                    RemoteInsertCarOut(rateModel.CalculateMoney, factReceive, carOutTime.ToString("yyyy-MM-dd HH:mm:sss"), carOutTime.ToString("yyyy-MM-dd HH:mm:sss"), tempguid.ToString(), carOutTime.ToString("yyyy-MM-dd HH:mm:sss"), StopTime.ToString(), Receivable.ToString(), payStatus, isPay, "1", rateModel.ParkTime.ToString(), rateModel.CalculateMoney, cardNo, rateModel.CalculateMoney, 0, 10, businessModel.BerthsecId, "0");
                }            
            }*/

            _p4ChatService.CreateChatService().Clients.Group(P4Consts.SensorGroup + tempmodel.TenantId).SensorStatusMessage(entity.SensorNumber, "0");
            return tempmodel;
        }

        /// <summary>
        /// 服务器远程出场业务
        /// </summary>
        /// <param name="Receivable"></param>
        /// <param name="FactReceive"></param>
        /// <param name="CarOutTime"></param>
        /// <param name="CarPayTime"></param>
        /// <param name="guid"></param>
        /// <param name="SensorsOutCarTime"></param>
        /// <param name="SensorsStopTime"></param>
        /// <param name="SensorsReceivable"></param>
        /// <param name="PayStatus"></param>
        /// <param name="IsPay"></param>
        /// <param name="FeeType"></param>
        /// <param name="StopTime"></param>
        /// <param name="Money"></param>
        /// <param name="CardNo"></param>
        /// <param name="BeforeDiscount"></param>
        /// <param name="DiscountMoney"></param>
        /// <param name="DiscountRate"></param>
        /// <param name="BerthsecId"></param>
        /// <param name="OutBatchNo"></param>
        /// <returns></returns>

        public bool RemoteInsertCarOut(decimal Receivable, string FactReceive, string CarOutTime, string CarPayTime, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money, string CardNo, decimal BeforeDiscount, decimal DiscountMoney, double DiscountRate, int BerthsecId, string OutBatchNo)
        {


            if (PayStatus != "4")
                CardNo = "0";
            Guid g = new Guid(guid);
            List<BusinessDetail> entitys = new List<BusinessDetail>();
            entitys = Abp.DataProcessHelper.GetEntityFromTable<BusinessDetail>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpBusinessDetail with(nolock) where  guid = '" + g + "'").Tables[0]);
            if (entitys.Count == 0)
            {
                throw new AbpAuthorizationException("出场失败：guid不存在！", "22");
            }
            BusinessDetail entity = new BusinessDetail();
            entity = entitys[0];

            int? tenantId = entity.TenantId;
            int? companyId = entity.CompanyId;

            var userId = entity.InOperaId;
            if ((entity.Status != 1 && entity.Status != 3) || entity.LastModifierUserId.HasValue)//已经出场处理
                throw new AbpAuthorizationException("出场失败：该数据已出场！", "201");

            Berth berthmodel = Abp.DataProcessHelper.GetEntityFromTable<Berth>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpBerths with(nolock) where BerthsecId = " + BerthsecId + " and BerthNumber = '" + entity.BerthNumber + "'").Tables[0])[0];

            DateTime? SensorsOutCarTime1 = null;

            //处理白名单         
            var whiteList = _abpCacheManager.GetCache(string.Format(Abp.Configuration.SettingManager.CacheWhiteCarValue, entity.CompanyId)).Get(string.Format(Abp.Configuration.SettingManager.CacheWhiteCarValue, entity.CompanyId), () => GetWhiteListCache(entity.TenantId, entity.CompanyId)) as List<WhiteListsDto>;
            var whiteModel = whiteList.FirstOrDefault(e => e.RelateNumber == berthmodel.RelateNumber);
            if (whiteModel != default(WhiteListsDto))
            {
                Receivable = 0;
                FactReceive = "0";
                Money = 0;
            }
            else //包月车辆数据
            {
                List<MonthlyCarDto> MonthlyCarList = new List<MonthlyCarDto>();
                var monthlyCarList = _abpCacheManager.GetCache(string.Format(Abp.Configuration.SettingManager.CacheMonthyValue, entity.CompanyId))
                    .Get(string.Format(Abp.Configuration.SettingManager.CacheMonthyValue, entity.CompanyId), () => GetMonthlyCarCache(entity.TenantId, entity.CompanyId)) as List<MonthlyCarDto>;
                var list = monthlyCarList.Where(e => e.PlateNumber == berthmodel.RelateNumber && e.BeginTime <= DateTime.Now && e.EndTime >= DateTime.Now).ToList();
                var parkId = "," + berthmodel.ParkId + ",";
                for (int i = 0; i < list.Count; i++)
                {
                    if (("," + list[i].ParkIds + ",").Contains(parkId) || list[i].ParkIds == "0")
                    {
                        MonthlyCarList.Add(list[i]);
                        break;
                    }
                }
                if (MonthlyCarList.Count > 0 && MonthlyCarList[0].MonthyType == "MonthyAll")
                {
                    Receivable = 0;
                    FactReceive = "0";
                    Money = 0;
                }
            }

            if (berthmodel == default(Berth))
            {
                SensorsOutCarTime1 = null;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(berthmodel.SensorNumber))
                {
                    SensorsOutCarTime1 = berthmodel.SensorsOutCarTime;
                }
            }

            if (entity != null)
            {

                //var listModel = Abp.DataProcessHelper.GetEntityFromTable<WhiteLists.WhiteList>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpWhiteList with(nolock) where Name = 'EscapeLock' and UserId is Null and TenantId = " + tenantId).Tables[0]);

                //if (bool.Parse(_settingStore.GetSettingOrNull(tenantId, null, "EscapeLock").Value))
                //{
                //    long? userid = AbpSession.UserId;
                //    _businessRepository.PlateNumberDebLock(userid, entity.PlateNumber);
                //}
                entity.OutDeviceCode = "000000000000000";               //出场设备


                //if (AbpSession.UserSource != 1)
                //{
                //    entity.ChargeOperaId = AbpSession.UserId;                   //收费操作员Id
                //    entity.ChargeDeviceCode = AbpSession.DeviceCode;            //收费员设备
                //    entity.OutOperaId = Convert.ToInt16(AbpSession.UserId);     //出场收费员ID
                //}

                entity.CarOutTime = Convert.ToDateTime(CarOutTime);         //出场时间
                entity.CarPayTime = Convert.ToDateTime(CarPayTime);         //支付时间
                entity.OutBatchNo = OutBatchNo;                          //出厂批次号
                entity.guid = g;                                         //唯一guid

                if (entity.guid == berthmodel.SensorGuid)                    //如果车检器与收费记录中guid相等，关联车检器入场出场信息
                {
                    entity.SensorsOutCarTime = DateTime.Parse(SensorsOutCarTime);//车检器出场时间

                    if (entity.SensorsInCarTime.HasValue && entity.SensorsOutCarTime.HasValue)
                    {
                        entity.SensorsStopTime = Convert.ToInt32(entity.SensorsOutCarTime.Value.Subtract(entity.SensorsInCarTime.Value).TotalMinutes);
                    }
                    entity.SensorsReceivable = Convert.ToDecimal(SensorsReceivable);//车检收器应
                }
                entity.Money = Money;//实际应收金额
                entity.IsPay = Convert.ToBoolean(IsPay);//是否支付

                if (!entity.IsPay)  //未支付
                {
                    entity.Status = 3;
                    entity.EscapeTime = Convert.ToDateTime(CarOutTime);//逃逸时间
                    if (Convert.ToDecimal(Money) > entity.Prepaid)
                    {
                        entity.Arrearage = Convert.ToDecimal(Money) - entity.Prepaid;   //如果车主逃逸（未付费）  写入欠费字段
                    }
                }
                else  //支付状态为true   只是交了部分的费用  还有一定金额的欠费
                {
                    if (Convert.ToDecimal(Money) > Convert.ToDecimal(FactReceive))//总应收大于总实收（属于余额不足）
                    {
                        entity.Status = 5; // 记录状态  1.不完整数据 2.正常收费已缴费 3.逃逸未收费 4.逃逸已收费 5.正常收费，但是余额不足欠费
                        entity.Arrearage = Convert.ToDecimal(Money) - Convert.ToDecimal(FactReceive);
                    }
                    else  //总应收小于等于总实收
                    {
                        entity.Status = 2;
                    }
                }

                if (!string.IsNullOrEmpty(PayStatus))
                {
                    entity.PayStatus = Convert.ToInt16(PayStatus);  // 出场支付类型（出场应收） 1.现金 2.刷卡 3.在线支付 4.MOne卡
                }

                if (PayStatus == "1")                                           //现金
                {
                    entity.FactReceive = Convert.ToDecimal(FactReceive);        //实收
                    entity.DiscountRate = DiscountRate;//折扣率
                    entity.DiscountMoney = DiscountMoney;//折扣金额
                    entity.BeforeDiscount = BeforeDiscount;//打折前应收
                    entity.Arrearage = 0;
                }
                else if (PayStatus == "3")                                      //微信支付
                {
                    entity.FactReceive = Convert.ToDecimal(FactReceive);        //实收
                    entity.DiscountRate = DiscountRate;//折扣率
                    entity.DiscountMoney = DiscountMoney;//折扣金额
                    entity.BeforeDiscount = BeforeDiscount;//打折前应收
                    entity.Arrearage = 0;
                }
                else if (PayStatus == "4")                                      //账号支付
                {
                    entity.FactReceive = Convert.ToDecimal(FactReceive);        //实收
                    entity.DiscountRate = DiscountRate;//折扣率
                    entity.DiscountMoney = DiscountMoney;//折扣金额
                    entity.BeforeDiscount = BeforeDiscount;//打折前应收
                    entity.Arrearage = 0;
                }

                entity.FeeType = Convert.ToInt16(FeeType);//费用类型（1.正常收费，2.追缴收费）
                entity.StopTime = Convert.ToInt32(StopTime.Split('.')[0]);//停车时长
                entity.ReceivableCarNo = CardNo;   //出场卡号   如果为0的话，出场支付类型是现金

                var count = SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, "update abpberths set BerthStatus = '2', RelateNumber = '" + entity.PlateNumber + "', OutCarTime = '" + CarOutTime + "', SensorGuid = '00000000-0000-0000-0000-000000000000' where BerthNumber = '" + entity.BerthNumber + "' and IsActive = 1 and BerthsecId = " + BerthsecId);
                if (count == 0)
                    throw new AbpAuthorizationException("出场失败：不存在该泊位编号！", "23");

                var otherAccountList = Abp.DataProcessHelper.GetEntityFromTable<OtherAccountDto>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from ExtOtherAccount with(nolock) where CardNo = '" + CardNo + "' and IsActive = 1 and IsDeleted = 0 and TenantId = " + tenantId).Tables[0]);
                if (otherAccountList.Count > 0)
                {
                    var otherAccount = otherAccountList[0];
                    if (Money == entity.Prepaid) //消费金额等于预缴金额  添加消费记录
                    {
                        var money = Money - entity.Prepaid;
                        var inTime = Convert.ToDateTime(CarOutTime);
                        var EmployeeId = AbpSession.UserId.Value;
                        var EndMoney = otherAccount.Wallet - (Money - entity.Prepaid);
                        string deductionRecordsSql = "Insert INTO AbpDeductionRecords(OtherAccountId,OperType,Money,PayStatus,InTime,Remark,CardNo,EmployeeId,UserId,PlateNumber,TenantId,CompanyId,BeginMoney,EndMoney) Values (" + otherAccount.Id + ",2,'" + money + "',1,'" + inTime + "','消费','" + CardNo + "'," + EmployeeId + "," + EmployeeId + ",'" + entity.PlateNumber + "'," + tenantId + "," + companyId + ",'" + otherAccount.Wallet + "','" + EndMoney + "')";
                        //添加消费记录
                        SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, deductionRecordsSql);

                        if (otherAccount.TelePhone != null && entity.Money > 0)
                        {
                            //消费金额 以及账户余额（短信发送）
                            _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto() { Destnumbers = otherAccount.TelePhone, Msg = string.Format(_smsModelAppService.GetSmsModelByType("ConsumptionEqualModel").SmsContext, Convert.ToDateTime(CarOutTime).ToString("yyyy年MM月dd日HH时mm分"), entity.BerthNumber, entity.Prepaid, entity.PlateNumber, otherAccount.Wallet, entity.StopTime, entity.Money) });
                        }
                        otherAccount.Wallet = otherAccount.Wallet - money;  //账户余额减掉消费金额
                        string extOtherAccountSql = "Update ExtOtherAccount set Wallet = '" + otherAccount.Wallet + "' where Id =" + otherAccount.Id;
                        SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, extOtherAccountSql);
                    }
                    else if (Money < entity.Prepaid)  //消费金额小于预付  （返还）
                    {
                        var money = entity.Prepaid - Money;  //消费金额
                        var EmployeeId = AbpSession.UserId.Value;
                        var EndMoney = otherAccount.Wallet + entity.Prepaid - Money;
                        var inTime = Convert.ToDateTime(CarOutTime);
                        string deductionRecordsSql = "Insert INTO AbpDeductionRecords(OtherAccountId,OperType,Money,PayStatus,InTime,Remark,CardNo,EmployeeId,UserId,PlateNumber,TenantId,CompanyId,BeginMoney,EndMoney) Values (" + otherAccount.Id + ",4,'" + money + "',1,'" + inTime + "','返还','" + CardNo + "'," + EmployeeId + "," + EmployeeId + ",'" + entity.PlateNumber + "'," + tenantId + "," + companyId + ",'" + otherAccount.Wallet + "','" + EndMoney + "')";
                        //添加返还消费记录
                        SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, deductionRecordsSql);

                        if (otherAccount.TelePhone != null)
                        {
                            //返回金额 以及账户余额（短信发送）
                            _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto() { Destnumbers = otherAccount.TelePhone, Msg = string.Format(_smsModelAppService.GetSmsModelByType("ConsumptionGreaterModel").SmsContext, Convert.ToDateTime(CarOutTime).ToString("yyyy年MM月dd日HH时mm分"), entity.BerthNumber, entity.Prepaid, money, entity.PlateNumber, otherAccount.Wallet + money) });
                        }

                        otherAccount.Wallet = otherAccount.Wallet + money;  //账户余额加上返还金额
                        string extOtherAccountSql = "Update ExtOtherAccount set Wallet = '" + otherAccount.Wallet + "' where Id =" + otherAccount.Id;
                        SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, extOtherAccountSql);
                    }
                    else if (Money > entity.Prepaid && entity.Status != 5 && entity.Status != 3)  //消费金额大于预付（消费）
                    {
                        //判断是否存在优惠券，确定优惠券金额
                        decimal tempmoney = Money;
                        var list = Abp.DataProcessHelper.GetEntityFromTable<Coupon>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpCoupons where IsActive = 0 and money > 0 and EndTime > getdate() and tel = '" + otherAccount.TelePhone + "' order by Money desc").Tables[0]);
                        if (list.Count > 0)
                        {
                            Coupon model = new Coupon();
                            model = list[0];
                            if (list[0].Money <= Money)//优惠券金额小于等于消费金额
                            {
                                var tel = list[0].tel;
                                var NewMoney = 0;
                                var CouponNo = list[0].CouponNo;
                                var OldMoney = list[0].Money;
                                var ConsumptionMoney = list[0].Money;
                                var Platenumber = entity.PlateNumber;
                                var Source = "平台消费";
                                var CouponType = 2;
                                var time = DateTime.Now;
                                string couponDetailSql = "Insert INTO AbpCouponDetails(CouponNo,Platenumber,OldMoney,NewMoney,ConsumptionMoney,tel,CreationTime,TenantId,CouponType,Source) Values ('" + CouponNo + "','" + Platenumber + "','" + OldMoney + "'," + NewMoney + ",'" + ConsumptionMoney + "','" + tel + "','" + time + "'," + tenantId + "," + CouponType + ",'" + Source + "')";
                                SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, couponDetailSql);

                                tempmoney = tempmoney - list[0].Money;
                                model.Money = 0;
                                string couponsSql = "Update AbpCoupons set Money = '" + model.Money + "',IsActive = 1 where CouponNo ='" + CouponNo + "' and tel = '" + tel + "'";
                                SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, couponsSql);
                            }
                            else//如果优惠券金额大于消费金额
                            {
                                var tel = list[0].tel;
                                var NewMoney = model.Money - tempmoney;
                                var CouponNo = list[0].CouponNo;
                                var OldMoney = list[0].Money;
                                var ConsumptionMoney = tempmoney;
                                var Platenumber = entity.PlateNumber;
                                var Source = "平台消费";
                                var CouponType = 2;
                                var time = DateTime.Now;
                                string couponDetailSql = "Insert INTO AbpCouponDetails(CouponNo,Platenumber,OldMoney,NewMoney,ConsumptionMoney,tel,CreationTime,TenantId,CouponType,Source) Values ('" + CouponNo + "','" + Platenumber + "','" + OldMoney + "'," + NewMoney + ",'" + ConsumptionMoney + "','" + tel + "','" + time + "'," + tenantId + "," + CouponType + ",'" + Source + "')";
                                SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, couponDetailSql);

                                model.Money = model.Money - tempmoney;
                                string couponsSql = "Update AbpCoupons set Money = '" + model.Money + "' where CouponNo ='" + CouponNo + "' and tel = '" + tel + "'";
                                SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, couponsSql);
                                tempmoney = 0;
                            }
                        }
                        if (tempmoney > 0)
                        {
                            //添加消费记录
                            var Moneypay = tempmoney - entity.Prepaid;  //消费金额
                            var EmployeeId = entity.InOperaId;
                            var InTime = Convert.ToDateTime(CarOutTime);
                            var EndMoney = otherAccount.Wallet - (tempmoney - entity.Prepaid);

                            string deductionRecordsSql = "Insert INTO AbpDeductionRecords(OtherAccountId,OperType,Money,PayStatus,InTime,Remark,CardNo,EmployeeId,UserId,PlateNumber,TenantId,CompanyId,BeginMoney,EndMoney) Values (" + otherAccount.Id + ",2,'" + Moneypay + "',1,'" + InTime + "','消费','" + CardNo + "'," + EmployeeId + "," + EmployeeId + ",'" + entity.PlateNumber + "'," + tenantId + "," + companyId + ",'" + otherAccount.Wallet + "','" + EndMoney + "')";
                            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, deductionRecordsSql);

                            if (!string.IsNullOrEmpty(otherAccount.TelePhone) && Moneypay > 0)
                            {
                                //消费金额 以及账户余额（短信发送）
                                _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto() { Destnumbers = otherAccount.TelePhone, Msg = string.Format(_smsModelAppService.GetSmsModelByType("ConsumptionLessModel").SmsContext, Convert.ToDateTime(CarOutTime).ToString("yyyy年MM月dd日HH时mm分"), entity.BerthNumber, entity.Prepaid, Moneypay, entity.PlateNumber, otherAccount.Wallet - Moneypay) });
                            }
                            otherAccount.Wallet = otherAccount.Wallet - Moneypay;  //账户余额减掉消费金额
                            string extOtherAccountSql = "Update ExtOtherAccount set Wallet = '" + otherAccount.Wallet + "' where Id =" + otherAccount.Id;
                            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, extOtherAccountSql);
                        }
                    }
                    //出场应收 
                    entity.FactReceive = Convert.ToDecimal(FactReceive);        //实收
                    entity.BeforeDiscount = BeforeDiscount;//折前应收
                    entity.DiscountMoney = DiscountMoney;//折扣金额    
                    entity.DiscountRate = DiscountRate;//折扣
                }
                else if (CardNo != "0" && otherAccountList.Count == 0)  //刷卡出场，但是数据库当中没有卡记录 当做欠费处理
                {
                    entity.Status = 3;
                    entity.EscapeTime = Convert.ToDateTime(CarOutTime);//逃逸时间
                    if (Convert.ToDecimal(Money) > entity.Prepaid)
                    {
                        entity.Arrearage = Convert.ToDecimal(Money) - entity.Prepaid;   //如果车主逃逸（未付费）  写入欠费字段
                    }
                }
                entity.Receivable = Receivable;
                var booln = 0;
                if (entity.IsPay)
                {
                    booln = 1;
                }

                string businessSql = "Update AbpBusinessDetail set Receivable = '" + entity.Receivable + "',StopTime = " + StopTime + ",FeeType = " + entity.FeeType + ",ReceivableCarNo = '" + entity.ReceivableCarNo + "',IsPay = " + booln + ",Money = '" + entity.Money + "',SensorsReceivable = '" + SensorsReceivable + "',Arrearage = '" + entity.Arrearage + "',FactReceive = '" + entity.FactReceive + "',PayStatus = " + entity.PayStatus + ",Status = " + entity.Status + ",EscapeTime = '" + entity.EscapeTime + "',SensorsStopTime = '" + entity.SensorsStopTime + "',SensorsOutCarTime = '" + SensorsOutCarTime + "',OutBatchNo = '" + entity.OutBatchNo + "',CarPayTime = '" + entity.CarPayTime + "',CarOutTime = '" + entity.CarOutTime + "',OutDeviceCode = '" + entity.OutDeviceCode + "'  where guid = '" + entity.guid + "'";
                SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text, businessSql);

                //if (AbpSession.UserSource == 1)
                //{
                //    return true;
                //}
                var employeeList = Abp.DataProcessHelper.GetEntityFromTable<Employee>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpEmployees with(nolock) where Id = " + userId).Tables[0]);
                var employee = employeeList[0];
                var berthsecList = Abp.DataProcessHelper.GetEntityFromTable<Berthsec>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpBerthsecs with(nolock) where Id = " + entity.BerthsecId).Tables[0]);
                string berthsecName = "";
                if (berthsecList.Count > 0)
                {
                    berthsecName = berthsecList[0].BerthsecName;
                }

                if (entity.Status == 3)
                {
                    string message = "出场车辆" + entity.PlateNumber + ",驶入时间:" + entity.CarInTime + ",驶出时间:" + entity.CarOutTime + "停靠" + berthsecName + entity.BerthNumber + "泊位，停车时长:" + StopTimes(entity.StopTime) + ",应收金额" + entity.Money + "元，预缴金额:" + entity.Prepaid + "元,支付金额:" + entity.FactReceive + "元，欠费金额:" + entity.Arrearage + "元，操作收费员:" + employee.TrueName + "(" + employeeList[0].UserName + ")";
                    _p4ChatService.CreateChatService().Clients.Group(P4Consts.EmployeeGroup + entity.TenantId).EmployeeMessage(employee.TrueName + "(" + employee.UserName + ")", message, 4);
                    _p4ChatService.CreateChatService().Clients.Group("index" + entity.TenantId).sendIndexMessage(berthsecName, entity.BerthsecId, employee.Id, entity.Arrearage, 3);
                    _p4ChatService.CreateChatService().Clients.Group("index" + entity.TenantId).sendIndexMessage(berthsecName, entity.BerthsecId, employee.Id, entity.Money, 1);

                    _subscribeAppService.SendMessage("CarInOutManagement", message, "");
                }
                else
                {
                    string message = "出场车辆" + entity.PlateNumber + ",驶入时间:" + entity.CarInTime + ",驶出时间:" + entity.CarOutTime + "停靠" + berthsecName + entity.BerthNumber + "泊位，停车时长:" + StopTimes(entity.StopTime) + ",应收金额:" + entity.Money + "元，预缴金额:" + entity.Prepaid + "元,支付金额:" + entity.FactReceive + "元,操作收费员:" + employee.TrueName + "(" + employee.UserName + ")";
                    _p4ChatService.CreateChatService().Clients.Group(P4Consts.EmployeeGroup + entity.TenantId).EmployeeMessage(employee.TrueName + "(" + employee.UserName + ")", message, 3);
                    _p4ChatService.CreateChatService().Clients.Group("index" + entity.TenantId).sendIndexMessage(berthsecName, entity.BerthsecId, employee.Id, entity.FactReceive, 2);
                    _p4ChatService.CreateChatService().Clients.Group("index" + entity.TenantId).sendIndexMessage(berthsecName, entity.BerthsecId, employee.Id, entity.Money, 1);
                    _subscribeAppService.SendMessage("CarInOutManagement", message, "");
                }

                //黑名单检测，黑名单检测短信报警
                var blackList = Abp.DataProcessHelper.GetEntityFromTable<BlackList>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpBlackList with(nolock) where  IsActive = 1 and RelateNumber = '" + entity.PlateNumber + "'").Tables[0]);
                if (blackList.Count > 0)
                {
                    var blackModel = blackList[0];
                    //黑名单车辆{0},车主姓名{1}，在{2}驶出{3}{4}泊位号，请悉知！
                    var model = new SmsAccountDto() { Msg = string.Format(_smsModelAppService.GetSmsModelByType("BlackCarOutModel").SmsContext, blackModel.RelateNumber, blackModel.CarOwner, Convert.ToDateTime(entity.CarOutTime).ToString("yyyy年MM月dd日hh时mm分"), berthsecName, entity.BerthNumber), Destnumbers = blackModel.IdNumber };
                    _smsManagementAppService.SendSmsNoTenant(model);
                    _subscribeAppService.SendMessage("BlackManagement", model.MsgValue, "");
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StopTime"></param>
        /// <returns></returns>
        private string StopTimes(int? StopTime)
        {
            if (!StopTime.HasValue)
                return "0分钟";
            int Getstoptime = Convert.ToInt32(StopTime);
            TimeSpan ts = new TimeSpan(0, 0, Getstoptime, 0);
            string dateDiff = "";
            if (Getstoptime >= 1440)//判断是否大于24小时
            {
                dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟";
            }
            else
            {
                dateDiff = ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟";
            }
            return dateDiff;
        }

        /// <summary>
        /// 获取白名单
        /// </summary>
        /// <returns></returns>
        public List<WhiteListsDto> GetWhiteListCache(int tenantId, int companyId)
        {
            var whiteList = Abp.DataProcessHelper.GetEntityFromTable<WhiteLists.WhiteList>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpWhiteList with(nolock) where IsActive = 1 and IsDeleted = 0 and TenantId = " + tenantId + " and CompanyId =" + companyId).Tables[0]);
            return whiteList.MapTo<List<WhiteListsDto>>();
        }

        /// <summary>
        /// 获取包月车辆
        /// </summary>
        /// <returns></returns>
        public List<MonthlyCarDto> GetMonthlyCarCache(int tenantId, int companyId)
        {
            var monthlyCarList = Abp.DataProcessHelper.GetEntityFromTable<MonthlyCar>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select * from AbpMonthlyCars with(nolock) where  IsDeleted = 0 and TenantId = " + tenantId + " and CompanyId =" + companyId).Tables[0]);
            return monthlyCarList.MapTo<List<MonthlyCarDto>>();
        }



        /// <summary>
        /// 通讯数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public SensorLog InsertSensorLog(SensorLogInput entity)
        {
            return _sensorLogAppService.Insert(entity.MapTo<SensorLog>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="Exception"></param>
        /// <param name="ReceiptCmd"></param>
        /// <returns></returns>
        public SensorLog InsertSensorLog(string Content, string Exception, string ReceiptCmd)
        {
            return InsertSensorLog(new SensorLogInput() { Content = Content, Exception = Exception, ReceiptCmd = ReceiptCmd });
        }

        /// <summary>
        /// 车辆入场
        /// </summary>
        /// <param name="CarInTime"></param>
        /// <param name="Indicate"></param>
        /// <param name="SensorNumber"></param>
        /// <returns></returns>
        public SensorBusinessDetail InsertCarAdmission(DateTime CarInTime, string Indicate, string SensorNumber)
        {
            return InsertCarAdmission(new SensorBusinessDetail()
            {
                CarInTime = CarInTime,
                Indicate = Indicate,
                SensorNumber = SensorNumber
            });
        }

        /// <summary>
        /// 车辆出场
        /// </summary>
        /// <param name="CarOutTime"></param>
        /// <param name="Indicate"></param>
        /// <param name="SensorNumber"></param>
        /// <returns></returns>
        public SensorBusinessDetail InsertCarEntrance(DateTime CarOutTime, string Indicate, string SensorNumber)
        {
            return InsertCarEntrance(new SensorBusinessDetail()
            {
                CarOutTime = CarOutTime,
                Indicate = Indicate,
                SensorNumber = SensorNumber
            });
        }


        /// <summary>
        /// 车检器复位
        /// </summary>
        /// <param name="TransmitterNumber"></param>
        /// <param name="SensorNumber"></param>
        /// <returns></returns>
        public bool SensorReset(string TransmitterNumber, string SensorNumber)
        {
            //var entry = _sensorRepository.FirstOrDefault(entity => entity.SensorNumber == SensorNumber);
            //if (entry == default(Sensor))
            //{
            //    entry = InsertSensor(0, 0, TransmitterNumber, SensorNumber, 0);
            //    return true;
            //}
            //entry.ParkStatus = 0;
            //_sensorRepository.Update(entry);
            _abpsensorRepository.SensorReset(TransmitterNumber, SensorNumber);
            return true;
        }

        /// <summary>
        /// 根据泊位段ID读取车检器
        /// </summary>
        /// <param name="berthsecid"></param>
        /// <returns></returns>
        public List<SensorDto> GetSensorByBerthsecId(int berthsecid)
        {
            List<SensorDto> sensorList = new List<SensorDto>();
            SensorDto model = new SensorDto();
            var sensorlist = _sensorRepository.GetAll().OrderBy(entity => entity.BerthNumber).Where(s => s.BerthsecId == berthsecid).MapTo<List<SensorDto>>();

            foreach (var entity in sensorlist)
            {
                model = new SensorDto();

                model = entity;
                var berthmodel = _berthAppService.FirstOrDefault(entry => entry.BerthNumber == entity.BerthNumber);
                model.EmplopyeeCarInTime = berthmodel.InCarTime.HasValue == true ? berthmodel.InCarTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                model.EmplopyeeCarOutTime = berthmodel.OutCarTime.HasValue == true ? berthmodel.OutCarTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                model.BerthStatus = berthmodel.BerthStatus;
                model.EmplopyeePlateNumber = berthmodel.RelateNumber;
                sensorList.Add(model);
            }

            return sensorList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<SensorInduciblesDto> GetAllSensor()
        {
            return Abp.DataProcessHelper.GetEntityFromTable<SensorInduciblesDto>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, "select ParkId, ParkStatus from AbpSensors with(nolock) where TenantId = 1").Tables[0]);
            //return _sensorRepository.GetAll().Where(entity => entity.TenantId == 1).MapTo<List<SensorDto>>();
        }

        /// <summary>
        /// 通过基站获取所有车检器设备状态 
        /// </summary>
        /// <param name="TransmitterNumber"></param>
        /// <returns></returns>
        public List<SensorDto> GetSensors(string TransmitterNumber)
        {
            return _sensorRepository.GetAll().Where(entity => entity.TransmitterNumber == TransmitterNumber).MapTo<List<SensorDto>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllSensorsListOutput GetAllSensorsList(SearchSensorsInput input)
        {
            input.TenantId = AbpSession.TenantId;
            return _abpsensorRepository.GetAllSensorList(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetSensorInfoOutPut GetSensorInfoOnlineOrNot(SearchSensorsInput input)
        {
            var list = new List<int>();
            list.Add(-1);
            input.CompanyIds = input.CompanyId.HasValue == false ? AbpSession.CompanyIds ?? list : new List<int> { input.CompanyId.Value };
            return _abpsensorRepository.GetSensorInfoOnlineOrNot(input);
        }
        /// <summary>
        /// 获取基站信息
        /// </summary>
        /// <param name="TransmitterNumber"></param>
        /// <returns></returns>
        public Transmitter GetTransmitterInfo(string TransmitterNumber)
        {
            return _transmitterAppService.FirstOrDefault(entity => entity.TransmitterNumber == TransmitterNumber);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parkId"></param>
        /// <returns></returns>
        public int GetEmptyCount(int parkId)
        {
            return _sensorRepository.GetAll().Where(entity => entity.ParkId == parkId).Count(ent => ent.ParkStatus == 0);
        }

        /// <summary>
        /// 车检器复位
        /// </summary>
        /// <param name="TransmitterNumber"></param>
        /// <param name="SensorNumber"></param>
        /// <returns></returns>
        public SensorDto SensorResetFirstDefault(string TransmitterNumber, string SensorNumber)
        {
            using (
                _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany,
                    AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHaveCompany, AbpDataFilters.MustHaveBerthsec))
            {
                //更新泊位表
                //var berth = _berthAppService.FirstOrDefault(entry1 => entry1.SensorNumber == SensorNumber);
                //if (berth != default(Berth))
                //{
                //    berth.SensorsOutCarTime = DateTime.Now;
                //    berth.SensorBeatTime = DateTime.Now;
                //    berth.ParkStatus = 0;
                //    _berthAppService.Update(berth);
                //}

                _abpsensorRepository.SensorResetUpdateBerthStatus(DateTime.Now, DateTime.Now, 0, SensorNumber);

                //更新车检器表
                var entry = _sensorRepository.FirstOrDefault(entity => entity.SensorNumber == SensorNumber);
                if (entry == default(Sensor))
                {
                    return InsertSensor(0, 0, TransmitterNumber, SensorNumber, 0).MapTo<SensorDto>();
                }
                entry.ParkStatus = 0;
                entry.BeatDatetime = DateTime.Now;
                return _sensorRepository.Update(entry).MapTo<SensorDto>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TransmitterNumber"></param>
        /// <param name="SensorNumber"></param>
        /// <param name="Number"></param>
        /// <param name="Voltage"></param>
        /// <param name="Signal"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        public void InsertSensorMagnetic(string TransmitterNumber, string SensorNumber, int Number, int Voltage, int Signal, int X, int Y, int Z)
        {
            var entity = ProcessSensorStatus(X, Y, Z, SensorNumber);
            entity.Number = Number;
            entity.Signal = Signal;
            entity.Voltage = Voltage;
            InsertSensor(null, Voltage, TransmitterNumber, SensorNumber, entity.Status);
            _sensorMagneticAppService.Insert(entity);
        }

        /// <summary>
        /// 处理车位有车无车状态
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        /// <param name="SensorNumber"></param>
        /// <returns></returns>
        private SensorMagnetic ProcessSensorStatus(int X, int Y, int Z, string SensorNumber)
        {
            var entity = _sensorRepository.FirstOrDefault(p => p.SensorNumber == SensorNumber);
            SensorMagnetic entry = new SensorMagnetic() { X = X, Y = Y, Z = Z, X0 = entity.X, Y0 = entity.Y, Z0 = entity.Z, SensorNumber = SensorNumber };

            if (entity != default(Sensor) && entity.X.HasValue && entity.Y.HasValue && entity.Z.HasValue)
            {
                var val = Math.Sqrt(Math.Pow(X - entity.X.Value, 2) + Math.Pow(Y - entity.Y.Value, 2) + Math.Pow(Z - entity.Z.Value, 2));
                entry.Variance = val;
                if (val >= int.Parse(entity.Range))
                    entry.Status = 1;
                else
                    entry.Status = 0;
            }
            else
                entry.Status = -1;//未知状态，没有初始化磁场
            entry.X0 = entity.X;
            return entry;
        }

        /// <summary>
        /// 基站日志列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetSensorLogOutput GetSensorLogsList(SearchSensorLogInput input)
        {
            int records = _sensorLogAppService.GetAll().Filters(input).Count();
            return new GetSensorLogOutput()
            {
                rows = _sensorLogAppService.GetAll().Filters(input)
                .Orders(input)
                .PageBy(input).ToList().MapTo<List<SensorLogDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 更新地磁电量
        /// </summary>
        /// <param name="sensornumber"></param>
        /// <param name="battary"></param>
        private void UpdateSensorBattary(string sensornumber, string battary)
        {
            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, CommandType.Text,
               "update AbpSensors set Battery = " + battary + ", Updatetime = getdate(), BeatDatetime = getdate() where SensorNumber = '" + sensornumber + "'");
        }

        /// <summary>
        /// 车辆出场
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public string SendDeviceByINmotion(INmotionDto dto)
        {
            switch (dto.cmd)
            {
                case "sendParkStatu":
                    if (dto.body.parkingStatu == "1")
                    {
                        InsertCarAdmission(DateTime.ParseExact(dto.body.time, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture), dto.body.sequence, dto.body.deviceID);//车辆进场
                    }
                    else
                    {
                        InsertCarEntrance(DateTime.ParseExact(dto.body.time, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture), dto.body.flag == true ? (int.Parse(dto.body.sequence) - 1).ToString() : dto.body.sequence, dto.body.deviceID);//车辆出场
                    }
                    UpdateSensorBattary(dto.body.deviceID, dto.body.battary);
                    break;
                case "sendDeviceHeartbeat":
                    decimal battary = 0;
                    if (!string.IsNullOrWhiteSpace(dto.body.battary))
                    {
                        battary = decimal.Parse(dto.body.battary);
                    }
                    InsertSensor(null, battary, "1", dto.body.deviceID, short.Parse(dto.body.parkingStatu));
                    InsertTransmitter("1", 0);
                    break;
                case "SendRegister":
                    SensorReset("1", dto.body.deviceID);
                    break;
                default:
                    return "{\"code\":101, \"body\":{\"msg\":\"失败\"}} ";
            }
            return "{\"code\":100, \"body\":{\"msg\":\"成功\"}} ";
        }

        /// <summary>
        /// 设备注册信息
        /// </summary>
        /// <param name="deviceID"></param>
        private void DeviceRegister(string deviceID)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetSensorPosOutPut GetSensorPosList(SearchSensorPosInput input)
        {
            string sqlwhere = " and CreationTime > '" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            if (!string.IsNullOrWhiteSpace(input.filters))
            {
                sqlwhere = "";
                FilterDto _filterDto = JsonConvert.DeserializeObject(input.filters, typeof(FilterDto)) as FilterDto;
                foreach (var entity in _filterDto.rules)
                {
                   
                    switch (entity.field)
                    {
                        case "ParkName":
                            sqlwhere += " and ParkId = " + entity.data;
                            break;
                        case "BerthsecName":
                            sqlwhere += " and BerthsecId = " + entity.data;
                            break;
                        case "BerthNumber":
                            sqlwhere += " and BerthNumber = '" + entity.data + "'";
                            break;
                        case "SensorNumber":
                            sqlwhere += " and SensorNumber = '" + entity.data + "'";
                            break;
                        case "CreationTime":
                            if (entity.op == "lt")//小于
                            {
                                sqlwhere += " and CreationTime < '" + entity.data + "'";
                            }
                            else if (entity.op == "gt") {//大于
                                sqlwhere += " and CreationTime > '" + entity.data + "'";
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            if (AbpSession.TenantId.HasValue)
                sqlwhere += " and TenantId = " + AbpSession.TenantId.Value;
            if (AbpSession.CompanyId.HasValue)
                sqlwhere += " and CompanyId = " + AbpSession.CompanyId.Value;
            string sql = "select RegionName, RowNumber, convert(varchar(50), guid) as guid, StopType, InOperaId, ParkName, PlateNumber, BerthsecName, Indicate, BerthNumber, SensorNumber, PosCarInTime, PosCarOutTime, SensorCarInTime, SensorCarOutTime, CreationTime, PosStopTime, SensorStopTime, PosMoney, SensorMoney  from ( select *, ROW_NUMBER() OVER(Order by  BerthNumber Asc, CreationTime desc) AS RowNumber from (select RegionName, ParkName, BerthsecName, A.StopTime as PosStopTime, B.StopTime as SensorStopTime, A.Money as PosMoney, B.Receivable as SensorMoney, Indicate, case when A.BerthNumber is not null then A.BerthNumber else B.BerthNumber end as BerthNumber, case when A.guid is not null then A.guid else B.guid end as guid, A.PlateNumber, CONVERT(varchar(21),  Case when A.CreationTime is null then B.CreationTime else A.CreationTime end, 120) as CreationTime, CONVERT(varchar(21), A.CarInTime, 120) as PosCarInTime, CONVERT(varchar(21), A.CarOutTime, 120) as PosCarOutTime, CONVERT(varchar(21), B.CarInTime, 120) as SensorCarInTime, CONVERT(varchar(21), B.CarOutTime, 120) as SensorCarOutTime, B.SensorNumber,A.InOperaId , A.StopType from(select * from AbpBusinessDetail with(nolock) where IsDeleted = 0 " + sqlwhere + ") as A full join(select * from abpsensorbusinessdetail with(nolock) where 1 = 1 " + sqlwhere + ") as B  on A.guid = B.guid left join AbpRegions on AbpRegions.id = A.RegionId left join AbpParks on (case when  A.ParkId is null then B.parkid else A.parkid end) = AbpParks.Id left join AbpBerthsecs on AbpBerthsecs.Id = (case when  A.BerthsecId is null then B.BerthsecId else A.BerthsecId end)) AS B) AS D where RowNumber between @BeginSize and @EndSize";



            string sqlother = "select count(1) as count from(select * from AbpBusinessDetail  with(nolock) where IsDeleted = 0 " + sqlwhere + ") as A full join(select * from abpsensorbusinessdetail  with(nolock) where 1 = 1 " + sqlwhere + ") as B  on A.guid = B.guid";
            int records = int.Parse(SqlHelper.ExecuteScalar(SqlHelper.connectionString, CommandType.Text, sqlother).ToString());

            List<SqlParameter> SqlParam = new List<SqlParameter>();
            SqlParam.Add(new SqlParameter("@BeginSize", input.page * input.rows - input.rows + 1));
            SqlParam.Add(new SqlParameter("@EndSize", input.page * input.rows));

            List<SearchSensorPosDto> listtemp = Abp.DataProcessHelper.GetEntityFromTable<SearchSensorPosDto>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, sql, SqlParam.ToArray()).Tables[0]);
            //select * from AbpBusinessDetail  with(nolock) where IsDeleted = 0 " + sqlwhere + "
            List<SearchSensorPosDto> list = new List<SearchSensorPosDto>();
            foreach (var dto in listtemp)
            {
                if (!string.IsNullOrWhiteSpace(dto.SensorCarInTime) && string.IsNullOrWhiteSpace(dto.SensorCarOutTime) && !string.IsNullOrWhiteSpace(dto.PosCarOutTime))
                {
                    dto.PosCarOutTime = dto.PosCarOutTime + "&";
                }
                if (!string.IsNullOrWhiteSpace(dto.SensorCarInTime) && !string.IsNullOrWhiteSpace(dto.PosCarInTime))
                {
                    TimeSpan ts1 = new TimeSpan(DateTime.Parse(dto.CreationTime).Ticks);
                    TimeSpan ts2 = new TimeSpan(DateTime.Parse(dto.PosCarInTime).Ticks);
                    TimeSpan ts = ts1.Subtract(ts2).Duration();
                    if (ts.TotalMinutes > 15)
                        dto.PosCarInTime = dto.PosCarInTime + "&";
                }
                if (dto.InOperaId != 0)
                    dto.InOperaName = _employeeAppService.Load(dto.InOperaId).TrueName;

                list.Add(dto);
            }

            int temp = 0;
            int.TryParse(SqlHelper.ExecuteScalar(SqlHelper.connectionString, CommandType.Text, "select count(1) as count from AbpBusinessDetail  with(nolock) where IsDeleted = 0 " + sqlwhere).ToString(), out temp);
            int PosCount = temp;
            temp = 0;
            int.TryParse(SqlHelper.ExecuteScalar(SqlHelper.connectionString, CommandType.Text, "select count(1) as count from abpsensorbusinessdetail  with(nolock) where 1 = 1  " + sqlwhere).ToString(), out temp);
            int SensorCount = temp;
            temp = 0;
            int.TryParse(SqlHelper.ExecuteScalar(SqlHelper.connectionString, CommandType.Text, "select sum(StopTime) as StopTime from AbpBusinessDetail  with(nolock) where IsDeleted = 0 " + sqlwhere).ToString(), out temp);
            int PosStopTime = temp;
            temp = 0;
           int.TryParse(SqlHelper.ExecuteScalar(SqlHelper.connectionString, CommandType.Text, "select sum(StopTime) as StopTime from abpsensorbusinessdetail  with(nolock) where 1 = 1  " + sqlwhere).ToString(), out temp);
            int SensorStopTime = temp;
            decimal temp1 = 0;
            decimal.TryParse(Abp.Helper.SqlHelper.ExecuteScalar(SqlHelper.connectionString, CommandType.Text, "select sum(Money) as PosMoney from AbpBusinessDetail  with(nolock) where IsDeleted = 0 " + sqlwhere).ToString(), out temp1);
            decimal PosMoney = temp1;
            decimal.TryParse(Abp.Helper.SqlHelper.ExecuteScalar(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select sum(Receivable) as SensorMoney from abpsensorbusinessdetail  with(nolock) where 1 = 1  " + sqlwhere).ToString(), out temp1);
            decimal SensorMoney = temp1;
            return new GetSensorPosOutPut()
            {
                rows = list,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0),
                userdata = new SensorPosUserData() { ParkName = "汇总", PosCarInTime = "Pos记录", PosStopTime = PosStopTime, SensorStopTime = SensorStopTime, PosCarOutTime = PosCount.ToString() + "条", SensorCarInTime = "地磁记录", SensorCarOutTime = SensorCount.ToString() + "条", SensorMoney = SensorMoney, PosMoney = PosMoney }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <param name="device"></param>
        /// <param name="flag"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public string SendDeviceByINmotionTest(string time, string device, string flag, string sequence)
        {
            if (flag == "1")
                InsertCarAdmission(DateTime.ParseExact(time, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture), sequence, device);//车辆进场
            else
                InsertCarEntrance(DateTime.ParseExact(time, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture), sequence, device);//车辆出场
            return "{\"code\":100, \"body\":{\"msg\":\"成功\"}} ";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetSensorPosOutPut GetSensorBerthList(SearchSensorPosInput input)
        {
            var entity = from b in _berthAppService.GetAll()
                         join s in _abpsensorRepository.GetAll() on b.guid equals s.Guid
                         select new SearchSensorPosDto
                         {
                             ParkName = b.Park.ParkName,
                             BerthsecName = b.BerthsecId.ToString(),
                             BerthNumber = b.BerthNumber,
                             PlateNumber = b.RelateNumber,
                             PosCarInTime = b.InCarTime.ToString(),
                             PosCarOutTime = b.OutCarTime.ToString(),
                             PosStopTime = 9,
                             PosMoney = 0,
                             SensorCarInTime = s.InCarTime.ToString(),
                             SensorCarOutTime = s.OutCarTime.ToString(),
                             SensorMoney = 0,
                             SensorNumber = s.SensorNumber,
                             Indicate = "0",
                             CreationTime = b.LastModificationTime.ToString()
                         };
            return new GetSensorPosOutPut
            {
                rows = entity.ToList(),
                records = 0
            };
        }

        /// <summary>
        /// Post地磁请求，gcj修改地磁
        /// </summary>
        /// <returns></returns>
        public string SendDeviceByINmotion()
        {            
            Stream stream = System.Web.HttpContext.Current.Request.InputStream;
            byte[] vs = new byte[stream.Length];
            stream.Read(vs, 0, (int)stream.Length+1);
            var parameters = System.Text.Encoding.UTF8.GetString(vs);
            Newtonsoft.Json.Linq.JObject jsonObject = Newtonsoft.Json.Linq.JObject.Parse(parameters);
            string cmd = jsonObject["cmd"].ToString();
            string flag = jsonObject["body"]["flag"].ToString();
            string parkingStatu = jsonObject["body"]["parkingStatu"].ToString();
            string time = jsonObject["body"]["time"].ToString();
            string deviceID = jsonObject["body"]["deviceID"].ToString();
            string sequence = jsonObject["body"]["sequence"].ToString();
            string battary = jsonObject["body"]["battary"].ToString();
            /*string[] sArray = parameters.Split(',');
            string cmd = sArray[0].Split(':')[1];
            string flag = sArray[10].Split(':')[1];
            string parkingStatu = sArray[9].Split(':')[1];
            string time = sArray[2].Split(':')[1];
            string deviceID = sArray[1].Split(':')[1];
            string sequence = sArray[8].Split(':')[1];
            string battary = sArray[4].Split(':')[1];*/


            bool flags = true;
            if (flag == "False")
            {
                flags = false;
            }
            switch (cmd)
            {
                case "sendParkStatu":
                    if (parkingStatu == "1")
                    {
                        InsertCarAdmission(DateTime.ParseExact(time, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture), sequence, deviceID);//车辆进场
                    }
                    else
                    {
                        //  InsertCarEntrance(DateTime.ParseExact(time, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture), flags == true ? (int.Parse(sequence) - 1).ToString() : sequence, deviceID);//车辆出场
                        InsertCarEntrance(DateTime.ParseExact(time, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture), flags == true ? (int.Parse(sequence) - 1).ToString() : sequence, deviceID);//车辆出场
                    }
                    UpdateSensorBattary(deviceID, battary);
                    break;
                case "sendDeviceHeartbeat":
                    decimal battarys = 0;
                    if (!string.IsNullOrWhiteSpace(battary))
                    {
                        battarys = decimal.Parse(battary);
                    }
                    InsertSensor(null, battarys, "1", deviceID, short.Parse(parkingStatu));
                    InsertTransmitter("1", 0);
                    break;
                case "SendRegister":
                    SensorReset("1", deviceID);
                    break;
                default:
                    return "{\"code\":101, \"body\":{\"msg\":\"失败\"}} ";
            }
            return "{\"code\":100, \"body\":{\"msg\":\"成功\"}} ";
        }
    }
}
