using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Signo
{
    /// <summary>
    /// 系统硬件异常记录
    /// </summary>
    [Table("AbpVehiclesError")]
    public class VehiclesError : Entity<long>, IMustHaveCompany, IMustHaveTenant
    {
        /// <summary>
        /// 序号
        /// </summary>
        public virtual int SID { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        [MaxLength(50)]
        public virtual string SDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(50)]
        public virtual string SMemo { get; set; }
        /// <summary>
        /// 商户ID
        /// </summary>
        public virtual int TenantId { get; set; }
        /// <summary>
        /// 分公司
        /// </summary>
        public virtual int CompanyId { get; set; }     
        /// <summary>
        /// 停车场Id
        /// </summary>
        public virtual int ParkId { get; set; }
    }
}
