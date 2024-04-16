using Abp.Application.Services.Dto;
using P4.EquipmentMaintain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.CompanyCustomerExpressManagement.Dto
{
    public class GetAllCompanyCustomerExpress : PagedResultOutput<CompanyCustomerExpress>, IOutputDto
    {
        public List<CompanyCustomerExpressDto> rows { get; set; }
    }
}
