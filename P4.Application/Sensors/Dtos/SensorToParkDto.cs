using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Sensors.Dtos
{

    /// <summary>
    /// 停车车位Dto
    /// </summary>
    public class SensorToParkDto
    {
        /// <summary>
        /// 停车场id
        /// </summary>
        public int ParkId { get; set; }


        /// <summary>
        /// 空车位
        /// </summary>
        public int EmptyCount { get; set; }

        /// <summary>
        /// 总车位
        /// </summary>
        public int BerthCount { get; set; }
    }
}
