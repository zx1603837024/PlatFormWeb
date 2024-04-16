using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.SchedulingSystem
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpSchedulingSystemEmployees")]
    public class SchedulingSystemEmployee : Entity, IMustHaveTenant, IMustHaveCompany
    {

        /// <summary>
        /// 
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 班次
        /// 1早班
        /// 2中班
        /// 3晚班
        /// 0不限班次
        /// </summary>
        public int WorkType { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TenantId { get; set; }
    }
}
