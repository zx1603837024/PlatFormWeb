using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.ParkingLot.Dto
{
    /// <summary>
    /// 修改停车场通道
    /// </summary>
    public class ModifyParkChannelInput
    {
        /// <summary>
        /// 停车场ID
        /// </summary>
        public int parkId { get; set; }

        /// <summary>
        /// 通道列表
        /// </summary>
        public List<ModifyParkChannelInput_Channel> Channels { get; set; }
    }

    /// <summary>
    /// 通道
    /// </summary>
    public class ModifyParkChannelInput_Channel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 通道编码
        /// </summary>
        public string ChannelCode { get; set; }

        /// <summary>
        /// 通道名称
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// 通道方向
        /// </summary>
        public int ChannelDirection { get; set; }

        /// <summary>
        /// 平台ID
        /// </summary>
        public string ZhiBoChannelId { get; set; }
    }
}
