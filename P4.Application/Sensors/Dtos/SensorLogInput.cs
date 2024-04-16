using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;

namespace P4.Sensors.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(SensorLog))]
    public class SensorLogInput : IInputDto
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
        /// 录入时间
        /// </summary>
        public DateTime CreationTime
        {
            get { return DateTime.Now; }
        }
    }
}
