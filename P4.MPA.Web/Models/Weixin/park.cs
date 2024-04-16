using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models.Weixin
{
    public class park
    {
        public int ID { get; set; }

        public string parkname { get; set; }

        public bool isactive { get; set; }

        public string lat { get; set; }

        public string lng { get; set; }

        public int allcount { get; set; }

        public int emptycount { get; set; }

        public string remark { get; set; }
    }
}