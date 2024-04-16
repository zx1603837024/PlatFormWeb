using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Sensors
{
    /// <summary>
    /// 车检器检测车辆进出场数据
    /// </summary>
    [Table("AbpSensorBusinessDetail")]
    public class SensorBusinessDetail : Entity<long>, IHasCreationTime
    {
        public virtual int? TenantId { get; set; }

        public virtual int? CompanyId { get; set; }

        public virtual int? RegionId { get; set; }

        public virtual int? ParkId { get; set; }

        public virtual int? BerthsecId { get; set; }

        /// <summary>
        /// 泊位号
        /// </summary>
        [MaxLength(20)]
        public virtual string BerthNumber { get; set; }

        /// <summary>
        /// 车检器编号
        /// </summary>
        [MaxLength(30)]
        public virtual string SensorNumber { get; set; }

        /// <summary>
        /// 应收
        /// </summary>
        public virtual decimal? Receivable { get; set; }

        /// <summary>
        /// 车牌号
        /// 如果未识别车辆，为空?
        /// </summary>
        [MaxLength(10)]
        public virtual string PlateNumber { get; set; }

        /// <summary>
        /// 车辆入场时间
        /// </summary>
        public virtual DateTime? CarInTime { get; set; }

        /// <summary>
        /// 车辆出场时间
        /// </summary>
        public virtual DateTime? CarOutTime { get; set; }

        /// <summary>
        /// 停车时长
        /// </summary>
        public virtual int? StopTime { get; set; }

        /// <summary>
        /// 唯一标示
        /// 关联收费明细表中guid字段
        /// </summary>
        public virtual Guid guid { get; set; }

        /// <summary>
        /// 配对标示
        /// </summary>
        [MaxLength(10)]
        public virtual string Indicate { get; set; }

        /// <summary>
        /// 数据接收时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 记录是否完整
        /// true： 完整
        /// false：不完整
        /// </summary>
        public virtual bool Status { get; set; }
    }
}
