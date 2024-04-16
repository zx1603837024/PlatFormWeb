using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2.SystemWork.Models
{
    public class PatrolCarModel : ParamModel
    {
        public string id { get; set; }
        public string oper { get; set; }
        public int? IsUse { get; set; }
        public string PatrolType { get; set; }
        public string PatrolCarNumber { get; set; }
        public string Remark { get; set; }
        public string AuditStatus { get; set; }
        public string PlateNumber { get; set; }
        public string RegionId { get; set; }
        public string ParkId { get; set; }
        public string BerthsecId { get; set; }
        public string BerthNumber { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
