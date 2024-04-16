using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.StaticReport.Dtos
{
    public class TbucketBerthsecStaticRExcelDto
    {
        public string BerthsecName { get; set; }

        public string Time { get; set; }

        public Int32 StopTime { get; set; }

        public decimal Prepaid { get; set; }

        public decimal Receivable { get; set; }

        public decimal FactReceive { get; set; }

        public decimal Cash { get; set; }

        public decimal ByCard { get; set; }

        public decimal Arrearage { get; set; }
    }
}
