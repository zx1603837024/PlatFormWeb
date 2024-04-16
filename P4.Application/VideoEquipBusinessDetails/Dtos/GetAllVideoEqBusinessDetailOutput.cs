using Abp.Application.Services.Dto;
using P4.VideoEquips;
using System.Collections.Generic;

namespace P4.VideoEquipBusinessDetails.Dtos
{
    public class GetAllVideoEqBusinessDetailOutput : PagedResultOutput<VideoEquipBusinessDetail>, IOutputDto
    {
        public List<VideoEqBusinessDetailDto> rows { get; set; }
    }
}
