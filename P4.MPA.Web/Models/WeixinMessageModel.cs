using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    public class WeixinMessageModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string SendMonthRechargeMsg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SendMsgOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SendMsgOrderOut { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SendMsgStopCar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SendRechargeMsg { get; set; }

        /// <summary>
        /// 停车场预约信息
        /// </summary>
        public string SendMsgReservePark { get; set; }
    }
}