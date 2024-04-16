using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2.SystemWork.Models
{
    public class VideoGraphParamModel: ParamModel
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string VedioEqNumber { get; set; }
        public string VedioEqType { get; set; }
        public bool IsDistinct { get; set; }

        public string BerthNumber { get; set; }
    }
}