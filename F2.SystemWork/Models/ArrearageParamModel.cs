using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2.SystemWork.Models
{
    public class ArrearageParamModel : ParamModel
    {
        public string PlateNumber { get; set; }
        public string carInTime { get; set; }
        public string carOutTime { get; set; }
    }
}
