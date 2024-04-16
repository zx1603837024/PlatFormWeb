using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Businesses.Dtos
{
    /// <summary>
    /// 欠费排名
    /// </summary>
    public class EscapeRankDto
    {

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 总欠费金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 欠费次数
        /// </summary>
        public int Times { get; set; }

        /// <summary>
        /// 平均欠费金额
        /// </summary>
        public decimal Average { get; set; }

    }
}
