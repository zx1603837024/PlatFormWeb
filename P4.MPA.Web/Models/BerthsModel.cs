using P4.Berths.Dtos;
using P4.Berthsecs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class BerthsModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<BerthDto> berthList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<BerthsecDto> berthsecList { get; set; }
    }
}