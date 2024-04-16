using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models.Weixin
{
    public class Wexinparkorder
    {
        //berthnumber, parkid, parkname, carintime, caroutime, money
        public int ID { get; set; }
        public string berthnumber { get; set; }

        public int parkid { get; set; }

        public string parkname { get; set; }

        public DateTime carintime { get; set; }

        public DateTime? caroutime { get; set; }

        public int money { get; set; }

        public string carnumber { get; set; }

        public int othermoney { get; set; }

        public int time { get; set; }

        /// <summary>
        /// 剩余时间
        /// </summary>
        public double Surplus { get {
                System.TimeSpan t3 = DateTime.Now - carintime;
                return t3.TotalSeconds;
            } }
    }
}