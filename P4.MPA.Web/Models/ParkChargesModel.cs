using P4.Berthsecs.Dto;
using P4.Parks.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ParkChargesModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ParkDto> ParkList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<BerthsecDto> BerthsecList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AllOption { get; set; }
    }
}