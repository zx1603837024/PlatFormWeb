using P4.Authorization;
using P4.Berths;
using P4.Berthsecs;
using P4.BlackLists;
using P4.Businesses;
using P4.Companys;
using P4.Dictionarys;
using P4.Employees;
using P4.Icons;
using P4.Inspectors;
using P4.Menus;
using P4.Messages;
using P4.MonthlyCars;
using P4.MultiTenancy;
using P4.Notices;
using P4.OperationPermissions;
using P4.Organizations;
using P4.OtherAccounts;
using P4.OtherPlateNumbers;
using P4.Parks;
using P4.Rates;
using P4.Regions;
using P4.Smss;
using P4.Tasks;
using P4.TestJobs;
using P4.Users;
using P4.UserTrials;
using System.Data.Entity;
using P4.WhiteLists;
using P4.WorkGroups;
using P4.Equipments;
using P4.Inducibles;
using P4.Sensors;
using P4.SpecialLists;
using P4.Reports;
using P4.App;
using P4.DecisionAnalysis;
using P4.Webchat;
using P4.Weixin;
using P4.WorkGroupInspectors;
using P4.Card;
using P4.ExportCommon;
using P4.EquipmentMaintain;
using P4.AliPay;
using P4.SchedulingSystem;
using P4.TicketCss;
using P4.Signo;
using P4.Camera;
using P4.XianPay;
using P4.ParkingLot;
using P4.VideoEquips;
using P4.VideoCars;
using P4.PayLevelSetting;

