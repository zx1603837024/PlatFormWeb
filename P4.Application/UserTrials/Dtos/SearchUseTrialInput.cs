using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;

namespace P4.UserTrials.Dtos
{
    /// <summary>
    /// 
    /// </summary>
     [AutoMapFrom(typeof(UserTrial))]
    public class SearchUseTrialInput : OrderDto, IInputDto, IPagedResultRequest, IFilters
    {
        public int page { get; set; }

        public int rows { get; set; }

        public string filters { get; set; }
    }
}
