using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using P4.MultiTenancy;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Rates
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpRates")]
    public class Rate : FullAuditedEntity<int, Users.User>, IMustHaveTenant, IMustHaveCompany, IPassivable
   {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public virtual string RateName { get; set; }
       
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(4000)]
        public virtual string RateJson { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private  string _ratePDA;
        /// <summary>
        /// 此字段在编辑
        /// </summary>
        [MaxLength(4000)]
        public virtual string RatePDA
        {
            get { return _ratePDA; }
            set { _ratePDA = RateJson; }
        }

        /// <summary>
        /// 是否激活
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 费率备注
        /// </summary>
        [MaxLength(200)]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("TenantId")]
        public virtual Tenant Tenant { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("CompanyId")]
        public virtual Companys.OperatorsCompany OperatorsCompany { get; set; }
   }
}
