namespace P4
{
    /// <summary>
    /// 常量值
    /// </summary>
    public class P4Consts
    {
        /// <summary>
        /// 
        /// </summary>
        public const string LocalizationSourceName = "P4";
        /// <summary>
        /// 数据操作类型
        /// </summary>
        public const string OperationType = "A10007";

        /// <summary>
        /// 巡查员签退状态
        /// </summary>
        public const string InspectorCheckOutType = "A10006";

        /// <summary>
        /// 巡查员状态
        /// </summary>
        public const string InspectorStatus = "A10005";

        /// <summary>
        /// 收费员签退类型
        /// </summary>
        public const string EmployeeCheckOutType = "A10004";

        /// <summary>
        /// 收费员状态
        /// </summary>
        public const string EmployeeStatus = "A10003";

        /// <summary>
        /// 系统语言
        /// </summary>
        public const string SystemLanguage = "A10002";
        /// <summary>
        /// 泊位在停状态
        /// </summary>
        public const string  BerthStatus = "B10001";
        /// <summary>
        /// 车检器停车状态 
        /// </summary>
        public const string ParkStatus = "A10012";
        /// <summary>
        /// 车检器在线状态
        /// </summary>
        public const string IsOnline = "A10011";

        /// <summary>
        /// pda设备类型
        /// </summary>
        public const string PDAType = "A10013";

        /// <summary>
        /// pda设备制式
        /// </summary>
        public const string PDAStandard = "A10014";

        /// <summary>
        /// 
        /// </summary>
        public const string PDADeviceType = "A10017";

        /// <summary>
        /// pda设备使用状态
        /// </summary>
        public const string PDAUseStatus = "A10015";

        /// <summary>
        /// 巡查任务状态
        /// </summary>
        public const string InspectorTaskStatus = "A10016";

        /// <summary>
        /// 车辆类型
        /// </summary>
        public const string VehicleType = "A10008";
        /// <summary>
        /// 费率收费  0 按时    1 按次
        /// </summary>
        public const string RateType = "A10009";
       /// <summary>
        /// 计费方式 0 按分钟计费  1 按小时计费    2 按车次计费
       /// </summary>
        public const string ChargeType = "A10010";
        
        /// <summary>
        /// 提示框位置
        /// </summary>
        public const string SystemPromptBox = "A10001";

        #region JqGrid参数常量
        public const string JqGridInsert = "add";
        public const string JqGridDelete = "del";
        public const string JqGridUpdate = "edit";
        #endregion

        #region 操作权限常量
        /// <summary>
        /// 
        /// </summary>
        public const string OperationInsert = Abp.AbpConsts.OperationInsert;

        /// <summary>
        /// 
        /// </summary>
        public const string OperationDelete = Abp.AbpConsts.OperationDelete;

        /// <summary>
        /// 
        /// </summary>
        public const string OperationModify = Abp.AbpConsts.OperationModify;

        /// <summary>
        /// 
        /// </summary>
        public const string OperationInfo = Abp.AbpConsts.OperationInfo;

        /// <summary>
        /// 
        /// </summary>
        public const string OperationSearch = Abp.AbpConsts.OperationSearch;

        /// <summary>
        /// 
        /// </summary>
        public const string OperationRefresh = Abp.AbpConsts.OperationRefresh;
        #endregion

        public const string InitPassword = "123qwe";

        #region 推送组
        /// <summary>
        /// 车检器推送组
        /// </summary>
        public const string SensorGroup = "Sensor";

        /// <summary>
        /// 收费员推送组
        /// </summary>
        public const string EmployeeGroup = "Employee";
        /// <summary>
        /// 诱导推送组
        /// </summary>
        public const string InducibleGroup = "Inducible";

        /// <summary>
        /// 微信推送组
        /// </summary>
        public const string WeixinGroup = "Weixin";
        #endregion

        #region 车检器配置参数

        /// <summary>
        /// 白天车检器离线时间单位分钟
        /// </summary>
        public const int SensorDayOnline = 60 * 12;

        /// <summary>
        /// 晚上时间车检器离线时间分钟
        /// </summary>
        public const int SensorNightOnline = 60 * 12;


        #endregion

        #region 道闸活跃时间
        public const int SignoOnline = 60;
        #endregion

