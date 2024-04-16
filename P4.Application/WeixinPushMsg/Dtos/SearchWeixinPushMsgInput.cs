using Abp.Application.Services.Dto;
using System;
namespace P4.WeixinPushMsg.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchWeixinPushMsgInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
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

    }
}
