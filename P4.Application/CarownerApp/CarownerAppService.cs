/********************************************************************************
** auth： 黎通
** date： 2015/12/16 20:24:50
** desc： 尚未编写描述
** Ver.:  V1.0.0
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.App;
using P4.SmsManagements;
using P4.Businesses;
using P4.Berths;
using Abp.Domain.Uow;
using P4.OtherAccounts;
using P4.Berthsecs;
using P4.SmsManagements.Dtos;
using Abp.Authorization;
using P4.CarownerApp.Dtos;
using Abp.AutoMapper;
using System.Web;
using System.Data;
using Abp;
using P4.OtherAccounts.Dtos;
using P4.OtherPlateNumbers;
using P4.Weixin;

namespace P4.CarownerApp
{
    /// <summary>
    /// 车主端接口
    /// </summary>
    public class CarownerAppService : ApplicationService, ICarownerAppService
    {
        #region Var
        private readonly IRepository<AppAccount, long> _abpAppAccountRepository;
        private readonly ISmsManagementAppService _smsManagementAppService;
        private readonly IRepository<BusinessDetail, long> _abpBusinessDetailRepositroy;
        private readonly IRepository<Berth, long> _abpBerthRepository;
        private readonly IRepository<Berthsec> _abpBerthsecRepository;
        private readonly IRepository<DeductionRecord, long> _abpDeductionRecordRepository;
        private readonly IRepository<OtherAccount, long> _abpOtherAccountRepository;
        private readonly IRepository<OtherPlateNumber, long> _abpOtherPlateNumberRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ISmsModelAppService _smsModelAppService;
        private readonly IRepository<WeixinConfig> _abpweixinConfigRepository;
        private readonly IRepository<Dictionarys.DictionaryValue> _abpDictionaryValueRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpAppAccountRepository"></param>
        /// <param name="smsManagementAppService"></param>
        /// <param name="abpBusinessDetailRepositroy"></param>
        /// <param name="abpBerthRepository"></param>
        /// <param name="abpBerthsecRepository"></param>
        /// <param name="abpDeductionRecordRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="abpOtherAccountRepository"></param>
        /// <param name="abpOtherPlateNumberRepository"></param>
        /// <param name="smsModelAppService"></param>
        /// <param name="abpweixinConfigRepository"></param>
        /// <param name="abpDictionaryValueRepository"></param>
        public CarownerAppService(IRepository<AppAccount, long> abpAppAccountRepository, ISmsManagementAppService smsManagementAppService,
            IRepository<BusinessDetail, long> abpBusinessDetailRepositroy, IRepository<Berth, long> abpBerthRepository,
            IRepository<Berthsec> abpBerthsecRepository, IRepository<DeductionRecord, long> abpDeductionRecordRepository, IUnitOfWorkManager unitOfWorkManager, IRepository<OtherAccount, long> abpOtherAccountRepository, IRepository<OtherPlateNumber, long> abpOtherPlateNumberRepository, ISmsModelAppService smsModelAppService, IRepository<WeixinConfig> abpweixinConfigRepository, IRepository<Dictionarys.DictionaryValue> abpDictionaryValueRepository)
        {
            _abpAppAccountRepository = abpAppAccountRepository;
            _smsManagementAppService = smsManagementAppService;
            _abpBusinessDetailRepositroy = abpBusinessDetailRepositroy;
            _abpBerthRepository = abpBerthRepository;
            _abpBerthsecRepository = abpBerthsecRepository;
            _abpDeductionRecordRepository = abpDeductionRecordRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _abpOtherAccountRepository = abpOtherAccountRepository;
            _abpOtherPlateNumberRepository = abpOtherPlateNumberRepository;
            _smsModelAppService = smsModelAppService;
            _abpweixinConfigRepository = abpweixinConfigRepository;
            _abpDictionaryValueRepository = abpDictionaryValueRepository;
        }

        /// <summary>
        /// 获取停车场详细信息
        /// </summary>
        /// <param name="parkId"></param>
        /// <returns></returns>
        public ParkDto GetParkInfo(int parkId)
        {
            string sql = "select AbpParks.Id, ParkName as name, X as lng, Y as lat, C.BerthCount as total, B.free as free, mark as [desc] from AbpParks with(nolock) left join (select parkid,  count(1) as free from AbpBerths with(nolock) where ParkStatus = 0 and BerthsecId <> -1 group by parkid) as B on B.ParkId = AbpParks.id left join (select parkid,count(1) as BerthCount from AbpBerths with(nolock) where BerthsecId <> -1 group by parkid) as C on C.ParkId = AbpParks.id where IsDeleted = 0 and AbpParks.Id = " + parkId;
            string sql1 = "select BerthNumber as berthslist, ParkStatus as state from AbpBerths left join AbpParks on AbpParks.Id = AbpBerths.ParkId where AbpParks.Id = " + parkId;

            DataTable dt = Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql).Tables[0];
            DataTable dt1 = Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql1).Tables[0];
            ParkDto dto = DataProcessHelper.GetEntityFromTable<ParkDto>(dt)[0];
            dto.berthslist = DataProcessHelper.GetEntityFromTable<AppBerthListDto>(dt1);
            return dto;

        }


        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="userPhone"></param>
        /// <returns></returns>
        public string GetPhoneCode(string userPhone)
        {
            if (string.IsNullOrEmpty(userPhone))
                return "1";
            Random rm = new Random();
            string phoneCode = "";
            for (int i = 0; i < 4; i++)
            {
                phoneCode += rm.Next(0, 9).ToString();
            }
            AppAccount appAccount = _abpAppAccountRepository.FirstOrDefault(entity => entity.Phone == userPhone && entity.IsLock == false);

            if (appAccount == null || appAccount == default(AppAccount))
            {
                AppAccount model = new AppAccount
                {
                    UserName = userPhone,
                    Password = phoneCode,
                    Phone = userPhone,
                    PhoneCode = phoneCode,
                    CodeTime = DateTime.Now,
                    Wallet = 0,
                    //VersionNum = null,
                    IsLock = false,
                    CreationTime = DateTime.Now,
                    Status1 = false,
                    Status2 = false,
                    Status3 = false
                };

               var modelsss = _abpAppAccountRepository.Insert(model);            
            }
            else
            {
                appAccount.Password = phoneCode;
                appAccount.PhoneCode = phoneCode;
                appAccount.CodeTime = DateTime.Now;
                appAccount.LastModificationTime = DateTime.Now;
                _abpAppAccountRepository.Update(appAccount);
            }

            OtherAccount extOtherAccount = _abpOtherAccountRepository.FirstOrDefault(param => param.TelePhone == userPhone && param.IsLock == false && param.IsActive == true);
            if(extOtherAccount == null)
            {
                OtherAccount othermodel = new OtherAccount
                {
                    AuthenticationSource = "APP注册",
                    Client = "APP",
                    UserName = userPhone,
                    Name = userPhone,
                    Password = "123456",
                    TelePhone = userPhone,
                    IsPhoneConfirmed = true,
                    AccountLoginDatetime = DateTime.Now,
                    IsActive = true,
                    CardNo = userPhone,
                    Wallet = 0,
                    IsLock = false,
                    IsEnabled = true,
                    EnabledTime = DateTime.Now,
                    EnabledUserId = AbpSession.UserId,
                    ProductNo = userPhone,
                    SendSmsDatetime = DateTime.Now,
                    SendSmsNumber = 0
                };
                _abpOtherAccountRepository.Insert(othermodel).MapTo<OtherAccountDto>();
            }

            _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto()
            {
                Destnumbers = userPhone,
                Msg = string.Format(_smsModelAppService.GetSmsModelByType("RegisterAppModel").SmsContext, phoneCode)
                //Msg = string.Format(P4Consts.RegisterModel, deductionRecord.InTime.ToString("yyyy年MM月dd日hh时mm分"), returnOtherAccountModel.Wallet)
            });
            return "0";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="phonecode"></param>
        /// <returns></returns>
        public string AppLogin(string username, string phonecode)
        {
            AppAccount appaccount = _abpAppAccountRepository.FirstOrDefault(entity => entity.UserName == username && entity.IsLock == false);
            if (appaccount == default(AppAccount))
                return "0";
            if (appaccount.PhoneCode != phonecode)
                return "0";
            DateTime nowdt = DateTime.Now;
            if (nowdt - appaccount.CodeTime > new TimeSpan(0, 5, 0))
            {
                return "1";
            }
            appaccount.LastModificationTime = DateTime.Now;
            _abpAppAccountRepository.Update(appaccount);
            return "1";
            //_abpAppAccountRepository.FirstOrDefault(entity => entity.UserName==username&&entity.Password==password&&entity.IsLock==false);
            // _abpAppAccountRepository.FirstOrDefault(entity => entity.UserName == username && entity.PhoneCode == phonecode && entity.IsLock == false&&entity);
        }

        /// <summary>
        /// 车辆入场
        /// </summary>
        /// <param name="BerthNumber">泊位号</param>
        /// <param name="BerthSecId">泊位段</param>
        /// <param name="PlateNumber">车牌号</param>
        /// <param name="CarType">车类型</param>
        /// <param name="Prepaid">预付费</param>
        /// <param name="CarInTime">进场时间</param>
        /// <param name="guid">GUID</param>
        /// <param name="SensorsInCarTime">车检器入场时间</param>
        /// <param name="StopType">停车类型</param>       
        /// <param name="UserName">车主</param>
        /// <returns></returns>
        public bool InsertCarIn(string BerthNumber, long BerthSecId, string PlateNumber, string CarType, string Prepaid, string CarInTime, string guid, string SensorsInCarTime, string StopType, string UserName)
        {
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveCompany, AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark, AbpDataFilters.MustHaveBerthsec);
            Guid g = new Guid(guid);
            //Guid chen = Guid.NewGuid();
            int count = 0;
            if (count > 0)
            {
                return false;
            }
            Berthsec berthsec = _abpBerthsecRepository.FirstOrDefault(bb => bb.Id == BerthSecId);
            if (berthsec == default(Berthsec))
                return false;
            //车主扣费
            AppAccount appAccount = _abpAppAccountRepository.FirstOrDefault(aa => aa.UserName == UserName && aa.IsLock == false);
            if (appAccount == default(AppAccount))
                return false;

            appAccount.LastModificationTime = DateTime.Now;
            appAccount.Wallet -= Convert.ToDecimal(Prepaid);
            _abpAppAccountRepository.Update(appAccount);

            DeductionRecord deductionrecord = new DeductionRecord()
            {
                OtherAccountId = appAccount.Id,
                OperType = 3,
                Money = Convert.ToDecimal(Prepaid),
                PayStatus = true,
                Remark = "车主端预付",
                InTime = DateTime.Now
            };

            BusinessDetail entity = new BusinessDetail()
            {
                BerthNumber = BerthNumber,//泊位号 
                PlateNumber = PlateNumber,//车牌号
                CarType = Convert.ToInt16(CarType),//车辆类型
                Prepaid = Convert.ToDecimal(Prepaid),//预付费
                CarInTime = Convert.ToDateTime(CarInTime),//车辆入场时间
                //InOperaId = AbpSession.UserId.Value,//入场收费员ID
                //InDeviceCode = AbpSession.DeviceCode,//入场设备
                InOperaId = Convert.ToInt64(berthsec.CheckInEmployeeId),//入场收费员ID
                InDeviceCode = berthsec.CheckInDeviceCode,//入场设备
                guid = new Guid(guid),//
                SensorsInCarTime = Convert.ToDateTime(SensorsInCarTime),//车检器入场时间
                StopType = Convert.ToInt16(StopType),//停车类型（是否为包月车）
                //RegionId = AbpSession.RegionIds[0],//区域ID
                //ParkId = AbpSession.ParkIds[0],//停车场ID
                //BerthsecId = AbpSession.BerthsecIds[0],//泊位段ID
                RegionId = berthsec.RegionId,//区域ID
                ParkId = berthsec.ParkId,//停车场ID
                BerthsecId = berthsec.Id,//泊位段ID
                AppAccountId = appAccount.Id,
                Status = 1
            };

            _abpBusinessDetailRepositroy.Insert(entity);

            //车辆入场处理泊位表
            var berth = _abpBerthRepository.FirstOrDefault(a => a.BerthNumber == BerthNumber && a.BerthsecId == BerthSecId);
            if (berth != null)
            {
                berth.BerthStatus = "1";  //泊位状态
                //berth.LastModificationTime = DateTime.Now; //最后修改时间
                //berth.LastModifierUserId = AbpSession.UserId;//最后修改用户
                berth.RelateNumber = PlateNumber;//车牌号
                berth.InCarTime = Convert.ToDateTime(CarInTime);//车辆入场时间
                //berth.SensorsInCarTime = Convert.ToDateTime(CarInTime);//车检器入场时间
                berth.guid = g;
                berth.CarType = Convert.ToInt16(CarType);  //车型
                //berth.ParkStatus = 1;//车检器停车状态
                berth.CardNo = UserName;//卡号
                berth.Prepaid = Convert.ToDecimal(Prepaid);//预付费
                _abpBerthRepository.Update(berth);
            }
            //预付  短信发送
            //_smsManagementAppService.SendSms(new SmsAccountDto() { Destnumbers =otherAccount.TelePhone, Msg = string.Format(P4Consts.PaymentModel,Convert.ToDateTime(CarInTime).ToString("yyyy年MM月dd日hh时mm分"),BerthNumber,Prepaid) });
            //}

            return true;
        }
        /// <summary>
        /// 车辆出场
        /// </summary>
        /// <param name="Receivable">应收</param>
        /// <param name="FactReceive">实收</param>
        /// <param name="CarOutTime">车辆出场时间</param>
        /// <param name="CarPayTime">支付时间</param>
        /// <param name="guid">停车记录Guid</param>
        /// <param name="SensorsOutCarTime">车检器出场时间</param>
        /// <param name="SensorsStopTime">车检器停车时长</param>
        /// <param name="SensorsReceivable">车检器应收</param>
        /// <param name="PayStatus">支付类型 1.现金 2.刷卡 3.在线支付 4.MOne卡</param>
        /// <param name="IsPay">是否支付</param>
        /// <param name="FeeType">费用类型（1.正常收费，2.追缴收费）</param>
        /// <param name="StopTime">停车时长</param>
        /// <param name="Money">收费金额</param>
        /// <param name="UserName">车主用户名</param>
        /// <returns></returns>
        public string InsertCarOut(decimal Receivable, string FactReceive, string CarOutTime, string CarPayTime, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money, string UserName)
        {
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveCompany, AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark, AbpDataFilters.MustHaveBerthsec);
            Guid g = new Guid(guid);
            BusinessDetail entity = new BusinessDetail();
            entity = _abpBusinessDetailRepositroy.FirstOrDefault(b => b.guid == g && b.Status == 1);
            AppAccount appAccount = _abpAppAccountRepository.FirstOrDefault(aa => aa.UserName == UserName && aa.IsLock == false);
            if (appAccount == default(AppAccount))//?????????
                return null;
            if (entity != null)
            {

                Berthsec berthsec = _abpBerthsecRepository.FirstOrDefault(b => b.Id == entity.BerthsecId);

                entity.OutDeviceCode = AbpSession.DeviceCode;               //出场设备
                //entity.Receivable = Convert.ToDecimal(Receivable);        //应收
                entity.Receivable = Receivable;                             //出场应收 
                entity.FactReceive = Convert.ToDecimal(FactReceive);        //实收
                //entity.Arrearage = Convert.ToDecimal(Arrearage);          //欠费
                //entity.ChargeOperaId = AbpSession.UserId;                   //收费操作员Id
                //entity.ChargeDeviceCode = AbpSession.DeviceCode;            //收费员设备
                entity.ChargeOperaId = berthsec.CheckInEmployeeId;                   //收费操作员Id?????????
                entity.ChargeDeviceCode = berthsec.CheckInDeviceCode;            //收费员设备????????
                entity.CarOutTime = Convert.ToDateTime(CarOutTime);         //出场时间
                entity.CarPayTime = Convert.ToDateTime(CarPayTime);         //支付时间
                //entity.OutOperaId = Convert.ToInt16(AbpSession.UserId);     //出场收费员ID
                entity.OutOperaId = Convert.ToInt16(berthsec.CheckInEmployeeId);     //出场收费员ID
                entity.guid = new Guid(guid);                               //唯一guid
                entity.SensorsOutCarTime = Convert.ToDateTime(SensorsOutCarTime);//车检器出场时间
                entity.SensorsStopTime = Convert.ToInt32(SensorsStopTime);//车检器停车时长
                entity.SensorsReceivable = Convert.ToDecimal(SensorsReceivable);//车检器应收
                entity.Money = Money;//实际应收金额
                entity.IsPay = Convert.ToBoolean(IsPay);//是否支付
                entity.AppAccountId = appAccount.Id;
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
                    entity.PayStatus = Convert.ToInt16(PayStatus);  /// 支付类型 1.现金 2.刷卡 3.在线支付 4.MOne卡
                }
                entity.FeeType = Convert.ToInt16(FeeType);//费用类型（1.正常收费，2.追缴收费）
                entity.StopTime = Convert.ToInt32(StopTime);//停车时长

                _abpBusinessDetailRepositroy.Update(entity);
                //车辆出场处理泊位表
                var berth = _abpBerthRepository.FirstOrDefault(a => a.BerthNumber == entity.BerthNumber);
                if (berth != null)
                {
                    berth.BerthStatus = "2";  //泊位状态
                    // berth.LastModificationTime = DateTime.Now; //最后修改时间
                    //berth.LastModifierUserId = AbpSession.UserId;//最后修改用户
                    berth.RelateNumber = entity.PlateNumber;//车牌号
                    berth.OutCarTime = Convert.ToDateTime(CarOutTime);//车辆出场时间
                    //berth.SensorsInCarTime = Convert.ToDateTime(CarOutTime);//车检器出场时间
                    //berth.ParkStatus = 2;//车检器停车状态
                    // berth.CardNo = entity.c;//卡号
                    // berth.Prepaid = Convert.ToDecimal(Prepaid);//预付费
                    _abpBerthRepository.Update(berth);
                }
                //车主扣费
                if (Money == entity.Prepaid) //消费金额等于预缴金额  添加消费记录
                {
                    //添加消费记录
                    DeductionRecord deductionrecord = new DeductionRecord()
                    {
                        OtherAccountId = appAccount.Id,
                        OperType = 2,
                        Money = Money - entity.Prepaid,  //消费金额
                        PayStatus = true,
                        Remark = "车主端消费",
                        InTime = DateTime.Now
                    };
                    _abpDeductionRecordRepository.Insert(deductionrecord);


                    //消费金额 以及账户余额（短信发送）
                    _smsManagementAppService.SendSmsNoTenant(new SmsAccountDto()
                    {
                        Destnumbers = appAccount.Phone,
                        Msg = string.Format(_smsModelAppService.GetSmsModelByType("ConsumptionEqualModel").SmsContext, Convert.ToDateTime(CarOutTime).ToString("yyyy年MM月dd日hh时mm分"), entity.BerthNumber, entity.Prepaid, entity.PlateNumber, appAccount.Wallet)
                    });

                    appAccount.LastModificationTime = DateTime.Now;
                    appAccount.Wallet -= Convert.ToDecimal(deductionrecord.Money);
                    _abpAppAccountRepository.Update(appAccount);


                }
                else if (Money < entity.Prepaid)  //消费金额小于预付  （返还）
                {
                    //添加返还消费记录
                    DeductionRecord dedu = new DeductionRecord()
                    {
                        OtherAccountId = appAccount.Id,
                        OperType = 4,
                        Money = entity.Prepaid - Money,  //消费金额
                        PayStatus = true,
                        Remark = "返还",
                        InTime = DateTime.Now
                    };
                    _abpDeductionRecordRepository.Insert(dedu);
                    //返回金额 以及账户余额（短信发送）
                    _smsManagementAppService.SendSms(new SmsAccountDto()
                    {
                        Destnumbers = appAccount.Phone,
                        Msg = string.Format(_smsModelAppService.GetSmsModelByType("ConsumptionGreaterModel").SmsContext, Convert.ToDateTime(CarOutTime).ToString("yyyy年MM月dd日hh时mm分"), entity.BerthNumber, entity.Prepaid, entity.PlateNumber, appAccount.Wallet + dedu.Money)
                    });

                    appAccount.LastModificationTime = DateTime.Now;
                    appAccount.Wallet += Convert.ToDecimal(dedu.Money);
                    _abpAppAccountRepository.Update(appAccount);
                }
                else if (Money > entity.Prepaid && entity.Status != 5 && entity.Status != 3)  //消费金额大于预付（消费）
                {
                    //添加消费记录
                    DeductionRecord deductionrecord = new DeductionRecord()
                    {
                        OtherAccountId = appAccount.Id,
                        OperType = 2,
                        Money = Money - entity.Prepaid,  //消费金额
                        PayStatus = true,
                        Remark = "消费",
                        InTime = DateTime.Now
                    };
                    _abpDeductionRecordRepository.Insert(deductionrecord);
                    //消费金额 以及账户余额（短信发送）
                    _smsManagementAppService.SendSms(new SmsAccountDto()
                    {
                        Destnumbers = appAccount.Phone,
                        Msg = string.Format(_smsModelAppService.GetSmsModelByType("ConsumptionLessModel").SmsContext, Convert.ToDateTime(CarOutTime).ToString("yyyy年MM月dd日hh时mm分"), entity.BerthNumber, entity.Prepaid, entity.PlateNumber, appAccount.Wallet - deductionrecord.Money)
                    });

                    appAccount.LastModificationTime = DateTime.Now;
                    appAccount.Wallet -= Convert.ToDecimal(deductionrecord.Money);
                    _abpAppAccountRepository.Update(appAccount);
                }

                return "成功";
            }
            return "成功";
        }

        //下载进场记录
        //现在数据库没有车牌号 没办法写车牌查询

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PlateNumber"></param>
        public void QueryCarInRecord(string UserName, string PlateNumber)
        {
            if (UserName != "null")
            {
                AppAccount appAccount = _abpAppAccountRepository.FirstOrDefault(entity => entity.UserName == UserName && entity.IsLock == false);

                if (appAccount == default(AppAccount))
                    return;
                List<BusinessDetail> businessdetailList = _abpBusinessDetailRepositroy.GetAll().Where(entity => entity.AppAccountId == appAccount.Id).ToList();
            }

        }
        //欠费查询  不完整 ？？？？？

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PlateNumber"></param>
        public void QueryArrearage(string UserName, string PlateNumber)
        {
            if (UserName != "null")
            {
                AppAccount appAccount = _abpAppAccountRepository.FirstOrDefault(entity => entity.UserName == UserName && entity.IsLock == false);

                if (appAccount == default(AppAccount))
                    return;
                List<BusinessDetail> businessdetailList = _abpBusinessDetailRepositroy.GetAll().Where(entity => entity.AppAccountId == appAccount.Id && entity.Status == 3).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Repayment"></param>
        /// <param name="CarRepaymentTime"></param>
        /// <param name="IsEscapePay"></param>
        /// <param name="EscapePayStatus"></param>
        /// <param name="PaymentType"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string FeeBack(int id, decimal Repayment, DateTime CarRepaymentTime, bool IsEscapePay, short EscapePayStatus, short PaymentType, string userName)
        {
            BusinessDetail entity = new BusinessDetail();
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MustHaveCompany, AbpDataFilters.MustHaveRegion, AbpDataFilters.MustHavePark, AbpDataFilters.MustHaveBerthsec);

            entity = _abpBusinessDetailRepositroy.FirstOrDefault(b => b.Id == id);

            if (entity != null)
            {

                if (entity.Arrearage != 0)  //有欠费
                {
                    Berthsec berthsec = _abpBerthsecRepository.FirstOrDefault(bsr => bsr.Id == entity.BerthsecId);
                    entity.Repayment = Repayment;//补缴金额
                    //entity.Arrearage = entity.Arrearage - Repayment;  //欠费金额等于欠费金额减去补缴金额
                    entity.CarRepaymentTime = CarRepaymentTime;//补缴时间
                    entity.EscapePayStatus = EscapePayStatus;//逃逸追缴支付类型   1.现金支付 2.刷机支付
                    entity.IsEscapePay = IsEscapePay;//逃逸是否支付
                    //entity.EscapeOperaId = AbpSession.UserId;//逃逸追缴收费员ID
                    //entity.EscapeDeviceCode = AbpSession.DeviceCode;//逃逸追缴设备
                    //entity.EscapeTenantId = AbpSession.TenantId;//追缴商户ID
                    entity.EscapeOperaId = berthsec.CheckInEmployeeId;//逃逸追缴收费员ID
                    entity.EscapeDeviceCode = berthsec.CheckInDeviceCode;//逃逸追缴设备
                    entity.EscapeTenantId = berthsec.TenantId;//追缴商户ID
                    entity.PaymentType = PaymentType;// 追缴类型 1.前端追缴2.后台追缴3.微信追缴
                    entity.Status = 4;//  2.逃逸已收费
                    _abpBusinessDetailRepositroy.Update(entity);
                    //钱包扣费记录

                    AppAccount appaccount = _abpAppAccountRepository.FirstOrDefault(aar => aar.UserName == userName);
                    if (appaccount != default(AppAccount))
                    {
                        appaccount.LastModificationTime = DateTime.Now;
                        appaccount.Wallet -= Convert.ToDecimal(Repayment);
                        _abpAppAccountRepository.Update(appaccount);

                        //添加消费记录   缴费
                        DeductionRecord deductionrecord = new DeductionRecord()
                        {
                            OtherAccountId = appaccount.Id,
                            OperType = 2,
                            Money = Repayment,  //消费金额
                            PayStatus = true,
                            Remark = "车主端消费",
                            InTime = DateTime.Now
                        };
                        _abpDeductionRecordRepository.Insert(deductionrecord);
                    }

                }
                else
                {
                    return "该车没有欠费！";
                }
            }
            return "费用补缴成功！";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="msg"></param>
        public void SendAlarmMsg(string phone, string msg)
        {
            _smsManagementAppService.SendSms(new SmsAccountDto() { Destnumbers = phone, Msg = msg });
        }

      

        /// <summary>
        /// 获取车主账户信息
        /// </summary>
        /// <param name="p"></param>
        /// <param name="v"></param>
        /// <param name="action"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public AppAccountOutput GetAccountInfo(string p, string v, string action, string mobile)
        {
           var account = _abpAppAccountRepository.FirstOrDefault(entity => entity.Phone == mobile);
            AppAccountOutput accountOutput = new AppAccountOutput();
            if (account != default(AppAccount))
            {
                accountOutput.Phone = account.Phone;
                accountOutput.PlateNumber1 = account.PlateNumber1;
                accountOutput.PlateNumber2 = account.PlateNumber2;
                accountOutput.PlateNumber3 = account.PlateNumber3;
                accountOutput.Status1 = account.Status1;
                accountOutput.Status2 = account.Status2;
                accountOutput.Status3 = account.Status3;
                accountOutput.Wallet = account.Wallet;
            }
            return accountOutput;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="v"></param>
        /// <param name="action"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public List<Dtos.DeductionRecordDto> GetDeductionRecordList(string p, string v, string action, string mobile)
        {
            return _abpDeductionRecordRepository.GetAll().Take(30).ToList().MapTo<List<Dtos.DeductionRecordDto>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Phone"></param>
        /// <param name="Platenumber1"></param>
        /// <param name="Platenumber2"></param>
        /// <param name="Platenumber3"></param>
        /// <returns></returns>
        public string UploadPlatenumber(string Phone, string Platenumber1, string Platenumber2, string Platenumber3)
        {
            AppAccount appacount = _abpAppAccountRepository.FirstOrDefault(entity => entity.Phone == Phone);
            if (appacount != null)
            {
                if (Platenumber1 != "0")
                    appacount.PlateNumber1 = HttpUtility.UrlDecode(Platenumber1);//Unicode转中文
                if (Platenumber2 != "0")
                    appacount.PlateNumber2 = HttpUtility.UrlDecode(Platenumber2);
                if (Platenumber3 != "0")
                    appacount.PlateNumber3 = HttpUtility.UrlDecode(Platenumber3);
                if (Platenumber1 != "0" || Platenumber2 != "0" || Platenumber3 != "0")
                    _abpAppAccountRepository.Update(appacount);
                return "成功";
            }
            return "失败";
        }


        /// <summary>
        /// 获取停车场数据
        /// </summary>
        /// <param name="payable"></param>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <param name="p"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public ParkOutput GetParkList(string payable, string lat, string lng, string p, string v)
        {
            string sql = "";
            var offset = 100;
            sql = "select Id, ParkName as name, X as lng, Y as lat, C.BerthCount as total, B.free as free, mark as [desc] from AbpParks with(nolock) left join (select parkid,  count(1) as free from AbpBerths with(nolock) where BerthStatus = 2 and BerthSecId <> -1 group by parkid) as B on B.ParkId = AbpParks.id left join (select parkid,  count(1) as BerthCount from AbpBerths with(nolock) where BerthSecId <> -1 group by parkid) as C on C.ParkId = AbpParks.id where IsDeleted = 0 and X is not null and " + (decimal.Parse(lng) - offset) + " < CONVERT(decimal, x) and CONVERT(decimal, x) < " + (decimal.Parse(lng) + offset) + " and " + (decimal.Parse(lat) - offset) + " < CONVERT(decimal, y) and CONVERT(decimal, y) < " + (decimal.Parse(lat) + offset);
            
            DataTable dt = Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql).Tables[0];
            ParkOutput parkOutput = new Dtos.ParkOutput();
            parkOutput.rows = DataProcessHelper.GetEntityFromTable<ParkDto>(dt);
            return parkOutput;

        }

        /// <summary>
        /// 车主获取历史列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="v"></param>
        /// <param name="size"></param>
        /// <param name="page"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public HistoryParkingOutput GetHistoryOrderList(string p, string v, int size, int page, string mobile)
        {
            string sql = "select * from (select ROW_NUMBER() OVER(ORDER BY CarInTime desc) AS RowNumber, CONVERT(varchar(22), CarInTime, 120) as date, AbpBusinessDetail.Id as orderid, AbpBerthsecs.BerthsecName as parkname, CONVERT(varchar(10), money) as total from AbpBusinessDetail with(nolock)left join AbpBerthsecs on AbpBerthsecs.IsDeleted = 0 and AbpBerthsecs.id = AbpBusinessDetail.BerthsecId where Status in(2, 4) and PlateNumber in (select PlateNumber from ExtOtherAccount left join ExtOtherPlateNumber on ExtOtherPlateNumber.AssignedOtherAccountId = ExtOtherAccount.Id where TelePhone = '" + mobile + "' and ExtOtherPlateNumber.IsActive = 1)) AS Temp where RowNumber between " + (page - 1) * size + " and " + page * size;
            DataTable dt = Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql).Tables[0];
            HistoryParkingOutput historyParkingOutput = new Dtos.HistoryParkingOutput();
            historyParkingOutput.rows = DataProcessHelper.GetEntityFromTable<HistoryParkingDto>(dt);
            return historyParkingOutput;
        }

        /// <summary>
        /// 获取历史订单
        /// </summary>
        /// <param name="p"></param>
        /// <param name="v"></param>
        /// <param name="size"></param>
        /// <param name="page"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        List<HistoryParkingDto> ICarownerAppService.GetHistoryOrderList(string p, string v, int size, int page, string mobile)
        {
            return GetHistoryOrderList(p, v, size, page, mobile).rows;
        }

        /// <summary>
        /// 绑定车牌
        /// </summary>
        /// <param name="plateNumber"></param>
        /// <param name="oldplateNumber"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public bool BindPlateNumber(string plateNumber, string oldplateNumber, string mobile)
        {
            var entry = _abpAppAccountRepository.FirstOrDefault(AbpSession.UserId.Value);
            if (entry == default(AppAccount))
            {
                throw new AbpAuthorizationException("未找到此用户数据", "0");
            }

            if (oldplateNumber == "0")
            {
                if (string.IsNullOrEmpty(entry.PlateNumber1))
                {
                    entry.PlateNumber1 = plateNumber;
                }
                else if (string.IsNullOrEmpty(entry.PlateNumber2))
                    entry.PlateNumber2 = plateNumber;
                else
                    entry.PlateNumber3 = plateNumber;
            }
            else
            {
                if (entry.PlateNumber1 == oldplateNumber)
                {
                    entry.PlateNumber1 = plateNumber;
                }
                else if (entry.PlateNumber2 == oldplateNumber)
                    entry.PlateNumber2 = plateNumber;
                else
                    entry.PlateNumber3 = plateNumber;
            }

            if (_abpAppAccountRepository.Update(entry) != default(AppAccount))
                return true;
            return false;
        }

        /// <summary>
        /// 添加车牌号
        /// </summary>
        /// <param name="p"></param>
        /// <param name="v"></param>
        /// <param name="action"></param>
        /// <param name="mobile"></param>
        /// <param name="carnumber"></param>
        /// <param name="oldplateNumber"></param>
        /// <returns></returns>
        public int AddPlate(string p, string v, string action, string mobile, string carnumber, string oldplateNumber)
        {
            AppAccount appAccount = _abpAppAccountRepository.FirstOrDefault(entity => entity.Phone == mobile);
            var extOtherAccountId = _abpOtherAccountRepository.FirstOrDefault(entity => entity.TelePhone == mobile).Id;
            if (oldplateNumber == "0")
            {
                if (appAccount.PlateNumber1 == null)
                {
                    appAccount.PlateNumber1 = carnumber;
                }
                else if (appAccount.PlateNumber2 == null)
                {
                    appAccount.PlateNumber2 = carnumber;
                }
                else if (appAccount.PlateNumber3 == null)
                {
                    appAccount.PlateNumber3 = carnumber;
                }
                OtherPlateNumber model = new OtherPlateNumber
                {
                    AssignedOtherAccountId = extOtherAccountId,
                    PlateNumber = carnumber,
                    Order = 1,
                    IsActive = true,
                    CreationTime = DateTime.Now,
                    IsDeleted = false
                };
                var extOtherPlateNumberModel = _abpOtherPlateNumberRepository.Insert(model);
                _abpAppAccountRepository.Update(appAccount);
                return 1;
            }
            else
            {
                if (appAccount.PlateNumber1 == oldplateNumber)
                {
                    appAccount.PlateNumber1 = carnumber;
                }
                else if (appAccount.PlateNumber2 == oldplateNumber)
                {
                    appAccount.PlateNumber2 = carnumber;
                }
                else if (appAccount.PlateNumber3 == oldplateNumber)
                {
                    appAccount.PlateNumber3 = carnumber;
                }

                var ExtOtherPlateNumberModel = _abpOtherPlateNumberRepository.GetAllList(entity => entity.AssignedOtherAccountId == extOtherAccountId && entity.PlateNumber == oldplateNumber)[0];
                ExtOtherPlateNumberModel.PlateNumber = carnumber;
                ExtOtherPlateNumberModel.LastModificationTime = DateTime.Now;
                _abpOtherPlateNumberRepository.Update(ExtOtherPlateNumberModel);
                _abpAppAccountRepository.Update(appAccount);
                return 0;
            }
        }

        /// <summary>
        /// 获取车牌列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="v"></param>
        /// <param name="action"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public List<PlateDto> GetPlateList(string p, string v, string action, string mobile)
        {
            var model = _abpOtherAccountRepository.FirstOrDefault(entity => entity.TelePhone == mobile);
            var result = _abpOtherPlateNumberRepository.GetAllList(entity => entity.AssignedOtherAccountId == model.Id).MapTo<List<PlateDto>>();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long ConvertDataTimeLong(DateTime dt)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = dt.Subtract(dtStart);
            long timeStamp = toNow.Ticks;
            timeStamp = long.Parse(timeStamp.ToString().Substring(0, timeStamp.ToString().Length - 4));
            return timeStamp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public OrderDto GetOrderDetail(string guid)
        {
            OrderDto model = null;
            string sql = "select top 1 CONVERT(varchar(22), CarInTime, 120) as bdate, CarOutTime, AbpBerthsecs.BerthsecName, Status, AbpBusinessDetail.Id as orderid, AbpBusinessDetail.ParkId, AbpBerthsecs.BerthsecName as parkname, CONVERT(varchar(10), money) as total from AbpBusinessDetail with(nolock) " +
                " left join AbpBerthsecs on AbpBerthsecs.IsDeleted = 0 and AbpBerthsecs.id = AbpBusinessDetail.BerthsecId where AbpBusinessDetail.Id = " + guid + " order by CarInTime desc";
            DataTable dt = Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql).Tables[0];

            if (dt.Rows.Count > 0)
            {
                model = new OrderDto()
                {
                    state = dt.Rows[0]["Status"].ToString(),
                    orderid = dt.Rows[0]["orderid"].ToString(),
                    parkname = dt.Rows[0]["parkname"].ToString(),
                    parkid = dt.Rows[0]["ParkId"].ToString(),
                    total = dt.Rows[0]["total"].ToString(),
                    address = dt.Rows[0]["BerthsecName"].ToString(),
                    btime = ConvertDataTimeLong(DateTime.Parse(dt.Rows[0]["bdate"].ToString())).ToString(),
                    etime = string.IsNullOrWhiteSpace(dt.Rows[0]["CarOutTime"].ToString()) == true ? ConvertDataTimeLong(DateTime.Now).ToString()
                    : ConvertDataTimeLong(DateTime.Parse(dt.Rows[0]["CarOutTime"].ToString())).ToString()
                };
            }
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="v"></param>
        /// <param name="action"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public OrderDto GetCurrentOrder(string p, string v, string action, string mobile)
        {
            //select top 1 CONVERT(varchar(22), CarInTime, 120) as date, guid as orderid, AbpBerthsecs.BerthsecName as parkname, CONVERT(varchar(10), money) as total from AbpBusinessDetail left join AbpBerthsecs on AbpBerthsecs.IsDeleted = 0 and AbpBerthsecs.id = AbpBusinessDetail.BerthsecId where Status in(1) and PlateNumber in (select PlateNumber from ExtOtherAccount left join ExtOtherPlateNumber on ExtOtherPlateNumber.AssignedOtherAccountId = ExtOtherAccount.Id where TelePhone = '13851483025' and ExtOtherPlateNumber.IsActive = 1) order by CarInTime desc
            OrderDto model = null;
            string sql = "select top 1 CONVERT(varchar(22), CarInTime, 120) as bdate, CarOutTime, AbpBerthsecs.BerthsecName, Status, guid as orderid, AbpBusinessDetail.ParkId, AbpBerthsecs.BerthsecName as parkname, AbpBusinessDetail.PlateNumber, CONVERT(varchar(10), money) as total from AbpBusinessDetail with(nolock) " +
                " left join AbpBerthsecs on AbpBerthsecs.IsDeleted = 0 and AbpBerthsecs.id = AbpBusinessDetail.BerthsecId where Status in(1, 3) and PlateNumber in (select PlateNumber from ExtOtherAccount left join ExtOtherPlateNumber on ExtOtherPlateNumber.AssignedOtherAccountId = ExtOtherAccount.Id where TelePhone = '" + mobile + "' and ExtOtherPlateNumber.IsActive = 1) order by CarInTime desc";
            DataTable dt = Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql).Tables[0];

            if (dt.Rows.Count > 0)
            {
                model = new OrderDto()
                {
                    state = dt.Rows[0]["Status"].ToString(),
                    orderid = dt.Rows[0]["orderid"].ToString(),
                    parkname = dt.Rows[0]["parkname"].ToString(),
                    PlateNumber = dt.Rows[0]["PlateNumber"].ToString(),
                    parkid = dt.Rows[0]["ParkId"].ToString(),
                    total = dt.Rows[0]["total"].ToString(),
                    address = dt.Rows[0]["BerthsecName"].ToString(),
                    btime = ConvertDataTimeLong(DateTime.Parse(dt.Rows[0]["bdate"].ToString())).ToString(),
                    etime = string.IsNullOrWhiteSpace(dt.Rows[0]["CarOutTime"].ToString()) == true ? ConvertDataTimeLong(DateTime.Now).ToString()
                    : ConvertDataTimeLong(DateTime.Parse(dt.Rows[0]["CarOutTime"].ToString())).ToString()
                };
            }
            return model;
        }

        /// <summary>
        /// 车位预约
        /// </summary>
        /// <param name="p"></param>
        /// <param name="v"></param>
        /// <param name="action"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        //public int ADDParkReservation(string p,string v,string action,string mobile)
        //{
           
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="v"></param>
        /// <param name="action"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public SettingInfoDto GetProf(string p, string v, string action, string mobile)
        {
            return new SettingInfoDto();
        }

        /// <summary>
        /// 停车场id转换成停车场名称
        /// </summary>
        /// <param name="parkids"></param>
        /// <returns></returns>
        private string GetParkName(string parkids)
        {
            string parkname = "";
            if (parkids == "0")
                return "不限停车场";
            else
            {
                string sql = "select * from AbpParks with(nolock) where IsDeleted = 0 and Id in (" + parkids + ")";
                DataTable dt = Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        parkname += dr["ParkName"].ToString() + "|";
                    }
                }
                if (parkname.Length > 0)
                    parkname = parkname.Substring(0, parkname.Length - 1);
                return parkname;
            }
        }


        /// <summary>
        /// 获取包月卡信息
        /// </summary>
        /// <param name="action"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public List<MonthyDto> GetUseMonthyList(string action, string mobile)
        {
            List<MonthyDto> modellist = new List<MonthyDto>();
            string sql = "select * from AbpMonthlyCars with(nolock) where Telphone = '" + mobile + "' and IsDeleted = 0";
            DataTable dt = Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var model = new MonthyDto()
                    {
                        limitdate = dr["EndTime"].ToString(),
                        limitday =
                        ((DateTime.Parse(dr["EndTime"].ToString()) -
                        DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"))).TotalDays >= 0 ? (DateTime.Parse(dr["EndTime"].ToString()) -
                        DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"))).TotalDays : 0).ToString(),
                        name = dr["PlateNumber"].ToString(),
                        parkname = GetParkName(dr["ParkIds"].ToString()),
                        price = dr["Money"].ToString()
                    };
                    model.limittime = dr["MonthyType"].ToString();
                    model.limittime = _abpDictionaryValueRepository.FirstOrDefault(entity => entity.ValueCode == model.limittime && entity.TypeCode == P4Consts.StopType).ValueData;
                    modellist.Add(model);
                }
            }
            return modellist;
        }    
    }
}
