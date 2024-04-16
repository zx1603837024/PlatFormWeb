using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;

namespace P4.Sensors.Dtos
{

    /// <summary>
    /// 基站通讯日志
    /// </summary>
    [AutoMapFrom(typeof(SensorLog))]
    public class SensorLogDto : EntityDto<long>
    {
        /// <summary>
        /// 通讯内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否发生异常
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// 回执命令
        /// </summary>
        public string ReceiptCmd { get; set; }

        /// <summary>
        ///  上传时间
        /// </summary>
        public DateTime CreationTime { get; set; }

    }
}
