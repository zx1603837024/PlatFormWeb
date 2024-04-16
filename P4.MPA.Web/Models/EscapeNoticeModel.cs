using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class EscapeNoticeModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string SentTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Boolean WeixinSent { get; set; }
        

        /// <summary>
        /// 
        /// </summary>
        public Boolean SmsSent { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Boolean UnpaidRecovery { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int UnpaidMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int IsolationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SendTimes { get; set; }

    }
}