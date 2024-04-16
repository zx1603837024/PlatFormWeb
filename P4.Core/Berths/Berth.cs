using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Berths
{
    /// <summary>
    /// 泊位表
    /// </summary>
    [Table("AbpBerths")]
    public class Berth : Entity<long>, IModificationAudited, ICreationAudited, IMustHaveRegion, IMustHavePark, IMustHaveCompany, IMustHaveBerthsec, IMustHaveTenant, IPassivable
    {

        /// <summary>
        /// 泊位号
        /// </summary>
        [MaxLength(20)]
        public virtual string BerthNumber { get; set; }

        /// <summary>
        /// 泊位再停状态
        /// 1: 在停
        /// 2: 未停
        /// 3: 15分钟免费停
        /// </summary>
        [MaxLength(10)]
        public virtual string BerthStatus { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        [MaxLength(10)]
        public virtual string RelateNumber { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public virtual Int16 CarType { get; set; }
        /// <summary>
        /// 进场时间
        /// </summary>
        public virtual DateTime? InCarTime { get; set; }
        /// <summary> 
        /// 
        /// 出场时间
        /// </summary>
        public virtual DateTime? OutCarTime { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public virtual int RegionId { get; set; }

        /// <summary>
        /// 停车场
        /// </summary>
        public virtual int ParkId { get; set; }

        /// <summary>
        /// 车检器编号
        /// </summary>
        [MaxLength(30)]
        public virtual string SensorNumber { get; set; }

        /// <summary>
        /// 车检器入场时间
        /// </summary>
        public virtual DateTime? SensorsInCarTime { get; set; }

        /// <summary>
        /// 车检器出场时间
        /// </summary>
        public virtual DateTime? SensorsOutCarTime { get; set; }

        /// <summary>
        /// 车检器停车状态
        /// </summary>
        public virtual Int16? ParkStatus { get; set; }

        /// <summary>
        /// 用于pda同步用
        /// </summary>
        public virtual DateTime? SensorBeatTime { get; set; }


        [ForeignKey("ParkId")]
        public virtual Parks.Park Park { get; set; }

        /// <summary>
        /// 泊位段编号
        /// </summary>
        public virtual int BerthsecId { get; set; }


        public virtual int TenantId { get; set; }

        [ForeignKey("TenantId")]
        public virtual MultiTenancy.Tenant Tenant { get; set; }

        public virtual DateTime? LastModificationTime { get; set; }

        public virtual long? LastModifierUserId { get; set; }

        public virtual long? CreatorUserId { get; set; }

        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 分公司
        /// </summary>
        public virtual int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("CompanyId")]
        public virtual Companys.OperatorsCompany Company { get; set; }

        /// <summary>
        /// 唯一key
        /// </summary>
        public virtual Guid guid { get; set; }

        /// <summary>
        /// 车检器guid
        /// </summary>
        public virtual Guid? SensorGuid { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        [MaxLength(20)]
        public virtual string CardNo { get; set; }

        /// <summary>
        /// 预付费
        /// </summary>
        public virtual decimal Prepaid { get; set; }

        /// <summary>
        /// 数据来源是否视频设备
        /// </summary>
        public virtual bool? IsSourceVideo { get; set; }

        /// <summary>
        /// 标识泊位是否异常
        /// </summary>
        public virtual int? IsFaultFlag { get; set; }
    }
}
