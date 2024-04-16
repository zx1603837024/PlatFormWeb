using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Employees
{
    /// <summary>
    /// 员工签到表
    /// </summary>
    [Table("AbpEmployeeCheck")]
    public class EmployeeCheck : Entity<long>, IMustHaveTenant, IMustHaveCompany
    {
        [ForeignKey("EmployeeId")]
        public virtual Employee EmployeeEntity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual long EmployeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public virtual string ParkId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool CheckIn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime? CheckInTime { get; set; }

        public virtual bool CheckOut { get; set; }

        public virtual DateTime? CheckOutTime { get; set; }

        /// <summary>
        /// 签到设备编号
        /// </summary>
        [MaxLength(40)]
        public virtual string DeviceCode { get; set; }
        /// <summary>
        /// 后台签退用户Id
        /// </summary>
        public virtual long? CheckOutUserId { get; set; }

        /// <summary>
        /// 后台签退用户
        /// </summary>
        [ForeignKey("CheckOutUserId")]
        public virtual Users.User User { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public virtual string BerthsecId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int CompanyId { get; set; }


        public virtual int TenantId { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [MaxLength(20)]
        public virtual string BatchNo { get; set; }

        /// <summary>
        /// 是否对账
        /// </summary>
        public virtual bool IsRepeal { get; set; }

        /// <summary>
        /// 是否正常
        /// </summary>
        public virtual bool IsNormal { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
