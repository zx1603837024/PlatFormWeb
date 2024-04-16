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
    public class StatisticsMoneyDto
    {
        /// <summary>
        /// 
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal CashPay { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal OnlinePay { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal EscapeMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal PaymentCashPay { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal PaymentOnlinePay { get; set; }
    }
}
