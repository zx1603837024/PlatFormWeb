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
    /// PDA软件版本Dto
    /// </summary>
    [AutoMapFrom(typeof(P4.Equipments.EquipmentVersion))]
    public class EquipmentVersionDto : EntityDto
    {
        /// <summary>
        /// 设备型号
        /// </summary>
        public virtual string EqupmentType { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        public virtual string TenantName { get; set; }

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
    }
}
