using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.VideoEquips;

namespace P4.VideoEquipFaultsNs.Dtos
{
    /// <summary>
    /// VM模型
    /// </summary>
    [AutoMapFrom(typeof(VideoEquipFaults))]
    public class VideoEqFaultDto : EntityDto<long>
    {
        /// <summary>
        /// 
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BerthsecName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BerthNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ParkId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BerthsecId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OssPathURL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VideoNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StatusTime { get; set; }

        /// <summary>
        /// 异常描述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 视频类型
        /// </summary>
        public string VideoTypeName { get; set; }
    }
}
