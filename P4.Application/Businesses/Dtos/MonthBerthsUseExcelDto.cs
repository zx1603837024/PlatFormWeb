using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Businesses.Dtos
{
    public class MonthBerthsUseExcelDto
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string MBUEId { get; set; }

        /// <summary>
        /// 车位号
        /// </summary>
        public string MBUEBerthNumber { get; set; }

        /// <summary>
        /// 应收车位费
        /// </summary>
        public decimal MBUEReceivable { get; set; }

        /// <summary>
        /// 现金收款
        /// </summary>
        public decimal MBUECashCollection { get; set; }

        /// <summary>
        /// 刷卡额
        /// </summary>
        public decimal MBUEPaybyCardValue { get; set; }
        public decimal XJ1 { get; set; }
        //刷卡补缴金额
        public decimal MBUEPByCardRepay { get; set; }
        /// <summary>
        ///现金补缴金额
        /// </summary>
        public decimal MBUEPByCashRepay { get; set; }


        public decimal XJ2 { get; set; }
        /// <summary>
        /// 消费小计
        /// </summary>
        public decimal MBUETotalConsumption { get; set; }

        /// <summary>
        /// 未收款金额
        /// </summary>
        public decimal MBUEArrearage { get; set; }


        
    }
}
