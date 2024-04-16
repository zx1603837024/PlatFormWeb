using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.CarownerApp.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class ParkOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ParkDto> rows { get; set; }

        /// <summary>
        /// 推荐停车场id
        /// </summary>
        public string suggid { get { return "49"; } }
    }
}
