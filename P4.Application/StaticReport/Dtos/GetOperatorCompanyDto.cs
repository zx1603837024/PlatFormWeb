using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.StaticReport.Dtos
{
   public class GetOperatorCompanyDto
    {    
       public string CompanyName { get; set; }

        public decimal Receivable { get; set; }
        public decimal FactReceive { get; set; }
        public decimal Arrearage { get; set; }
        public decimal Repayment { get; set; }
        public decimal Cash { get; set; }
        public decimal ByCard { get; set; }
       
      

    }
}