        #region 钱包参数

        /// <summary>
        /// 钱包扣费   预付金额
        /// </summary>
        public const decimal PaymentMoney = 15;
        /// <summary>
        /// 账户最低余额
        /// </summary>
        public const decimal AccountLowMoney = 15;

        #endregion

        #region 短信发送模板
        //public const string RegisterModel = "尊敬的车主，您已于{0}，成功注册成为大理市城市停车会员。您的余额为{1}元。祝您生活愉快。";
        //public const string RegisterHavaPlateNumberModel = "尊敬的车主，您已于{0}，成功注册成为大理市城市停车会员，车牌号为{1}。您的余额为{2}元。祝您生活愉快。";

        //public const string RenewCardModel = "尊敬的车主，您的账户于{0}，成功充值{1}元，余额为{2}元。祝您生活愉快。";
        //public const string RegisterAppModel = "尊敬的车主，您的验证码为【{0}】,五分钟有效！";

        /// <summary>
        /// 预付
        /// </summary>
        //public const string PaymentModel = "尊敬的车主，您的账户于{0}，在{1}泊车位预付{2}元停车费。祝您生活愉快。";

        /// <summary>
        /// 消费扣款(预付款刚好等于停车费)
        /// </summary>
        //public const string ConsumptionEqualModel = "尊敬的车主，您的账户于{0}，在{1}泊车位缴纳{2}元停车费，已从预付款中收取，本次消费车辆车牌号为{3}。您的余额为{4}元，祝您生活愉快。";

        /// <summary>
        /// 消费扣款(预付款<停车费)
        /// </summary>
        //public const string ConsumptionLessModel = "尊敬的车主，您的账户于{0}，在{1}泊车位缴纳{2}元停车费，由于预付款不足，剩余金额{3}已从您的账户中扣取，本次消费车辆车牌号为{4}。您的余额为{5}元，祝您生活愉快。";

        /// <summary>
        /// 消费扣款(预付款>停车费)
        /// </summary>
        //public const string ConsumptionGreaterModel = "尊敬的车主，您的账户于{0}，在{1}泊车位缴纳{2}元停车费，剩余的预付款{3}已退回您的账户，本次消费车辆车牌号为{4}。您的余额为{5}元，祝您生活愉快。";

        /// <summary>
        /// 欠费追缴
        /// </summary>
        //public const string ConsumptionPaymentModel = "尊敬的车主，您的账户于{0}，追缴{1}元停车费，本次消费车辆车牌号为{2}。您的余额为{3}元，祝您生活愉快。";

        /// <summary>
        /// 包月车辆模板
        /// </summary>
        //public const string ConsumptionMonthModel = "尊敬的车主，您的爱车({0})于{1}在我公司开通大理市{2}路段的占道停车服务，服务期限{3}至{4}，感谢您的支持，祝您身体健康，家庭幸福！更多咨讯，敬请关注“苍洱停车”公众微信号。";

        /// <summary>
        /// 包月车辆续费模板
        /// </summary>
        //public const string ConsumptionMonthRenewModel = "尊敬的车主，您的爱车({0})于{1}在我公司续费大理市{2}路段的占道停车服务，服务期限{3}至{4}，感谢您的支持，祝您身体健康，家庭幸福！更多咨讯，敬请关注“苍洱停车”公众微信号。";

        #endregion

        /// <summary>
        /// 刷卡折扣率 xx折
        /// </summary>
        public const string CardDiscount = "A10020";

        /// <summary>
        /// 金额大于SubstantialOrder值为大额订单
        /// </summary>
        public const int SubstantialOrder = 500;

        /// <summary>
        /// 设备型号
        /// </summary>
        public const string EquipmentVersion = "A10017";

        /// <summary>
        /// 刷卡消费类型
        /// </summary>
        public const string OperTypeName = "A10018";

        /// <summary>
        /// 短信模板
        /// </summary>
        public const string SmsModel = "A10019";

        /// <summary>
        /// 配件型号
        /// </summary>
        public const string PartsType = "A10021";

        /// <summary>
        /// 停车类型
        /// </summary>
        public const string StopType = "A10022";

        /// <summary>
        /// 支付类型
        /// </summary>
        public const string PayType = "A10023";
    }
}