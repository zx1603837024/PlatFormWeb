using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Businesses
{
    /// <summary>
    /// 关联图片
    /// </summary>
    [Table("AbpBusinessDetailPicture")]
    public class BusinessDetailPicture : CreationAuditedEntity
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
        /// 图片顺序
        /// </summary>
        public virtual UInt16 Order { get; set; }

        /// <summary>
        /// 图片类型
        /// 1.车牌识别账号，2进场照片，3.出场照片
        /// </summary>
        public virtual int PicType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
