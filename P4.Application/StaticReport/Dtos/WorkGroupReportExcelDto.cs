using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.StaticReport.Dtos
{
    public class WorkGroupReportExcelDto
    {
        public string WorkGroupName { get; set; }

        public decimal? WorkGroupPrepaid { get; set; }

        public decimal? WorkGroupReceivable { get; set; }

        public decimal? WorkGroupFactReceive { get; set; }

        public decimal? WorkGroupCash { get; set; }

        public decimal? WorkGroupByCard { get; set; }

        public decimal? WorkGroupArrearage { get; set; }
    }
}
