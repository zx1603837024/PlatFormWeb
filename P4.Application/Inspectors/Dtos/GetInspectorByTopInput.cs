using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inspectors.Dtos
{
    /// <summary>
    /// 获取指定记录条数输入类
    /// </summary>
    public class GetInspectorByTopInput : IInputDto
    {
        public int Count { get; set; }
    }
}
