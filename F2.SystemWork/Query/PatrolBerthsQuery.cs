using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2.SystemWork.Query
{
    public class PatrolBerthsQuery
    {
        public int page { get; set; }
        public int rows { get; set; }
        public int start { get; set; }
        public string PatrolCarNumber { get; set; }
        public string BerthsId { get; set; }


    }
}
