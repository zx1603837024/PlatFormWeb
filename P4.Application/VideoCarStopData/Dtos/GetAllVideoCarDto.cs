﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.VideoEquips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.VideoCarStopData.Dtos
{
    [AutoMapFrom(typeof(VideoCars.VideoCar))]
    public class GetAllVideoCarDto : EntityDto<long>
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
        public int TenantId { get; set; }

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
        /// 视频设备编号
        /// </summary>
        public string VideoCarNumber { get; set; }

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

        /// <summary>
        /// 入场照片
        /// </summary>
        public string BeforePathURL { get; set; }
        /// <summary>
        /// 出场照片
        /// </summary>
        public string AfterPathURL { get; set; }

        /// <summary>
        /// 视频类型
        /// </summary>
        public string VideoTypeName { get; set; }

        /// <summary>
        /// 修正照片
        /// </summary>
        public string OutBeforePathURL { get; set; }

        /// <summary>
        /// 特写照片
        /// </summary>
        public string OutAfterPathURL { get; set; }

        public int Speed { get; set; }
        public string VideoCarPlate { get; set; }
        
    }
}