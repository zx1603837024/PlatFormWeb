using P4.Berthsecs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    /// <summary>
    /// 泊位段实体Model
    /// </summary>
    public class BerthsecModel
    {
        public List<BerthsecDto> BerthsecList { get; set; }

        public string AllOption { get; set; }
    }
}