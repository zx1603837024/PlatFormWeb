using Abp.Application.Services.Dto;

namespace P4.Weixin.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchWeixinUserInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
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
