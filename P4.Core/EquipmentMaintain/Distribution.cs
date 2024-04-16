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
    /// 领用分配表
    /// </summary>
    [Table("AbpDistribution")]
    public class Distribution : Entity,IHasCreationTime
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        [MaxLength(50)]
        public virtual string EqId { get; set; }

        /// <summary>
        /// 领用员工编号
        /// </summary>
        [MaxLength(30)]
        public virtual string DistributionEmployeeId { get; set; }

        /// <summary>
        /// 领用员工名称
        /// </summary>
        [MaxLength(20)]
        public virtual string DistributionEmployeeName { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
