using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Businesses
{
    [Table("AbpIllegalOpenSignoPicture")]
    public class IllegalOpenSignoPicture :  CreationAuditedEntity
    {
        /// <summary>
        /// 收费记录id
        /// </summary>
        public virtual long BusinessDetailId { get; set; }

        /// <summary>
        /// 关联收费记录guid
        /// </summary>
        public virtual Guid BusinessDetailGuid { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        [MaxLength(500)]
        public virtual string BusinessDetailPictureUrl { get; set; }

        /// <summary>
        /// 道闸设备编号
        /// </summary>
        [MaxLength(50)]
        public virtual string SignoDeviceNo { get; set; }

        /// <summary>
        /// 拍照时间
        /// </summary>
        public virtual DateTime PhotoTime { get; set; }
    }
}
