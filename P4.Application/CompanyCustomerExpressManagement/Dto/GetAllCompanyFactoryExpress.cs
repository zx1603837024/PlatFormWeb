using Abp.Application.Services.Dto;
using P4.EquipmentMaintain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.CompanyCustomerExpressManagement.Dto
{
    public class GetAllCompanyFactoryExpress : PagedResultOutput<CompanyFactoryExpress>, IOutputDto
    {
        public List<CompanyFactoryExpressDto> rows { get; set; }
    }
}
