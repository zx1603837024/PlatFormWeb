﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Businesses.Dtos
{
    public class EscapeDetailsDtoTow
    {


        public long Id { get; set; }
        /// <summary>
        /// 泊位号
        /// </summary>
        public string BerthNumber { get; set; }
        /// <summary>
        /// 欠费金额
        /// </summary>
        public virtual decimal Arrearage { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Prepaid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Receivable { get; set; }

        /// <summary>
        /// 车辆入场时间
        /// </summary>
        public DateTime? CarInTime { get; set; }
        public string CarInTimeString
        {
            get { return Convert.ToDateTime(CarInTime).ToString("yyyy-MM-dd HH:mm:ss"); }
        }
        /// <summary>
        /// 车辆出场时间
        /// </summary>
        public DateTime? CarOutTime { get; set; }
        public string CarOutTimeString
        {
            get
            {
                //判断出场时间是否为空
                if (CarOutTime == null)
                {
                    return "";
                }
                else
                {
                    return Convert.ToDateTime(CarOutTime).ToString("yyyy-MM-dd HH:mm:ss");
                }

            }
        }
        ///


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
        /// 逃逸追缴收费员
        /// </summary>
        public virtual long? EscapeOperaId { get; set; }

        /// <summary>
        /// 逃逸追缴收费员名称
        /// </summary>

        public string EscapeEmployeeName
        {
            get
            {
                if (EscapeEmployeeName == null)
                    return "";
                else
                    return EscapeEmployeeName;
            }
            
        }


        /// <summary>
        /// 逃逸追缴设备
        /// </summary>

        public virtual string EscapeDeviceCode 
        {
            get 
            {
                if (EscapeDeviceCode == null)
                    return "";
                else
                    return EscapeDeviceCode;
            }
          
        }

        /// <summary>
        /// 出场设备
        /// </summary>
        public string OutDeviceCode { get; set; }

        /// <summary>
        /// 停车场
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BerthsecName { get; set; }

        /// <summary>
        /// 停车时长
        /// </summary>
        public Int32? StopTime { get; set; }

        public string StopTimes
        {
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
        /// 是否已支付
        /// </summary>
        public bool IsEscapePay { get; set; }

        /// <summary>
        /// 后台追缴类型
        /// </summary>
        public Int16 PaymentType { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal? Repayment { get; set; }

        /// <summary>
        /// 补缴时间
        /// </summary>
        public DateTime? CarRepaymentTime { get; set; }

        /// <summary>
        /// 商户名
        /// </summary>
        public string Tenant { get; set; }


        /// <summary>
        /// 记录状态
        /// </summary>
        public Int16 Status { get; set; }

        /// <summary>
        /// 记录状态名称
        /// </summary>
        public string StatusName
        {
            get
            {
                switch (Status)
                {
                    case 3:
                        return "未收费";
                    case 4:
                        return "已收费";
                    case 5:
                        return "欠费";
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

                    default:
                        return "未知";
                }
            }
        }

        public string ChargeOperaName 
        {
            get 
            {
                if (ChargeOperaName == null)
                {
                    return "";
                }
                else
                {
                    return ChargeOperaName;
                }
            }
         
        }

    }
}
