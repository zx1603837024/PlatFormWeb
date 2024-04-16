using P4.Berthsecs.Dto;
using P4.Parks.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    public class UtilizationModel
    {
        public List<ParkDto> parkList { get; set; }

        public List<BerthsecDto> berthsecList { get; set; }

        public string AllOption { get; set; }
    }
}