using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Sensors.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchSensorPosDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int64 RowNumber { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string guid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BerthsecName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BerthNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SensorNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PosCarInTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PosCarOutTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SensorCarInTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SensorCarOutTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreationTime { get; set; }

        /// <summary>
        /// pos机停车时长
        /// </summary>
        public int PosStopTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PosStopTimes
        {
            get
            {
                if (string.IsNullOrWhiteSpace(PosCarOutTime))
                    return "";
                int Getstoptime = Convert.ToInt32(PosStopTime);
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
        /// 地磁停车时长
        /// </summary>
        public int SensorStopTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SensorStopTimes
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SensorCarOutTime))
                    return "";
                int Getstoptime = Convert.ToInt32(SensorStopTime);
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
        /// 
        /// </summary>
        public decimal PosMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal SensorMoney { get; set; }

        /// <summary>
        /// 流水号
        /// </summary>
        public string Indicate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int StopType { get; set; }
        /// <summary>
        /// 停车类型
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
        /// 
        /// </summary>
        public Int64 InOperaId { get; set; }
        /// <summary>
        /// 进场收费员
        /// </summary>
        public string InOperaName { get; set; }
    }
}
