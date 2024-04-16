using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.DecisionAnalysis.Dtos
{
    /// <summary>
    /// 泊位利用率
    /// </summary>
    public class UtilizationHourDto
    {
        /// <summary>
        /// 
        /// </summary>
        public int berthsecid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BerthsecName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DateTimeValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StatusValue
        {
            get
            {
                if (Status == 1)
                    return "周转次数";
                else
                    return "利用率";
            }
        }

        public string Utilization0 { get; set; }
        public string Utilization1 { get; set; }
        public string Utilization2 { get; set; }
        public string Utilization3 { get; set; }
        public string Utilization4 { get; set; }
        public string Utilization5 { get; set; }
        public string Utilization6 { get; set; }
        public string Utilization7 { get; set; }
        public string Utilization8 { get; set; }
        public string Utilization9 { get; set; }
        public string Utilization10 { get; set; }
        public string Utilization11 { get; set; }
        public string Utilization12 { get; set; }
        public string Utilization13 { get; set; }
        public string Utilization14 { get; set; }
        public string Utilization15 { get; set; }
        public string Utilization16 { get; set; }
        public string Utilization17 { get; set; }
        public string Utilization18 { get; set; }
        public string Utilization19 { get; set; }
        public string Utilization20 { get; set; }
        public string Utilization21 { get; set; }
        public string Utilization22 { get; set; }
        public string Utilization23 { get; set; }

    }
}
