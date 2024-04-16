using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Businesses.Dtos
{

    /// <summary>
    /// 
    /// </summary>
    public class BusinessDecisionDto
    {
        /// <summary>
        /// 应收金额
        /// </summary>
        public string AllMoney { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public string AllFactReceive { get; set; }

        /// <summary>
        /// 逃逸金额
        /// </summary>
        public string AllArrearage { get; set; }

        /// <summary>
        /// 追缴金额
        /// </summary>
        public string AllRepayment { get; set; }

        /// <summary>
        /// 总停车时长
        /// </summary>
        public string AllStopTime { get; set; }

        /// <summary>
        /// 总停车次数
        /// </summary>
        public string AllStopTimes { get; set; }
    }
}
