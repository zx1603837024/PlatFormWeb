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
    /// 钱包扣费记录
    /// </summary>
    [Table("AbpDeductionRecords")]
    public class DeductionRecord : Entity<long>, IMustHaveTenant, IMustHaveCompany
    {
        /// <summary>
        /// 账号id
        /// </summary>
        public virtual long OtherAccountId { get; set; }

        [ForeignKey("OtherAccountId")]
        public virtual OtherAccount OtherAccount { get; set; }

        /// <summary>
        /// 费用类型
        /// 1开卡（充值，续费），2消费
        /// ，3预付，4返还 
        /// </summary>
        public virtual Int16 OperType { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public virtual decimal Money { get; set; }

        /// <summary>
        /// 成功状态
        /// true成功
        /// false未成功
        /// </summary>
        public virtual bool PayStatus { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public virtual DateTime InTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(30)]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        [MaxLength(20)]
        public virtual string CardNo { get; set; }

        /// <summary>
        /// 收费员id
        /// </summary>
        public virtual long EmployeeId { get; set; }

        /// <summary>
        /// 系统账号id
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [MaxLength(20)]
        public virtual string PlateNumber { get; set; }

        /// <summary>
        /// 商户
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 分公司
        /// </summary>
        public virtual int CompanyId { get; set; }

        /// <summary>
        /// 操作前金额
        /// </summary>
        public virtual decimal BeginMoney { get; set; }

        /// <summary>
        /// 操作后金额
        /// </summary>
        public virtual decimal EndMoney { get; set; }
    }
}
