using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Sensors.Dtos
{
    [AutoMapFrom(typeof(SensorRunStatus))]
    public class SensorRunStatusDto : EntityDto<long>
    {
        /// <summary>
        /// 车检器编号
        /// </summary>
        [MaxLength(15)]
        public virtual string SensorNumber { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime BeginTime { get; set; }

        public virtual string BeginTimeString { get { return BeginTime.ToString("yyyy-MM-dd HH:mm:ss"); } }

        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime? EndTime { get; set; }

        public virtual string EndTimeString
        {
            get
            {
                if (EndTime.HasValue)
                    return EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                else
                    return "";
            }
        }

        /// <summary>
        /// 时长
        /// </summary>
        public virtual int Duration { get; set; }

        /// <summary>
        /// 停车时长
        /// </summary>
        public string StopTimes
        {
            get
            {
                int Getstoptime = Convert.ToInt32(Duration);
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
        /// true： 正常
        /// false：异常
        /// </summary>
        public virtual bool Status { get; set; }

        public virtual string StatusString
        {
            get
            {
                if (Status)
                    return "正常";
                else
                    return "故障";
            }
        }
    }
}
