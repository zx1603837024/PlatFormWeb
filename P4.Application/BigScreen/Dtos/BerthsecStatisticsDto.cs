using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.BigScreen.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class BerthsecStatisticsDto
    {
        /// <summary>
        /// 
        /// </summary>
        public int BerthsecId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BerthsecName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Money { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? FaceMoney { get; set; }

        /// <summary>
        /// 追缴
        /// </summary>
        public decimal? Repayment { get; set; }

        /// <summary>
        /// 总额
        /// </summary>
        public decimal? ToatalMoney { get; set; /*get { return Repayment + FaceMoney; }*/}
    }
}
