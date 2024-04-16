using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models.Weixin
{
    public class parking
    {
        public int ID { get; set; }
        public int parkid { get; set; }

        public string ParkName { get; set; }

        public string berthnumber { get; set; }

        public string carnumber { get; set; }

        public int status { get; set; }

        public DateTime carintime { get; set; }

        public DateTime carouttime { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }
    }
}