using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2.SystemWork.Query
{
    public class CouponsDetailQuery
    {
        public int page { get; set; }
        public int rows { get; set; }
        public int start { get; set; }

        public string uid { get; set; }
        public string tel { get; set; }
        public int? DetailsStatus { get; set; }

        public int? PlanStatus { get; set; }
    }
}
