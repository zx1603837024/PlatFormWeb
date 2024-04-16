using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Businesses
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpPictureStores")]
    public class PictureStore : CreationAuditedEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid BusinessDetailGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BusinessDetailId { get; set; }

        /// <summary>
        /// 图片类型
        /// 1：车牌识别账号
        /// 2：进场照片
        /// 3：出场照片
        /// </summary>
        public int PicType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// 是否识别
        /// </summary>
        public int OcrType { get; set; }

        /// <summary>
        /// 文件保存路径
        /// </summary>
        public string FileSavePath { get; set; }
    }
}
