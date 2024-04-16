using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Parks;
using P4.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.StaticReport.Dtos
{
     [AutoMapFrom(typeof(BerthsecReport))]
    public class BerthsecReportDto : EntityDto
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
        /// 预付费
        /// </summary>
        public virtual decimal? Prepaid { get; set; }

        /// <summary>
        /// 停车次数
        /// </summary>
        public virtual Int32 StopTimes { get; set; }

        /// <summary>
        /// 逃逸停车次数
        /// </summary>
        public virtual Int32 EscapeStopTimes { get; set; }

        /// <summary>
        /// 应收
        /// </summary>
        public virtual decimal? Receivable { get; set; }


        /// <summary>
        /// 现金支付
        /// </summary>
        public virtual decimal? Cash { get; set; }

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

        /// <summary>
        /// 修改修改时间
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 最后修改用户id
        /// </summary>
        public virtual long? LastModifierUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int RegionId { get; set; }

       
        public virtual Park park { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual int ParkId { get; set; }

        public virtual int BerthsecId { get; set; }
    }
}
