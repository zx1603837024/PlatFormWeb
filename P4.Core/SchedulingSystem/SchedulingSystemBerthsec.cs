using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.SchedulingSystem
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpSchedulingSystemBerthsecs")]
    public  class SchedulingSystemBerthsec : Entity, IMustHaveTenant, IMustHaveCompany
    {
        /// <summary>
        /// 泊位段Id
        /// </summary>
        public int BerthsecId { get; set; }

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
