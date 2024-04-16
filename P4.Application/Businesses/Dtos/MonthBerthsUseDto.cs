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
    public class MonthBerthsUseDto
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string YearMonth { get; set; }

        /// <summary>
        /// 车位号
        /// </summary>
        public string BerthNumber { get; set; }

        /// <summary>
        /// 应收车位费
        /// </summary>
        public decimal Receivable { get; set; }

        /// <summary>
        /// 现金收款
        /// </summary>
        public decimal CashCollection { get; set; }

        /// <summary>
        /// 刷卡额
        /// </summary>
        public decimal PaybyCardValue { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 消费小计
        /// </summary>
        public decimal TotalConsumption { get; set; }

        /// <summary>
        /// 未收款金额
        /// </summary>
        public decimal Arrearage { get; set; }
        /// <summary>
        /// 刷卡补缴金额
        /// </summary>
        public decimal ByCardRepay { get; set; }
        /// <summary>
        ///现金补缴金额
        /// </summary>
        public decimal ByCashRepay { get; set; }
       

        /// <summary>
        /// 未收款原因
        /// </summary>
        public string ArrearageReason { get; set; }
    }
}
