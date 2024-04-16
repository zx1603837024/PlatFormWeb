using Abp.Application.Services;
using P4.VideoCarStopData.Dtos;
using P4.VideoEquipBusinessDetails.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.VideoCarStopData
{
    public interface IVideoCarDetailAppService : IApplicationService
    {
        VideoCarOutDto GetAllList(VideoCarInputDto input);
        void Update(CreateOrUpdateVideoCarDto input);
        void Delete(int Id);
    }
}
