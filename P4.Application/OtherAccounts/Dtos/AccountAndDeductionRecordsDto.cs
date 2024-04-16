using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.OtherAccounts.Dtos
{
    public class AccountAndDeductionRecordsDto
    {
        /// <summary>
        /// 序号
        /// </summary>
        public Int64 Id { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }

        public string YearMonth { get; set; }

        /// <summary>
        /// 首发面值
        /// </summary>
        public decimal TheFirstValue { get; set; }

        /// <summary>
        /// 首发售价
        /// </summary>
        public decimal TheFirstPrice { get { return TheFirstValue; } set{} }

        /// <summary>
        /// 月初结存额
        /// </summary>
        public decimal EarlyMonthBalance { get; set; }

        /// <summary>
        /// 储值额
        /// </summary>
        public decimal StoredValue { get; set; }

        /// <summary>
        /// 售价
        /// </summary>
        public decimal Price { get { return StoredValue; } set { } }

        /// <summary>
        /// 本期消费额
        /// </summary>
        public decimal CurrentConsumptionValue { get; set; }

        /// <summary>
        /// 期末结存额
        /// </summary>
        public decimal TheFinalBalance { get; set; }
    }
}
