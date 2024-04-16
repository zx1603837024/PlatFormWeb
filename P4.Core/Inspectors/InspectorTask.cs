using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inspectors
{
    /// <summary>
    /// 巡查任务
    /// </summary>
    [Table("AbpInspectorTasks")]
    public class InspectorTask : FullAuditedEntity<long>, IMustHaveTenant, IMustHaveCompany
    {

        /// <summary>
        /// 停车场id，使用,分割
        /// </summary>
        [MaxLength(500)]
        public virtual string ParkId { get; set; }

        /// <summary>
        /// 生效日志
        /// </summary>
        public virtual DateTime EffectiveTime { get; set; }

        /// <summary>
        /// 巡查员id
        /// </summary>
        public virtual long InspectorId { get; set; }

        /// <summary>
        /// 任务备注
        /// </summary>
         [MaxLength(500)]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 任务状态
        /// 1发布
        /// 2禁用
        /// 3领用
        /// 4已处理
        /// 5关闭
        /// </summary>
        public virtual Int16 Status { get; set; }

        public virtual int TenantId { get; set; }

        public virtual int CompanyId { get; set; }
    }
}
