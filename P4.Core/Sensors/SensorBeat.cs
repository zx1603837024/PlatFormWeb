using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Sensors
{
    /// <summary>
    /// 车检器心跳
    /// 30分钟计算一次
    /// </summary>
    [Table("AbpSensorBeats")]
    public class SensorBeat: Entity, IMayHaveTenant, IMayHaveCompany
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
        /// 分公司Id
        /// </summary>
        public virtual int? CompanyId { get; set; }
    }
}
