using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2.SystemWork.Query
{
    public class AlarmDataQuery
    {
        public int page { get; set; }
        public int rows { get; set; }
        public int start { get; set; }
        public string PatrolCarNumber { get; set; }
        public int? RegionId { get; set; }

        public int? ParkId { get; set; }
        public int? BerthsecId { get; set; }

        public string BerthNumber { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
