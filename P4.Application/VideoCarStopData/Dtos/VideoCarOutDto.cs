using Abp.Application.Services.Dto;
using P4.VideoCars;
using P4.VideoEquips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.VideoCarStopData.Dtos
{
    public class VideoCarOutDto : PagedResultOutput<VideoCarBusinessDetail>, IOutputDto
    {
        public List<GetAllVideoCarDto> rows { get; set; }
    }
}
