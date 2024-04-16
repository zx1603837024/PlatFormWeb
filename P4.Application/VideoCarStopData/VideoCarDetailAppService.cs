using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.VideoCarStopData.Dtos;
using P4.VideoEquipFaultsNs;
using P4.VideoEquips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.VideoCarStopData
{
    public class VideoCarDetailAppService : ApplicationService, IVideoCarDetailAppService
    {
        #region Var
        private readonly IRepository<VideoEquipFaults, long> _abpVideoFaultrepository;
        private readonly IVideoCarDetailRepository _VideoFaultrepository;
        #endregion

        public VideoCarDetailAppService(IRepository<VideoEquipFaults, long> a, IVideoCarDetailRepository b)

        {
            _abpVideoFaultrepository = a;
            _VideoFaultrepository = b;
        }

        public Dtos.VideoCarOutDto GetAllList(Dtos.VideoCarInputDto input)
        {
            return _VideoFaultrepository.GetAllList(input, AbpSession);
        }

        public void Update(CreateOrUpdateVideoCarDto input)
        {
            _VideoFaultrepository.UpdatePlate(input, AbpSession);
        }

        public void Delete(int Id)
        {
            _VideoFaultrepository.Delete(Id);
        }
    }
}
