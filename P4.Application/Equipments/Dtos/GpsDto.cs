using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace P4.Equipments.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(EquipmentGps))]
    public class GpsDto : EntityDto<long>
    {
        /// <summary>
        /// 
        /// </summary>
        public  string PDA { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public  string X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public  string Y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public  DateTime CreationTime { get; set; }
        /// <summary>
        /// 创建用户（收费员Id）
        /// </summary>
        public  long? CreatorUserId { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; }
    }
}
