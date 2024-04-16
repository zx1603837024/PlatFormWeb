using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace P4.Businesses.Dtos
{

    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(BusinessDetail))]
    public class BusinessDetailsDto : EntityDto<long>
    {
        /// <summary>
        /// 
        /// </summary>
        public int GroupUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BerthNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OcrPlateNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal Arrearage { get; set; }

        /// <summary>
        /// 预付
        /// </summary>
        public decimal Prepaid { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal? Money { get; set; }

        /// <summary>
        /// 应收
        /// </summary>
        public decimal Receivable { get; set; }

        /// <summary>
        /// 车辆入场时间
        /// </summary>
        public DateTime? CarInTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CarInTimeString {
            get { return Convert.ToDateTime(CarInTime).ToString("yyyy-MM-dd HH:mm:ss"); }
        }
        /// <summary>
        /// 车辆出场时间
        /// </summary>
        public DateTime? CarOutTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CarOutTimeString
        {
            get
            {
                //判断出场时间是否为空
                if (CarOutTime==null)
                {
                    return "";
                }
                else
                {
                    return Convert.ToDateTime(CarOutTime).ToString("yyyy-MM-dd HH:mm:ss");
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string StopTimes {
            get 
            {
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
        } 
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? CarPayTime { get; set; }

        /// <summary>
        /// 入场收费员
        /// </summary>
        public string InEmployeeName { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string InDeviceCode { get; set; }

        /// <summary>
        /// 出场收费员
        /// </summary>
        public string OutEmployeeName { get; set; }

        /// <summary>
        /// 出场设备
        /// </summary>
        public string OutDeviceCode { get; set; }

        /// <summary>
        /// 分公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 分公司id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 收费收费员
        /// </summary>
        public string ChargeEmployeeName { get; set; }

        /// <summary>
        /// 收费设备
        /// </summary>
        public string ChargeDeviceCode { get; set; }

        /// <summary>
        /// 实收
        /// </summary>
        public decimal FactReceive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ParkId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BerthsecName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BerthsecId { get; set; }

        /// <summary>
        /// 停车时长
        /// </summary>
        public Int32? StopTime { get; set; }

        /// <summary>
        /// 商户名
        /// </summary>
        public string Tenant { get; set; }

        /// <summary>
        /// 记录状态
        /// </summary>
        public Int16 Status { get; set; }

        /// <summary>
        /// 停车场类型
        /// </summary>
        public string ParkType { get; set; }

        /// <summary>
        /// 记录状态名称
        /// 1.不完整数据
        /// 2.正常收费已缴费
        /// 3.逃逸未收费
        /// 4.逃逸已收费
        /// 5.正常收费，但是余额不足欠费 
        /// 6.电子订单已发送
        /// </summary>
        public string StatusName
        {
            get
            {
                switch (Status)
                {
                    case 1:
                        return "不完整";
                    case 2:
                        return "正常收费";
                    case 3:
                        return "逃逸";
                    case 4:
                        return "追缴";
                    case 5:
                        return "欠费";
                    case 6:
                        return "待支付";
                    default:
                        return "未知";
                }
            }
        }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public virtual Int16 CarType { get; set; }
        /// <summary>
        /// 记录状态名称
        /// 1.大型车
        /// 2.小型车
        /// 3.摩托车

        /// </summary>
        public string CarTypeName
        {
            get
            {
                switch (CarType)
                {
                    case 1:
                        return "大型车";
                    case 2:
                        return "小型车";
                    case 3:
                        return "摩托车";
                    case 4:
                        return "中型车";
                    case 5:
                        return "其他车辆";
                  
                    default:
                        return "未知";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AppAccountId { get; set; }

        /// <summary>
        /// 唯一guid
        /// </summary>
        public Guid guid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Int16 StopType { get; set; }


        /// <summary>
        /// 1.普通停车
        /// 2.包月停车
        /// 3.VIP停车
        /// 4.特殊停车
        /// 5.刷卡临时停车
        /// 6.新能源停车
        /// </summary>
        public string StopTypeName
        {

            get
            {
                switch (StopType)
                {
                    case 1:
                        return "普通停车";
                    case 2:
                        return "包月停车";
                    case 3:
                        return "VIP停车";
                    case 4:
                        return "特殊停车";
                    case 5:
                        return "刷卡临时停车";
                    case 6:
                        return "新能源停车";
                    case 7:
                        return "白名单停车";
                    default:
                        return "普通停车";
                }
            }
        }

        /// <summary>
        /// 预付支付账号关联id
        /// 现在支付无关联账号
        /// </summary>
        public long? OtherAccountId { get; set; }
        /// <summary>
        /// 出场支付账号关联id
        /// 现在支付无关联账号
        /// </summary>
        public long? ReceivableOtherAccountId { get; set; }

        /// <summary>
        /// 追缴的卡号，如果追缴是现金的话 追缴卡号为0
        /// </summary>
        public virtual string EscapeCardNo { get; set; }


        /// <summary>
        /// 追缴支付账号关联id
        /// 现在支付无关联账号
        /// </summary>
        public virtual long? EscapeOtherAccountId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Int16 PayStatus { get; set; }

        /// <summary>
        /// 出场支付类型
        /// </summary>
        public virtual string PayStatusName
        {
            get
            {
                switch (PayStatus)
                {
                    case 1:
                        return "现金";
                    case 2:
                        return "刷卡支付";
                    case 3:
                        return "微信支付";
                    case 4:
                        return "账号支付";
                    case 5:
                        return "未付";
                    case 6:
                        return "支付宝支付";
                    case 7:
                        return "其他";
                    default:
                        return "未知";
                }
            }
        }
        
        /// <summary>
        /// 卡号
        /// </summary>
        public virtual string ReceivableCarNo { get; set; }
        /// <summary>
        /// 应收车位费
        /// </summary>
       

        /// <summary>
        /// 未收款原因
        /// </summary>
        public string ArrearageReason { get; set; }
        /// <summary>
        /// 车检器入场时间
        /// </summary>
        public DateTime? SensorsInCarTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SensorsInCarTimeString
        {
            get
            {
                //判断车检器入场时间是否为空
                if (SensorsInCarTime == null)
                {
                    return "";
                }
                else
                {
                    return Convert.ToDateTime(SensorsInCarTime).ToString("yyyy-MM-dd HH:mm:ss");
                }

            }
        }

        /// <summary>
        /// 车检器出场时间
        /// </summary>
        public DateTime? SensorsOutCarTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SensorsOutCarTimeString
        {
            get
            {
                //判断车检器出场时间是否为空
                if (SensorsOutCarTime == null)
                {
                    return "";
                }
                else
                {
                    return Convert.ToDateTime(SensorsOutCarTime).ToString("yyyy-MM-dd HH:mm:ss");
                }

            }
        }

        /// <summary>
        /// 车检器停车时长
        /// </summary>
        public Int32? SensorsStopTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SensorsStopTimes
        {
            get
            {
                int Getstoptime = Convert.ToInt32(SensorsStopTime);
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
        } 

        /// <summary>
        /// 车检器应收
        /// </summary>
        public decimal? SensorsReceivable { get; set; }
        /// <summary>
        /// 打折前应收 
        /// </summary>
        public virtual decimal? BeforeDiscount { get; set; }
        /// <summary>
        /// 折扣金额= 打折前应收-总应收
        /// </summary>
        public virtual decimal? DiscountMoney { get; set; }
        /// <summary>
        /// 折扣率 9.5 九五折 8 8折
        /// </summary>
        public virtual double? DiscountRate { get; set; }

        /// <summary>
        /// 消费明细总条数
        /// </summary>
        public int BusinessCount { get; set; }





        /// <summary>
        /// 进场批次号
        /// </summary>
        public string InBatchNo { get; set; }
        /// <summary>
        /// 出厂批次号
        /// </summary>
        public string OutBatchNo { get; set; }
        /// <summary>
        /// 追缴批次号
        /// </summary>
        public string PaymentBatchNo { get; set; }
    }
}
