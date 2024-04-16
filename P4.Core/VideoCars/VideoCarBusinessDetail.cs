﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.VideoCars
{
    /// <summary>
    /// 巡检车停车数据
    /// </summary>
    [Table("AbpVideoCarBusinessDetail")]
    public class VideoCarBusinessDetail : Entity<long>, IHasCreationTime
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
        /// 泊位号
        /// </summary>
        [MaxLength(20)]
        public virtual string BerthNumber { get; set; }

        /// <summary>
        /// 巡检车编号
        /// </summary>
        [MaxLength(30)]
        public virtual string VedioCarNumber { get; set; }

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
        /// 数据接收时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 记录是否完整
        /// true： 完整
        /// false：不完整
        /// </summary>
        public virtual bool Status { get; set; }

        /// <summary>
        ///车位状态。1=空车位，2=驶入，3=有车，4=驶出
        /// </summary>
        public virtual int State { get; set; }

        /// <summary>
        /// 车牌颜色： 0, 未知。1,蓝牌。 2,黄牌。3，白牌。 4,黑牌。5，绿牌。
        /// </summary>
        public virtual int PlateColor { get; set; }

        /// <summary>
        /// 泊位信度
        /// </summary>
        public int Confidence { get; set; }

        /// <summary>
        /// 拍摄时速
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// 视频设备第三方唯一码
        /// </summary>
        [MaxLength(50)]
        public virtual string VID { get; set; }

        /// <summary>
        /// 图片URL
        /// </summary>
        [MaxLength(250)]
        public virtual string BeforePathURL { get; set; }

        /// <summary>
        /// 图片URL
        /// </summary>
        [MaxLength(250)]
        public virtual string AfterPathURL { get; set; }

        /// <summary>
        /// 图片URL
        /// </summary>
        [MaxLength(250)]
        public virtual string OutBeforePathURL { get; set; }

        /// <summary>
        /// 特写图片URL
        /// </summary>
        [MaxLength(250)]
        public virtual string DetailOssPathURL { get; set; }

        /// <summary>
        /// 图片URL
        /// </summary>
        [MaxLength(250)]
        public virtual string OutAfterPathURL { get; set; }


        /// <summary>
        /// 图片URL
        /// </summary>
        [MaxLength(250)]
        public virtual string FixOssPathURL { get; set; }
    }
}