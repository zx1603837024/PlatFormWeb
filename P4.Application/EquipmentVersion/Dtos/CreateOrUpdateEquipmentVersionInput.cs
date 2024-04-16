using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EquipmentVersion.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(P4.Equipments.EquipmentVersion))]
    public class CreateOrUpdateEquipmentVersionInput : EntityDto, IInputDto, IOperDto
    {

        /// <summary>
        /// 设备型号
        /// </summary>
        public virtual string EqupmentType { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public virtual string Version { get; set; }

        /// <summary>
        /// 是否整个型号自动升级（针对商户）
        /// </summary>
        public virtual bool IsUpgrade { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool IsActive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string oper { get; set; }
    }
}
