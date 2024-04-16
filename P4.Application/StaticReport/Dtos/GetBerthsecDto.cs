using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.StaticReport.Dtos
{
    public class GetBerthsecDto
    {
        public string BerthsecName { get; set; }
        public int CarInCount { get; set; }
        public int CarOutCount { get; set; }
        public decimal Receivable { get; set; }
        public decimal FactReceive { get; set; }
        public decimal Arrearage { get; set; }

        public decimal Cash { get; set; }

        public decimal BerthsecByCard { get; set; }
        public string FactPercent { get; set; }
        public string ByCardPercent { get; set; }

    }
}
