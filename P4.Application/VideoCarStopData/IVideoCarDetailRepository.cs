using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using P4.VideoCars;
using P4.VideoCarStopData.Dtos;
using P4.VideoEquipFaultsNs.Dtos;
using P4.VideoEquips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.VideoCarStopData
{
    public  interface IVideoCarDetailRepository : IRepository<VideoCarBusinessDetail, long>
    {
        VideoCarOutDto GetAllList(VideoCarInputDto input, IAbpSession abpsession);
        void UpdatePlate(CreateOrUpdateVideoCarDto input, IAbpSession abpsession);
    }
}
