using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.StaticReport.Dtos
{
   public class DetailsParkingDto
    {
       public Int64 Id { get; set; }

       public string YM { get; set; }
        public string BerthNumber { get; set; }
        public decimal? AllMoney { get; set; }

        /// <summary>
        /// 现金收款
        /// </summary>
        public decimal? CashFactReceive { get; set; } 
        /// <summary>
        ///现金补缴金额
        /// </summary>
        public decimal? RepayByCard { get; set; }

        /// <summary>
        /// 刷卡额
        /// </summary>
        public decimal? CardFactReceive { get; set; }
        /// <summary>
        /// 刷卡补缴金额
        /// </summary>
        public decimal? RepayByCash { get; set; }
        /// <summary>
        /// 消费小计
        /// </summary>
        public decimal? TotalConsumption
        {
            get
            {
                return CardFactReceive.Value + CashFactReceive.Value;
            }
        }

        /// <summary>
        /// 未收款金额
        /// </summary>
        public decimal? AllArrearage { get; set; }
       
    
    }
}
