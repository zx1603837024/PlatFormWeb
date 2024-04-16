using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2.SystemWork.Models
{
    public class VideoEquipsModel 
    {
        public string berthsecName { get; set; }
        public string videoEqNumber { get; set; }

        public int? videoEqType { get; set; }

        public List<string> berthNumbers { get; set; }

        public string enableTime { get; set; }

        public string stopTime { get; set; }
        
    }
}
