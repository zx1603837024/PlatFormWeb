using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EquipmentMaintain.Dtos
{
    [AutoMapFrom(typeof(EquipmentsM))]
   public class EquipmentsMDto:EntityDto
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public virtual string EqId { get; set; }

       /// <summary>
       /// 设备名称
       /// </summary>
       public virtual string EqName { get; set; }

       /// <summary>
       /// 设备型号
       /// </summary>
       public virtual string EqVersion { get; set; }

       /// <summary>
       /// 批次号
       /// </summary>
       public virtual int BatchNum { get; set; }

       /// <summary>
       /// 生产工厂
       /// </summary>
       public virtual string EqFactory { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
       public virtual DateTime CreationTime { get; set; }

       /// <summary>
       /// 设备型号名称
       /// </summary>
       public virtual string EqVersionName { get; set; }
    }
}
