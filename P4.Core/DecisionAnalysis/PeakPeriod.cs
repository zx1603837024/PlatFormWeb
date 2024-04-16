using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.DecisionAnalysis
{
    /// <summary>
    /// 预警高峰期
    /// 泊位利用率大于50%
    /// </summary>
    [Table("AbpPeakPeriod")]
    public class PeakPeriod : Entity, IPassivable, IHasCreationTime, IMustHaveBerthsec
    {
        /// <summary>
        /// 利用率
        /// </summary>
        public virtual decimal? Utilization { get; set; }

        /// <summary>
        /// 泊位段id
        /// </summary>
        public virtual int BerthsecId { get; set; }

        /// <summary>
        /// true    已过期
        /// false   未过期
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime? EndTime { get; set; }

        /// <summary>
        /// 备注内容
        /// </summary>
        [MaxLength(200)]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
    

    }
}
