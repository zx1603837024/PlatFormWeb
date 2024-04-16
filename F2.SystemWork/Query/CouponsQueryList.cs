using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2.SystemWork.Query
{
    public class CouponsQueryList
    {
        public int page { get; set; }
        public int rows { get; set; }
        public int start { get; set; }

        public int? CouponsType     { get; set; }

        public int? Status { get; set; }

        public int? Type { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

    }
}
