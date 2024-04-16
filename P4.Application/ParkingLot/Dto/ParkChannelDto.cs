using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Parks;
using System;
using System.ComponentModel.DataAnnotations;

namespace P4.ParkingLot.Dtos
{
    /// <summary>
    /// 停车场通道
    /// </summary>
    [AutoMapFrom(typeof(ParkChannel))]
    public class ParkChannelDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 停车场Id
        /// </summary>
        public  int ParkId { get; set; }

        /// <summary>
        /// 停车场
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 停车场平台通道ID
        /// </summary>
        public string ZhiBoChannelId { get; set; }

        /// <summary>
        /// 停车场类型
        /// </summary>
        public string ChannelCode { get; set; }

        /// <summary>
        /// 通道名称
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// 通道方向
        /// </summary>
        public string ChannelDirection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorUserName { get; set; }

    }
}
