using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Berths.Dtos
{
    /// <summary>
    /// 环比金额
    /// </summary>
    public class MoneyChain
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string Daytime { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal MoneyTotal { get; set; }
    }
}
