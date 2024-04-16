using Abp.Application.Services.Dto;
using P4.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Collectors.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetBerthsecReportTotalOutput : GetReportTotalOutput, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<BerthsecReport> berthsecReportList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long BerthsecId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BerthsecName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FactPercent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ByCardPercent { get; set; }
        /// <summary>
        /// 刷卡实收
        /// </summary>
        public decimal BerthsecByCard { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CarInCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CarOutCount { get; set; }

        ///// <summary>
        ///// 泊位段--预缴总额
        ///// </summary>
        //public decimal berthsecPrepaidMoney { get; set; }
        ///// <summary>
        ///// 泊位段--停车次数总计
        ///// </summary>
        //public int berthsecStopTimes { get; set; }              
        ///// <summary>
        ///// 泊位段--停车时长总计
        ///// </summary>
        //public int berthsecStopTime { get; set; }       
        ///// <summary>
        ///// 泊位段--逃逸停车次数总计
        ///// </summary>
        //public virtual Int32 berthsecEscapeStopTimes { get; set; }
        ///// <summary>
        ///// 泊位段--应收总计
        ///// </summary>
        //public decimal berthsecReceivable { get; set; }
        ///// <summary>
        ///// 泊位段--现金支付总计
        ///// </summary>
        //public virtual decimal berthsecCash { get; set; }
        ///// <summary>
        ///// 泊位段--实收总计
        ///// </summary>
        //public virtual decimal berthsecFactReceive { get; set; }
        ///// <summary>
        ///// 泊位段--欠费金额总计
        ///// </summary>
        //public virtual decimal berthsecArrearage { get; set; }
        ///// <summary>
        ///// 泊位段--补缴金额总计
        ///// </summary>
        //public virtual decimal berthsecRepayment { get; set; }
        ///// <summary>
        ///// 泊位段--车检器停车时长总计
        ///// </summary>
        //public virtual Int32 berthsecSensorsStopTime { get; set; }
        ///// <summary>
        ///// 泊位段车--检器应收总计
        ///// </summary>
        //public decimal berthsecSensorsReceivable { get; set; }
    }
}

    