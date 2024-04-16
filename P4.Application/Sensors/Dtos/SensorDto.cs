using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace P4.Sensors.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Sensor))]
    public class SensorDto : EntityDto
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual int? CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int? RegionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int? ParkId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int? BerthsecId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string BerthsecName { get; set; }
        /// <summary>
        /// 泊位号
        /// </summary>

        public virtual string BerthNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string IsOnlineValue { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>

        public virtual string RelateNumber { get; set; }
        /// <summary>
        /// 进场时间
        /// </summary>

        public virtual DateTime? InCarTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string InCarTimeString
        {
            get
            {
                if (InCarTime != null)
                    return InCarTime.ToString();
                else return "";

            }

        }
        /// <summary> 
        /// 
        /// 出场时间
        /// </summary>
        public virtual DateTime? OutCarTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string OutCarTimeString
        {
            get
            {
                if (OutCarTime != null)
                    return OutCarTime.ToString();
                else return "";

            }
        }

        /// <summary>
        /// 应收
        /// </summary>
        public virtual decimal Receivable { get; set; }

        /// <summary>
        /// 停车状态
        /// 1：再停
        /// 0：未停
        /// </summary>
        public  string ParkStatus { get; set; }

        /// <summary>
        /// 电容电量
        /// </summary>
        public virtual decimal? Magnetism { get; set; }
        /// <summary>
        /// 电池电量
        /// </summary>
        public virtual decimal? Battery { get; set; }

        /// <summary>
        /// 电压上传时间
        /// </summary>
        public virtual DateTime? Updatetime { get; set; }
        /// <summary>
        /// 基站编号
        /// </summary>

        public virtual string TransmitterNumber { get; set; }

        /// <summary>
        /// 车检器编号
        /// </summary>

        public virtual string SensorNumber { get; set; }

        /// <summary>
        /// 录入时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 心跳时间
        /// </summary>
        public virtual DateTime? BeatDatetime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string BeatDateTimeString
        {
            get
            {
                if (BeatDatetime != null)
                    return BeatDatetime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                else return "";

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string IsOnline
        {
            get
            {
                if (!BeatDatetime.HasValue)
                    return "0";

                if ((DateTime.Now - BeatDatetime.Value).TotalMinutes > P4Consts.SensorDayOnline)
                {
                    return "0";
                }
                return "1";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 时间差判断车检器状态
        /// </summary>
        public int TimeDiff { get; set; }
        /// <summary>
        /// 车检器的状态
        /// </summary>
        public string SensorStatus { get; set; }
        /// <summary>
        /// 时间段内车检器的掉线次数
        /// </summary>
        public int SensorARPCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RowNumber { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string EmplopyeeCarInTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EmplopyeeCarOutTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BerthStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EmplopyeePlateNumber { get; set; }
    }
}
