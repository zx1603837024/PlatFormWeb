using Abp.Application.Services.Dto;
using P4.Webchat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.WebChat.Dtos
{
    public class GetAdsOutput : PagedResultOutput<Ad>, IOutputDto
    {
        public List<AdDto> rows { get; set; }
    }
}
