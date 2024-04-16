using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Companys.Dtos
{
    /// <summary>
    /// 获取公司信息类
    /// </summary>
    public class GetCompanyInput : EntityRequestInput, IInputDto
    {
        public string LoginCredentials { get; set; }
    }
}
