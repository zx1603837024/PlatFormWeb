using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Sensors.Dtos
{
    [AutoMapFrom(typeof(SensorBeat))]
    public class SensorBeatDto : EntityDto
    {
        /// <summary>
        /// 总车检器数
        /// </summary>
        public virtual int SensorCount { get; set; }

        /// <summary>
        /// 故障数据
        /// </summary>
        public virtual int FaultCount { get; set; }

        /// <summary>
        /// 商户id
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 分公司id
        /// </summary>
        public virtual int? CompanyId { get; set; }
    }
}
