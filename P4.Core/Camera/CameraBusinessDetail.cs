using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Camera
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpCameraBusinessDetails")]
    public class CameraBusinessDetail : Entity<long>,  IHasCreationTime
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(10)]
        public string cmd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int picnum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(10)]
        public string license { get; set; }

        /// <summary>
        /// 停车状态
        /// 0未知
        /// 1车辆驶入
        /// 2车辆驶离
        /// 3车辆停稳
        /// </summary>
        public int direction { get; set; }

        /// <summary>
        /// query:需要服务器端回复 
        /// message:不需要服务器端回复 
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 车牌颜色
        /// </summary>
        public int colortype { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int confidence { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(10)]
        public string newlicense { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int newtype { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int newcolortype { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int newconfidence { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public string devid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(10)]
        public string BaseId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
