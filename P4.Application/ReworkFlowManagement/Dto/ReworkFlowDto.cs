using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.EquipmentMaintain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.ReworkFlowManagement.Dto
{
    [AutoMapFrom(typeof(ReworkFlow))]
    public class ReworkFlowDto : EntityDto
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public virtual string EqId { get; set; }

        /// <summary>
        /// 配件ID
        /// </summary>
        public virtual string PartsId { get; set; }

        /// <summary>
        /// 故障关键字
        /// </summary>
        public virtual string FaultKeyword { get; set; }

        /// <summary>
        /// 故障描述
        /// </summary>
        public virtual string FaultDescription { get; set; }

        /// <summary>
        /// 维修描述
        /// </summary>
        public virtual string MaintainDescription { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public virtual int BatchNum { get; set; }

        /// <summary>
        /// 返修状态
        /// </summary>
        public virtual string MaintainState { get; set; }
    }
}
