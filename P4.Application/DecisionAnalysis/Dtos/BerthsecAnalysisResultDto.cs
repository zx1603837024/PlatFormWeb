using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.DecisionAnalysis.Dtos
{
    /// <summary>
    /// 泊位段参数信息
    /// </summary>
    public class BerthsecAnalysisResultDto
    {
        /// <summary>
        /// 泊位段id
        /// </summary>
        public string BerthsecId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RateName { get; set; }

        /// <summary>
        /// 泊位段名称
        /// </summary>
        public string BerthsecName { get; set; }

        public string BerthCount { get; set; }

        /// <summary>
        /// 停车次数
        /// </summary>
        public string StopTimes { get; set; }

        /// <summary>
        /// 停车时长
        /// </summary>
        public string StopTime { get; set; }

        /// <summary>
        /// 免费停车次数
        /// </summary>
        public string NoStopTimes { get; set; }

        /// <summary>
        /// 免费停车时长
        /// </summary>
        public string NoStopTime { get; set; }    

        /// <summary>
        /// 总欠费
        /// </summary>
        public string Arrearage { get; set; }

        /// <summary>
        /// 总实收
        /// </summary>
        public string FactReceive { get; set; }

        /// <summary>
        /// 总应收
        /// </summary>
        public string Money { get; set; }

        /// <summary>
        /// 总追缴
        /// </summary>
        public string Repayment { get; set; }

        /// <summary>
        /// 周转率
        /// </summary>
        public string Turnover { get; set; }

        /// <summary>
        /// 利用率
        /// </summary>
        public string Utilization { get; set; }


        /// <summary>
        /// 费率备注
        /// </summary>
        public string Remark { get; set; }
    }
}
