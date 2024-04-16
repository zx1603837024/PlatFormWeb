using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Equipments;
using System;

namespace P4.PrintAdvertisement.Dtos
{

    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(PrintAd))]
    public class PrintAdDto : EntityDto 
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
        public virtual DateTime BeginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string BeginTimeStr
        {
            get { return BeginTime.ToString("yyyy-MM-dd HH:mm:ss"); }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime EndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string EndTimeStr {
          get { return EndTime.ToString("yyyy-MM-dd HH:mm:ss"); }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsActive { get; set; }
    }
}
