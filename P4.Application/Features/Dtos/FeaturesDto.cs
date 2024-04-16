using Abp.Application.Features;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Features.Dtos
{
    [AutoMapFrom(typeof(FeatureLimitation))]
    public class FeaturesDto : EntityDto<int>
    {
        public string Name { get; set; }

        public string DefaultValue { get; set; }


        public Int16 Scope { get; set; }
    }
}
