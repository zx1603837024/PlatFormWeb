﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Berths.Dtos
{
    /// <summary>
    /// 泊位车检器对应关系dto
    /// </summary>
    [AutoMapFrom(typeof(Berth))]
    public class BerthSensorDto : EntityDto<long>
    {
        /// <summary>
        /// 泊位号
        /// </summary>
        public string BerthNumber { get; set; }
        /// <summary>
        /// 车检器编号
        /// </summary>
        [MaxLength(30)]
        public virtual string SensorNumber { get; set; }

        /// <summary>
        /// 车检器入场时间
        /// </summary>
        public virtual DateTime? SensorsInCarTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SensorsInCarTimeStr
        {
            get{                      
                if (SensorsInCarTime.HasValue)
                {
                    return SensorsInCarTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// 车检器出场时间
        /// </summary>
        public virtual DateTime? SensorsOutCarTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SensorsOutCarTimeStr
        {
            get
            {
                if (SensorsOutCarTime.HasValue)
                {
                    return SensorsOutCarTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// 停车状态
        /// 1：再停
        /// 0：未停
        /// </summary>
        public virtual Int16? ParkStatus { get; set; }

        /// <summary>
        /// 用于pda同步用
        /// </summary>
        public virtual DateTime? SensorBeatTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SensorBeatTimeStr
        {
            get
            {
                if (SensorBeatTime.HasValue)
                {
                    return SensorBeatTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                    return null;
            }
        }

        

        /// <summary>
        /// guid 由车检器传送过来
        /// </summary>
        public virtual Guid guid { get; set; }

        /// <summary>
        /// 车检器guid
        /// </summary>
        public virtual Guid? SensorGuid { get; set; }


        #region Pos产生的字段数据

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime? InCarTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string InCarTimeStr
        {
            get
            {
                if (InCarTime.HasValue)
                {
                    return InCarTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// pos车位状态
        /// 1有车
        /// 2无车
        /// </summary>
        public virtual string BerthStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string RelateNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime? OutCarTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OutCarTimeStr
        {
            get
            {
                if (OutCarTime.HasValue)
                {
                    return OutCarTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// 停车场id
        /// </summary>
        public int ParkId { get; set; }
        #endregion
    }
}
