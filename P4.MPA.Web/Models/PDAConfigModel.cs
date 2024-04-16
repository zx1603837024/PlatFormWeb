using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    public class PDAConfigModel
    {

        /// <summary>
        /// 进场是否追缴
        /// </summary>
        public bool PDAInCarEscape { get; set; }


        /// <summary>
        /// 设备是否绑定收费员
        /// </summary>
        public bool PDABindUser { get; set; }

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
        /// 
        /// </summary>
        public string Password1 { get; set; }
       
        /// <summary>
        /// 
        /// </summary>
        public string Password2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password4 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password5 { get; set; }

        /// <summary>
        /// 是否进场预缴
        /// </summary>
        public bool PDAPrepaidFlag { get; set; }

        /// <summary>
        /// 预缴金额
        /// </summary>
        public string PDAPrepaid { get; set; }

        /// <summary>
        /// 是否自动升级
        /// </summary>
        public bool PDAUpgrate { get; set; }

        /// <summary>
        /// 地磁绑定
        /// </summary>
        public bool Sensorbind { get; set; }

        /// <summary>
        /// 采用地磁计费
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
        /// 是否启用微信支付
        /// </summary>
        public bool WeixinPay { get; set; }

        /// <summary>
        /// 在线支付路径
        /// </summary>
        public string OnlinePayUrl { get; set; }

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
        /// 现金支付
        /// </summary>
        public bool CashPay { get; set; }

        /// <summary>
        /// 现金支付折扣
        /// </summary>
        public string CashPayDiscount { get; set; }


        /// <summary>
        /// 刷卡支付
        /// </summary>
        public bool IPassCardPay { get; set; }

        /// <summary>
        /// 刷卡折扣
        /// </summary>
        public string IPassCardDiscount { get; set; }

        /// <summary>
        /// 账号支付
        /// </summary>
        public bool AccountPay { get; set; }

        /// <summary>
        /// 账号折扣
        /// </summary>
        public string AccountDiscount { get; set; }

        /// <summary>
        /// 出场逃逸是否打印
        /// </summary>
        public bool EscapePrint { get; set; }

        /// <summary>
        /// 出场逃逸是否打印二维码
        /// </summary>
        public bool EscapeXingCode { get; set; }

        /// <summary>
        /// 
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
        /// 车牌区域数组
        /// </summary>
        public List<string> PlateNumber
        {
            get
            {
                return new List<string>() { "陕", "沪", "豫", "闽", "京", "渝", "宁", "甘", "浙", "鄂", "皖", "川", "云", "贵", "苏", "吉", "青", "藏", "粤", "湘", "新", "晋", "津", "冀", "黑", "辽", "赣", "琼", "鲁", "桂", "蒙" };
            }
        }

        /// <summary>
        /// 拍照数数组
        /// </summary>
        public List<string> Number
        {
            get { return new List<string>() { "1", "2", "3", "4", "5" }; }
        }

        /// <summary>
        /// 同步PDA数据
        /// </summary>
        public bool PDASyncData { get; set; }

        /// <summary>
        /// 电子围栏
        /// </summary>
        public bool ElectronicFence { get; set; }

        /// <summary>
        /// 位置偏移
        /// </summary>
        public string ElectronicFenceOffset { get; set; }

        /// <summary>
        /// 特殊车辆是否打印小票
        /// </summary>
        public bool PrivilegeCarReceipt { get; set; }

        /// <summary>
        /// 建行聚合支付
        /// </summary>
        public bool CCBAggregatePay { get; set; }
    }
}