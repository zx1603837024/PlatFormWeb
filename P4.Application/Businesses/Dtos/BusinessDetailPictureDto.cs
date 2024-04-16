using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;

namespace P4.Businesses.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(BusinessDetailPicture))]
    public class BusinessDetailPictureDto : EntityDto
    {

        /// <summary>
        /// 
        /// </summary>
        public long BusinessDetailId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BusinessDetailPictureUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PicType { get; set; }

        /// <summary>
        /// 
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

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 道闸类型
        /// </summary>
        public string ParkDoorStatus { get; set; }

        /// <summary>
        /// 道闸编号
        /// </summary>
        public string SignoDeviceNo { get; set; }
    }
}
