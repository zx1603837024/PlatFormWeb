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
    /// 泊位段利用率周转率
    /// </summary>
    [Table("AbpUtilizationAndTurnovers")]
    public class UtilizationAndTurnover : Entity, IHasCreationTime, IMustHaveBerthsec
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual int BerthsecId { get; set; }

        /// <summary>
        /// 利用率
        /// </summary>
        public virtual decimal Utilization { get; set; }

        /// <summary>
        /// 周转率
        /// </summary>
        public virtual decimal Turnover { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(10)]
        public virtual string Time { get; set; }


        public virtual DateTime CreationTime { get; set; }

        
    }
}
