using Abp.Application.Services.Dto;
using P4.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.PrintAdvertisement.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllPrintAdsOutput : PagedResultOutput<PrintAd>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<PrintAdDto> rows { get; set; }
    }
}
