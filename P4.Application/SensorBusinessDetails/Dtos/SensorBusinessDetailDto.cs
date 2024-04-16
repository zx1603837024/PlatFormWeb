using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using P4.Sensors;
using Abp.Application.Services.Dto;

namespace P4.SensorBusinessDetails.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(SensorBusinessDetail))]
    public class SensorBusinessDetailDto : EntityDto<long>
    {
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
        public  int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ParkId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BerthsecId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SensorNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Receivable { get; set; }

        /// <summary>
        /// 地磁入场时间
        /// </summary>
        public virtual DateTime? CarInTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string InCarTimeStr
        {
            get
            {
                if (CarInTime.HasValue)
                {
                    return CarInTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                return null;
            }
        }

        /// <summary>
        /// 地磁出场时间
        /// </summary>
        public virtual DateTime? CarOutTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OutCarTimeStr
        {
            get
            {
                if (CarOutTime.HasValue)
                {
                    return CarOutTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Int32? StopTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
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
    }
}
