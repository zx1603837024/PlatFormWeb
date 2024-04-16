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
    /// 公司客户快递表
    /// </summary>
     [Table("AbpCompanyCustomerExpress")]
    public class CompanyCustomerExpress : FullAuditedEntity,IMustHaveCompany
    {
        /// <summary>
        /// 公司客户快递流水号
        /// </summary>
        [MaxLength(30)]
         public virtual string CompanyCustomerExpressSerialNum { get; set; }

        /// <summary>
        /// 公司客户快递单号
        /// </summary>
        [MaxLength(30)]
        public virtual string CompanyCustomerExpressId { get; set; }

        /// <summary>
        /// 公司客户快递状态
        /// </summary>
        [MaxLength(10)]
        public virtual string CompanyCustomerExpressState { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public virtual int BatchNum { get; set; }

        ///// <summary>
        ///// 客户
        ///// </summary>
        //[MaxLength(50)]
        //public virtual string Customer { get; set; }

          /// <summary>
        /// 设备发货类型(正常发货/返修)
        /// </summary>
        [MaxLength(10)]
        public virtual string EquipmentDeliveryType { get; set; }


        public int CompanyId { get; set; }
    }
}
