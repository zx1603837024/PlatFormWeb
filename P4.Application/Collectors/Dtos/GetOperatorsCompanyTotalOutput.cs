using Abp.Application.Services.Dto;
using P4.Companys;
using P4.Companys.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Collectors.Dtos
{
    public class GetOperatorsCompanyTotalOutput : GetReportTotalOutput, IOutputDto
    {
        public List<OperatorsCompany> BerthsecReportList { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
    }
}
