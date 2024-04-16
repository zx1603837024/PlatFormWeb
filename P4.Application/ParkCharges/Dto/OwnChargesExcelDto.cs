using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.ParkCharges.Dto
{
    /// <summary>
    /// 欠费动态导出报表Model
    /// </summary>
    public class OwnChargesExcelDto
    {
        /// <summary>
        /// 停车场名称
        /// </summary>
        public string ParkName { get; set; }
        /// <summary>
        /// 当月欠费总额
        /// </summary>
        public decimal? SumOwn { get; set; }
        /// <summary>
        /// 当月已追缴
        /// </summary>
        public decimal? SumRecovered { get; set; }
        /// <summary>
        /// 跨月追缴
        /// </summary>
        public decimal? SpanSumRecovered { get; set; }
        /// <summary>
        /// 删除前月追缴
        /// </summary>
        public decimal? DelSumRecovered { get; set; }


    }
}
