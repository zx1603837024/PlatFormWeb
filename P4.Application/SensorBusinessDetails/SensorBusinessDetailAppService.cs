using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.Sensors;
using System;
using P4.SensorBusinessDetails.Dtos;
using Abp.AutoMapper;
using System.Collections.Generic;

namespace P4.SensorBusinessDetails
{
    /// <summary>
    /// 车检器业务管理类
    /// </summary>
    public class SensorBusinessDetailAppService : ApplicationService, ISensorBusinessDetailAppService
    {
        #region Var
        private readonly IRepository<SensorBusinessDetail, long> _abpSensorBusinessDetailRepository;
        private readonly ISensorBusinessDetailRepository _SensorBusinessDetailRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpSensorBusinessDetailRepository"></param>
        /// <param name="SensorBusinessDetailRepository"></param>
        public SensorBusinessDetailAppService(IRepository<SensorBusinessDetail, long> abpSensorBusinessDetailRepository, ISensorBusinessDetailRepository SensorBusinessDetailRepository)

        {
            _abpSensorBusinessDetailRepository = abpSensorBusinessDetailRepository;
            _SensorBusinessDetailRepository = SensorBusinessDetailRepository;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.GetAllSensorBusinessDetailOutput GetAllSensorBusinessDetaillist(Dtos.SensorBusinessDetailInput input)
        {
           
            return _SensorBusinessDetailRepository.GetAllSensorBusinessDetaillist(input,AbpSession);
            

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(int Id)
        {
            _SensorBusinessDetailRepository.Delete(Id);
        }

        /// <summary>
        /// 获取地磁进出场信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public string GetSensorBusinessdetail(string guid)
        {
            Guid tempguid = new Guid(guid);

            var model = _SensorBusinessDetailRepository.FirstOrDefault(entity => entity.guid == tempguid);
            if (model == default(SensorBusinessDetail))
                return "";
            else
                return model.CarOutTime.HasValue == true ? model.CarOutTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
        }
    }
}
