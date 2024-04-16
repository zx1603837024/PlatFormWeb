using Abp.Application.Services.Dto;

namespace P4.Card.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllIPassCardInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
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
