using P4.Employees.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    public class EmployeeChargesModel
    {
        public List<EmployeeDto> EmployeeList { get; set; }
        public string AllOption { get; set; }
    }
}