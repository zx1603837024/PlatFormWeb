using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2.SystemWork.Models
{
    public class TUserQuery
    {
        public int page { get; set; }
        public int rows { get; set; }
        public int start { get; set; }

        public string tel { get; set; }

        public string CarNumber { get; set; }
        public string nickName { get; set; }
    }
}
