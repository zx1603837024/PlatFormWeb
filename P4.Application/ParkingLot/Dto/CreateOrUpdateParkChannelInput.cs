using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace P4.ParkingLot.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(ParkChannel))]
    public class CreateOrUpdateParkChannelInput : EntityDto, IInputDto, IOperDto
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ParkId
        {
            get
            {
                if (!string.IsNullOrEmpty(ParkName))
                    return int.Parse(ParkName);
                return 0;
            }
        }

        /// <summary>
        /// 公司ID
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 停车场平台通道ID
        /// </summary>
        public string ZhiBoChannelId { get; set; }

        /// <summary>
        /// 停车场类型
        /// </summary>
        public string ChannelCode { get; set; }

        /// <summary>
        /// 通道名称
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// 通道方向
        /// </summary>
        public string ChannelDirection { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public string oper { get; set; }
    }
}
