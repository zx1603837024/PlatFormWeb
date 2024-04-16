using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Inspectors
{

    /// <summary>
    /// 员工签到表
    /// </summary>
    [Table("AbpInspectorCheck")]
    public class InspectorCheck : Entity<long>, IMustHaveTenant, IMustHaveCompany
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual long InspectorId { get; set; }

        [ForeignKey("InspectorId")]
        public virtual Inspector InspectorEntity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int ParkId { get; set; }

        public virtual bool CheckIn { get; set; }

        public virtual DateTime? CheckInTime { get; set; }

        public virtual bool CheckOut { get; set; }

        public virtual DateTime? CheckOutTime { get; set; }

        /// <summary>
        /// 签到设备编号
        /// </summary>
        [MaxLength(40)]
        public virtual string DeviceCode { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual int BerthsecId { get; set; }

        public virtual int CompanyId { get; set; }

        public virtual int TenantId { get; set; }
    }
}
