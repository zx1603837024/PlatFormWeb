using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.EquipmentMaintain
{
    /// <summary>
    /// 设备表
    /// </summary>
    [Table("AbpEquipmentsM")]
    public class EquipmentsM : Entity, IMayHaveTenant, IMayHaveCompany, IHasCreationTime
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        [MaxLength(50)]
        public virtual string EqId { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        [MaxLength(100)]
        public virtual string EqName { get; set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        [MaxLength(30)]
        public virtual string EqVersion { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public virtual int BatchNum { get; set; }

        /// <summary>
        /// 生产工厂
        /// </summary>
        [MaxLength(50)]
        public virtual string EqFactory { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 分公司编号
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// 企业编号
        /// </summary>
        public virtual int? CompanyId { get; set; }
    }
}
