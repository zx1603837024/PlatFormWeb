using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.OtherAccounts
{
    /// <summary>
    /// 充值折扣规则
    /// </summary>
    [Table("AbpRechargeRule")]
    public class RechargeRule : Entity, IMustHaveTenant, IMustHaveCompany
    {

        /// <summary>
        /// 优惠名称
        /// </summary>
        [MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 赠送金额
        /// </summary>
        public decimal Preferential { get; set; }


        public int CompanyId { get; set; }

        public int TenantId { get; set; }
    }
}
