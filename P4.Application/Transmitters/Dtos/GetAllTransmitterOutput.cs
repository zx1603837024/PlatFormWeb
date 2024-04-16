using Abp.Application.Services.Dto;
using P4.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Transmitters.Dtos
{
    public class GetAllTransmitterOutput : PagedResultOutput<Transmitter>, IOutputDto
    {
        public List<TransmitterDto> rows { get; set; }
    }
}