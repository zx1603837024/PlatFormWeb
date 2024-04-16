using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.VideoEquips
{
    /// <summary>
    /// 视频桩异常推送表
    /// </summary>
    [Table("AbpVideoEquipFaults")]
    public class VideoEquipFaults : Entity<long>, IHasCreationTime
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
        /// 泊位号Id
        /// </summary>
        public virtual int? BerthsecId { get; set; }

        /// <summary>
        /// 泊位号
        /// </summary>
        [MaxLength(20)]
        public virtual string BerthNumber { get; set; }

        /// <summary>
        /// 视频桩编号
        /// </summary>
        [MaxLength(30)]
        public virtual string VedioEqNumber { get; set; }

        /// <summary>
        /// 数据接收时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 视频设备第三方唯一码
        /// </summary>
        [MaxLength(50)]
        public virtual string VID { get; set; }

        /// <summary>
        /// 设备状态
        /// </summary>
        public virtual string Status { get; set; }


        /// <summary>
        /// 故障描述
        /// </summary>
        public virtual string Remark { get; set; }

        /// <summary>
        /// 故障时间
        /// </summary>
        public virtual string StatusTime { get; set; }

        /// <summary>
        /// 故障图片URL
        /// </summary>
        [MaxLength(250)]
        public virtual string OssPathURL { get; set; }
    }
}
