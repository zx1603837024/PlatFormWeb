using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Berthsecs;
using P4.Employees;
using P4.Inspectors;
using P4.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.WorkGroupInspectors.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(WorkGroupsInspectors))]
    public class CreateOrUpdateWorkGroupInspectorsInput : EntityRequestInput, IOperDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
         public string TenantName { get; set; }

        /// <summary>
        /// 
        /// </summary>
         public int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
         public string WorkGroupName { get; set; }

        /// <summary>
        /// 
        /// </summary>
         public int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
         public string CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
         public string EmployeesString { get; set; }

        /// <summary>
        /// 
        /// </summary>
         public string BerthSecsString { get; set; }

        /// <summary>
        /// 
        /// </summary>
         public List<Berthsec> Berthsecs { get; set; }

        /// <summary>
        /// 
        /// </summary>
         public List<Inspector> Employees {  get; set; }
        
    }
}
