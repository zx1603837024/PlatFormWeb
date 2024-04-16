using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.MonthlyCars.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class DecisionDto
    {
        /// <summary>
        /// 包月车辆总计
        /// </summary>
        public int AllMonthyCount { get; set; }

        /// <summary>
        /// 包月车辆总金额
        /// </summary>
        public string AllMonthyMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int NewMonthyCount { get; set; }

        /// <summary>
        /// 新增车辆总金额
        /// </summary>
        public string NewMonthyMoney { get; set; }
    }
}
