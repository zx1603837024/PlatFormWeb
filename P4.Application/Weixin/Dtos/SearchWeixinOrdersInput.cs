using Abp.Application.Services.Dto;
using System;

namespace P4.Weixin.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchWeixinOrdersInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        /// <summary>
        /// 
        /// </summary>
        public string filters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int rows { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? DateBegin { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? DateEnd { get; set; }

        /// <summary>
        /// 微信订单号
        /// </summary>
        public string WeChatOrder { get; set; }
    }
}
