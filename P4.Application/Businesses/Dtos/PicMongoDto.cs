using System;

namespace P4.Businesses.Dtos
{
    /// <summary>
    /// mongodb存储图片
    /// </summary>
    public class PicMongoDto
    {
        public string CreateTime;

        /// <summary>
        /// 
        /// </summary>
        public Guid BusinessDetailGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BusinessDetailId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PicType { get; set; }

        /// <summary>
        /// 文件保存路径
        /// </summary>
        public string FileSavePath { get; set; }
    }
}
