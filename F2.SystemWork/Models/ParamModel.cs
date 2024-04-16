using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2.SystemWork.Models
{
    public class ParamModel
    {
        public int? page { get; set; }
        public int? rows { get; set; }
        public string sidx { get; set; }
        public string sord { get; set; }
        public string searchField { get; set; }
        public string searchString { get; set; }
        public string searchOper { get; set; }
        public string filters { get; set; }

    }
}
