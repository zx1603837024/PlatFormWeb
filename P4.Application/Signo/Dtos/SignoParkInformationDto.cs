using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Signo.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(SignoParkInformation))]
    public class SignoParkInformationDto: EntityDto<long>
    {
        /// <summary>
        /// 停车场
        /// </summary>
        public  int ParkId { get; set; }

        /// <summary>
        /// 通讯时间
        /// </summary>
        public  DateTime CommunicationTime { get; set; }
        /// <summary>
        /// 唯一key
        /// </summary>
        public  Guid Guid { get; set; }

        /// <summary>
        /// 进场道闸数
        /// </summary>
        public int SignoInSize { get; set; }

         /// <summary>
        /// 出场道闸数
        /// </summary>
        public int SignoOutSize { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int ParkDoorStatus { get; set; }
    }
}
