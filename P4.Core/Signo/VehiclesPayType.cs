using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Signo
{
    /// <summary>
    ///  车辆收费方式
    /// </summary>
    [Table("AbpVehiclesPayType")]
    public class VehiclesPayType : Entity<long>, IMustHaveCompany, IMustHaveTenant
    {
        /// <summary>
        /// 序号
        /// </summary>
        public virtual int SRateTypeID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(50)]
        public virtual string SRateTypeName { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public virtual int SCarStyleID { get; set; }
        /// <summary>
        /// 编号名称
        /// </summary>
        [MaxLength(50)]
        public virtual string SCarStyleName { get; set; }
        /// <summary>
        /// 商户ID
        /// </summary>
        public virtual int TenantId { get; set; }
        /// <summary>
        /// 分公司
        /// </summary>
        public virtual int CompanyId { get; set; }
    }
}
