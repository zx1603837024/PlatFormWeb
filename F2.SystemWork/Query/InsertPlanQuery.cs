using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2.SystemWork.Query
{
    public class InsertPlanQuery
    {
        public int? id { get; set; }
        public Decimal? CouponsMoney { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Mark { get; set; }
        public int? Type { get; set; }
        public Decimal?TermMoney { get; set; }

        public int? Status { get; set; }

        
        
        public int? CouponNumber { get; set; }

        public int? GetNumber { get; set; }

        public int? CouponsType { get; set; }

        public string oper { get; set; }
    }
}
