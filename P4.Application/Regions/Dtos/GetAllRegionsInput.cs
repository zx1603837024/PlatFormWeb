using Abp.Application.Services.Dto;

namespace P4.Regions.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllRegionsInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        /// <summary>
        /// 
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int rows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string filters { get; set; }
    }
}
