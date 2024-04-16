using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2.SystemWork.Query
{
    public class OpinionListQuery
    {
        public int page { get; set; }
        public int rows { get; set; }
        public int start { get; set; }

        public string tel { get; set; }

        public int? status { get; set; }

        public int? type { get; set; }

    }
}
