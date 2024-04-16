using Abp.Application.Services.Dto;

namespace P4.Coupons.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchCouponInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
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
