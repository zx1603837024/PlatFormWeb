using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Reports
{
    [Table("AbpEmployeeReport")]
    public class EmployeeReport : Entity, IModificationAudited, IMustHaveTenant, IMustHaveCompany
    {
        /// <summary>
        /// 统计时间节点
        /// demo: 2015-01-01 01:00:00 统计到小时
        /// </summary>
        public virtual DateTime Time { get; set; }

        /// <summary>
        /// 停车时长
        /// </summary>
        public virtual Int32? StopTime { get; set; }

        /// <summary>
        /// 停车次数
        /// </summary>
        public virtual int StopTimes { get; set; }

        /// <summary>
        /// 预付费
        /// </summary>
        public virtual decimal? Prepaid { get; set; }

        /// <summary>
        /// 应收
        /// </summary>
        public virtual decimal? Receivable { get; set; }

        /// <summary>
        /// 实收
        /// </summary>
        public virtual decimal? FactReceive { get; set; }
       
        /// <summary>
        /// 欠费金额
        /// </summary>
        public virtual decimal? Arrearage { get; set; }

        /// <summary>
        /// 补缴金额
        /// </summary>
        public virtual decimal? Repayment { get; set; }

        /// <summary>
        /// 车检器停车时长
        /// </summary>
        public virtual Int32? SensorsStopTime { get; set; }

        /// <summary>
        /// 车检器应收
        /// </summary>
        public decimal? SensorsReceivable { get; set; }


        public virtual long EmployeeId { get; set; }

        public virtual DateTime? LastModificationTime { get; set; }

        public virtual long? LastModifierUserId { get; set; }

        public virtual int TenantId { get; set; }

        public virtual int CompanyId { get; set; }
    }
}
