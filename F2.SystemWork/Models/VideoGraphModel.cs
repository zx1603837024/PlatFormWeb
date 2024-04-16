using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2.SystemWork.Models
{
    public class VideoGraphModel
    {
        public int? Id { get; set; }
        public string VedioEqNumber { get; set; }
        public int? OfflineCount { get; set; }
        public string BerthNumber { get; set; }
        public int? VedioEqType { get; set; }
        public string CreationTime { get; set; }
        public string ParkName { get; set; }
        public string BerthsecName { get; set; }
        public decimal OnlineRate { get; set; }
    }
}
