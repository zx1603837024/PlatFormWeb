using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Sensors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Transmitters.Dtos
{
    [AutoMapFrom(typeof(Transmitter))]
    public class TransmitterDto : EntityDto
    {
        /// <summary>
        /// 基站编号
        /// </summary>

        public virtual string TransmitterNumber { get; set; }
        public virtual string TransmitterName { get; set; }

        /// <summary>
        /// 电池电量
        /// </summary>
        public virtual decimal? VoltageCaution { get; set; }

        /// <summary>
        /// 电压上传时间
        /// </summary>
        public virtual DateTime? Updatetime { get; set; }


        public virtual string X { get; set; }

        public virtual string Y { get; set; }

        /// <summary>
        /// 设备地址
        /// </summary>

        public virtual string Address { get; set; }
        /// <summary>
        /// 商户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 录入时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 心跳时间
        /// </summary>
        public virtual DateTime? BeatDatetime { get; set; }

        /// <summary>
        /// 基站是否在线
        /// </summary>
        public string IsOnline
        {
            get
            {
                if (BeatDatetime.Value == null)
                    return "0";

                if ((DateTime.Now - BeatDatetime.Value).TotalMinutes > P4Consts.SensorDayOnline)
                {
                    return "0";
                }
                return "1";
            }
        }
    }
}
