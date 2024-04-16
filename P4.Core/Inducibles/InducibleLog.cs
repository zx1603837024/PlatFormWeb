/********************************************************************************
** auth： 黎通
** date： 2016/2/13 10:36:44
** desc： 诱导心跳表
** Ver.:  V1.0.0
*********************************************************************************/
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace P4.Inducibles
{
    /// <summary>
    /// 诱导日志表
    /// </summary>
    [Table("AbpInducibleLogs")]
    public class InducibleLog : Entity<long>, IHasCreationTime
    {
        /// <summary>
        /// 诱导编号
        /// </summary>
        public int InducibleId
        { get; set; }

        /// <summary>
        /// 诱导编号
        /// </summary>
        [MaxLength(40)]
        public string EquipmentId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
