using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Berthsecs;
using P4.Employees;
using P4.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.WorkGroups.Dto
{
    [AutoMapTo(typeof(WorkGroup))]
    public class CreateOrUpdateWorkGroupInput :EntityRequestInput, IOperDto
    {
        public string oper { get; set; }
        public bool IsActive { get; set; }
         public string TenantName { get; set; }
         public int TenantId { get; set; }
         public string WorkGroupName { get; set; }
         public int CompanyId { get; set; }

         public string CompanyName { get; set; }
         public string EmployeesString { get; set; }
         public string BerthSecsString { get; set; }

        /// <summary>
        /// 上班类型（1为早班，2为中班，3为晚班）
        /// </summary>
        public int Status { get; set; }

         public List<Berthsec> Berthsecs { get; set; }
         public List<Employee> Employees {  get; set; }
        
    }
}
