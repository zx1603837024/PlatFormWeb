using System;
using Abp.Application.Services.Dto;

namespace P4.CardCode.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllIPassCardCodeInput: OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        public int page { get; set; }

        public int rows { get; set; }

        public string filters { get; set; }

       

    }
}
