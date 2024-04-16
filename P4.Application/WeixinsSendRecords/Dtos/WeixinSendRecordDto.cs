using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.WeixinSendRecord.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class WeixinSendRecordDto  
    {
        /// <summary>
        /// 
        /// </summary>
        public int args { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string openId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string nickName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal SUMArrearage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SendWeixinNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime SendWeixinDatetime { get; set; }
    }
}
