using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Businesses.Dtos
{

    /// <summary>
    /// //车辆批量进场实体
    /// </summary>
    public class BInsertCarInDto
    {


        /// <summary>
        /// //泊位号
        /// </summary>
        public string BerthNumber { get; set; }
        //车牌号
        /// <summary>
        /// 
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// //车辆类型
        /// </summary>
        public string CarType { get; set; }


        /// <summary>
        /// //预付费
        /// </summary>
        public string Prepaid { get; set; }

        /// <summary>
        ///  //进车时间
        /// </summary>

        public string CarInTime { get; set; }

        /// <summary>
        /// 唯一guid
        /// </summary>
        public string guid { get; set; }


        /// <summary>
        /// 车检器进场时间
        /// </summary>
        public string SensorsInCarTime { get; set; }

        /// <summary>
        /// //停车类型
        /// </summary>
        public string StopType { get; set; }

        /// <summary>
        /// //卡号/
        /// </summary>
        public string CardNo { get; set; }

    }
}
