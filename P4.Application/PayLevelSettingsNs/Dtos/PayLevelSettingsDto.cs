using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.PayLevelSetting;


namespace P4.PayLevelSettingsNs.Dtos
{
    [AutoMapFrom(typeof(PayLevelSettings))]
    public class PayLevelSettingsDto
    {
        public virtual int Id { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual Int16? IsDelete { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public virtual int? DeviceType { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        [MaxLength(30)]
        public virtual string DeviceName { get; set; }

        /// <summary>
        /// 设备优先级
        /// </summary>
        public virtual int? DeviceOrder { get; set; }

        /// <summary>
        /// 最后操作时间
        /// </summary>
        [MaxLength(30)]
        public virtual string LastUser { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public virtual DateTime LastUpdateTime { get; set; }
    }
}
