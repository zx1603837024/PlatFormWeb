using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace P4.Employees.Dtos
{
    /// <summary>
    /// 
    /// </summary>
     [AutoMapFrom(typeof(Employee))]
    public class EmployeeSelectDto : EntityDto<long>
    {
         public string TrueName { get; set; }
    }
}
