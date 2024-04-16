using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.BigScreen.Dtos
{
    /// <summary>
    /// 
    /// </summary>
   public  class StatisticsDto
    {
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
        /// 泊位段收入
        /// </summary>
        public List<BerthsecStatisticsDto> berthsecList { get; set; }

        /// <summary>
        /// 收费员实收
        /// </summary>
        public List<EmployeeFactReceiveDto> TodayFactReceiveList { get; set; }

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
        public decimal SumRepayment { get; set; }
    }
}
