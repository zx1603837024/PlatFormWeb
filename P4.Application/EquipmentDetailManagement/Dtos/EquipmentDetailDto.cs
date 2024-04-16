using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.EquipmentMaintain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EquipmentDetailManagement.Dtos
{
    [AutoMapFrom(typeof(EquipmentDetail))]
    public class EquipmentDetailDto : EntityDto
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public virtual string EqId { get; set; }

        /// <summary>
        /// 配件类型
        /// </summary>
        public virtual string PartsType { get; set; }

        /// <summary>
        /// 配件编号
        /// </summary>
        public virtual string PartsId { get; set; }

        ///// <summary>
        ///// 电池编号
        ///// </summary>
        //public virtual string BatteryId { get; set; }

        ///// <summary>
        ///// SIM卡id
        ///// </summary>
        //public virtual string SimId { get; set; }

        ///// <summary>
        ///// TF卡id
        ///// </summary>
        //public virtual string TfId { get; set; }

        ///// <summary>
        ///// SAM卡id
        ///// </summary>
        //public virtual string SamId { get; set; }


        ///// <summary>
        ///// 打印机id
        ///// </summary>
        //public virtual string PrinterId { get; set; }

        ///// <summary>
        ///// 打印机电池id
        ///// </summary>
        //public virtual string PrinterBatteryId { get; set; }


        ///// <summary>
        ///// 卡带数
        ///// </summary>
        //public virtual string Cassette { get; set; }

        public virtual DateTime CreationTime { get; set; }
    }
}
