using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.App
{
    /// <summary>
    /// app消费明细
    /// </summary>
    [Table("AbpAppAccountRecords")]
    public class AppAccountRecord : Entity<long>
    {
        /// <summary>
        /// 交易时间
        /// </summary>
        public virtual DateTime TransactionTime { get; set; }

        /// <summary>
        /// 账号id
        /// </summary>
        public virtual long AppAccountId { get; set; }

        /// <summary>
        /// 车牌
        /// </summary>
        [MaxLength(10)]
        public virtual string PlateNumber { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(50)]        
        public virtual string Remark { get; set; }
    }
}
