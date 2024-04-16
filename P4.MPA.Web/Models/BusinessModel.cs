using P4.Berthsecs.Dto;
using P4.Businesses.Dtos;
using P4.Dictionarys.Dtos;
using P4.Employees.Dtos;
using P4.Parks.Dtos;
using P4.Regions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    public class BusinessModel
    {
        public List<RegionDto> regionList { get; set; }

        public List<ParkDto> parkList { get; set; }

        public List<BerthsecDto> berthsecList { get; set; }

        public List<EmployeeDto> employeeList { get; set; }

        public List<GetAllValueDataDto> StopType { get; set; }

        public List<GetAllValueDataDto> PayType { get; set; }

        public string AllOption { get; set; }
    }
}