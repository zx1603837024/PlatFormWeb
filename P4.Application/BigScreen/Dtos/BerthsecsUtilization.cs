using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.BigScreen.Dtos
{
    /// <summary>
    /// 泊位使用情况
    /// </summary>
    public class BerthsecsUtilization
    {
        /// <summary>
        /// 停车场Id
        /// </summary>
        public int ParkId { get; set; }
        /// <summary>
        /// 停车场名称
        /// </summary>
        public string ParkName { get; set; }
        /// <summary>
        /// 泊位总数
        /// </summary>
        public int BerthsTotal { get; set; }
        /// <summary>
        /// 已使用泊位数量
        /// </summary>
        public int BerthsUse { get; set; }
        /// <summary>
        /// 剩余泊位
        /// </summary>
        public int BerthsUseness { get; set; }
    }
}
