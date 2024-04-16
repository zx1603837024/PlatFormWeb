﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.VideoCars
{
    /// <summary>
    /// 巡检车
    /// </summary>
    [Table("AbpVideoCars")]
    public class VideoCar : Entity, IHasCreationTime
    {
        /// <summary>
        /// 商户Id
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// 分公司Id
        /// </summary>
        public virtual int? CompanyId { get; set; }

        /// <summary>
        /// 区域Id
        /// </summary>
        public virtual int? RegionId { get; set; }

        /// <summary>
        /// 停车场Id
        /// </summary>
        public virtual int? ParkId { get; set; }

        /// <summary>
        /// 泊位段Id
        /// </summary>
        public virtual int? BerthsecId { get; set; }

        /// <summary>
        /// 泊位段Id字段外键
        /// </summary>
        [ForeignKey("BerthsecId")]
        public virtual Berthsecs.Berthsec BerthSec { get; set; }

        /// <summary>
        /// 泊位号
        /// </summary>
        [MaxLength(20)]
        public virtual string BerthNumber { get; set; }

        /// <summary>
        /// 停车状态
        /// 1：再停
        /// 0：未停
        /// </summary>
        public virtual Int16? ParkStatus { get; set; }

        /// <summary>
        /// 巡检车设备编码
        /// </summary>
        [MaxLength(30)]
        public virtual string VideoCarNumber { get; set; }

        /// <summary>
        /// 巡检车类型
        ///二轮 = 1,
        ///四轮 = 2,
        /// </summary>
        public virtual Int16? VedioEqType { get; set; }

        /// <summary>
        /// 录入时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 最后心跳时间
        /// </summary>
        public virtual DateTime? BeatDatetime { get; set; }

        /// <summary>
        /// guid
        /// </summary>
        public virtual Guid? Guid { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public virtual Int16? IsOnlineValue { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual Int16? IsUse { get; set; }

        /// <summary>
        /// 泊位号Id
        /// </summary>
        public virtual int? BerthId { get; set; }

        /// <summary>
        /// 巡检车号牌
        /// </summary>
        [MaxLength(10)]
        public virtual string VideoCarPlate { get; set; }
    }
}
