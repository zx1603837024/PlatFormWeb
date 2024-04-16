using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace P4.Rates.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Rate))]
    public class RateInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        //[Required]
        public string TenantId { get; set; }

        public int rows { get; set; }

        public int page { get; set; }


        public string filters { get; set; }


        public bool _search { get; set; }
    }
}