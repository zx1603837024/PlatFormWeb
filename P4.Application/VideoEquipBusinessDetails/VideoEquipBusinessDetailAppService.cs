using Abp.Application.Services;
using Abp.Domain.Repositories;
using System;
using Abp.AutoMapper;
using System.Collections.Generic;
using P4.SensorBusinessDetails;
using P4.VideoEquips;
using P4.VideoEquipBusinessDetails.Dtos;

namespace P4.VideoEquipBusinessDetails
{
    /// <summary>
    /// 视频设备业务管理类
    /// </summary>
    public class VideoEquipBusinessDetailAppService : ApplicationService, IVideoEquipBusinessDetailAppService
    {
        #region Var
        private readonly IRepository<VideoEquipBusinessDetail, long> _abpVedioEquipDetailRepository;
        private readonly IVideoEquipBusinessDetailRepository _VideoEquipBusinessDetailRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpVideoEquipBusinessDetailRepository"></param>
        /// <param name="VideoEquipBusinessDetailRepository"></param>
        public VideoEquipBusinessDetailAppService(IRepository<VideoEquipBusinessDetail, long> abpVideoEquipBusinessDetailRepository, IVideoEquipBusinessDetailRepository VideoEquipBusinessDetailRepository)

        {
            _abpVedioEquipDetailRepository = abpVideoEquipBusinessDetailRepository;
            _VideoEquipBusinessDetailRepository = VideoEquipBusinessDetailRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.GetAllVideoEqBusinessDetailOutput GetAllVideoEqBusinessDetaillist(Dtos.VideoEquipBusinessDetailInput input)
        {
            return _VideoEquipBusinessDetailRepository.GetAllVideoEqBusinessDetaillist(input, AbpSession);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(int Id)
        {
            _VideoEquipBusinessDetailRepository.Delete(Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.GetAllVideoEqBusinessDetailOutput GetAllVideoPileBusinessDetaillist(Dtos.VideoEquipBusinessDetailInput input)
        {
            return _VideoEquipBusinessDetailRepository.GetAllVideoPileBusinessDetaillist(input, AbpSession);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.GetAllVideoEqBusinessDetailOutput GetAllVideoRoadRollBusinessDetaillist(Dtos.VideoEquipBusinessDetailInput input)
        {
            return _VideoEquipBusinessDetailRepository.GetAllVideoRoadRollBusinessDetaillist(input, AbpSession);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public void Delete1(int Id)
        {
            _VideoEquipBusinessDetailRepository.Delete(Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public void Delete2(int Id)
        {
            _VideoEquipBusinessDetailRepository.Delete(Id);
        }


        public void Update(CreateOrUpdateVideoEqBusDetail input)
        {
            _VideoEquipBusinessDetailRepository.UpdatePlate(input,AbpSession);
        }
    }
}