/// <summary>
/// 
/// </summary>
namespace P4.EntityFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class P4DbContext : Abp.Zero.EntityFramework.AbpZeroDbContext<Tenant, Role, User>
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<ParkReservation> ParkReservation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<VehiclesError> VehiclesError { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<VehiclesExit> VehiclesExit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<VehiclesFControlBarrierGate> VehiclesFControlBarrierGate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<VehiclesHandMoveCutOff> VehiclesHandMoveCutOff { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<VehiclesPayType> VehiclesPayType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<SignoParkInformation> SignoParkInformations { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Menu> Menus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Message> Messages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<SignalRMessageType> SignalRMessageTypes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Notice> Notices { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Task> Tasks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Icon> Icons { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Company> Companys { get; set; }

        #region 账号规则
        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<OtherAccount> OtherAccounts { get; set; }

        /// <summary>
        /// 消费明细
        /// </summary>
        public virtual IDbSet<DeductionRecord> DeductionRecords { get; set; }

        /// <summary>
        /// 账号绑定车牌号
        /// </summary>
        public virtual IDbSet<OtherPlateNumber> OtherPlateNumbers { get; set; }

        /// <summary>
        /// 充值规则
        /// </summary>
        public virtual IDbSet<P4.OtherAccounts.RechargeRule> RechargeRules { get; set; }
        #endregion

        #region 视频桩
        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<CameraHeartbeat> CameraHeartbeats { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Cameras> Cameras { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<CameraAlarmreport> CameraAlarmreports { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<CameraBusinessDetail> CameraBusinessDetails { get; set; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Sms> Smss { get; set; }

        /// <summary>
        /// 短信模板管理
        /// </summary>
        public virtual IDbSet<SmsModel> SmsModels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Park> Parks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Region> Regions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Berth> Berths { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<DictionaryType> DictionaryTypes { get; set; }

        /// <summary>
        /// 停车优惠券
        /// </summary>
        public virtual IDbSet<Coupon> Coupons { get; set; }

        /// <summary>
        /// 优惠券使用明细
        /// </summary>
        public virtual IDbSet<CouponDetail> CouponDetails { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<DictionaryTypeTemplate> DictionaryTypesTemplate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<DictionaryValue> DictionaryValues { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<DictionaryValueTemplate> DictionaryValuesTemplate { get; set; }

        /// <summary>
        /// 运营公司
        /// </summary>
        public virtual IDbSet<OperatorsCompany> OperatorsCompanys { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<BusinessDetail> BusinessDetails { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<IllegalOpenSignoPicture> IllegalOpenSignoPictures { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<EscapeDetail> EscapeDetails { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<EscapeGuid> EscapeGuids { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<RemoteGuid> RemoteGuids { get; set; }

        /// <summary>
        /// 收费记录关联图片表
        /// </summary>
        public virtual IDbSet<BusinessDetailPicture> BusinessDetailPictures { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<PictureStore> PictureStores { get; set; }

        /// <summary>
        /// 组织架构
        /// </summary>
        public virtual IDbSet<Organization> Organizations { get; set; }

        #region 决策分析

        /// <summary>
        /// 高峰预警
        /// </summary>
        public virtual IDbSet<PeakPeriod> PeakPeriods { get; set; }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Employee> Employees { get; set; }

        /// <summary>
        /// 收费签退签到
        /// </summary>
        public virtual IDbSet<EmployeeCheck> EmployeeChecks { get; set; }

        /// <summary>
        /// 巡查员签到、退
        /// </summary>
        public virtual IDbSet<InspectorCheck> InspectorChecks { get; set; }

        /// <summary>
        /// 巡查任务
        /// </summary>
        public virtual IDbSet<InspectorTask> InspectorTasks { get; set; }

        /// <summary>
        /// 巡查任务反馈
        /// </summary>
        public virtual IDbSet<InspectorTaskFeedback> InspectorTaskFeedbacks { get; set; }


        /// <summary>
        /// 操作权限
        /// </summary>
        public virtual IDbSet<OperationPermission> OperationPermissions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<UserTrial> UserTrials { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Inspector> Inspectors { get; set; }

        /// <summary>
        /// 泊位段
        /// </summary>
        public virtual IDbSet<Berthsec> Berthsecs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Job> Jobs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<MonthlyCar> MonthlyCars { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<MonthlyCarHistory> MonthlyCarHistorys { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<MonthlyCarAbnormal> MonthlyCarAbnormals { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Rate> Rates { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<WhiteList> WhiteLists { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<BlackList> BlackLists { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<SpecialList> SpecialLists { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<WorkGroup> WorkGroups { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<WorkGroupEmployee> WorkGroupEmployees { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<WorkGroupBerthsec> WorkGroupBerthsecs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<WorkGroupInspectors.WorkGroupInspectors> WorkGroupInspectors { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<WorkGroupsInspectors> WorkGroupsInspectors { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public virtual IDbSet<CarModel> CarModels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<WorkGroupInspectorsBerthsecs> WorkGroupInspectorsBerthsecs { get; set; }

        /// <summary>
        /// 打印机广告
        /// </summary>
        public virtual IDbSet<PrintAd> PrintAds { get; set; }

        #region 设备管理
        /// <summary>
        /// 设备管理
        /// </summary>
        public virtual IDbSet<Equipment> Equipments { get; set; }

        /// <summary>
        /// 设备维护记录
        /// </summary>
        public virtual IDbSet<EquipmentMaintenance> EquipmentMaintenances { get; set; }

        /// <summary>
        /// 设备心跳
        /// </summary>
        public virtual IDbSet<EquipmentBeat> EquipmentBeats { get; set; }

        /// <summary>
        /// 设备定位
        /// </summary>
        public virtual IDbSet<EquipmentGps> EquipmentGps { get; set; }

        /// <summary>
        /// pda软件版本管理
        /// </summary>
        public virtual IDbSet<P4.Equipments.EquipmentVersion> EquipmentVersions { get; set; }
        #endregion

        #region 诱导系统
        public virtual IDbSet<Inducible> Inducibles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<InducibleLog> InducibleLogs { get; set; }

        public virtual IDbSet<InducibleToAD> InducibleToADs { get; set; }

        public virtual IDbSet<InducibleToPark> InducibleToParks { get; set; }
        #endregion

        #region 车检器

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Sensor> Sensors { get; set; }

        public virtual IDbSet<SensorBusinessDetail> SensorBusinessDetails { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<SensorCaution> SensorCautions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<SensorBeat> SensorBeats { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<SensorMagnetic> SensorMagnetics { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<SensorFault> SensorFaults { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<SensorRunStatus> SensorRunStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<SensorLog> SensorLogs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Transmitter> Transmitters { get; set; }

        public virtual IDbSet<TransmitterCaution> TransmitterCautions { get; set; }
        #endregion

        #region 报表
        /// <summary>
        /// 泊位段
        /// </summary>
        public virtual IDbSet<BerthsecReport> BerthsecReports { get; set; }

        /// <summary>
        /// 收费员
        /// </summary>
        public virtual IDbSet<EmployeeReport> EmployeeReports { get; set; }

        #endregion

        #region app
        /// <summary>
        /// app账户
        /// </summary>
        public virtual IDbSet<AppAccount> AppAccounts { get; set; }

        /// <summary>
        /// 消费记录
        /// </summary>
        public virtual IDbSet<AppAccountRecord> AppAccountRecords { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<AppMessage> AppMessages { get; set; }

        #endregion

        #region 一卡通

        public virtual IDbSet<IPassCard> IPassCards { get; set; }

        /// <summary>
        /// 一卡通错误对照码
        /// </summary>
        public virtual IDbSet<IpassCardCode> IpassCardCodes { get; set; }

        /// <summary>
        /// 黑名单卡
        /// </summary>
        public virtual IDbSet<IPassBlackCard> IPassBlackCards { get; set; }

        #endregion

        /// <summary>
        /// 导出代码
        /// </summary>
        public virtual IDbSet<ExportCode> ExportCodes { get; set; }

        #region 微信管理

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Ad> Ads { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<WeixinMsg> WeixinMsgs { get; set; }

        /// <summary>
        /// 回复消息模板
        /// </summary>
        public virtual IDbSet<P4.Weixin.WeixinSendMsgModel> WeixinSendMsgModels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<WeixinConfig> WeixinConfigs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<WeixinMenu> WeixinMenus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Weixin.WeixinPushMsg> WeixinPushMsgs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<WeixinUser> WeixinUsers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<WeixinBank> WeixinBanks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<WeixinCurrencyType> WeixinCurrencyTypes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<WeixinTradeType> WeixinTradeTypes { get; set; }

        /// <summary>
        /// 微信意见反馈
        /// </summary>
        public virtual IDbSet<WeixinIdea> WeixinIdeas { get; set; }

        /// <summary>
        /// 微信注册用户
        /// </summary>
        public virtual IDbSet<TUser> WeixinTusers { get; set; }

        /// <summary>
        /// 微信支付流水
        /// </summary>
        public virtual IDbSet<WeixinOrder> WeixinOrders { get; set; }

        /// <summary>
        /// 支付宝支付流水
        /// </summary>
        public virtual IDbSet<AliPayOrder> AliPayOrders { get; set; }

        /// <summary>
        /// 西安支付流水
        /// </summary>
        public virtual IDbSet<XianPayOrder> XianPayOrders { get; set; }

        /// <summary>
        /// 充值规则
        /// </summary>
        public virtual IDbSet<WeixinRechargeRule> WeixinRechargeRules { get; set; }

        /// <summary>
        /// 包月规则
        /// </summary>
        public virtual IDbSet<MonthRule> WeixinMonthRules { get; set; }

        /// <summary>
        /// 微信发送记录
        /// </summary>
        public virtual IDbSet<P4.Weixin.WeixinSendRecord> WeixinSendRecords { get; set; }

        /// <summary>
        /// 车牌前缀
        /// </summary>
        public virtual IDbSet<Weixinplates> AbpWeixinplates { get; set; }

        /// <summary>
        /// 短信发送记录
        /// </summary>
        public virtual IDbSet<SmsSendRecord> SmsSendRecords { get; set; }
        #endregion

         #region 排班系统

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<SchedulingSystemBerthsec> SchedulingSystemBerthsecs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<SchedulingSystemEmployee> SchedulingSystemEmployees { get; set; }
        
        /// <summary>
        /// 收费员考勤功能
        /// </summary>

        public virtual IDbSet<SchedulingSystemEmployeeCheck> SchedulingSystemEmployeeChecks { get; set; }

        /// <summary>
        /// 节假日管理
        /// </summary>
        public virtual IDbSet<SchedulingSystemHolidaysManager> SchedulingSystemHolidaysManagers { get; set; }

        /// <summary>
        /// 请假管理
        /// </summary>
        public virtual IDbSet<SchedulingSystemLeaveAdministration> SchedulingSystemLeaveAdministrations { get; set; }

        #endregion

        /// <summary>
        /// 停车场 - 停车记录
        /// </summary>
        public virtual IDbSet<ParkingRecord> AbpParkingRecord { get; set; }

        /// <summary>
        /// 停车场通道
        /// </summary>
        public virtual IDbSet<ParkChannel> AbpParkChannel { get; set; }

        /// <summary>
        /// 包月
        /// </summary>
        public virtual IDbSet<ParkingMonthlyRent> AbpParkingMonthlyRent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public P4DbContext()
            : base("Default")
        {
            //Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        public P4DbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            //Configuration.ProxyCreationEnabled = false;
        }

        #region 设备维护管理子系统

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<EquipmentsM> EquipmentsMs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<EquipmentDetail> EquipmentDetails { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Distribution> Distributions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<ReworkFlow> ReworkFlows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<CompanyCustomerExpress> CompanyCustomerExpresss { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<CompanyFactoryExpress> CompanyFactoryExpresss { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<ReworkPicture> ReworkPictures { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<AndroidLog> AndroidLogs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<TicketStyle> TicketCSS { get; set; }
        #endregion

        #region 视频桩模块
        /// <summary>
        /// 视频桩设置表
        /// </summary>
        public virtual IDbSet<VideoEquip> VideoEquips { get; set; }

        /// <summary>
        /// 视频桩设置表
        /// </summary>
        public virtual IDbSet<VideoEquipBusinessDetail> VideoEquipBusinessDetail { get; set; }

        /// <summary>
        /// 视频桩异常推送表
        /// </summary>
        public virtual IDbSet<VideoEquipFaults> VideoEquipFaults { get; set; }
        #endregion

        #region 巡检车模块
        public virtual IDbSet<VideoCar> VideoCar { get; set; }
        public virtual IDbSet<VideoCarBusinessDetail> VideoCarBusinessDetail { get; set; }
        public virtual IDbSet<VideoCarFault> VideoCarFault { get; set; }
        #endregion

        /// <summary>
        /// 支付优先级
        /// </summary>
        public virtual IDbSet<PayLevelSettings> PayLevelSettings { get; set; }
    }
}
