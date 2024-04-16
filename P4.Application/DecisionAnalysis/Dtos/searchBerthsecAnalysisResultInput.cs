using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.DecisionAnalysis.Dtos
{
    public class SearchBerthsecAnalysisResultInput
    {
        public DateTime? BeginTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? ParkId { get; set; }
    }
}
