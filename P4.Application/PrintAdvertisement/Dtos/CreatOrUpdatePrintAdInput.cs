using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Equipments;
using System;
using System.ComponentModel.DataAnnotations;

namespace P4.PrintAdvertisement.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(PrintAd))]
    public class CreatOrUpdatePrintAdInput : EntityRequestInput, IOperDto
    {

        /// <summary>
        /// 广告名称
        /// </summary>
        public virtual string AdName { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual string Context { get; set; }

        /// <summary>
        /// 二维码
        /// </summary>
        public virtual string QrCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime BeginTime { get { return BeginTimeStr; } }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime BeginTimeStr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime EndTime { get { return EndTimeStr; } }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime EndTimeStr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }
    }
}
