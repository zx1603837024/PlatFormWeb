using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.MonthlyCars
{
    [Table("AbpMonthlyCarHistory")]
    public class MonthlyCarHistory : CreationAuditedEntity<long, Users.User>
    {
        /// <summary>
        /// 关联主表MonthlyCar中Id字段
        /// </summary>
        public virtual int MonthlyCarId { get; set; }

        public virtual decimal Money { get; set; }

        /// <summary>
        /// 包月卡生效的停车场
        /// </summary>
        [MaxLength(200)]
        public virtual string ParkIds { get; set; }

        /// <summary>
        /// 包月时间类型
        /// </summary>
        [MaxLength(100)]
        public virtual string MonthyType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime EndTime { get; set; }

        /// <summary>
        /// true为   开卡
        /// false 为 续费
        /// </summary>
        public virtual bool Status { get; set; }

        /// <summary>
        /// 支付类型
        /// 1：现金
        /// 2：微信支付
        /// 3：支付宝支付
        /// </summary>
        public virtual int PayStatus { get; set; }

        /// <summary>
        /// 关联流水号
        /// </summary>
        [MaxLength(100)]
        public virtual string tranid { get; set; }
    }
}
