using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models.Weixin
{
    public class Weixinuser
    {
        public int ID { get; set; }

        public string OpenId { get; set; }

        public string CarNumber1 { get; set; }

        public string CarNumber2 { get; set; }

        public string CarNumber3 { get; set; }
    }
}