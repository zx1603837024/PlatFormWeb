using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inspectors.Dtos
{
    /// <summary>
    /// 获取指定条数输出对象
    /// </summary>
    public class GetInspectorByTopOutput : IOutputDto
    {
        public List<InspectorDto> rows { get; set; }
    }
}
