using P4.Berths.Dtos;
using P4.Berthsecs.Dto;
using P4.BlackLists.Dtos;
using P4.Card.Dtos;
using P4.Dictionarys.Dtos;
using P4.MonthlyCars.Dtos;
using P4.Parks.Dtos;
using P4.PrintAdvertisement.Dtos;
using P4.SpecialLists.Dtos;
using P4.TicketManagement.Dto;
using P4.WhiteLists.Dtos;
using System;
using System.Collections.Generic;

namespace P4.Web.Models
{
    /// <summary>
    /// PDA与后台交互模型
    /// </summary>
    public class PDAModel
    {
        /// <summary>
        /// 分公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 车场类型
        /// 1封闭停车场
        /// 2道路停车场
        /// </summary>
        public Int16 ParkType { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int CompanyID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<BerthDto> Berths { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<BerthsecDto> BerthsecList { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public List<BlackListsDto> BlackList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<WhiteListsDto> WhiteList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<MonthlyCarDto> MonthlyCarList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<SpecialListsDto> SpecialCarList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<IPassBlackCardDto> IPassBlackCardList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ParkDto> ParkList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<GetAllValueDataDto> DictionaryValueList { get; set; }

        /// <summary>
        /// 收费员信息
        /// </summary>
        public Employees.Dtos.EmployeePDADto Employee { get; set; }

        /// <summary>
        /// 高级登录
        /// </summary>
        public string SysInfoPassword { get; set; }
        /// <summary>
        /// 发卡管理密码
        /// </summary>
        public string CardMenuPassword { get; set; }
        /// <summary>
        /// 收费汇总密码
        /// </summary>
        public string HrTotalPassword { get; set; }
        /// <summary>
        ///签退密码
        /// </summary>
        public string LogoutPassword { get; set; }

        /// <summary>
        /// 退出软件密码
        /// </summary>
        public string Password5 { get; set; }

        /// <summary>
        /// 进场是否追缴
        /// </summary>
        public bool PDAInCarEscape { get; set; }


        /// <summary>
        /// 出场是否追缴
        /// </summary>
        public bool PDAOutCarEscape { get; set; }

        /// <summary>
        /// 进场是否拍照
        /// </summary>
        public bool PDAInCarPhotoFlag { get; set; }

        /// <summary>
        /// 进场拍照张数
        /// </summary>
        public string PDAInCarPhotoNum { get; set; }

        /// <summary>
        /// 出场是否拍照
        /// </summary>
        public bool PDAOutCarPhotoFlag { get; set; }

        /// <summary>
        /// 出场拍照张数
        /// </summary>
        public string PDAOutCarPhotoNum { get; set; }

        /// <summary>
        /// 车牌区域
        /// </summary>
        public string PDARegion { get; set; }

        /// <summary>
        /// 车牌字母
        /// </summary>
        public string PDAChar { get; set; }

        /// <summary>
        /// 是否进场预缴
        /// </summary>
        public bool PDAPrepaidFlag { get; set; }

        /// <summary>
        /// 预缴金额
        /// </summary>
        public string PDAPrepaid { get; set; }

        /// <summary>
        /// 是否启用地磁计费
        /// </summary>
        public bool SensorTimer { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int SensorMorningDelay { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SensorMorningBegin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SensorMorningEnd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SensorNightDelay { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SensorNightBegin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SensorNightEnd { get; set; }

        /// <summary>
        /// 打印广告
        /// </summary>
        public List<PrintAdDto> PrintList { get; set; }

        /// <summary>
        /// 微信支付
        /// </summary>
        public bool WeixinPay { get; set; }

        /// <summary>
        /// 是否启用支付宝支付
        /// </summary>
        public bool AliPay { get; set; }

        /// <summary>
        /// 视频识别
        /// </summary>
        public bool VideoRecognition { get; set; }

        /// <summary>
        /// 微信折扣
        /// </summary>
        public string WeixinDiscount { get; set; }

        /// <summary>
        /// 刷卡支付
        /// </summary>
        public bool IPassCardPay { get; set; }

        /// <summary>
        /// 刷卡折扣
        /// </summary>
        public string IPassCardDiscount { get; set; }

        /// <summary>
        /// 是否现金支付
        /// </summary>
        public bool CashPay { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CashPayDiscount { get; set; }

        /// <summary>
        /// 出场是否车牌识别
        /// </summary>
        public bool OutCarRecognition { get; set; }


        /// <summary>
        /// 欠费黑名单
        /// </summary>
        public bool EscapeBlack { get; set; }

        /// <summary>
        /// 欠费黑名单金额
        /// </summary>
        public int EscapeBlackMoney { get; set; }

        /// <summary>
        /// 欠费拍照
        /// </summary>
        public bool EscapePhoto { get; set; }

        /// <summary>
        /// 欠费拍照金额
        /// </summary>
        public int EscapePhotoMoney { get; set; }


        /// <summary>
        /// 出场逃逸是否打印
        /// </summary>
        public bool EscapePrint { get; set; }

        /// <summary>
        /// 出场逃逸是否打印二维码
        /// </summary>
        public bool EscapeXingCode { get; set; }

        /// <summary>
        /// 是否启用时段预付
        /// </summary>
        public bool PeriodPaid { get; set; }

        /// <summary>
        /// 时段时间
        /// </summary>
        public string PeriodTime { get; set; }

        /// <summary>
        /// 时段时间1
        /// </summary>
        public string PeriodTime1 { get; set; }

        /// <summary>
        /// PDA同步数据
        /// </summary>
        public bool PDASyncData { get; set; }

        /// <summary>
        /// 特权车辆是否打印小票
        /// </summary>
        public bool PrivilegeCarReceipt { get; set; }

        /// <summary>
        /// 是否上传数据库
        /// </summary>
        public bool UploadSqlite { get; set; }

        /// <summary>
        /// 在线支付路径
        /// </summary>
        public string OnlinePayUrl { get; set; }

        /// <summary>
        /// 进场小票样式
        /// </summary>
        public TicketStyleDto CarInTicketCss { get; set; }

        /// <summary>
        /// 出场小票样式
        /// </summary>
        public TicketStyleDto CarOutTicketCss { get; set; }
        /// <summary>
        /// 历史欠费小票样式
        /// </summary>
        public TicketStyleDto OweTicketCSS{ get; set; }
        /// <summary>
        /// 追缴小票样式
        /// </summary>
        public TicketStyleDto RepayTicketCss { get; set; }
        /// <summary>
        /// 收费日报小票样式
        /// </summary>
        public TicketStyleDto DayChargeTicketCss { get; set; }

    }
}