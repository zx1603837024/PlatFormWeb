using P4.Inspectors.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class InspectorChargesModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<InspectorDto> InspectorList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AllOption { get; set; }
    }
}