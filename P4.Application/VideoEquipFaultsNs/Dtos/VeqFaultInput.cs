using Abp.Application.Services.Dto;

namespace P4.VideoEquipFaultsNs.Dtos
{
    /// <summary>
    /// 输入模型
    /// </summary>
    public class VeqFaultInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
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
