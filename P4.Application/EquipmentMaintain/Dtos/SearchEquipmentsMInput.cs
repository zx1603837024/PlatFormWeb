using Abp.Application.Services.Dto;
using System;
using Abp.Application.Services.Dto;

namespace P4.EquipmentMaintain.Dtos
{
    public class SearchEquipmentsMInput :OrderDto, IInputDto, IPagedResultRequest, IFilters
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
