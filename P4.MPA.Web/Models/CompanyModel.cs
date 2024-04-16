using P4.Companys.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    /// <summary>
    /// 分公司
    /// </summary>
    public class CompanyModel
    {
        public List<CompanyDto> CompanyDtoList { get; set; }
        public string AllOption { get; set; }
    }
}