using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.ParkingLot
{
    /// <summary>
    /// 停车记录图片列表
    /// </summary>
    public partial class AbpParkingRecord_Pic
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 关联订单ID
        /// </summary>
        public System.Guid RecordID { get; set; }

        /// <summary>
        /// 图片类型
        /// </summary>
        public short PicType { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string PicPath { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>

        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 图片类型
        /// </summary>
        public string PicTypeName
        {
            get
            {
                switch (PicType)
                {
                    case 1:
                        return "车牌识别";
                    case 2:
                        return "进场拍照";
                    case 3:
                        return "出场拍照";
                    default:
                        return "未知";

                }
            }
        }
    }
}
