using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Businesses.Dtos
{
    /// <summary>
    /// 汇总逃逸明细里面的金额
    /// </summary>
   public class EscapeDetailsUserData
    {

        /// <summary>
        /// 泊位号
        /// </summary>
        public string BerthNumber { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }
       //停车时长
       public string StopTimes { get; set; }
       /// <summary>
       /// 预付
       /// </summary>
       public decimal Prepaid { get; set; }

       /// <summary>
       /// 出场应收
       /// </summary>
       public decimal Receivable { get; set; }

       /// <summary>
       /// 欠费金额
       /// </summary>
       public virtual decimal Arrearage { get; set; }

       /// <summary>
       /// 补缴金额
       /// </summary>
       public decimal? Repayment { get; set; }

       /// <summary>
       /// 总应收
       /// </summary>
       public decimal? Money { get; set; }
    }
}
