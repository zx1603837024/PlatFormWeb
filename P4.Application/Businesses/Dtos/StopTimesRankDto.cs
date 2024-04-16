using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Businesses.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class StopTimesRankDto
    {
        //public DateTime? Time { get; set; }
        //public string TimeStr
        //{
        //    get
        //    {
        //        if (Time.HasValue)
        //        {
        //            return Time.Value.ToString("yyyy-MM");
        //        }
        //        return "";
        //    }
        //}
        //public int StopTimes { get; set; }

            /// <summary>
            /// 月份
            /// </summary>
        public string Month { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int Totals { get; set; }
    }
}
