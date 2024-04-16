using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.BigScreen.Dtos
{
    /// <summary>
    /// 获取人员坐标
    /// </summary>
    public class EmpolyeePoint
    {
        /// <summary>
        /// ID
        /// </summary>
        public long EmpolyeeId { get; set; }
        /// <summary>
        /// 收费员
        /// </summary>
        public string TrueName { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string X { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public string Y { get; set; }
        /// <summary>
        /// 实收
        /// </summary>
        public decimal FactReceive { get; set; }
        /// <summary>
        /// 追缴
        /// </summary>
        public decimal Repayment { get; set; }
    }
}
