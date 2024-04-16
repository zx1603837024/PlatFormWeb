using P4.BigScreen.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class BigScreenModel
    {
        /// <summary>
        /// 总应收
        /// </summary>
        public decimal SumReceivable { get; set; }
        /// <summary>
        /// 总实收
        /// </summary>
        public decimal SumFactReceive { get; set; }
        /// <summary>
        /// 总欠费
        /// </summary>
        public decimal SumArrearage { get; set; }
        /// <summary>
        /// 总追缴
        /// </summary>
        public decimal? SumRepayment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Today { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int AllStopTimes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int AverageStopTimes { get; set; }

        /// <summary>
        /// 泊位段收费情况
        /// </summary>
        public List<BerthsecStatisticsDto> BerthsecStatisticsList { get; set; }

        /// <summary>
        /// 收费员今日实收
        /// </summary>
        public List<EmployeeFactReceiveDto> EmployeeChargesList { get; set; }
    }
}