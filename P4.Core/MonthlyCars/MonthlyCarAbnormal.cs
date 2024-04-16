using Abp.Domain.Entities;
using P4.Businesses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.MonthlyCars
{
    /// <summary>
    /// 包月卡异常车辆
    /// </summary>
    [Table("AbpMonthlyCarAbnormal")]
    public class MonthlyCarAbnormal : Entity<long>, IMustHaveTenant, IMustHaveBerthsec
    {
        /// <summary>
        /// 包月卡主键Id
        /// </summary>
        public int MonthlyCarId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("MonthlyCarId")]
        public virtual MonthlyCar MonthlyCar { get; set; }

        /// <summary>
        /// 收费明细主键Id
        /// </summary>
        public long BusinessDetailId { get; set; }

        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime CreationTime { get; set; }


        /// <summary>
        /// 是否为异常数据
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; }

       /// <summary>
        /// 泊位号
        /// </summary>
        [MaxLength(20)]
        public string BerthNumber { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [MaxLength(10)]
        public string PlateNumber { get; set; }

        /// <summary>
        /// 车辆入场时间
        /// </summary>
        public DateTime CarInTime { get; set; }

        /// <summary>
        /// 车辆出场时间
        /// </summary>
        public DateTime? CarOutTime { get; set; }

        /// <summary>
        /// 实收
        /// </summary>
        public decimal FactReceive { get; set; }

        /// <summary>
        /// 是否逃逸
        /// </summary>
        public bool IsEscapePay { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int BerthsecId { get; set; }

        /// <summary>
        /// 包月时段
        /// </summary>
        [MaxLength(100)]
        public virtual string MonthyType { get; set; }
    }
}
