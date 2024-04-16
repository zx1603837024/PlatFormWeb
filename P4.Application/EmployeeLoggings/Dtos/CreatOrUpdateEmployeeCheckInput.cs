using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using P4.Employees;

namespace P4.EmployeeLoggings.Dtos
{
    [AutoMapTo(typeof(EmployeeCheck))]
    public class CreatOrUpdateEmployeeCheckInput : EntityDto, IInputDto, IOperDto
    {
        public string oper { get; set; }
    }
}
