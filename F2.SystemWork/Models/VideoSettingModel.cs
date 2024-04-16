using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2.SystemWork.Models
{
    public class VideoSettingModel
    {
        public int? Id { get; set; }
        public String Name { get; set; }
        public String CompanyName { get; set; }
        public String RegionName { get; set; }
        public String ParkName { get; set; }
        public String BerthsecName { get; set; }
        public int? BerthId { get; set; }
        public String BerthNumber { get; set; }
        public String VedioEqNumber { get; set; }
        public int? ParkStatus { get; set; }
        public int? IsOnlineValue { get; set; }
        public String BeatDatetime { get; set; }
        public String TenancyName { get; set; }
        public int? VedioEqType { get; set; }
        public int? IsUse { get; set; }
        public String CreationTime { get; set; }
        public int? FatherId { get; set; }
        public String EnableTime { get; set; }
        public String StopTime { get; set; }
        public String Remark { get; set; }
        public int? Battery { get; set; }
        public string oper { get; set; }
    }
}
