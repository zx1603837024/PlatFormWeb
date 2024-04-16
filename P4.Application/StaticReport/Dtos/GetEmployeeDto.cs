using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.StaticReport.Dtos
{
    public class GetEmployeeDto 
    {

        public string EmployeeName { get; set; }

        public decimal FactReceive { get; set; }

        public decimal Arrearage { get; set; }

        public decimal Repayment { get; set; }

       
    }
}
 