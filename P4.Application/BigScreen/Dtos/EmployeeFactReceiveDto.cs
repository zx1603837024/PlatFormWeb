using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.BigScreen.Dtos
{
    /// <summary>
    /// 收费员今日收入
    /// </summary>
    public class EmployeeFactReceiveDto
    {
        /// <summary>
        /// 出场收费员Id
        /// </summary>
        public long EmployeeId { get; set; }
        /// <summary>
        /// 收费员姓名
        /// </summary>
        public string EmployName { get; set; }

        /// <summary>
        /// 出场实收
        /// </summary>
        public decimal? FactReceive { get; set; }
        /// <summary>
        /// 追缴收费
        /// </summary>
        public decimal? Repayment { get; set; }
    }
}
