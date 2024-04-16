using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EquipmentMaintain
{
    /// <summary>
    /// 公司工厂快递表
    /// </summary>
    [Table("AbpCompanyFactoryExpress")]
    public class CompanyFactoryExpress : FullAuditedEntity
    {
        /// <summary>
        /// 公司工厂快递流水号
        /// </summary>
        [MaxLength(30)]
        public virtual string CompanyFactoryExpressSerialNum { get; set; }

        /// <summary>
        /// 公司工厂快递单号
        /// </summary>
        [MaxLength(30)]
        public virtual string CompanyFactoryExpressId { get; set; }

        /// <summary>
        /// 公司工厂快递状态
        /// </summary>
        [MaxLength(10)]
        public virtual string CompanyFactoryExpressState { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public virtual int BatchNum { get; set; }

        /// <summary>
        /// 工厂
        /// </summary>
        [MaxLength(50)]
        public virtual string FactoryId { get; set; }

        /// <summary>
        /// 设备发货类型(正常发货/返修)
        /// </summary>
        [MaxLength(10)]
        public virtual string EquipmentDeliveryType { get; set; }
    }
}
