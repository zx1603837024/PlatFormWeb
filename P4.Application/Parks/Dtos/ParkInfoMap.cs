using System;

namespace P4.Parks.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class ParkInfoMap 
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string X { get; set; }
        
        /// <summary>
        /// 纬度
        /// </summary>
        public string Y { get; set; }
        /// <summary>
        /// 泊位段总数
        /// </summary>
        public int BerthsecCount { get; set; }
        /// <summary>
        /// 泊位总数
        /// </summary>
        public int BerthCount { get; set; }
        /// <summary>
        /// 再停泊位数
        /// </summary>
        public int BerthOnTimeCount { get; set; }
        /// <summary>
        /// 未停泊位数
        /// </summary>
        public int BerthNotTimeCount { get; set; }


        /// <summary>
        /// 停车场类型
        /// </summary>
        public Int16 ParkType { get; set; }

        /// <summary>
        /// 泊位在停率
        /// </summary>
        public string BerthOntimePercentage
        {
            get
            {
                string B = (Convert.ToDouble(BerthOnTimeCount) / Convert.ToDouble(BerthCount)).ToString("0.0%");
                return B;
            }
        }
    }
}
