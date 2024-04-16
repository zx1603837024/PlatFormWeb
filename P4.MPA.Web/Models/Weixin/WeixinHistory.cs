using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models.Weixin
{
    public class WeixinHistory
    {
        public int ID { get; set; }

        public string CarNumber { get; set; }

        public DateTime CarIntime { get; set; }

        public DateTime OutTime { get; set; }

        public string OpenID { get; set; }

        public string BerthId { get; set; }

        public int Status { get; set; }
    }
}