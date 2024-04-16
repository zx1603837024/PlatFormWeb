using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using P4.Companys;
using P4.MultiTenancy;
using P4.Parks;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Signo
{
    /// <summary>
    /// 道闸出入信息
    /// </summary>
    [Table("AbpSignoParkInformations")]
    public class SignoParkInformation : Entity<long>, IModificationAudited, ICreationAudited, IMustHaveRegion, IMustHavePark, IMustHaveCompany, IMustHaveBerthsec, IMustHaveTenant, IPassivable
    {
        /// <summary>
        /// 商户
        /// </summary>
        public virtual int TenantId { get; set; }
        [ForeignKey("TenantId")]
        public virtual Tenant Tenant { get; set; }
        /// <summary>
        /// 分公司
        /// </summary>
        public virtual int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual OperatorsCompany Company { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public virtual int RegionId { get; set; }
        /// <summary>
        /// 停车场
        /// </summary>
        public virtual int ParkId { get; set; }
        [ForeignKey("ParkId")]
        public virtual Park Park { get; set; }
        /// <summary>
        /// 泊位段编号
        /// </summary>
        public virtual int BerthsecId { get; set; }
        /// <summary>
        /// 停车场出入口名称
        /// </summary>
        [MaxLength(50)]
        public virtual string ParkDoorName { get; set; }
        /// <summary>
        /// 停车场出入口设备编号
        /// </summary>
        [MaxLength(50)]
        public virtual string SignoDeviceNo { get; set; }
        /// <summary>
        /// 停车场出入口类型
        /// </summary>
        public virtual int ParkDoorStatus { get; set; }
        /// <summary>
        /// 唯一key
        /// </summary>
        public virtual Guid Guid { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool IsActive { get; set; }

        public virtual long? CreatorUserId { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public virtual DateTime? LastModificationTime { get; set; }

        public virtual long? LastModifierUserId { get; set; }
    }
}
