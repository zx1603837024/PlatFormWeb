using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CompanySetting
    {

        /// <summary>
        /// 
        /// </summary>
        public int? CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }
       
        public string TelePhone { get; set; }
        public string Contacts { get; set; }
        public string X { get; set; }
        public string Y { get; set; }

        public string Password1 { get; set; }
        public string Password2 { get; set; }
        public string Password3 { get; set; }
        public string Password4 { get; set; }
        public string Password5 { get; set; }

    }
}